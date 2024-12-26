using System.Data;
using Microsoft.Data.SqlClient;
using ZapEnvioSeguro.Classes;

namespace ZapEnvioSeguro.Forms
{
    public partial class VerMensagemForm : Form
    {
        private long contatoId;
        private MainForm mainForm;
        private DatabaseHelper dbHelper;

        public VerMensagemForm(MainForm form, long id)
        {
            mainForm = form;
            contatoId = id;
            InitializeComponent();
        }

        private async void VerMensagemForm_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM Mensagens WHERE Id = @Id";
                SqlParameter[] sp = new SqlParameter[]
                {
                new SqlParameter("@Id",contatoId)
                };

                DataTable dt = await dbHelper.ExecuteQueryAsync(query, sp);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtMensagem.Text = row["Mensagem"].ToString();
                    lbDataEnvio.Text = row["DataEnvio"].ToString();
                    lbEnvioSolicitado.Text = row["QuantidadeContatosSolicitados"].ToString();
                    lbEnviosSucesso.Text = row["QuantidadeContatosSucesso"].ToString();
                    lbEnvioFalha.Text = (Int32.Parse(row["QuantidadeContatosSolicitados"].ToString()) - Int32.Parse(row["QuantidadeContatosSucesso"].ToString())).ToString();
                }
                else
                {
                    MessageBox.Show("Mensagem não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:", ex.Message);
                this.Close();
            }
        }
    }
}
