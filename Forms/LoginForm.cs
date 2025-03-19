
using System.Management;
using System.Media;
using Velopack.Sources;
using Velopack;
using ZapEnvioSeguro.Classes;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using System.Net;

namespace ZapEnvioSeguro.Forms {
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService;

        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthService(new DatabaseHelper());
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            string appVersion = Application.ProductVersion;
            lbVersaoAtualUpdate.Text = appVersion;
            labelVersion.Text = appVersion;
            this.Enabled = false;

            string savedEmail = Properties.Settings.Default.UserEmail;
            if (!string.IsNullOrEmpty(savedEmail))
            {
                txtEmail.Text = savedEmail;
                chkSaveLogin.Checked = true;
            }

            GetDeviceId();

#if !DEBUG
            await UpdateMyApp();
#endif
            this.Enabled = true;
            
        }

        private async Task UpdateMyApp()
        {
            panelUpdate.Invoke((Action)(() => panelUpdate.Visible = true));

            GithubSource source = new GithubSource(
                    repoUrl: "https://github.com/tainanflores/ZapEnvioSeguro",
                    accessToken: "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",
                    false
                );
            var mgr = new UpdateManager(source);

            var newVersion = await mgr.CheckForUpdatesAsync();
            if (newVersion == null)
            {
                panelUpdate.Invoke((Action)(() => panelUpdate.Visible = false));
                return;
            }
            panelLogin.Invoke((Action)(() => lbTituloAtu.Text = "Nova Versão Encontrada"));
            panelLogin.Invoke((Action)(() => lbNovaVersaoUpdate.Text = newVersion.TargetFullRelease.Version.ToString()));
            progressBarUpdate.Invoke((Action)(() => progressBarUpdate.Value = 0));
            lbStatusUpdate.Invoke((Action)(() => lbStatusUpdate.Text = "Baixando nova versão..."));

            await mgr.DownloadUpdatesAsync(
                newVersion,
                progress: value =>
                {
                    progressBarUpdate.Invoke((Action)(() => progressBarUpdate.Value = value));
                });

            mgr.ApplyUpdatesAndRestart(newVersion);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                btnLogin.Visible = false;
                btnCadastro.Visible = false;
                lbconectando.Visible = true;
                bool online = IsOnline();
                if (!online)                
                {
                    this.Enabled = true;
                    btnLogin.Enabled = true;
                    lbconectando.Visible = false;
                    btnLogin.Visible = true;
                    btnCadastro.Visible = true;
                    return;
                }
                string email = txtEmail.Text;
                string password = txtPassword.Text;

                if (chkSaveLogin.Checked)
                {
                    Properties.Settings.Default.UserEmail = txtEmail.Text;
                    Properties.Settings.Default.Save();
                }

                bool loginSuccess = await _authService.Login(email, password, Evento.DeviceId);

                if (!loginSuccess)
                {
                    MessageBox.Show("Falha no login.");
                    btnLogin.Enabled = true;
                    lbconectando.Visible = false;
                    btnLogin.Visible = false;
                    btnCadastro.Visible = false;
                }
                else
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }

            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();

                if (ex.Message == "Senha incorreta.") txtPassword.Clear();
                MessageBox.Show($"{ex.Message}");

                this.Enabled = true;
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private void GetDeviceId()
        {
            string savedUuid = Properties.Settings.Default.DeviceId;

            if (string.IsNullOrEmpty(savedUuid))
            {
                savedUuid = GetDeviceUuid();
                Properties.Settings.Default.DeviceId = savedUuid;
                Properties.Settings.Default.Save();
            }

            Evento.DeviceId = savedUuid;
        }

        private string GetDeviceUuid()
        {
            string uuid = null;

            try
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct");

                foreach (var queryObj in searcher.Get())
                {
                    uuid = queryObj["UUID"]?.ToString();
                    //MessageBox.Show(uuid);
                }

                if (string.IsNullOrEmpty(uuid))
                {
                    uuid = Guid.NewGuid().ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter UUID: {ex.Message}");

                uuid = Guid.NewGuid().ToString();
            }

            return uuid;
        }

        private void chkVerSenha_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkVerSenha.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin.PerformClick();
            }
        }

        private void chkVerSenha_CheckedChanged_1(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkVerSenha.Checked;
        }

        private bool IsOnline()
        {
            try
            {
                DateTime dataLocal = DateTime.Now;
                DateTime dataOnline = GetNetworkTime();

                if (dataOnline == new DateTime(2001, 1, 1, 0, 0, 0))
                {
                    return false;
                }

                int toleranciaEmMinutos = 30;

                if (!EstaoAproximadas(dataLocal, dataOnline, toleranciaEmMinutos))
                {
                    MessageBox.Show("Por favor, atualize a data do dipositivo antes de continuar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        static DateTime ObterDataOnline()
        {
            const int maxTentativas = 5; // Número máximo de tentativas
            const string url = "http://worldtimeapi.org/api/timezone/Etc/UTC";

            for (int tentativa = 1; tentativa <= maxTentativas; tentativa++)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var resposta = client.GetStringAsync(url).Result;

                        var json = JObject.Parse(resposta);
                        string dataHoraStr = json["datetime"].ToString();

                        DateTime dataHora = DateTime.Parse(dataHoraStr);
                        return dataHora; // Retorna a data/hora se bem-sucedido
                    }
                }
                catch
                {
                    if (tentativa == maxTentativas)
                    {
                        // Exibe erro apenas na última tentativa
                        MessageBox.Show(
                            "Não foi possível conectar ao servidor. Verifique sua conexão com a internet e tente novamente.",
                            "Falha ao conectar",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );                        
                    }
                }
            }

            // Retorna a data padrão se todas as tentativas falharem
            return new DateTime(2001, 1, 1, 0, 0, 0);
        }

        public static DateTime GetNetworkTime()
        {
            const string ntpServer = "pool.ntp.org";
            byte[] ntpData = new byte[48];
            ntpData[0] = 0x1B;

            IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(ntpServer)[0], 123);
            using (UdpClient client = new UdpClient())
            {
                client.Connect(endPoint);
                client.Send(ntpData, ntpData.Length);
                ntpData = client.Receive(ref endPoint);
            }

            ulong intPart = BitConverter.ToUInt32(ntpData, 40);
            ulong fractPart = BitConverter.ToUInt32(ntpData, 44);
            intPart = (uint)IPAddress.NetworkToHostOrder((int)intPart);
            fractPart = (uint)IPAddress.NetworkToHostOrder((int)fractPart);

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            DateTime networkDateTime = new DateTime(1900, 1, 1).AddMilliseconds((long)milliseconds);

            return networkDateTime.ToLocalTime();
        }


        static bool EstaoAproximadas(DateTime data1, DateTime data2, int toleranciaEmMinutos)
        {
            TimeSpan diferenca = data1 - data2;

            return Math.Abs(diferenca.TotalMinutes) <= toleranciaEmMinutos;
        }
    }

}
