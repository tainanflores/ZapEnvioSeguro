using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZapEnvioSeguro.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ZapEnvioSeguro.Forms
{
    public partial class RegisterForm : Form
    {
        private readonly AuthService _authService;
        private bool validatedEmail = false;
        private bool validatedPassword = false;
        private bool equalPassword = false;
        public RegisterForm()
        {
            InitializeComponent();
            _authService = new AuthService(new DatabaseHelper());
        }

        private async void btnCadastro_Click(object sender, EventArgs e)
        {
            if(!validatedEmail)
            {
                SystemSounds.Hand.Play();
                return;
            }
            if(!validatedPassword)
            {
                SystemSounds.Hand.Play();
                return;
            }
            if(!equalPassword)
            {
                SystemSounds.Hand.Play();
                return;
            }

            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string passwor2 = txtPassword2.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                bool isRegistered = await _authService.RegisterUser(email, password);
                if (isRegistered)
                {
                    MessageBox.Show("Usuário registrado com sucesso! Faça o Login agora", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erro ao registrar o usuário. Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            ValidatePassword(txtPassword.Text);
            ValidateConfirmPassword(txtPassword.Text, txtPassword2.Text);
        }

        private void txtPassword2_TextChanged(object sender, EventArgs e)
        {
            ValidateConfirmPassword(txtPassword.Text, txtPassword2.Text);
        }
        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            ValidateEmailFormat(txtEmail.Text);
        }

        private void ValidateEmailFormat(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{3}$";
            string emailPattern2 = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{3}.[a-zA-Z]{2}$";
            bool isValidEmail = Regex.IsMatch(email, emailPattern) || Regex.IsMatch(email, emailPattern2);

            if (!isValidEmail)
            {
                toolTip.SetToolTip(txtEmail, "Por favor, insira um email válido.");
                lbEmailInvalido.Visible = true;
                validatedEmail = false;
            }
            else
            {
                toolTip.SetToolTip(txtEmail, string.Empty);
                lbEmailInvalido.Visible = false;
                validatedEmail = true;
            }
        }

        private void ValidatePassword(string password)
        {
            bool isValidLength = password.Length >= 8;
            bool hasUpperCase = Regex.IsMatch(password, @"[A-Z]");
            bool hasLowerCase = Regex.IsMatch(password, @"[a-z]");
            bool hasSymbol = Regex.IsMatch(password, @"[\W_]");

            string passwordTooltip = "A senha deve ter pelo menos 8 caracteres, com letras maiúsculas, minúsculas e um símbolo.";

            if (!isValidLength || !hasUpperCase || !hasLowerCase || !hasSymbol)
            {
                toolTip.SetToolTip(txtPassword, passwordTooltip);
                lbSenhaInvalida.Visible = true;
                validatedPassword = false;
            }
            else
            {
                toolTip.SetToolTip(txtPassword, string.Empty);
                lbSenhaInvalida.Visible = false;
                validatedPassword = true;
            }
        }

        private void ValidateConfirmPassword(string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                toolTip.SetToolTip(txtPassword2, "As senhas não coincidem.");
                lbSenhaDiferente.Visible = true;
                equalPassword = false;
            }
            else
            {
                toolTip.SetToolTip(txtPassword2, string.Empty);
                lbSenhaDiferente.Visible = false;
                equalPassword = true;
            }
        }

        private void chkVerSenha_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkVerSenha.Checked;
            txtPassword2.UseSystemPasswordChar = !chkVerSenha.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}
