
using System.Management;
using System.Media;
using Velopack.Sources;
using Velopack;
using ZapEnvioSeguro.Classes;

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

            //await UpdateMyApp();
            GetDeviceId();

            string savedEmail = Properties.Settings.Default.UserEmail;

            if (!string.IsNullOrEmpty(savedEmail))
            {
                txtEmail.Text = savedEmail;
                chkSaveLogin.Checked = true;
            }
        }

        private async Task UpdateMyApp()
        {
            GithubSource source = new GithubSource(
                    repoUrl: "https://github.com/tainanflores/ZapEnvioSeguro",
                    accessToken: "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",
                    false
                );
            var mgr = new UpdateManager(source);

            var newVersion = await mgr.CheckForUpdatesAsync();
            if (newVersion == null)
                return;

            panelUpdate.Invoke((Action)(() => panelUpdate.Visible = true));
            panelLogin.Invoke((Action)(() => panelLogin.Enabled = false));
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
                string email = txtEmail.Text;
                string password = txtPassword.Text;

                if (chkSaveLogin.Checked)
                {
                    Properties.Settings.Default.UserEmail = txtEmail.Text;
                    Properties.Settings.Default.Save();
                }
                this.Enabled = false;

                bool loginSuccess = await _authService.Login(email, password, Evento.DeviceId);

                if (!loginSuccess)
                {

                    MessageBox.Show("Falha no login.");
                    btnLogin.Enabled = true;
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
    }

}
