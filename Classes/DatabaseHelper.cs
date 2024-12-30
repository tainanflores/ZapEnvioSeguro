using System.Data;
using Microsoft.Data.SqlClient;

namespace ZapEnvioSeguro.Classes
{
    internal class DatabaseHelper
    {
        // String de conexão do SQL Server
        private readonly string _connectionString;

        // Construtor da classe que recebe a string de conexão
        public DatabaseHelper(string server, string database, string username, string password)
        {
            _connectionString = $"Server={server};Database={database};User Id={username};Password={password};Pooling=False;Encrypt=False";
        }

        // Construtor opcional que recebe parâmetros diretamente (para facilitar em casos específicos)
        public DatabaseHelper() : this("mundodigital.ddns.net,4200", "ZapEnvioFacil", "mundodigital", "135246") { }

        // Método para abrir uma conexão com o banco de dados
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Método para executar uma consulta SQL e retornar um DataTable com os resultados
        public async Task<DataTable?> ExecuteQueryAsync(string query, SqlParameter[] parameters)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        // Adicionar parâmetros diretamente
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            return dataTable;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para executar uma consulta SQL e retornar um DataTable com os resultados
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        // Adicionar parâmetros diretamente
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            return dataTable;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para executar comandos SQL como INSERT, UPDATE, DELETE (não retorna resultados)
        public async Task<int> ExecuteNonQueryAsync(string query, SqlParameter[] parameters)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        // Adicionar parâmetros diretamente
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        return await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para inserir dados com parâmetros, evitando SQL Injection
        public async Task<int> InsertDataAsync(string query, SqlParameter[] parameters)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        // Adicionar parâmetros diretamente
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para obter um valor único (como resultado de uma consulta COUNT, MAX, etc.)
        public async Task<object?> ExecuteScalarAsync(string query, SqlParameter[] parameters)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        // Adicionar parâmetros diretamente
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        return await command.ExecuteScalarAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para executar uma transação com múltiplas operações
        public async Task<bool> ExecuteTransactionAsync(List<Tuple<string, SqlParameter[]>> queries, IProgress<ProgressReport> progress = null, string loteDeLotes = "")
        {
            SqlTransaction transaction = null;

            try
            {
                using (var connection = GetConnection())
                {
                    await connection.OpenAsync();
                    transaction = (SqlTransaction)await connection.BeginTransactionAsync();

                    int total = queries.Count;
                    int current = 0;

                    foreach (var queryItem in queries)
                    {
                        string query = queryItem.Item1; // A consulta SQL
                        SqlParameter[] parameters = queryItem.Item2; // Parâmetros correspondentes à consulta

                        using (var command = new SqlCommand(query, connection, transaction))
                        {
                            command.CommandTimeout = 60;
                            // Adicionar parâmetros à consulta
                            if (parameters != null)
                            {
                                command.Parameters.AddRange(parameters);
                            }

                            // Executa a consulta
                            await command.ExecuteNonQueryAsync();
                        }

                        current++;
                        int percent = (int)((double)current / total * 100);
                        // Reportar o progresso
                        progress?.Report(new ProgressReport { Percent = percent, Message = $"{current} de {total} - {loteDeLotes}" });
                    }

                    // Se todas as consultas forem executadas com sucesso, comita a transação
                    await transaction.CommitAsync();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                // Se ocorrer um erro, faz o rollback da transação
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }
                throw new Exception(ex.Message);
            }
        }
    }

}
