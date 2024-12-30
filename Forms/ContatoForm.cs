using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using ZapEnvioSeguro.Classes;

namespace ZapEnvioSeguro.Forms
{
    public partial class ContatoForm : Form
    {
        private long contatoId;
        private MainForm mainForm;
        private DatabaseHelper dbHelper;

        public ContatoForm(MainForm form, long id)
        {
            InitializeComponent();
            mainForm = form;
            contatoId = id;
            dbHelper = new DatabaseHelper();

            
        }

        private void ContatoForm_Load(object sender, EventArgs e)
        {
            txtTelefone.MaxLength = 15;
            this.Enabled = false;
            if (contatoId == 0)
            {
                lblTitulo.Text = "Adicionar Novo Contato";
                btnSalvar.Text = "Adicionar";
            }
            else
            {
                CarregarDadosContato();
                lblTitulo.Text = "Editar Contato";
            }
            this.Enabled = true;
        }

        private async void CarregarDadosContato()
        {
            try
            {
                // SQL Server query
                string query = "SELECT Nome, Telefone, Sexo FROM Contatos WHERE Id = @Id";
                var parameters = new SqlParameter[] {
                    new SqlParameter("@Id", contatoId)
                };

                DataTable dataTable = await dbHelper.ExecuteQueryAsync(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    txtNome.Text = row["Nome"].ToString();
                    txtTelefone.Text = row["Telefone"].ToString();
                    cmbSexo.SelectedItem = row["Sexo"].ToString();
                }
                else
                {
                    MessageBox.Show("Contato não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados do contato: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string telefone = txtTelefone.Text;
            var sexo = cmbSexo.SelectedItem;

            if(!ValidarTelefone(telefone))
            {
                MessageBox.Show("Número de telefone inválido. Certifique-se de usar o formato (12) 91234-5678.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(telefone))
            {
                MessageBox.Show("Os campos nome e telefone são obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string query;
                SqlParameter[] parameters;

                if (contatoId == 0) // Inserir novo contato
                {
                    query = "INSERT INTO Contatos (Nome, Telefone, Sexo) VALUES (@Nome, @Telefone, @Sexo)";
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Nome", nome),
                        new SqlParameter("@Telefone", telefone),
                        new SqlParameter("@Sexo", sexo == null ? "O" : sexo.ToString() )
                    };
                }
                else // Atualizar contato existente
                {
                    query = "UPDATE Contatos SET Nome = @Nome, Telefone = @Telefone, Sexo = @Sexo WHERE Id = @Id";
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Nome", nome),
                        new SqlParameter("@Telefone", telefone),
                        new SqlParameter("@Sexo", sexo == null ? "O" : sexo.ToString()),
                        new SqlParameter("@Id", contatoId)
                    };
                }
                // Executa a query (inserção ou atualização)
                await dbHelper.ExecuteNonQueryAsync(query, parameters);

                MessageBox.Show(contatoId == 0 ? "Novo contato adicionado com sucesso!" : "Contato atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (mainForm != null)
                {
                    mainForm.LoadContacts(); // Atualiza os contatos na tela principal
                }

                this.Close(); // Fecha o formulário

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar o contato: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void txtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas números e a tecla Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true; // Impede a digitação de qualquer caractere não numérico
                System.Media.SystemSounds.Beep.Play(); // Toca um som de erro
            }
        }

        private void txtTelefone_TextChanged(object sender, EventArgs e)
        {
            // Obtém o texto atual
            string texto = txtTelefone.Text;

            // Remove qualquer caracter não numérico
            texto = string.Concat(texto.Where(char.IsDigit));

            // Limita o comprimento para 10 dígitos (formato de telefone brasileiro comum)
            if (texto.Length > 11)
                texto = texto.Substring(0, 11);

            // Aplica a formatação conforme o número de caracteres
            if (texto.Length > 6)
            {
                // Se tiver mais de 6 dígitos, formata como (XX) XXXX-XXXX
                texto = string.Format("({0}) {1}-{2}",
                    texto.Substring(0, 2),
                    texto.Substring(2, 5),
                    texto.Substring(7));
            }
            else if (texto.Length > 2)
            {
                // Se tiver mais de 2, mas menos de 7, formata como (XX) XXXX
                texto = string.Format("({0}) {1}",
                    texto.Substring(0, 2),
                    texto.Substring(2));
            }
            else if (texto.Length > 0)
            {
                // Se tiver 1 ou 2 números, formata como (XX
                texto = string.Format("({0}", texto.Substring(0, texto.Length));
            }
            else
            {
                // Caso vazio, nada a fazer
                texto = string.Empty;
            }

            // Atualiza o texto no TextBox com a formatação
            txtTelefone.Text = texto;

            // Coloca o cursor no final do texto
            int cursorPosition = txtTelefone.Text.Length;

            // Se o último caracter for um `)` ou `-`, move o cursor um caractere antes
            if (cursorPosition > 0 && (texto[cursorPosition - 1] == ')' || texto[cursorPosition - 1] == '-'))
            {
                cursorPosition--;
            }

            // Coloca o cursor no final do texto
            txtTelefone.SelectionStart = cursorPosition;
        }

        static bool ValidarTelefone(string telefone)
        {
            // Expressão regular para o formato (99) 99999-9999
            string pattern = @"^\(\d{2}\) \d{5}-\d{4}$";
            return Regex.IsMatch(telefone, pattern);
        }

    }
}
