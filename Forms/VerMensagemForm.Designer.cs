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
            SuspendLayout();
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(12, 60);
            label6.Name = "label6";
            label6.Size = new Size(263, 25);
            label6.TabIndex = 13;
            label6.Text = "Texto da mensagem enviada";
            // 
            // txtMensagem
            // 
            txtMensagem.AcceptsTab = true;
            txtMensagem.AutoWordSelection = true;
            txtMensagem.Cursor = Cursors.IBeam;
            txtMensagem.Enabled = false;
            txtMensagem.Location = new Point(12, 98);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtMensagem.Size = new Size(489, 529);
            txtMensagem.TabIndex = 12;
            txtMensagem.Text = "";
            // 
            // lbDataEnvio
            // 
            lbDataEnvio.AutoSize = true;
            lbDataEnvio.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbDataEnvio.Location = new Point(726, 98);
            lbDataEnvio.Name = "lbDataEnvio";
            lbDataEnvio.Size = new Size(135, 25);
            lbDataEnvio.TabIndex = 14;
            lbDataEnvio.Text = "00/00/00 00:00";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(579, 98);
            label1.Name = "label1";
            label1.Size = new Size(141, 25);
            label1.TabIndex = 15;
            label1.Text = "Data do envio:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(579, 153);
            label2.Name = "label2";
            label2.Size = new Size(172, 25);
            label2.TabIndex = 17;
            label2.Text = "Envios solicitados:";
            // 
            // lbEnvioSolicitado
            // 
            lbEnvioSolicitado.AutoSize = true;
            lbEnvioSolicitado.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbEnvioSolicitado.Location = new Point(776, 153);
            lbEnvioSolicitado.Name = "lbEnvioSolicitado";
            lbEnvioSolicitado.Size = new Size(102, 25);
            lbEnvioSolicitado.TabIndex = 16;
            lbEnvioSolicitado.Text = "0 Contatos";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(579, 205);
            label4.Name = "label4";
            label4.Size = new Size(189, 25);
            label4.TabIndex = 19;
            label4.Text = "Envios com sucesso:";
            // 
            // lbEnviosSucesso
            // 
            lbEnviosSucesso.AutoSize = true;
            lbEnviosSucesso.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbEnviosSucesso.Location = new Point(776, 205);
            lbEnviosSucesso.Name = "lbEnviosSucesso";
            lbEnviosSucesso.Size = new Size(102, 25);
            lbEnviosSucesso.TabIndex = 18;
            lbEnviosSucesso.Text = "0 Contatos";
            // 
            // btnRepetirEnvio
            // 
            btnRepetirEnvio.BackColor = SystemColors.MenuHighlight;
            btnRepetirEnvio.Cursor = Cursors.WaitCursor;
            btnRepetirEnvio.FlatStyle = FlatStyle.Popup;
            btnRepetirEnvio.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRepetirEnvio.ForeColor = SystemColors.Window;
            btnRepetirEnvio.Location = new Point(879, 572);
            btnRepetirEnvio.Name = "btnRepetirEnvio";
            btnRepetirEnvio.Size = new Size(181, 55);
            btnRepetirEnvio.TabIndex = 20;
            btnRepetirEnvio.Text = "Reenviar Todos";
            btnRepetirEnvio.UseVisualStyleBackColor = false;
            // 
            // btnEnviarFalhas
            // 
            btnEnviarFalhas.BackColor = Color.IndianRed;
            btnEnviarFalhas.Cursor = Cursors.Hand;
            btnEnviarFalhas.FlatStyle = FlatStyle.Popup;
            btnEnviarFalhas.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEnviarFalhas.ForeColor = SystemColors.Window;
            btnEnviarFalhas.Location = new Point(1077, 572);
            btnEnviarFalhas.Name = "btnEnviarFalhas";
            btnEnviarFalhas.Size = new Size(160, 55);
            btnEnviarFalhas.TabIndex = 21;
            btnEnviarFalhas.Text = "Enviar Falhas";
            btnEnviarFalhas.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(579, 252);
            label7.Name = "label7";
            label7.Size = new Size(165, 25);
            label7.TabIndex = 23;
            label7.Text = "Envios com falha:";
            // 
            // lbEnvioFalha
            // 
            lbEnvioFalha.AutoSize = true;
            lbEnvioFalha.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbEnvioFalha.Location = new Point(776, 252);
            lbEnvioFalha.Name = "lbEnvioFalha";
            lbEnvioFalha.Size = new Size(102, 25);
            lbEnvioFalha.TabIndex = 22;
            lbEnvioFalha.Text = "0 Contatos";
            // 
            // VerMensagemForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(label7);
            Controls.Add(lbEnvioFalha);
            Controls.Add(btnEnviarFalhas);
            Controls.Add(btnRepetirEnvio);
            Controls.Add(label4);
            Controls.Add(lbEnviosSucesso);
            Controls.Add(label2);
            Controls.Add(lbEnvioSolicitado);
            Controls.Add(label1);
            Controls.Add(lbDataEnvio);
            Controls.Add(label6);
            Controls.Add(txtMensagem);
            Name = "VerMensagemForm";
            Text = "VerMensagemForm";
            Load += VerMensagemForm_Load;
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
    }
}