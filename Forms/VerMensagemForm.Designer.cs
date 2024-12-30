namespace ZapEnvioSeguro.Forms
{
    partial class VerMensagemForm
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
            label6 = new Label();
            txtMensagem = new RichTextBox();
            lbDataEnvio = new Label();
            label1 = new Label();
            label2 = new Label();
            lbEnvioSolicitado = new Label();
            label4 = new Label();
            lbEnviosSucesso = new Label();
            btnRepetirEnvio = new Button();
            btnEnviarFalhas = new Button();
            label7 = new Label();
            lbEnvioFalha = new Label();
            panel2 = new Panel();
            button1 = new Button();
            label3 = new Label();
            panel1 = new Panel();
            lbNaoPodeRepetir = new Label();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(13, 61);
            label6.Name = "label6";
            label6.Size = new Size(263, 25);
            label6.TabIndex = 13;
            label6.Text = "Texto da mensagem enviada";
            // 
            // txtMensagem
            // 
            txtMensagem.AcceptsTab = true;
            txtMensagem.AutoWordSelection = true;
            txtMensagem.BackColor = SystemColors.HighlightText;
            txtMensagem.Cursor = Cursors.IBeam;
            txtMensagem.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMensagem.Location = new Point(12, 98);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.ReadOnly = true;
            txtMensagem.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtMensagem.Size = new Size(489, 529);
            txtMensagem.TabIndex = 12;
            txtMensagem.Text = "";
            // 
            // lbDataEnvio
            // 
            lbDataEnvio.AutoSize = true;
            lbDataEnvio.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbDataEnvio.Location = new Point(159, 27);
            lbDataEnvio.Name = "lbDataEnvio";
            lbDataEnvio.Size = new Size(119, 21);
            lbDataEnvio.TabIndex = 14;
            lbDataEnvio.Text = "00/00/00 00:00";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 24);
            label1.Name = "label1";
            label1.Size = new Size(141, 25);
            label1.TabIndex = 15;
            label1.Text = "Data do envio:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 69);
            label2.Name = "label2";
            label2.Size = new Size(172, 25);
            label2.TabIndex = 17;
            label2.Text = "Envios solicitados:";
            // 
            // lbEnvioSolicitado
            // 
            lbEnvioSolicitado.AutoSize = true;
            lbEnvioSolicitado.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbEnvioSolicitado.Location = new Point(190, 69);
            lbEnvioSolicitado.Name = "lbEnvioSolicitado";
            lbEnvioSolicitado.Size = new Size(102, 25);
            lbEnvioSolicitado.TabIndex = 16;
            lbEnvioSolicitado.Text = "0 Contatos";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(12, 114);
            label4.Name = "label4";
            label4.Size = new Size(189, 25);
            label4.TabIndex = 19;
            label4.Text = "Envios com sucesso:";
            // 
            // lbEnviosSucesso
            // 
            lbEnviosSucesso.AutoSize = true;
            lbEnviosSucesso.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbEnviosSucesso.ForeColor = SystemColors.MenuHighlight;
            lbEnviosSucesso.Location = new Point(207, 114);
            lbEnviosSucesso.Name = "lbEnviosSucesso";
            lbEnviosSucesso.Size = new Size(102, 25);
            lbEnviosSucesso.TabIndex = 18;
            lbEnviosSucesso.Text = "0 Contatos";
            // 
            // btnRepetirEnvio
            // 
            btnRepetirEnvio.BackColor = SystemColors.MenuHighlight;
            btnRepetirEnvio.Cursor = Cursors.Hand;
            btnRepetirEnvio.FlatStyle = FlatStyle.Popup;
            btnRepetirEnvio.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRepetirEnvio.ForeColor = SystemColors.Window;
            btnRepetirEnvio.Location = new Point(536, 572);
            btnRepetirEnvio.Name = "btnRepetirEnvio";
            btnRepetirEnvio.Size = new Size(181, 55);
            btnRepetirEnvio.TabIndex = 20;
            btnRepetirEnvio.Text = "Reenviar Todos";
            btnRepetirEnvio.UseVisualStyleBackColor = false;
            btnRepetirEnvio.Click += btnRepetirEnvio_Click;
            // 
            // btnEnviarFalhas
            // 
            btnEnviarFalhas.BackColor = Color.IndianRed;
            btnEnviarFalhas.Cursor = Cursors.Hand;
            btnEnviarFalhas.FlatStyle = FlatStyle.Popup;
            btnEnviarFalhas.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEnviarFalhas.ForeColor = SystemColors.Window;
            btnEnviarFalhas.Location = new Point(734, 572);
            btnEnviarFalhas.Name = "btnEnviarFalhas";
            btnEnviarFalhas.Size = new Size(160, 55);
            btnEnviarFalhas.TabIndex = 21;
            btnEnviarFalhas.Text = "Enviar Falhas";
            btnEnviarFalhas.UseVisualStyleBackColor = false;
            btnEnviarFalhas.Click += btnEnviarFalhas_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(12, 159);
            label7.Name = "label7";
            label7.Size = new Size(165, 25);
            label7.TabIndex = 23;
            label7.Text = "Envios com falha:";
            // 
            // lbEnvioFalha
            // 
            lbEnvioFalha.AutoSize = true;
            lbEnvioFalha.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbEnvioFalha.ForeColor = Color.IndianRed;
            lbEnvioFalha.Location = new Point(183, 159);
            lbEnvioFalha.Name = "lbEnvioFalha";
            lbEnvioFalha.Size = new Size(102, 25);
            lbEnvioFalha.TabIndex = 22;
            lbEnvioFalha.Text = "0 Contatos";
            // 
            // panel2
            // 
            panel2.BackColor = Color.SteelBlue;
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label3);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(1, 1);
            panel2.Name = "panel2";
            panel2.Size = new Size(923, 30);
            panel2.TabIndex = 24;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.Cursor = Cursors.Hand;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(897, 2);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(26, 26);
            button1.TabIndex = 1;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe Print", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.Window;
            label3.Location = new Point(0, -1);
            label3.Name = "label3";
            label3.Size = new Size(155, 28);
            label3.TabIndex = 0;
            label3.Text = "Zap Envio Seguro";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.HighlightText;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lbDataEnvio);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(lbEnvioSolicitado);
            panel1.Controls.Add(lbEnvioFalha);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(lbEnviosSucesso);
            panel1.Controls.Add(label4);
            panel1.Location = new Point(549, 98);
            panel1.Name = "panel1";
            panel1.Size = new Size(337, 215);
            panel1.TabIndex = 25;
            // 
            // lbNaoPodeRepetir
            // 
            lbNaoPodeRepetir.AutoSize = true;
            lbNaoPodeRepetir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbNaoPodeRepetir.ForeColor = Color.IndianRed;
            lbNaoPodeRepetir.Location = new Point(518, 552);
            lbNaoPodeRepetir.Name = "lbNaoPodeRepetir";
            lbNaoPodeRepetir.Size = new Size(390, 17);
            lbNaoPodeRepetir.TabIndex = 26;
            lbNaoPodeRepetir.Text = "Esse envio teve uma falha inesperada e não pode ser repetido";
            lbNaoPodeRepetir.Visible = false;
            // 
            // VerMensagemForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(925, 645);
            Controls.Add(lbNaoPodeRepetir);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(btnEnviarFalhas);
            Controls.Add(btnRepetirEnvio);
            Controls.Add(label6);
            Controls.Add(txtMensagem);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VerMensagemForm";
            Padding = new Padding(1);
            Load += VerMensagemForm_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label6;
        private RichTextBox txtMensagem;
        private Label lbDataEnvio;
        private Label label1;
        private Label label2;
        private Label lbEnvioSolicitado;
        private Label label4;
        private Label lbEnviosSucesso;
        private Button btnRepetirEnvio;
        private Button btnEnviarFalhas;
        private Label label7;
        private Label lbEnvioFalha;
        private Panel panel2;
        private Button button1;
        private Label label3;
        private Panel panel1;
        private Label lbNaoPodeRepetir;
    }
}