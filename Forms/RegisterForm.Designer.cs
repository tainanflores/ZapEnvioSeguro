namespace ZapEnvioSeguro.Forms
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            panel1 = new Panel();
            lbEmailInvalido = new Label();
            lbSenhaDiferente = new Label();
            lbSenhaInvalida = new Label();
            chkVerSenha = new CheckBox();
            label5 = new Label();
            label4 = new Label();
            txtPassword2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnCadastro = new Button();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            toolTip = new ToolTip(components);
            panel2 = new Panel();
            button1 = new Button();
            label6 = new Label();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(lbEmailInvalido);
            panel1.Controls.Add(lbSenhaDiferente);
            panel1.Controls.Add(lbSenhaInvalida);
            panel1.Controls.Add(chkVerSenha);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtPassword2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnCadastro);
            panel1.Controls.Add(txtEmail);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(btnLogin);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(334, 360);
            panel1.TabIndex = 9;
            // 
            // lbEmailInvalido
            // 
            lbEmailInvalido.AutoSize = true;
            lbEmailInvalido.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbEmailInvalido.ForeColor = Color.DarkRed;
            lbEmailInvalido.Location = new Point(102, 186);
            lbEmailInvalido.Name = "lbEmailInvalido";
            lbEmailInvalido.Size = new Size(71, 13);
            lbEmailInvalido.TabIndex = 13;
            lbEmailInvalido.Text = "Email inválido";
            lbEmailInvalido.Visible = false;
            // 
            // lbSenhaDiferente
            // 
            lbSenhaDiferente.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbSenhaDiferente.ForeColor = Color.DarkRed;
            lbSenhaDiferente.Location = new Point(242, 237);
            lbSenhaDiferente.Name = "lbSenhaDiferente";
            lbSenhaDiferente.Size = new Size(77, 28);
            lbSenhaDiferente.TabIndex = 12;
            lbSenhaDiferente.Text = "As senhas não são iguais";
            lbSenhaDiferente.Visible = false;
            // 
            // lbSenhaInvalida
            // 
            lbSenhaInvalida.AutoSize = true;
            lbSenhaInvalida.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbSenhaInvalida.ForeColor = Color.DarkRed;
            lbSenhaInvalida.Location = new Point(242, 214);
            lbSenhaInvalida.Name = "lbSenhaInvalida";
            lbSenhaInvalida.Size = new Size(77, 13);
            lbSenhaInvalida.TabIndex = 11;
            lbSenhaInvalida.Text = "Senha inválida";
            lbSenhaInvalida.Visible = false;
            // 
            // chkVerSenha
            // 
            chkVerSenha.AutoSize = true;
            chkVerSenha.Location = new Point(98, 266);
            chkVerSenha.Name = "chkVerSenha";
            chkVerSenha.Size = new Size(81, 19);
            chkVerSenha.TabIndex = 3;
            chkVerSenha.Text = "Ver senhas";
            chkVerSenha.UseVisualStyleBackColor = true;
            chkVerSenha.CheckedChanged += chkVerSenha_CheckedChanged;
            // 
            // label5
            // 
            label5.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(10, 333);
            label5.Name = "label5";
            label5.Size = new Size(313, 30);
            label5.TabIndex = 9;
            label5.Text = "A senha deve conter no mínimo 8 caracteres com uma mistura de letras maiúsculas, minúsculas, números e símbolos";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 240);
            label4.Name = "label4";
            label4.Size = new Size(82, 15);
            label4.TabIndex = 8;
            label4.Text = "Repetir Senha:";
            // 
            // txtPassword2
            // 
            txtPassword2.Location = new Point(98, 237);
            txtPassword2.Name = "txtPassword2";
            txtPassword2.Size = new Size(138, 23);
            txtPassword2.TabIndex = 2;
            txtPassword2.UseSystemPasswordChar = true;
            txtPassword2.TextChanged += txtPassword2_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 166);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 2;
            label1.Text = "Email:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 212);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 3;
            label2.Text = "Senha:";
            // 
            // btnCadastro
            // 
            btnCadastro.BackColor = SystemColors.MenuHighlight;
            btnCadastro.FlatStyle = FlatStyle.Popup;
            btnCadastro.ForeColor = SystemColors.ButtonFace;
            btnCadastro.Location = new Point(216, 295);
            btnCadastro.Name = "btnCadastro";
            btnCadastro.Size = new Size(75, 35);
            btnCadastro.TabIndex = 4;
            btnCadastro.Text = "Cadastro";
            btnCadastro.UseVisualStyleBackColor = false;
            btnCadastro.Click += btnCadastro_Click;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(98, 163);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(193, 23);
            txtEmail.TabIndex = 0;
            txtEmail.TextChanged += txtEmail_TextChanged;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(98, 208);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(138, 23);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.MidnightBlue;
            btnLogin.FlatStyle = FlatStyle.Popup;
            btnLogin.ForeColor = SystemColors.ButtonFace;
            btnLogin.Location = new Point(98, 295);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(114, 35);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Voltar para Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.SteelBlue;
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label6);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(334, 30);
            panel2.TabIndex = 10;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(305, 1);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(26, 26);
            button1.TabIndex = 1;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe Print", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.Window;
            label6.Location = new Point(3, 1);
            label6.Name = "label6";
            label6.Size = new Size(155, 28);
            label6.TabIndex = 0;
            label6.Text = "Zap Envio Seguro";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(67, 42);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(203, 105);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(334, 360);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "RegisterForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Register";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Label label1;
        private Label label2;
        private Button btnCadastro;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label label4;
        private TextBox txtPassword2;
        private Label label5;
        private ToolTip toolTip;
        private CheckBox chkVerSenha;
        private Label lbSenhaDiferente;
        private Label lbSenhaInvalida;
        private Label lbEmailInvalido;
        private Panel panel2;
        private Button button1;
        private Label label6;
        private PictureBox pictureBox1;
    }
}