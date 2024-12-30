using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using ZapEnvioSeguro.Classes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ZapEnvioSeguro.Forms
{
    public partial class VerMensagemForm : Form
    {
        private DataTable dtMessage;
        private long messageId;
        private MainForm mainForm;
        private DatabaseHelper dbHelper;

        public VerMensagemForm(MainForm form, DataTable dt)
        {
            mainForm = form;
            dtMessage = dt;
            dbHelper = new DatabaseHelper();
            InitializeComponent();
        }

        private async void VerMensagemForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (dtMessage.Rows.Count > 0)
                {
                    DataRow row = dtMessage.Rows[0];
                    txtMensagem.Text = row["Mensagem"].ToString();
                    lbDataEnvio.Text = row["DataEnvio"].ToString();
                    lbEnvioSolicitado.Text = row["QuantidadeContatosSolicitados"].ToString();
                    lbEnviosSucesso.Text = row["QuantidadeContatosSucesso"].ToString();
                    lbEnvioFalha.Text = (Int32.Parse(row["QuantidadeContatosSolicitados"].ToString()) - Int32.Parse(row["QuantidadeContatosSucesso"].ToString())).ToString();
                    messageId = Int64.Parse(row["Id"].ToString());

                    if (row["FalhaInesperada"].ToString() == "True")
                    {
                        lbNaoPodeRepetir.Visible = true;
                        btnEnviarFalhas.Enabled = false;
                        btnRepetirEnvio.Enabled = false;
                    }
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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnRepetirEnvio_Click(object sender, EventArgs e)
        {
            btnRepetirEnvio.Enabled = false;

            var result = MessageBox.Show(
                    $"Deseja reenviar para todos os {lbEnvioSolicitado.Text} contatos dessa mensagem?",
                    "Confirmar Envio",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (result == DialogResult.No)
            {
                btnRepetirEnvio.Enabled = true;
                return;
            }
            this.Enabled = false;
#if DEBUG
            //mainForm.tabControl1.SelectTab(0);
#endif

            List<Contato> contatosList = new List<Contato>();
            string mensagem = txtMensagem.Text;

            string query = "SELECT * FROM Contatos WHERE Id in (SELECT TelefoneId FROM MensagemEnviada WHERE MensagemId = @MensagemId)";

            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@MensagemId", messageId)
            };

            var dataTable = await dbHelper.ExecuteQueryAsync(query, sp);

            contatosList.Clear();

            foreach (DataRow row in dataTable.Rows)
            {
                contatosList.Add(new Contato
                {
                    Id = (long)row["Id"],
                    Nome = row["Nome"].ToString(),
                    Telefone = row["Telefone"].ToString(),
                    Telefone_Serialized = row["Telefone_Serialized"].ToString(),
                    Sexo = row["Sexo"].ToString(),
                    DateLastReceivedMsg = row.IsNull("DateLastReceivedMsg") ? (DateTime?)null : (DateTime)row["DateLastReceivedMsg"],
                    DateLastSentMsg = row.IsNull("DateLastSentMsg") ? (DateTime?)null : (DateTime)row["DateLastSentMsg"],
                    IsBusiness = row.IsNull("IsBusiness") ? false : (bool)row["IsBusiness"],
                    PushName = row["PushName"].ToString(),
                    IdEmpresa = (long)row["IdEmpresa"],
                    Selecionado = false,
                    TelefoneOrigem = row["TelefoneOrigem"].ToString()
                });
            }

            await mainForm.EnviarMensagemSegura(contatosList, mensagem, 0);
            this.Enabled = true;

            mainForm.LoadMessages();
            Close();
        }

        private async void btnEnviarFalhas_Click(object sender, EventArgs e)
        {
            if(lbEnvioFalha.Text == "0")
            {
                MessageBox.Show("Não houve nenhuma falha no envio dessa mensagem", "Aviso",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;                    
            }

            btnEnviarFalhas.Enabled = false;

            var result = MessageBox.Show(
                    $"Deseja reenviar para todos os {lbEnvioFalha.Text} contatos falharam o envio?",
                    "Confirmar Envio",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (result == DialogResult.No)
            {
                btnRepetirEnvio.Enabled = true;
                return;
            }
            this.Enabled = false;


            List<Contato> contatosList = new List<Contato>();
            string mensagem = txtMensagem.Text;

            string query = "SELECT * FROM Contatos WHERE Id in (SELECT TelefoneId FROM MensagemEnviada WHERE MensagemId = @MensagemId AND SucessoEnviada = @SucessoEnviada)";

            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@MensagemId", messageId),
                new SqlParameter("@SucessoEnviada", false)
            };

            var dataTable = await dbHelper.ExecuteQueryAsync(query, sp);

            contatosList.Clear();

            foreach (DataRow row in dataTable.Rows)
            {
                contatosList.Add(new Contato
                {
                    Id = (long)row["Id"],
                    Nome = row["Nome"].ToString(),
                    Telefone = row["Telefone"].ToString(),
                    Telefone_Serialized = row["Telefone_Serialized"].ToString(),
                    Sexo = row["Sexo"].ToString(),
                    DateLastReceivedMsg = row.IsNull("DateLastReceivedMsg") ? (DateTime?)null : (DateTime)row["DateLastReceivedMsg"],
                    DateLastSentMsg = row.IsNull("DateLastSentMsg") ? (DateTime?)null : (DateTime)row["DateLastSentMsg"],
                    IsBusiness = row.IsNull("IsBusiness") ? false : (bool)row["IsBusiness"],
                    PushName = row["PushName"].ToString(),
                    IdEmpresa = (long)row["IdEmpresa"],
                    Selecionado = false, // Inicializa como não selecionado
                    TelefoneOrigem = row["TelefoneOrigem"].ToString()

                });
            }

            await mainForm.EnviarMensagemSegura(contatosList, mensagem, messageId);
            this.Enabled = true;
            this.Close();
        }
    }
}
