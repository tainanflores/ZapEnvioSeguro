namespace ZapEnvioSeguro.Forms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            panel2 = new Panel();
            button1 = new Button();
            label4 = new Label();
            panelLogin = new Panel();
            panelUpdate = new Panel();
            lbNovaVersaoUpdate = new Label();
            lbVersaoAtualUpdate = new Label();
            label6 = new Label();
            label5 = new Label();
            lbStatusUpdate = new Label();
            label3 = new Label();
            progressBarUpdate = new ProgressBar();
            labelVersion = new Label();
            pictureBox1 = new PictureBox();
            chkVerSenha = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            chkSaveLogin = new CheckBox();
            btnCadastro = new Button();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            panel2.SuspendLayout();
            panelLogin.SuspendLayout();
            panelUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.SteelBlue;
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label4);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(334, 30);
            panel2.TabIndex = 8;
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
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe Print", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.Window;
            label4.Location = new Point(0, -1);
            label4.Name = "label4";
            label4.Size = new Size(155, 28);
            label4.TabIndex = 0;
            label4.Text = "Zap Envio Seguro";
            // 
            // panelLogin
            // 
            panelLogin.Controls.Add(panel2);
            panelLogin.Controls.Add(panelUpdate);
            panelLogin.Controls.Add(labelVersion);
            panelLogin.Controls.Add(pictureBox1);
            panelLogin.Controls.Add(chkVerSenha);
            panelLogin.Controls.Add(label1);
            panelLogin.Controls.Add(label2);
            panelLogin.Controls.Add(chkSaveLogin);
            panelLogin.Controls.Add(btnCadastro);
            panelLogin.Controls.Add(txtEmail);
            panelLogin.Controls.Add(txtPassword);
            panelLogin.Controls.Add(btnLogin);
            panelLogin.Dock = DockStyle.Fill;
            panelLogin.Location = new Point(0, 0);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(334, 330);
            panelLogin.TabIndex = 14;
            // 
            // panelUpdate
            // 
            panelUpdate.BackColor = Color.MidnightBlue;
            panelUpdate.Controls.Add(lbNovaVersaoUpdate);
            panelUpdate.Controls.Add(lbVersaoAtualUpdate);
            panelUpdate.Controls.Add(label6);
            panelUpdate.Controls.Add(label5);
            panelUpdate.Controls.Add(lbStatusUpdate);
            panelUpdate.Controls.Add(label3);
            panelUpdate.Controls.Add(progressBarUpdate);
            panelUpdate.Location = new Point(12, 166);
            panelUpdate.Name = "panelUpdate";
            panelUpdate.Size = new Size(310, 153);
            panelUpdate.TabIndex = 13;
            panelUpdate.Visible = false;
            // 
            // lbNovaVersaoUpdate
            // 
            lbNovaVersaoUpdate.AutoSize = true;
            lbNovaVersaoUpdate.BackColor = Color.Transparent;
            lbNovaVersaoUpdate.ForeColor = SystemColors.ButtonFace;
            lbNovaVersaoUpdate.Location = new Point(95, 74);
            lbNovaVersaoUpdate.Name = "lbNovaVersaoUpdate";
            lbNovaVersaoUpdate.Size = new Size(31, 15);
            lbNovaVersaoUpdate.TabIndex = 6;
            lbNovaVersaoUpdate.Text = "0.0.2";
            // 
            // lbVersaoAtualUpdate
            // 
            lbVersaoAtualUpdate.AutoSize = true;
            lbVersaoAtualUpdate.BackColor = Color.Transparent;
            lbVersaoAtualUpdate.ForeColor = SystemColors.ButtonFace;
            lbVersaoAtualUpdate.Location = new Point(95, 44);
            lbVersaoAtualUpdate.Name = "lbVersaoAtualUpdate";
            lbVersaoAtualUpdate.Size = new Size(31, 15);
            lbVersaoAtualUpdate.TabIndex = 5;
            lbVersaoAtualUpdate.Text = "0.0.1";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.ForeColor = SystemColors.ButtonFace;
            label6.Location = new Point(16, 74);
            label6.Name = "label6";
            label6.Size = new Size(75, 15);
            label6.TabIndex = 4;
            label6.Text = "Nova versão:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.ForeColor = SystemColors.ButtonFace;
            label5.Location = new Point(16, 44);
            label5.Name = "label5";
            label5.Size = new Size(73, 15);
            label5.TabIndex = 3;
            label5.Text = "Versão atual:";
            // 
            // lbStatusUpdate
            // 
            lbStatusUpdate.BackColor = Color.Transparent;
            lbStatusUpdate.Dock = DockStyle.Bottom;
            lbStatusUpdate.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbStatusUpdate.ForeColor = SystemColors.ButtonFace;
            lbStatusUpdate.Location = new Point(0, 131);
            lbStatusUpdate.Name = "lbStatusUpdate";
            lbStatusUpdate.Size = new Size(310, 22);
            lbStatusUpdate.TabIndex = 2;
            lbStatusUpdate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Dock = DockStyle.Top;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(310, 32);
            label3.TabIndex = 1;
            label3.Text = "Nova atualização encontrada";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBarUpdate
            // 
            progressBarUpdate.Location = new Point(9, 105);
            progressBarUpdate.Name = "progressBarUpdate";
            progressBarUpdate.Size = new Size(281, 20);
            progressBarUpdate.TabIndex = 0;
            // 
            // labelVersion
            // 
            labelVersion.Dock = DockStyle.Bottom;
            labelVersion.Location = new Point(0, 315);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(334, 15);
            labelVersion.TabIndex = 23;
            labelVersion.Text = "v";
            labelVersion.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(66, 35);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 125);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 22;
            pictureBox1.TabStop = false;
            // 
            // chkVerSenha
            // 
            chkVerSenha.AutoSize = true;
            chkVerSenha.Location = new Point(226, 235);
            chkVerSenha.Name = "chkVerSenha";
            chkVerSenha.Size = new Size(76, 19);
            chkVerSenha.TabIndex = 5;
            chkVerSenha.Text = "Ver senha";
            chkVerSenha.UseVisualStyleBackColor = true;
            chkVerSenha.CheckedChanged += chkVerSenha_CheckedChanged_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 181);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 16;
            label1.Text = "Email:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 210);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 17;
            label2.Text = "Senha:";
            // 
            // chkSaveLogin
            // 
            chkSaveLogin.AutoSize = true;
            chkSaveLogin.Location = new Point(66, 235);
            chkSaveLogin.Name = "chkSaveLogin";
            chkSaveLogin.Size = new Size(102, 19);
            chkSaveLogin.TabIndex = 3;
            chkSaveLogin.Text = "Lembrar Email";
            chkSaveLogin.UseVisualStyleBackColor = true;
            // 
            // btnCadastro
            // 
            btnCadastro.BackColor = Color.MidnightBlue;
            btnCadastro.FlatStyle = FlatStyle.Popup;
            btnCadastro.ForeColor = SystemColors.ButtonFace;
            btnCadastro.Location = new Point(143, 270);
            btnCadastro.Name = "btnCadastro";
            btnCadastro.Size = new Size(75, 35);
            btnCadastro.TabIndex = 6;
            btnCadastro.Text = "Cadastro";
            btnCadastro.UseVisualStyleBackColor = false;
            btnCadastro.Click += btnCadastro_Click;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(66, 177);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(236, 23);
            txtEmail.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(66, 206);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(236, 23);
            txtPassword.TabIndex = 2;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.KeyDown += txtPassword_KeyDown;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = SystemColors.MenuHighlight;
            btnLogin.FlatStyle = FlatStyle.Popup;
            btnLogin.ForeColor = SystemColors.ButtonFace;
            btnLogin.Location = new Point(224, 270);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(75, 35);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Entrar";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(334, 330);
            Controls.Add(panelLogin);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LoginForm";
            RightToLeftLayout = true;
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginForm";
            Load += LoginForm_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            panelUpdate.ResumeLayout(false);
            panelUpdate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private Label label4;
        private Button button1;
        private Panel panelLogin;
        private Panel panelUpdate;
        private Label lbNovaVersaoUpdate;
        private Label lbVersaoAtualUpdate;
        private Label label6;
        private Label label5;
        private Label lbStatusUpdate;
        private Label label3;
        public ProgressBar progressBarUpdate;
        private Label labelVersion;
        private PictureBox pictureBox1;
        private CheckBox chkVerSenha;
        private Label label1;
        private Label label2;
        private CheckBox chkSaveLogin;
        private Button btnCadastro;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnLogin;
    }
}