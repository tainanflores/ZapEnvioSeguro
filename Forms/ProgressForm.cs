using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZapEnvioSeguro.Forms
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;            
        }

        public void CenterToParentForm(Form parent)
        {
            if (parent != null)
            {
                // Calcula a posição central
                int x = parent.Location.X + (parent.Width / 2) - (this.Width / 2);
                int y = parent.Location.Y + (parent.Height / 2) - (this.Height / 2);
                this.Location = new System.Drawing.Point(x, y);
            }
        }


        public void UpdateProgress(int percent, string status = "")
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int, string>(UpdateProgress), percent, status);
                return;
            }
            progressBarImport.Value = percent;
            labelStatus.Text = status;
        }

        public void SetMax(int max)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(SetMax), max);
                return;
            }

            progressBarImport.Maximum = max;
        }

        public void SetMin(int min)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(SetMin), min);
                return;
            }

            progressBarImport.Minimum = min;
        }
    }
}
