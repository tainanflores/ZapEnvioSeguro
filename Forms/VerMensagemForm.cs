using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZapEnvioSeguro.Classes;

namespace ZapEnvioSeguro.Forms
{
    public partial class VerMensagemForm : Form
    {
        private long contatoId;
        private MainForm mainForm;
        public VerMensagemForm(MainForm form, long id)
        {
            mainForm = form;
            contatoId = id;
            InitializeComponent();
        }
    }
}
