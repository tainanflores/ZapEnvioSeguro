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
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnRepetirEnvio = new Button();
            btnEnviarFalhas = new Button();
            label7 = new Label();
            label8 = new Label();
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(776, 153);
            label3.Name = "label3";
            label3.Size = new Size(102, 25);
            label3.TabIndex = 16;
            label3.Text = "0 Contatos";
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
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(776, 205);
            label5.Name = "label5";
            label5.Size = new Size(102, 25);
            label5.TabIndex = 18;
            label5.Text = "0 Contatos";
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
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(776, 252);
            label8.Name = "label8";
            label8.Size = new Size(102, 25);
            label8.TabIndex = 22;
            label8.Text = "0 Contatos";
            // 
            // VerMensagemForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(btnEnviarFalhas);
            Controls.Add(btnRepetirEnvio);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(lbDataEnvio);
            Controls.Add(label6);
            Controls.Add(txtMensagem);
            Name = "VerMensagemForm";
            Text = "VerMensagemForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label6;
        private RichTextBox txtMensagem;
        private Label lbDataEnvio;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnRepetirEnvio;
        private Button btnEnviarFalhas;
        private Label label7;
        private Label label8;
    }
}