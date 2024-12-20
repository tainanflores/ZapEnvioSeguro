namespace ZapEnvioSeguro.Forms
{
    partial class ProgressForm
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
            progressBarImport = new ProgressBar();
            labelStatus = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            lbTituloProgress = new Label();
            btnCancelarProcesso = new Button();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // progressBarImport
            // 
            progressBarImport.ForeColor = Color.Lime;
            progressBarImport.Location = new Point(11, 46);
            progressBarImport.Name = "progressBarImport";
            progressBarImport.Size = new Size(360, 23);
            progressBarImport.Style = ProgressBarStyle.Continuous;
            progressBarImport.TabIndex = 0;
            // 
            // labelStatus
            // 
            labelStatus.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelStatus.ForeColor = SystemColors.Window;
            labelStatus.Location = new Point(11, 74);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(360, 23);
            labelStatus.TabIndex = 1;
            labelStatus.Text = "0 de 0";
            labelStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.SteelBlue;
            flowLayoutPanel1.Controls.Add(lbTituloProgress);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(384, 27);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // lbTituloProgress
            // 
            lbTituloProgress.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTituloProgress.ForeColor = SystemColors.Window;
            lbTituloProgress.Location = new Point(3, 0);
            lbTituloProgress.Name = "lbTituloProgress";
            lbTituloProgress.Size = new Size(381, 27);
            lbTituloProgress.TabIndex = 3;
            lbTituloProgress.Text = "Importando Contatos";
            lbTituloProgress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnCancelarProcesso
            // 
            btnCancelarProcesso.Location = new Point(296, 76);
            btnCancelarProcesso.Name = "btnCancelarProcesso";
            btnCancelarProcesso.Size = new Size(75, 23);
            btnCancelarProcesso.TabIndex = 5;
            btnCancelarProcesso.Text = "Cancelar";
            btnCancelarProcesso.UseVisualStyleBackColor = true;
            btnCancelarProcesso.Visible = false;
            // 
            // ProgressForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(384, 111);
            Controls.Add(btnCancelarProcesso);
            Controls.Add(labelStatus);
            Controls.Add(progressBarImport);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProgressForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "ProgressForm";
            TopMost = true;
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar progressBarImport;
        private Label labelStatus;
        private FlowLayoutPanel flowLayoutPanel1;
        public Label lbTituloProgress;
        private Button btnCancelarProcesso;
    }
}