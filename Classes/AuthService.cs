using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ZapEnvioSeguro.Classes
{
    internal class AuthService
    {
        private readonly DatabaseHelper _dbHelper;

        public AuthService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            if (await IsEmailAlreadyRegistered(email))
            {
                throw new Exception("Este email já está registrado.");
            }
            
            if (await IsDeviceAlreadyRegistered(Evento.DeviceId))
            {
                throw new Exception("Esse dispositivo já está vinculado a outro cadastro. Por favor contate o suporte.");
            }

            string passwordHash = HashPassword(password);            

            string query = "INSERT INTO Users (Email, PasswordHash, CreatedAt, PlanoAtual, AuthToken, IsBlocked, DeviceId) VALUES (@Email, @PasswordHash, GETDATE(), 0, 'AuthToken', 0, @DeviceId)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@PasswordHash", passwordHash),
                new SqlParameter("@DeviceId", Evento.DeviceId)
            };

            int rowsAffected = await _dbHelper.ExecuteNonQueryAsync(query, parameters);
            return rowsAffected > 0;
        }

        public async Task<bool> Login(string email, string password, string deviceId)
        {
            string query = "SELECT Id, PasswordHash, IsBlocked, PlanoAtual, CreatedAt, AuthToken, DeviceId FROM Users WHERE Email = @Email";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            };

            var userTable = await _dbHelper.ExecuteQueryAsync(query, parameters);

            if (userTable == null || userTable.Rows.Count == 0)
            {
                throw new Exception("Usuário não encontrado.");
            }

            var userRow = userTable.Rows[0];
            int idUser = Int32.Parse(userRow["Id"].ToString());
            string storedPasswordHash = userRow["PasswordHash"].ToString();
            bool isBlocked = Convert.ToBoolean(userRow["IsBlocked"]);
            string storedDeviceId = userRow["DeviceId"].ToString();
            int plan = Convert.ToInt32(userRow["PlanoAtual"]);
            DateTime createdAt = Convert.ToDateTime(userRow["CreatedAt"]);

            if (isBlocked)
            {
                throw new Exception("Usuário bloqueado. Por favor, entre em contato com o suporte");
            }

            if (!VerifyPassword(password, storedPasswordHash))
            {
                throw new Exception("Senha incorreta.");
            }

            if (plan == 0)
            {
                DateTime twoDays = createdAt.AddDays(2);

                if (DateTime.Now > twoDays)
                {
                    string queryBlock = "UPDATE Users SET IsBlocked = 1 WHERE Email = @Email";

                    SqlParameter[] parametersBlock = new SqlParameter[]
                    {
                        new SqlParameter("@Email", email)
                    };

                    int rowsAffectedBlock = await _dbHelper.ExecuteNonQueryAsync(queryBlock, parametersBlock);

                    if (rowsAffectedBlock > 0)
                    {
                        throw new Exception("Período de trial expirado. Entre em contato com o suporte financeiro por favor.");
                    }
                    else
                    {
                        throw new Exception("Erro ao atualizar o status de bloqueio.");
                    }
                }
            }

            if (deviceId != storedDeviceId)
            {
                string updateQueryDeviceId = "UPDATE Users SET IsBlocked = 1 WHERE Email = @Email";
                SqlParameter[] updateParametersDeviceId = new SqlParameter[]
                {
                    new SqlParameter("@Email", email)
                };

                int rowsAffectedDeviceId = await _dbHelper.ExecuteNonQueryAsync(updateQueryDeviceId, updateParametersDeviceId);
                if (rowsAffectedDeviceId > 0)
                {
                    throw new Exception("Seu dispositivo foi alterado. Sua conta foi bloqueada.");
                }
                else
                {
                    throw new Exception("Erro ao atualizar o status de bloqueio.");
                }
            }
            Evento.IdEmpresa = idUser;
            return true;
        }

        private bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            string inputPasswordHash = HashPassword(inputPassword);
            return inputPasswordHash == storedPasswordHash;
        }

        private string GenerateAuthToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private async Task<bool> IsEmailAlreadyRegistered(string email)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            };

            var result = await _dbHelper.ExecuteScalarAsync(query, parameters);
            return Convert.ToInt32(result) > 0;
        } 
        private async Task<bool> IsDeviceAlreadyRegistered(string guid)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE DeviceId = @DeviceId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DeviceId", guid)
            };

            var result = await _dbHelper.ExecuteScalarAsync(query, parameters);
            return Convert.ToInt32(result) > 0;
        }

    }
}
