using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using ZapEnvioSeguro.Classes;
using ZapEnvioSeguro.Entidades;
using ZapEnvioSeguro.Forms;



namespace ZapEnvioSeguro
{
    public partial class MainForm : Form
    {
        private Panel overlayPanel;
        private PictureBox loadingGif;

        private DatabaseHelper dbHelper;

        private readonly WppConnect wppConnect = new WppConnect();

        private System.Windows.Forms.Timer searchTimer;
        private System.Windows.Forms.Timer searchTimerMsg;
        private System.Windows.Forms.Timer SendTimer;

        public List<Contato> contatosList = new List<Contato>();
        public List<Mensagens> mensagensList = new List<Mensagens>();
        public List<Contato> contatosSelecionadosParaEnvio = new List<Contato>();
        private List<Contato> contatosListVirtualFiltered = new List<Contato>();
        private List<Mensagens> mensagensListVirtualFiltered = new List<Mensagens>();

        public static bool enviarClicado = false;
        public static bool wppInjetado = false;
        public static bool sincronizarContatos = false;
        public static bool iniciouSincronizar = false;
        public static bool contatosProntos = false;
        public static bool filtroAtivo = false;

        private bool isSendingMessage = false;
        private bool iniciouConversa = false;
        private bool filtrandoContatos = false;

        private static string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string ScriptFilePath = Path.Combine(appDataPath, "ZapEnvioSeguro", "wppconnect-wa.js");
        private static long mensagemIdEnviando;

        private static long telefoneIdEnviando;

        private string 
            mensagemTextoEnviando,
            telefoneEnviandoSerialized;

        private const int timeoutSeconds = 30;

        public MainForm()
        {
            dbHelper = new DatabaseHelper();
            InitializeComponent();

            SendTimer = new System.Windows.Forms.Timer();
            SendTimer.Interval = timeoutSeconds * 1000;
            SendTimer.Tick += SendTimer_Tick;
        }

        private void ShowLoading()
        {
            int panelSize = 200;

            overlayPanel = new Panel
            {
                Size = new Size(panelSize, panelSize),
                BackColor = Color.AliceBlue,
                Parent = this,
                Visible = true
            };

            overlayPanel.Location = new Point(
                (this.ClientSize.Width - overlayPanel.Width) / 2,
                (this.ClientSize.Height - overlayPanel.Height) / 2
            );

            string gifPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "loading.gif");
            loadingGif = new PictureBox
            {
                Image = Image.FromFile(gifPath),
                BackColor = Color.FromArgb(0,0,0,0), // Transpar�ncia no PictureBox
                Size = new Size(100, 100), // Tamanho do PictureBox
                SizeMode = PictureBoxSizeMode.Zoom, // Ajusta o GIF dentro do PictureBox
                Parent = overlayPanel
            };

            // Centralizar o GIF no painel
            loadingGif.Location = new Point(
                (overlayPanel.Width - loadingGif.Width) / 2,
                (overlayPanel.Height - loadingGif.Height) / 2
            );

            this.Controls.Add(overlayPanel);
            overlayPanel.BringToFront();
            loadingGif.BringToFront();
        }

        private void HideLoading()
        {
            if (overlayPanel != null)
            {
                overlayPanel.Visible = false;
                overlayPanel.Dispose();
            }
        }

        private async Task LoadDataAsync()
        {
            // Carregar os contatos e mensagens de forma ass�ncrona
            await Task.WhenAll(LoadContacts(), LoadMessages());

            HideLoading();
            tabControl1.Enabled = true;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Invoke((Action)ShowLoading);
            });
            tabControl1.Enabled = false;

            await wppConnect.DownloadWppJs();

            string webViewDataFolder = Path.Combine(appDataPath, "ZapEnvioSeguro", "WebView2");
            Directory.CreateDirectory(webViewDataFolder);

            var environment = await CoreWebView2Environment.CreateAsync(
                userDataFolder: webViewDataFolder
            );


            await webView21.EnsureCoreWebView2Async(environment);
            webView21.CoreWebView2.DOMContentLoaded += CoreWebView2_DOMContentLoaded;
            if (webView21.CoreWebView2 != null)
            {
                // Desabilitar os di�logos padr�o de JavaScript
                webView21.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;
                webView21.CoreWebView2.Settings.IsPinchZoomEnabled = false;
                webView21.CoreWebView2.Settings.IsZoomControlEnabled = false;
            }

            webView21.CoreWebView2.Navigate("https://web.whatsapp.com/");

            dataGridViewContatos.VirtualMode = true;
            dataGridViewMensagens.VirtualMode = true;

            // Habilita o double buffering
            dataGridViewContatos.EnableDoubleBuffering();
            dataGridViewMensagens.EnableDoubleBuffering();

            // Adiciona as colunas necess�rias (Selecionar, Editar, etc.)
            InitializeDataGridViewColumns();
            InitializeDataGridViewColumnsMessages();

            // Opcional: Melhorar a performance desabilitando alguns comportamentos
            dataGridViewContatos.RowHeadersVisible = false;
            dataGridViewContatos.AllowUserToAddRows = false;
            dataGridViewContatos.AllowUserToDeleteRows = false;
            dataGridViewContatos.AllowUserToResizeRows = false;
            dataGridViewContatos.AllowUserToOrderColumns = false;

            // Inicializa o Timer para debounce na busca
            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 300; // Intervalo de 300 ms
            searchTimer.Tick += SearchTimer_Tick;
            searchTimerMsg = new System.Windows.Forms.Timer();
            searchTimerMsg.Interval = 300; // Intervalo de 300 ms
            searchTimerMsg.Tick += SearchTimerMsg_Tick;

            await LoadDataAsync();
            //LoadContacts();
            //LoadMessages();
            txtDDD.MaxLength = 2;
            txtDias.MaxLength = 3;
            txtDddMensagem.MaxLength = 2;
            txtDiasMensagem.MaxLength = 3;
            contatosProntos = true;
        }

        public void SetSendingMessageState(bool state)
        {
            if (state)
            {
                isSendingMessage = true;

                SendTimer.Start();
            }
            else
            {
                isSendingMessage = false;

                searchTimer.Stop();
            }
        }

        private void SendTimer_Tick(object sender, EventArgs e)
        {
            if (isSendingMessage)
            {
                InjetarScriptWebview("window.chrome.webview.postMessage('mensagemNaoEnviada');");
            }
            SendTimer.Stop();
        }


        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();

            ApplyFiltersAsync();

        }
        private void SearchTimerMsg_Tick(object sender, EventArgs e)
        {
            searchTimerMsg.Stop();

            ApplyFiltersAsyncMensagens();

        }


        private void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            if (chkFiltroDDD.Checked && !string.IsNullOrEmpty(txtDDD.Text))
            {
                filtroAtivo = true;
                ApplyFiltersAsync();
                btnRemoverFiltros.Enabled = true;
            }
            else if (chkFiltroDias.Checked && !string.IsNullOrEmpty(txtDias.Text))
            {
                filtroAtivo = true;
                ApplyFiltersAsync();
                btnRemoverFiltros.Enabled = true;
            }
            else if (chkFiltroBusiness.Checked || chkFiltroSemConversa.Checked || cmbZapOrigem.SelectedIndex != 0)
            {
                filtroAtivo = true;
                ApplyFiltersAsync();
                btnRemoverFiltros.Enabled = true;
            }
            else
            {
                ApplyFiltersAsync();
            }
        }

        private void btnRemoverFiltros_Click(object sender, EventArgs e)
        {
            txtDDD.Clear();
            txtDias.Clear();
            filtroAtivo = false;

            chkFiltroDDD.Checked = false;
            chkFiltroDias.Checked = false;
            btnRemoverFiltros.Enabled = false;
            cmbZapOrigem.SelectedIndex = 0;

            ApplyFiltersAsync();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (enviarClicado && tabControl1.SelectedTab != tabEnviarMsg)
            {
                var mensagem = $"Se voc� sair dessa tela ir� limpar a mensagem digitada e os contatos confirmados. Deseja mesmo sair?";
                var result = MessageBox.Show(mensagem, "Deseja mesmo sair?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    enviarClicado = false;
                    panelFiltroMensagem.Enabled = true;
                    btConfirmarOpcoesMensagem.Enabled = true;
                    txtMensagem.Clear();
                    contatosSelecionadosParaEnvio.Clear();
                    lbQuantidadeContatos.Text = "0 Contatos";
                }
                else
                {
                    tabControl1.SelectedTab = tabEnviarMsg;
                }
            }

            if (!contatosProntos)
            {
                tabControl1.SelectedTab = tabWhatsApp;
            }

        }

        private void chkFiltroDias_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFiltroDias.Checked)
            {
                txtDias.Enabled = true;
                txtDias.Focus();
            }
            else
            {
                txtDias.Enabled = false;
                txtDias.Clear();
            }
        }

        private void chkFiltroDDD_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFiltroDDD.Checked)
            {
                txtDDD.Enabled = true;
                txtDDD.Focus();
            }
            else
            {
                txtDDD.Enabled = false;
                txtDDD.Clear();
            }
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {            
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void txtBusca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (chkBuscaTelefone.Checked)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    SystemSounds.Beep.Play();
                    e.Handled = true;
                }
            }
        }
        private void chkSelecionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (filtrandoContatos)
            {
                return;
            }

            bool selecionarTodos = chkSelecionarTodos.Checked;

            foreach (DataGridViewRow row in dataGridViewContatos.Rows)
            {
                row.Cells["Selecionar"].Value = selecionarTodos;
                var contatoId = (long)row.Cells["ID"].Value;
                var contato = contatosList.First(c => c.Id == contatoId); // Encontra o contato na lista
                contato.Selecionado = selecionarTodos;
                lbQtdSelcionados.Text = $"{contatosList.Count(c => c.Selecionado).ToString()} selecionados";

                if (selecionarTodos)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void txtDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        private void txtDDD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        private void dataGridViewContatos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se a c�lula alterada � o checkbox (primeira coluna de sele��o)
            if (e.ColumnIndex == 0 && e.RowIndex >= 0) // Coluna 0 � a coluna de checkbox
            {
                DataGridViewRow row = dataGridViewContatos.Rows[e.RowIndex];
                var contatoId = (long)row.Cells["ID"].Value;
                var contato = contatosList.First(c => c.Id == contatoId);
                contato.Selecionado = Convert.ToBoolean(row.Cells["Selecionar"].Value); // Atualiza a propriedade 'Selecionado'

                // Se o checkbox for marcado
                if (Convert.ToBoolean(row.Cells["Selecionar"].Value))
                {
                    // Alterar a cor de fundo da linha
                    row.DefaultCellStyle.BackColor = Color.LightGreen; // Cor quando marcado

                }
                else
                {
                    // Reverter a cor de fundo da linha
                    row.DefaultCellStyle.BackColor = Color.White; // Cor padr�o quando desmarcado
                }
            }
        }

        private void dataGridViewContatos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewContatos.IsCurrentCellDirty)
            {
                dataGridViewContatos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridViewContatos_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= contatosListVirtualFiltered.Count)
                return;

            var contato = contatosListVirtualFiltered[e.RowIndex];

            switch (dataGridViewContatos.Columns[e.ColumnIndex].Name)
            {
                case "Selecionar":
                    e.Value = contato.Selecionado;
                    break;
                case "ID":
                    e.Value = contato.Id;
                    break;
                case "Nome":
                    e.Value = string.IsNullOrEmpty(contato.Nome) ? "Sem Nome" : contato.Nome;
                    break;
                case "Telefone":
                    e.Value = contato.Telefone;
                    break;
                case "Business":
                    e.Value = contato.IsBusiness ? "Business" : "Normal";
                    break;
                case "Sexo":
                    e.Value = contato.Sexo;
                    break;
                case "�ltima Mensagem Recebida":
                    e.Value = contato.DateLastReceivedMsg?.ToString("dd/MM/yyyy HH:mm") ?? "";
                    break;
                case "�ltima Mensagem Enviada":
                    e.Value = contato.DateLastSentMsg?.ToString("dd/MM/yyyy HH:mm") ?? "";
                    break;
                case "Editar":
                    e.Value = "Editar";
                    break;
            }

            // Definir a cor da linha com base na sele��o
            if (dataGridViewContatos.Columns[e.ColumnIndex].Name == "Selecionar")
            {
                var row = dataGridViewContatos.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = contato.Selecionado ? Color.LightGreen : Color.White;
                lbQtdSelcionados.Text = $"{contatosList.Count(c => c.Selecionado).ToString()} selecionados";

            }
        }

        private void DataGridViewContatos_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= contatosListVirtualFiltered.Count)
                return;

            var contato = contatosListVirtualFiltered[e.RowIndex];

            if (dataGridViewContatos.Columns[e.ColumnIndex].Name == "Selecionar")
            {
                contato.Selecionado = Convert.ToBoolean(e.Value);

                dataGridViewContatos.InvalidateRow(e.RowIndex);
            }
        }

        private void dataGridViewContatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewContatos.Columns[e.ColumnIndex].Name == "Selecionar")
            {
                var contato = contatosList[e.RowIndex];
                e.Value = contato.Selecionado;
            }
        }

        private async void dataGridViewContatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dataGridViewContatos.Columns[e.ColumnIndex].Name == "Editar")
            {
                var contato = contatosListVirtualFiltered[e.RowIndex];

                // Implementar a l�gica de edi��o do contato
                if (e.ColumnIndex == dataGridViewContatos.Columns["Editar"].Index && e.RowIndex >= 0)
                {
                    long contatoId = contato.Id;
                    string query = "SELECT Id, Nome, Telefone, Sexo FROM Contatos WHERE Id = @Id";
                    var parameters = new SqlParameter[] {
                        new SqlParameter("@Id", contatoId)
                    };

                    ShowLoading();

                    DataTable dataTable = await dbHelper.ExecuteQueryAsync(query, parameters);

                    HideLoading();

                    ContatoForm editarForm = new ContatoForm(this, dataTable);
                    editarForm.StartPosition = FormStartPosition.CenterParent;

                    editarForm.ShowDialog(this);
                }
            }
        }

        private void InitializeDataGridViewColumns()
        {
            dataGridViewContatos.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter, // Centraliza o texto
                Font = new Font("Segoe UI", 9, FontStyle.Bold), // Define a fonte como negrito
                ForeColor = Color.White, // Cor do texto branca
                BackColor = Color.Gray // Cor de fundo cinza
            };

            if (dataGridViewContatos.Columns["Selecionar"] == null)
            {
                DataGridViewCheckBoxColumn selectColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "Selecionar",
                    HeaderText = "Selecionar",
                    Width = 100,
                    FalseValue = false,
                    TrueValue = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dataGridViewContatos.Columns.Add(selectColumn);
            }

            // Adiciona as demais colunas
            dataGridViewContatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "ID", HeaderText = "ID", ReadOnly = true, Width = 100, AutoSizeMode = DataGridViewAutoSizeColumnMode.None, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, } });
            dataGridViewContatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Nome", ReadOnly = true });
            dataGridViewContatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Telefone", HeaderText = "Telefone", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, } });
            dataGridViewContatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Business", HeaderText = "Business?", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, } });
            dataGridViewContatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Sexo", HeaderText = "Sexo", ReadOnly = true, Width = 100, AutoSizeMode = DataGridViewAutoSizeColumnMode.None, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, } });
            dataGridViewContatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "�ltima Mensagem Recebida", HeaderText = "Data da �ltima Mensagem Recebida", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dataGridViewContatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "�ltima Mensagem Enviada", HeaderText = "Data da �ltima Mensagem Enviada", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            if (dataGridViewContatos.Columns["Editar"] == null)
            {
                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Editar",
                    HeaderText = "",
                    Text = "Editar",
                    UseColumnTextForButtonValue = true,
                    Width = 100,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dataGridViewContatos.Columns.Add(editButtonColumn);
            }
        }

        public async Task LoadContacts()
        {

            var query = $"SELECT " +
                 $"Id, Nome, Telefone, Telefone_Serialized, Sexo, DateLastReceivedMsg, DateLastSentMsg, IsBusiness, PushName, IdEmpresa, TelefoneOrigem " +
                 $"FROM " +
                 $"Contatos " +
                 $"WHERE IdEmpresa = @IdEmpresa";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@IdEmpresa", Evento.IdEmpresa)
            };

            try
            {
                var dataTable = await dbHelper.ExecuteQueryAsync(query, parameters);
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
                        Selecionado = false, // Inicializa como n�o selecionado
                        TelefoneOrigem = row["TelefoneOrigem"].ToString()
                    });
                }

                contatosList = contatosList.OrderBy(x => x.Nome).ToList();

                // Inicializa a lista filtrada com todos os contatos
                contatosListVirtualFiltered = new List<Contato>(contatosList);

                // Atualiza o RowCount no modo virtual
                dataGridViewContatos.SuspendDrawing();

                try
                {
                    dataGridViewContatos.RowCount = contatosListVirtualFiltered.Count;
                }
                finally
                {
                    dataGridViewContatos.ResumeDrawing();
                    InicializarComboBoxZapOrigem();
                    lbQtdFiltrados.Text = $"{contatosListVirtualFiltered.Count.ToString()} Contatos";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar contatos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InicializarComboBoxZapOrigem()
        {
            // Adiciona a op��o "Todos"
            cmbZapOrigem.Items.Clear();
            cmbZapOrigem.Items.Add("Todos");

            // Obt�m os valores �nicos da coluna TelefoneOrigem
            var telefonesUnicos = contatosList
                .Select(contato => contato.TelefoneOrigem)
                .Distinct()
                .OrderBy(telefone => telefone)
                .ToList();

            // Adiciona os valores �nicos ao ComboBox
            cmbZapOrigem.Items.AddRange(telefonesUnicos.ToArray());

            // Define "Todos" como o valor padr�o selecionado
            cmbZapOrigem.SelectedIndex = 0;
        }

        private async void ApplyFiltersAsync()
        {
            try
            {
                filtrandoContatos = true;
                chkSelecionarTodos.Checked = false;

                // Captura o texto de busca atual
                string searchTerm = string.Empty;
                string zapOrigem = cmbZapOrigem.SelectedItem.ToString();

                if (txtBusca.InvokeRequired)
                {
                    txtBusca.Invoke(new MethodInvoker(delegate
                    {
                        searchTerm = txtBusca.Text.Trim().ToLower();
                    }));
                }
                else
                {
                    searchTerm = txtBusca.Text.Trim().ToLower();
                }

                searchTerm = RemoverAcentos(searchTerm);

                var filteredList = await Task.Run(() =>
                {
                    var query = contatosList.AsQueryable();

                    if (filtroAtivo)
                    {
                        if (chkFiltroSemConversa.Checked)
                        {
                            query = query.Where(c => c.DateLastReceivedMsg != null);
                        }

                        if (chkFiltroBusiness.Checked)
                        {
                            query = query.Where(c => c.IsBusiness);
                        }

                        if (zapOrigem != "Todos")
                        {
                            query = query.Where(c => c.TelefoneOrigem == zapOrigem);
                        }

                        // Filtro por Dias
                        if (chkFiltroDias.Checked && int.TryParse(txtDias.Text, out int dias))
                        {
                            var dataLimite = DateTime.Now.AddDays(-dias);
                            query = query.Where(c => c.DateLastReceivedMsg <= dataLimite || c.DateLastReceivedMsg == null);
                        }

                        // Filtro por DDD
                        if (chkFiltroDDD.Checked && !string.IsNullOrEmpty(txtDDD.Text))
                        {
                            var ddd = txtDDD.Text;
                            query = query.Where(c => c.Telefone.StartsWith($"({ddd})"));
                        }
                    }

                    // Filtro de Busca em Tempo Real
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        if (chkBuscaTelefone.Checked)
                        {
                            query = query.Where(c => string.Concat(c.Telefone.Where(char.IsDigit)).Contains(searchTerm));
                        }
                        else
                        {
                            query = query.Where(c => RemoverAcentos(c.Nome.ToLower()).Contains(searchTerm));
                        }
                    }

                    return query.ToList();
                });

                // Atualiza a lista filtrada
                dataGridViewContatos.SuspendDrawing();

                try
                {
                    contatosListVirtualFiltered = filteredList;
                    dataGridViewContatos.RowCount = contatosListVirtualFiltered.Count;
                }
                finally
                {
                    dataGridViewContatos.ResumeDrawing();
                }

                dataGridViewContatos.Refresh();

                lbQtdFiltrados.Text = $"{contatosListVirtualFiltered.Count.ToString()} Contatos";

                filtrandoContatos = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao aplicar filtros: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string RemoverAcentos(string texto)
        {
            var normalizedString = texto.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var character in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(character);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public void UpdateDataGridView(List<Contato>? filteredContacts = null)
        {
            var dataSource = filteredContacts ?? contatosList;

            // Atualiza o DataGridView com os dados filtrados
            var dt = new DataTable();

            dt.Columns.Add("ID", typeof(long));
            dt.Columns.Add("Nome");
            dt.Columns.Add("Telefone");
            dt.Columns.Add("Business");
            dt.Columns.Add("Sexo");
            dt.Columns.Add("�ltima Mensagem Recebida");
            dt.Columns.Add("�ltima Mensagem Enviada");

            foreach (var contato in dataSource)
            {
                dt.Rows.Add(
                    contato.Id,
                    contato.Nome,
                    contato.Telefone,
                    contato.IsBusiness ? "Business" : "Normal",
                    contato.Sexo,
                    contato.DateLastReceivedMsg?.ToString("dd/MM/yyyy HH:mm") ?? "",
                    contato.DateLastSentMsg?.ToString("dd/MM/yyyy HH:mm") ?? ""
                );
            }

            dataGridViewContatos.DataSource = dt;

            // Alterar todos os cabe�alhos para negrito
            dataGridViewContatos.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridViewContatos.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);

            // Tornar as outras colunas somente leitura
            foreach (DataGridViewColumn column in dataGridViewContatos.Columns)
            {
                if (column.Name != "Selecionar")
                {
                    column.ReadOnly = true;
                }
            }

            // Atualiza a cor de fundo das linhas com base no estado de 'Selecionado'
            foreach (DataGridViewRow row in dataGridViewContatos.Rows)
            {
                var contatoId = (long)row.Cells["ID"].Value;
                var contato = dataSource.First(c => c.Id == contatoId); // Encontra o contato correspondente
                row.Cells["Selecionar"].Value = contato.Selecionado;
                lbQtdSelcionados.Text = $"{contatosList.Count(c => c.Selecionado).ToString()} selecionados";

                if (contato.Selecionado)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen; // Cor quando selecionado
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White; // Cor padr�o quando desmarcado
                }
            }
        }

        private void btnAdicionarContato_Click(object sender, EventArgs e)
        {
            ContatoForm adicionarForm = new ContatoForm(this, null);
            adicionarForm.StartPosition = FormStartPosition.CenterParent;
            adicionarForm.ShowDialog(this);
        }

        private void btnEnviarMensagem_Click(object sender, EventArgs e)
        {
            var contatosSelecionados = contatosList.Where(c => c.Selecionado).ToList();

            if (contatosSelecionados.Count == 0)
            {
                MessageBox.Show("Por favor, selecione pelo menos um contato antes de enviar a mensagem.",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var mensagem = $"Voc� est� prestes a enviar uma mensagem para {contatosSelecionados.Count} contatos selecionados. Deseja continuar?";

            var result = MessageBox.Show(mensagem, "Confirmar Envio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                panelFiltroMensagem.Enabled = false;
                LimparPanelFiltros();
                btConfirmarOpcoesMensagem.Enabled = false;
                btCancelarOpcoesMensagem.Enabled = false;

                lbQuantidadeContatos.Text = $"{contatosSelecionados.Count} Contatos";

                tabControl1.SelectedIndex = 2;
                enviarClicado = true;

                contatosSelecionadosParaEnvio = contatosSelecionados;
            }
        }

        private void ckTodosContatosMensagem_CheckedChanged(object sender, EventArgs e)
        {
            if (ckTodosContatosMensagem.Checked)
            {
                chkDddMensagem.Checked = false;
                chkDiasMensagem.Checked = false;
                chkGeneroMensagem.Checked = false;
                cmbGeneroMensagem.SelectedIndex = -1;
                txtDiasMensagem.Clear();
                txtDddMensagem.Clear();
            }
        }

        private void chkDiasMensagem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiasMensagem.Checked)
            {
                txtDiasMensagem.Enabled = true;
                ckTodosContatosMensagem.Checked = false;
            }
            else
            {
                txtDiasMensagem.Enabled = false;
                txtDiasMensagem.Clear();
            }
        }

        private void chkDddMensagem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDddMensagem.Checked)
            {
                txtDddMensagem.Enabled = true;
                ckTodosContatosMensagem.Checked = false;
            }
            else
            {
                txtDddMensagem.Enabled = false;
                txtDddMensagem.Clear();
            }
        }

        private void chkGeneroMensagem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGeneroMensagem.Checked)
            {
                cmbGeneroMensagem.Enabled = true;
                ckTodosContatosMensagem.Checked = false;

            }
            else
            {
                cmbGeneroMensagem.Enabled = false;
                cmbGeneroMensagem.SelectedIndex = -1;
            }
        }

        private void btConfirmarOpcoesMensagem_Click(object sender, EventArgs e)
        {
            if (!chkDiasMensagem.Checked && !chkDddMensagem.Checked && !chkGeneroMensagem.Checked && !ckTodosContatosMensagem.Checked && !chkBusinessMensagem.Checked && !chkFiltroSemConversa.Checked)
            {
                MessageBox.Show("Por favor, selecione ao menos uma op��o para filtrar os contatos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<Contato> contatosFiltrados = contatosList;

            if (chkSemConversaMensagem.Checked)
            {
                contatosFiltrados = contatosFiltrados.Where(c => c.DateLastReceivedMsg.HasValue).ToList();

                if (contatosFiltrados.Count == 0)
                {
                    MessageBox.Show("Nenhum contato corresponde aos crit�rios selecionados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (chkBusinessMensagem.Checked)
            {
                contatosFiltrados = contatosFiltrados.Where(c => c.IsBusiness).ToList();

                if (contatosFiltrados.Count == 0)
                {
                    MessageBox.Show("Nenhum contato corresponde aos crit�rios selecionados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (chkDiasMensagem.Checked)
            {
                if (string.IsNullOrEmpty(txtDiasMensagem.Text) || !int.TryParse(txtDiasMensagem.Text, out int dias))
                {
                    MessageBox.Show("Por favor, insira um n�mero v�lido para Dias.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime dataLimite = DateTime.Now.AddDays(-dias);
                contatosFiltrados = contatosFiltrados.Where(c => c.DateLastReceivedMsg.HasValue && c.DateLastReceivedMsg.Value <= dataLimite || c.DateLastReceivedMsg == null).ToList();

                if (contatosFiltrados.Count == 0)
                {
                    MessageBox.Show("Nenhum contato corresponde aos crit�rios selecionados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (chkDddMensagem.Checked)
            {
                if (string.IsNullOrEmpty(txtDddMensagem.Text) || !txtDddMensagem.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Por favor, insira um DDD v�lido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ddd = txtDddMensagem.Text;

                contatosFiltrados = contatosFiltrados.Where(c => c.Telefone.StartsWith($"({ddd})")).ToList();

                if (contatosFiltrados.Count == 0)
                {
                    MessageBox.Show("Nenhum contato corresponde aos crit�rios selecionados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (chkGeneroMensagem.Checked)
            {
                if (cmbGeneroMensagem.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, selecione um g�nero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string generoSelecionado = cmbGeneroMensagem.SelectedItem.ToString();

                contatosFiltrados = contatosFiltrados.Where(c => c.Sexo.Equals(generoSelecionado, StringComparison.OrdinalIgnoreCase)).ToList();

                if (contatosFiltrados.Count == 0)
                {
                    MessageBox.Show("Nenhum contato corresponde aos crit�rios selecionados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (ckTodosContatosMensagem.Checked)
            {
                contatosFiltrados = contatosList;
            }

            if (contatosFiltrados.Count == 0)
            {
                MessageBox.Show("Nenhum contato corresponde aos crit�rios selecionados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            contatosSelecionadosParaEnvio = contatosFiltrados;

            MessageBox.Show($"Foram selecionados {contatosSelecionadosParaEnvio.Count} contatos.", "Confirma��o", MessageBoxButtons.OK, MessageBoxIcon.Information);

            enviarClicado = true;
            panelFiltroMensagem.Enabled = false;
            btConfirmarOpcoesMensagem.Enabled = false;
            btCancelarOpcoesMensagem.Enabled = true;
            lbQuantidadeContatos.Text = $"{contatosSelecionadosParaEnvio.Count} Contatos";
        }

        private void btCancelarOpcoesMensagem_Click(object sender, EventArgs e)
        {
            enviarClicado = false;
            contatosSelecionadosParaEnvio.Clear();
            panelFiltroMensagem.Enabled = true;
            btConfirmarOpcoesMensagem.Enabled = true;
            btCancelarOpcoesMensagem.Enabled = false;
            lbQuantidadeContatos.Text = $"{contatosSelecionadosParaEnvio.Count} Contatos";
        }

        private void webView21_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {

            if (!wppInjetado)
            {
                string script = Classes.WhatsAppWebScripts.InjetarWPP;
                InjetarScriptWebview(script);
            }
        }

        private async void InjetarScriptWebview(string script)
        {
            try
            {
                await webView21.ExecuteScriptAsync(script);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private async void webView21_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var content = e.WebMessageAsJson;

            if (iniciouSincronizar) return;

            if (content.Contains("iniciouConversa"))
            {
                await webView21.ExecuteScriptAsync(WhatsAppWebScripts.ScriptUsarWhatsAppWeb);
                return;
            }

            if (content.Contains("usarWhatsAppWebClicado"))
            {
                await webView21.ExecuteScriptAsync(WhatsAppWebScripts.ScriptEnviarMensagem);
                return;
            }

            if (content.Contains("enviarClicado"))
            {
                //await Task.Delay(1000);                
                await webView21.ExecuteScriptAsync("window.chrome.webview.postMessage('mensagemEnviada')");
                //string query = "SELECT Telefone_Serialized FROM Contatos WHERE Id = @Id";
                //SqlParameter[] sp = new SqlParameter[]
                //    {
                //        new SqlParameter("@Id", telefoneIdEnviando),
                //    };

                //var idZap = await dbHelper.ExecuteScalarAsync(query, sp);

                //string script = WhatsAppWebScripts.MensagemPorContato(idZap.ToString());

                //await webView21.ExecuteScriptAsync(script);
                return;
            }

            if (content.Contains("mensagemEnviada") || content.Contains("mensagemNaoEnviada"))
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();                

                try
                {
                    SendTimer.Stop();
                    bool enviouSucesso = content.Contains("mensagemEnviada");
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@MensagemId", mensagemIdEnviando),
                        new SqlParameter("@TelefoneId", telefoneIdEnviando),
                    };

                    var existeMsg = await dbHelper.ExecuteScalarAsync("SELECT Id FROM MensagemEnviada WHERE MensagemId = @MensagemId AND TelefoneId = @TelefoneId", parameters);

                    string query = "INSERT INTO MensagemEnviada " +
                  "(MensagemId, TelefoneId, DataEnvio, IdEmpresa, SucessoEnviada) " +
                  "VALUES (@MensagemId, @TelefoneId, @DataEnvio, @IdEmpresa, @SucessoEnviada);";

                    if (existeMsg == null)
                    {
                        parameters = new SqlParameter[]
                        {
                            new SqlParameter("@MensagemId", mensagemIdEnviando),
                            new SqlParameter("@TelefoneId", telefoneIdEnviando),
                            new SqlParameter("@DataEnvio", DateTime.Now.ToString()),
                            new SqlParameter("@IdEmpresa", Evento.IdEmpresa),
                            new SqlParameter("@SucessoEnviada", enviouSucesso ? "ENVIADA" : "FALHA" )
                        };

                        var idMsg = await dbHelper.InsertDataAsync(query, parameters);
                    }
                    else
                    {
                        query = "UPDATE MensagemEnviada SET SucessoEnviada = @SucessoEnviada WHERE Id = @Id";

                        parameters = new SqlParameter[]
                        {
                            new SqlParameter("@Id",existeMsg),
                            new SqlParameter("@SucessoEnviada", enviouSucesso ? "ENVIADA" : "FALHA")
                        };

                        await dbHelper.ExecuteNonQueryAsync(query, parameters);
                    }

                    query = "UPDATE Contatos SET DateLastSentMsg = @DateLastSentMsg, LastSentMsgId = @LastSentMsgId WHERE Id = @Id";
                    parameters = new SqlParameter[]
                      {
                        new SqlParameter("@DateLastSentMsg", DateTime.Now.ToString()),
                        new SqlParameter("@LastSentMsgId", mensagemIdEnviando),
                        new SqlParameter("@Id", telefoneIdEnviando),
                        new SqlParameter("@IdEmpresa", Evento.IdEmpresa)
                      };

                    await dbHelper.ExecuteNonQueryAsync(query, parameters);
                    await Task.Delay(1000);
                    SetSendingMessageState(false);
                    iniciouConversa = false;

                    stopwatch.Stop();

                    Debug.WriteLine($"Tempo gasto para salvar na db: {stopwatch.ElapsedMilliseconds} ms");

                    return;
                }
                catch (Exception ex)
                {
                   MessageBox.Show("Erro:", ex.Message);
                }
            }

            if (content.Contains("conectado"))
            {
                string apiWPP = File.ReadAllText(ScriptFilePath);
                await webView21.CoreWebView2.ExecuteScriptAsync(apiWPP);
                return;
            }

            if (content.Contains("injetado"))
            {
                InjetarScriptWebview(WhatsAppWebScripts.contactList);
                InjetarScriptWebview(WhatsAppWebScripts.contatosComData);
                InjetarScriptWebview(WhatsAppWebScripts.apiMessageHandler);
                wppInjetado = true;
                return;
            }

            if (content.Contains("TelefoneOrigem:"))
            {
                var telefone = TelefoneFormatado(content.Split(':')[1].Replace("\\\"", ""));
                Evento.TelefoneOrigem = telefone;
            }

            if (content.StartsWith("[{\"Telefone") && sincronizarContatos)
            {
                ProgressForm progressForm = new ProgressForm();
                progressForm.CenterToParentForm(this);

                progressForm.SetMin(0);
                progressForm.SetMax(100);
                progressForm.lbTituloProgress.Text = "Importando Datas das �ltimas mensagens";
                this.Enabled = false;
                progressForm.Show();

                var progress = new Progress<ProgressReport>(report =>
                {
                    progressForm.UpdateProgress(report.Percent, report.Message);
                });

                var contatosComData = JsonConvert.DeserializeObject<List<ContatoComData>>(content);

                if (contatosComData == null) return;

                await AtualizarContatosComData(contatosComData, progress);
                this.Enabled = true;
                progressForm.Close();
                sincronizarContatos = false;
                return;
            }

            if (sincronizarContatos)
            {
                iniciouSincronizar = true;
                ProgressForm progressForm = new ProgressForm();
                progressForm.CenterToParentForm(this);
                progressForm.SetMin(0);
                progressForm.SetMax(100);
                progressForm.lbTituloProgress.Text = "Importando Contatos do WhatsApp Web";
                this.Enabled = false;
                progressForm.Show();

                var progress = new Progress<ProgressReport>(report =>
                {
                    progressForm.UpdateProgress(report.Percent, report.Message);
                });


                await ImportarContatos(content, progress);

                this.Enabled = true;

                progressForm.Close();
                iniciouSincronizar = false;
                await LoadContacts();
                InjetarScriptWebview("obterMensagensFiltradas();");
                return;

            }

            if (content.StartsWith("{\"type\":\"ContactList"))
            {
                var response = JsonConvert.DeserializeObject<ContactListResponse>(content);
                var contacts = response?.Data;

                if (contacts == null) { return; };

                int qtd = 0;

                var contatoList = ConvertToContatoList(contacts);
                qtd = await ContarNovosContatos(contatoList);

                if (qtd > 0)
                {
                    lbNovosContatos.Text = $"Foram encontrados {qtd.ToString()} novos contatos";
                    lbNovosContatos.Visible = true;
                    lbCliqueImportar.Visible = true;
                    lbAnalisandoContatos.Visible = false;
                    btnSincronizarContatos.Enabled = true;
                }

                if (tabControl1.SelectedIndex == 1)
                {
                    tabControl1.SelectTab(0);
                    tabControl1.SelectTab(1);
                }

                return;
            }

            if (content.StartsWith("{\"id\":{\"fromMe"))
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<WhatsAppMessage>(content);
                    if (!response.From.EndsWith("@c.us"))
                    {
                        return;
                    }

                    if (response.Id.FromMe)
                    {
                        return;
                        if (isSendingMessage)
                        {
                            string telefoneEnviando = response.To;
                            string msg = response.Body.Replace("\r", "").Replace("\n", "");                           

                            string idZap = telefoneEnviandoSerialized;

                            if (idZap == telefoneEnviando)
                            {
                                string textoMsgEnviando = mensagemTextoEnviando.Replace("\r","").Replace("\n","");

                                bool enviouSucesso = true;

                                //msg = msg.Replace("\\n", "\n");
                                string compararmsg = textoMsgEnviando.ToString();

                                if (string.Equals(msg, compararmsg, StringComparison.OrdinalIgnoreCase))
                                {
                                    await webView21.ExecuteScriptAsync("window.chrome.webview.postMessage('mensagemEnviada')");
                                }

                            }
                        }
                        return;
                    }
                    var contatosComDataList = new List<ContatoComData>();

                    if (response != null)
                    {
                        var contatoComData = new ContatoComData
                        {
                            Telefone = response.From,
                            DateLastReceivedMsg = DateTimeOffset.FromUnixTimeSeconds(response.T).DateTime
                        };

                        contatosComDataList.Add(contatoComData);
                    }

                    await AtualizarContatosComData(contatosComDataList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro:", ex.Message);
                }

            }
        }

        public async Task ImportarContatos(string content, IProgress<ProgressReport> progress = null)
        {
            try
            {
                // Carregar todos os contatos do banco para mem�ria
                var queryTodos = "SELECT Telefone_Serialized, Nome, Id FROM Contatos WHERE IdEmpresa = @IdEmpresa";
                SqlParameter[] parametersId = new SqlParameter[]
                {
                    new SqlParameter("@IdEmpresa", Evento.IdEmpresa)
                };

                var contatosExistentesDataTable = await dbHelper.ExecuteQueryAsync(queryTodos, parametersId);

                if (contatosExistentesDataTable == null)
                {
                    MessageBox.Show("Erro ao carregar contatos existentes.");
                    return;
                }

                var response = JsonConvert.DeserializeObject<ContactListResponse>(content);
                var contacts = response?.Data;

                if (contacts == null) { return; };

                var contatoList = ConvertToContatoList(contacts);

                //int total = contatoList.Count;
                //int current = 0;

                //var queries = new List<Tuple<string, SqlParameter[]>>();
                int total = contatoList.Count;
                int batchSize = 500;  // Tamanho do lote
                int totalBatches = (int)Math.Ceiling((double)total / batchSize);  // Total de lotes a serem processados
                var batchQueries = new List<Tuple<string, SqlParameter[]>>();

                int currentBatch = 0;  // Contador de lotes processados

                for (int i = 0; i < total; i++)
                {
                    var contato = contatoList[i];
                    // Verificar se o contato j� existe na lista carregada do banco
                    var contatoExistente = contatosExistentesDataTable.Rows.Cast<DataRow>()
                        .FirstOrDefault(row => row["Telefone_Serialized"].ToString() == contato.Telefone_Serialized);

                    string query;
                    SqlParameter[] parameters;

                    if (contatoExistente != null)
                    {
                        if (contato.Nome == contatoExistente.ItemArray[1].ToString())
                        {
                            continue;
                        }
                        // Atualizar o contato existente
                        query = "UPDATE Contatos SET Nome = @Nome, PushName = @PushName, IsBusiness = @IsBusiness WHERE Telefone = @Telefone AND IdEmpresa = @IdEmpresa";
                        parameters = new SqlParameter[]
                        {
                            new SqlParameter("@Telefone", contato.Telefone),
                            new SqlParameter("@IdEmpresa", Evento.IdEmpresa),
                            new SqlParameter("@Nome", contato.Nome ?? contato.PushName),
                            new SqlParameter("@PushName", contato.PushName ?? ""),
                            new SqlParameter("@IsBusiness", contato.IsBusiness)
                        };
                    }
                    else
                    {
                        // Inserir o novo contato
                        query = "INSERT INTO Contatos (Telefone, Telefone_Serialized, Nome, PushName, IsBusiness, IdEmpresa, TelefoneOrigem) " +
                                "VALUES (@Telefone, @Telefone_Serialized, @Nome, @PushName, @IsBusiness, @IdEmpresa, @TelefoneOrigem);";
                        parameters = new SqlParameter[]
                        {
                            new SqlParameter("@Telefone", contato.Telefone),
                            new SqlParameter("@Telefone_Serialized", contato.Telefone_Serialized),
                            new SqlParameter("@Nome", contato.Nome ?? contato.PushName),
                            new SqlParameter("@PushName", contato.PushName ?? ""),
                            new SqlParameter("@IsBusiness", contato.IsBusiness),
                            new SqlParameter("@IdEmpresa", Evento.IdEmpresa),
                            new SqlParameter("@TelefoneOrigem", Evento.TelefoneOrigem)
                         };
                    }

                    batchQueries.Add(Tuple.Create(query, parameters));

                    if (batchQueries.Count >= batchSize || i == total - 1)
                    {
                        currentBatch++;

                        // Executar o lote
                        await dbHelper.ExecuteTransactionAsync(batchQueries, progress, $"Lote {currentBatch} de {totalBatches}");

                        // Limpar as queries para o pr�ximo lote
                        batchQueries.Clear();

                    }

                }

                lbNovosContatos.Visible = false;
                lbCliqueImportar.Visible = false;
                lbAnalisandoContatos.Visible = false;
                btnSincronizarContatos.Enabled = false;
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
                return;
            }
        }

        public async Task AtualizarContatosComData(List<ContatoComData> contatosComData, IProgress<ProgressReport> progress = null)
        {
            try
            {
                string query = "UPDATE Contatos SET DateLastReceivedMsg = @DateLastReceivedMsg WHERE Telefone_Serialized = @Telefone_Serialized AND IdEmpresa = @IdEmpresa";

                SqlParameter[] parameters;
                var queries = new List<Tuple<string, SqlParameter[]>>();

                foreach (var contato in contatosComData)
                {
                    var contatoNaLista = contatosList.FirstOrDefault(c => c.Telefone_Serialized == contato.Telefone && c.IdEmpresa == Evento.IdEmpresa);
                    if (contatoNaLista != null)
                    {
                        if (contatoNaLista.DateLastReceivedMsg == contato.DateLastReceivedMsg)
                        {
                            continue;
                        }
                    }
                    DateTime lastReceived = (DateTime)contato.DateLastReceivedMsg;
                    parameters = new SqlParameter[]
                    {
                    new SqlParameter("@DateLastReceivedMsg", lastReceived.AddHours(-3)),
                    new SqlParameter("@Telefone_Serialized", contato.Telefone),
                    new SqlParameter("@IdEmpresa", Evento.IdEmpresa)
                    };

                    queries.Add(Tuple.Create(query, parameters));
                }

                await dbHelper.ExecuteTransactionAsync(queries, progress);

                foreach (var contatoAtualizado in contatosComData)
                {
                    var contatoNaLista = contatosList.FirstOrDefault(c => c.Telefone_Serialized == contatoAtualizado.Telefone && c.IdEmpresa == Evento.IdEmpresa);
                    if (contatoNaLista != null)
                    {
                        DateTime lastReceived = (DateTime)contatoAtualizado.DateLastReceivedMsg;

                        contatoNaLista.DateLastReceivedMsg = lastReceived.AddHours(-3);
                    }
                }

                foreach (var contatoAtualizado in contatosComData)
                {
                    var contatoNaListaVirtual = contatosListVirtualFiltered.FirstOrDefault(c => c.Telefone_Serialized == contatoAtualizado.Telefone && c.IdEmpresa == Evento.IdEmpresa);
                    if (contatoNaListaVirtual != null)
                    {
                        DateTime lastReceived = (DateTime)contatoAtualizado.DateLastReceivedMsg;

                        contatoNaListaVirtual.DateLastReceivedMsg = lastReceived.AddHours(-3);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:", ex.Message);
            }

        }

        public void ProcessarMensagem(string json)
        {
            WhatsAppMessage message = Parametros.NovaMensagem(json)[0];

            string textoMensagem = message.Body;
            string telefone = message.From;
        }

        private void webView21_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            wppInjetado = false;
        }

        private async void InjetarFuncions(string function)
        {
            var phone = await webView21.CoreWebView2.ExecuteScriptAsync("function ready() { if(WPP.isFullReady) {return true}} ready()");

            if (phone == "true")
            {
                sincronizarContatos = true;
                InjetarScriptWebview(function);
            }
            else
            {
                var zapConectado = await webView21.CoreWebView2.ExecuteScriptAsync("function ready() { if(window.localStorage['last-wid'] || window.localStorage['last-wid-md']) {return true}} ready()");
                if (zapConectado == "true")
                {
                    InjetarScriptWebview(WhatsAppWebScripts.InjetarWPP);
                }
                else
                {
                    MessageBox.Show("O WhatsApp Web est� desconectado. Por favor, reconecte antes de realizar qualquer a��o.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }

        private List<Contato> ConvertToContatoList(List<ContactFromZap> contacts)
        {
            var contatoList = new List<Contato>();

            foreach (var contact in contacts)
            {
                if (!contact.id.EndsWith("@c.us"))
                    continue;

                if (contact.id.Split("@")[0].Length < 12)
                {
                    continue;
                }

                if (!contact.id.StartsWith("55"))
                    continue;

                if (contact.name == null && contact.pushname == null)
                {
                    Console.WriteLine("break");
                }

                var telefone = TelefoneFormatado(contact.id);

                var contato = new Contato
                {
                    Id = 0, // ID ser� gerado pelo banco de dados ou outra l�gica
                    Nome = contact.name ?? contact.pushname ?? "", // Se o nome n�o estiver presente, usa o pushname ou "Desconhecido"
                    Telefone = telefone,
                    Telefone_Serialized = contact.id,
                    Sexo = "O", // Sexo padr�o como "Outros"
                    IsBusiness = contact.isBusiness,
                    PushName = contact.pushname,
                    DateLastReceivedMsg = null, // Data da �ltima mensagem pode ser preenchida conforme sua l�gica
                    DateLastSentMsg = null, // Mesma coisa para a data da �ltima mensagem enviada
                    LastSentMsgId = null, // Mesma l�gica
                    IdEmpresa = 0, // ID da empresa ser� setado posteriormente ou conforme sua l�gica
                    Selecionado = false, // Pode ser setado conforme necess�rio
                    TelefoneOrigem = Evento.TelefoneOrigem
                };

                contatoList.Add(contato);
            }

            return contatoList;
        }

        private string TelefoneFormatado(string phone)
        {
            try
            {
                var number = phone.Split('@')[0];

                if (number.StartsWith("55"))
                {
                    number = number.Substring(2);
                }

                if (number.Length == 10)
                {
                    number = number.Insert(2, "9");
                }

                return string.Format(
                    "({0}) {1}-{2}",
                    number.Substring(0, 2),
                    number.Substring(2, 5),
                    number.Substring(7)
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:", ex.Message);
                throw;
            }
        }

        public async Task<int> ContarNovosContatos(List<Contato> contatoList)
        {
            var contatosSalvos = new List<Contato>();
            var query = "SELECT Telefone_Serialized FROM Contatos WHERE IdEmpresa = @IdEmpresa";
            var parameters = new SqlParameter[] {
                    new SqlParameter("@IdEmpresa", Evento.IdEmpresa)
                };
            try
            {
                var dataTable = await dbHelper.ExecuteQueryAsync(query, parameters);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return contatoList.Count;
                }

                //var telefonesExistentes = new HashSet<string>();

                //foreach (DataRow row in dataTable.Rows)
                //{
                //    telefonesExistentes.Add(row["Telefone"].ToString());
                //}
                int contatosNaoExistemNaDataTable = contatoList.Count(
                    contato => !dataTable.AsEnumerable().Any(
                        row => row.Field<string>("Telefone_Serialized") == contato.Telefone_Serialized
                        )
                    );

                //int contatosNaoExistentes = contatoList.Count(c => !dataTable.AsEnumerable().Any(row => row.Field<string>("Telefone"))).Contains(c.Telefone));

                return contatosNaoExistemNaDataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro:{ex.Message}");
                throw;
            }
        }

        private void btnSincronizarContatos_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Deseja sincronizar os contatos do WhatsApp e salvar a data da �ltima mensagem recebida de cada contato?",
                "Confirmar Sincroniza��o",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                InjetarFuncions("contactList();");
            }
        }

        private void btnVarNome_Click(object sender, EventArgs e)
        {
            string textoParaInserir = "{nome_zap}";

            if (chkUsarNomeSalvo.Checked)
            {
                textoParaInserir  = "{nome_salvo}";
            }

            int posicaoCursor = txtMensagem.SelectionStart;

            txtMensagem.Text = txtMensagem.Text.Insert(posicaoCursor, textoParaInserir);
            txtMensagem.SelectionStart = posicaoCursor + textoParaInserir.Length;
            txtMensagem.Focus();



        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            btnEnviar.Enabled = false;
            enviarClicado = false;
            ProgressForm progressForm = new ProgressForm();

            try
            {
                var zapConectado = await webView21.CoreWebView2.ExecuteScriptAsync("function ready() { if(window.localStorage['last-wid'] || window.localStorage['last-wid-md']) {return true}} ready()");
                string mensagem = txtMensagem.Text.Trim();

                if (string.IsNullOrEmpty(mensagem))
                {
                    MessageBox.Show("Por favor, escreva a mensagem antes de enviar.", "Campos Vazios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnEnviar.Enabled = true;
                    return;
                }
                if (contatosSelecionadosParaEnvio.Count() == 0)
                {
                    MessageBox.Show("Por favor, selecione ou filtre os contatos antes de enviar.", "Campos Vazios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnEnviar.Enabled = true;
                    return;
                }

                var result = MessageBox.Show(
                    $"Deseja enviar para {contatosSelecionadosParaEnvio.Count()} contatos?",
                    "Confirmar Sincroniza��o",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No)
                {
                    btnEnviar.Enabled = true;
                    return;
                }

                if (zapConectado != "true")
                {
                    MessageBox.Show("O WhatsApp Web est� desconectado. Por favor, reconecte antes de realizar qualquer a��o.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnEnviar.Enabled = true;
                    return;
                }

                var query = "INSERT INTO Mensagens " +
                    "(Mensagem, DataEnvio, QuantidadeContatosSolicitados, IdEmpresa) " +
                    "VALUES (@Mensagem, @DataEnvio, @QuantidadeContatosSolicitados, @IdEmpresa) " +
                    "SELECT SCOPE_IDENTITY()";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Mensagem", mensagem),
                    new SqlParameter("@DataEnvio", DateTime.Now.ToString()),
                    new SqlParameter("@QuantidadeContatosSolicitados", contatosSelecionadosParaEnvio.Count()),
                    new SqlParameter("@IdEmpresa",Evento.IdEmpresa)
                };

                var idMensagem = await dbHelper.ExecuteScalarAsync(query, parameters);

                mensagemIdEnviando = Convert.ToInt64(idMensagem);

                await prepararMensagens();

                progressForm.CenterToParentForm(this);
                progressForm.SetMin(0);
                progressForm.SetMax(100);
                progressForm.lbTituloProgress.Text = "Enviando Mensagens";
                this.Enabled = false;
                progressForm.Show();

                var progressReport = new Progress<ProgressReport>(report =>
                {
                    progressForm.UpdateProgress(report.Percent, report.Message);
                });

                IProgress<ProgressReport> progress = progressReport;

                int total = contatosSelecionadosParaEnvio.Count();
                int current = 0;

                foreach (var contato in contatosSelecionadosParaEnvio)
                {
                    current++;
                    string nomeZap = Regex.Replace(contato.PushName.ToString().Trim(), @"[^a-zA-Z������������������������\s]", string.Empty);
                    string nomeSalvo = Regex.Replace(contato.Nome.ToString().Trim(), @"[^a-zA-Z������������������������\s]", string.Empty);

                    if (string.IsNullOrEmpty(nomeSalvo))
                    {
                        nomeSalvo = nomeZap;
                    }

                    string mensagemAlterada = mensagem.Replace("{nome_zap}", $"{nomeZap}").Replace("{nome_salvo}", $"{nomeSalvo}");
                    mensagemTextoEnviando = mensagemAlterada;
                    telefoneEnviandoSerialized = contato.Telefone_Serialized.ToString();
                    //tabControl1.SelectTab(0);
                    telefoneIdEnviando = contato.Id;
                    int percent = (int)((double)current / total * 100);
                    progress?.Report(new ProgressReport { Percent = percent, Message = $"{current} de {total}" });

                    SendTimer.Start();

                    await EnviarMensagemZap(contato.Telefone, mensagemAlterada);

                    SendTimer.Stop();

                    query = "SELECT COUNT(*) FROM MensagemEnviada WHERE MensagemId = @MensagemId AND SucessoEnviada = 'ENVIADA'";

                    parameters = new SqlParameter[]
                    {
                    new SqlParameter("@MensagemId", idMensagem)
                    };

                    var enviadas = await dbHelper.ExecuteScalarAsync(query, parameters);

                    query = "UPDATE Mensagens SET QuantidadeContatosSucesso = @QuantidadeContatosSucesso WHERE Id = @Id";

                    parameters = new SqlParameter[]
                    {
                    new SqlParameter("@QuantidadeContatosSucesso", Convert.ToInt64(enviadas)),
                    new SqlParameter("@Id", idMensagem),
                    };

                    await dbHelper.ExecuteNonQueryAsync(query, parameters);

                }

                query = "SELECT COUNT(*) FROM MensagemEnviada WHERE MensagemId = @MensagemId AND SucessoEnviada = 'ENVIADA'";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@MensagemId", idMensagem)
                };

                var sucesso = await dbHelper.ExecuteScalarAsync(query, parameters);

                int qtdFalha = Convert.ToInt32(total) - Convert.ToInt32(sucesso);

                query = "UPDATE Mensagens SET QuantidadeContatosSucesso = @QuantidadeContatosSucesso, FalhaInesperada = @FalhaInesperada WHERE Id = @Id";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@QuantidadeContatosSucesso", Convert.ToInt64(sucesso)),
                    new SqlParameter("@Id", idMensagem),
                    new SqlParameter("@FalhaInesperada", false)
                };

                await dbHelper.ExecuteNonQueryAsync(query, parameters);

                progressForm.Close();

                string msgSucesso = Convert.ToInt32(sucesso) > 0 ? $"{sucesso} mensagens enviadas com sucesso!" : "Nenhuma mensagem foi enviada";
                string msgFalha = qtdFalha > 0 ? $"{qtdFalha} mensagens n�o foram enviadas" : "N�o houve nenhuma falha ao enviar";

                stopwatch.Stop();
                Debug.WriteLine($"Tempo gasto total: {stopwatch.ElapsedMilliseconds} ms");

                MessageBox.Show($"O envio das mensagens foi finalizado.\n{msgSucesso}\n{msgFalha}",
                    "Mensagens enviadas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Enabled = true;
                btnEnviar.Enabled = true;
                panelFiltroMensagem.Enabled = true;
                lbQuantidadeContatos.Text = "0 Contatos";
                txtMensagem.Clear();
                txtBusca.Clear();
                contatosSelecionadosParaEnvio.Clear();
                LimparPanelFiltros();
                tabControl1.SelectTab(0);
                await Task.Delay(1000);
                this.BringToFront();
                webView21.Reload();
                SendKeys.Send("{ESC}");
                await LoadContacts();
                await LoadMessages();                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: ", ex.Message);
                btnEnviar.Enabled = true;
                progressForm.Close();
                this.Enabled = true;
            }
        }

        public async Task prepararMensagens()
        {
            try
            {
                int total = contatosSelecionadosParaEnvio.Count;
                int batchSize = 500;  // Tamanho do lote
                int totalBatches = (int)Math.Ceiling((double)total / batchSize);  // Total de lotes a serem processados
                var batchQueries = new List<Tuple<string, SqlParameter[]>>();

                int currentBatch = 0;  // Contador de lotes processados
                ProgressForm progressForm = new ProgressForm();

                for (int i = 0; i < total; i++)
                {
                    progressForm.CenterToParentForm(this);
                    progressForm.SetMin(0);
                    progressForm.SetMax(100);
                    progressForm.lbTituloProgress.Text = "Preparando Mensagens";
                    this.Enabled = false;
                    progressForm.Show();

                    var progressBar = new Progress<ProgressReport>(report =>
                    {
                        progressForm.UpdateProgress(report.Percent, report.Message);
                    });
                    var contato = contatosSelecionadosParaEnvio[i];                   

                    string query;
                    SqlParameter[] parameters;

                    query = "INSERT INTO MensagemEnviada " +
                             "(MensagemId, TelefoneId, DataEnvio, IdEmpresa, SucessoEnviada) " +
                             "VALUES (@MensagemId, @TelefoneId, @DataEnvio, @IdEmpresa, @SucessoEnviada);";
                    
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@MensagemId", mensagemIdEnviando),
                        new SqlParameter("@TelefoneId", contato.Id),
                        new SqlParameter("@DataEnvio", DateTime.Now.ToString()),
                        new SqlParameter("@IdEmpresa", Evento.IdEmpresa),
                        new SqlParameter("@SucessoEnviada", "PENDENTE")
                    };

                    batchQueries.Add(Tuple.Create(query, parameters));

                    if (batchQueries.Count >= batchSize || i == total - 1)
                    {
                        currentBatch++;

                        // Executar o lote
                        await dbHelper.ExecuteTransactionAsync(batchQueries, progressBar, $"Lote {currentBatch} de {totalBatches}");

                        // Limpar as queries para o pr�ximo lote
                        batchQueries.Clear();

                    }

                }

                lbNovosContatos.Visible = false;
                lbCliqueImportar.Visible = false;
                lbAnalisandoContatos.Visible = false;
                btnSincronizarContatos.Enabled = false;
                progressForm.Dispose();
                progressForm.Close();
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
                return;
            }
        }
        public async Task EnviarMensagemSegura(List<Contato> contatosEnviar, string mensagem, long mensagemId)
        {
            ProgressForm progressForm = new ProgressForm();
            try
            {
                var zapConectado = await webView21.CoreWebView2.ExecuteScriptAsync("function ready() { if(window.localStorage['last-wid'] || window.localStorage['last-wid-md']) {return true}} ready()");

                if (string.IsNullOrEmpty(mensagem))
                {
                    MessageBox.Show("Por favor, escreva a mensagem antes de enviar.", "Campos Vazios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnEnviar.Enabled = true;
                    return;
                }

                if (zapConectado != "true")
                {
                    MessageBox.Show("O WhatsApp Web est� desconectado. Por favor, reconecte antes de realizar qualquer a��o.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnEnviar.Enabled = true;
                    return;
                }

                string query;
                int total = contatosEnviar.Count();
                SqlParameter[] parameters;

                if (mensagemId == 0)
                {
                    query = "INSERT INTO Mensagens " +
                   "(Mensagem, DataEnvio, QuantidadeContatosSolicitados, IdEmpresa) " +
                   "VALUES (@Mensagem, @DataEnvio, @QuantidadeContatosSolicitados, @IdEmpresa) " +
                   "SELECT SCOPE_IDENTITY()";

                    parameters = new SqlParameter[]
                    {
                    new SqlParameter("@Mensagem", mensagem),
                    new SqlParameter("@DataEnvio", DateTime.Now.ToString()),
                    new SqlParameter("@QuantidadeContatosSolicitados", contatosEnviar.Count()),
                    new SqlParameter("@IdEmpresa",Evento.IdEmpresa)
                    };

                    var idMensagem = await dbHelper.ExecuteScalarAsync(query, parameters);
                    mensagemIdEnviando = Convert.ToInt64(idMensagem);

                    contatosSelecionadosParaEnvio = contatosEnviar;

                    await prepararMensagens();

                }
                else
                {
                    mensagemIdEnviando = Convert.ToInt64(mensagemId);//idMensagem.ToString();
                    //query = "SELECT COUNT(*) FROM MensagemEnviada WHERE MensagemId = @MensagemId";

                    //parameters = new SqlParameter[]
                    //{
                    //new SqlParameter("@MensagemId", mensagemIdEnviando)
                    //};
                    //total = Convert.ToInt32(await dbHelper.ExecuteScalarAsync(query, parameters));
                }

                progressForm.CenterToParentForm(this);
                progressForm.SetMin(0);
                progressForm.SetMax(100);
                progressForm.lbTituloProgress.Text = "Enviando Mensagens";
                this.Enabled = false;
                progressForm.Show();

                var progressReport = new Progress<ProgressReport>(report =>
                {
                    progressForm.UpdateProgress(report.Percent, report.Message);
                });

                IProgress<ProgressReport> progress = progressReport;

                int current = 0;

                foreach (var contato in contatosEnviar)
                {
                    string nomeZap = Regex.Replace(contato.PushName.ToString().Trim(), @"[^a-zA-Z������������������������\s]", string.Empty);
                    string nomeSalvo = Regex.Replace(contato.Nome.ToString().Trim(), @"[^a-zA-Z������������������������\s]", string.Empty);

                    if (string.IsNullOrEmpty(nomeSalvo))
                    {
                        nomeSalvo = nomeZap;
                    }

                    string mensagemAlterada = mensagem.Replace("{nome_zap}", $"{nomeZap}").Replace("{nome_salvo}", $"{nomeSalvo}");
                    mensagemTextoEnviando = mensagemAlterada;
                    telefoneEnviandoSerialized = contato.Telefone_Serialized.ToString();

                    current++;
                    //tabControl1.SelectTab(0);
                    telefoneIdEnviando = contato.Id;
                    int percent = (int)((double)current / total * 100);
                    progress?.Report(new ProgressReport { Percent = percent, Message = $"{current} de {total}" });

                    SendTimer.Start();
                    await EnviarMensagemZap(contato.Telefone, mensagemAlterada);
                    SendTimer.Stop();

                    query = "SELECT COUNT(*) FROM MensagemEnviada WHERE MensagemId = @MensagemId AND SucessoEnviada = 'ENVIADA'";

                    parameters = new SqlParameter[]
                    {
                    new SqlParameter("@MensagemId", mensagemIdEnviando)
                    };

                    var enviadas = await dbHelper.ExecuteScalarAsync(query, parameters);

                    query = "UPDATE Mensagens SET QuantidadeContatosSucesso = @QuantidadeContatosSucesso WHERE Id = @Id";

                    parameters = new SqlParameter[]
                    {
                    new SqlParameter("@QuantidadeContatosSucesso", Convert.ToInt64(enviadas)),
                    new SqlParameter("@Id", mensagemIdEnviando),
                    };

                    await dbHelper.ExecuteNonQueryAsync(query, parameters);

                }

                await Task.Delay(3000);

                query = "SELECT COUNT(*) FROM MensagemEnviada WHERE MensagemId = @MensagemId AND SucessoEnviada = 'ENVIADA'";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@MensagemId", mensagemIdEnviando)
                };

                var sucesso = await dbHelper.ExecuteScalarAsync(query, parameters);

                int qtdFalha = Convert.ToInt32(total) - Convert.ToInt32(sucesso);

                query = "UPDATE Mensagens SET QuantidadeContatosSucesso = @QuantidadeContatosSucesso, FalhaInesperada = @FalhaInesperada WHERE Id = @Id";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@QuantidadeContatosSucesso", Convert.ToInt64(sucesso)),
                    new SqlParameter("@Id", mensagemIdEnviando),
                    new SqlParameter("@FalhaInesperada", false)
                };

                await dbHelper.ExecuteNonQueryAsync(query, parameters);

                progressForm.Close();

                string msgSucesso = Convert.ToInt32(sucesso) > 0 ? $"{sucesso} mensagens enviadas com sucesso!" : "Nenhuma mensagem foi enviada";
                string msgFalha = qtdFalha > 0 ? $"{qtdFalha} mensagens n�o foram enviadas" : "N�o houve nenhuma falha ao enviar";

                MessageBox.Show($"O envio das mensagens foi finalizado.\n{msgSucesso}\n{msgFalha}",
                    "Mensagens enviadas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Enabled = true;
                btnEnviar.Enabled = true;
                txtMensagem.Clear();
                txtBusca.Clear();
                contatosEnviar.Clear();
                tabControl1.SelectTab(0);
                await Task.Delay(1000);
                this.BringToFront();
                webView21.Reload();                
                SendKeys.Send("{ESC}");
                await LoadContacts();
                await LoadMessages();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: ", ex.Message);
                progressForm.Close();
                this.Enabled = true;
            }
        }


        private void LimparPanelFiltros()
        {
            foreach (Control controle in panelFiltroMensagem.Controls)
            {
                if (controle is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                }

                else if (controle is TextBox textBox)
                {
                    textBox.Clear();
                }

                else if (controle is ComboBox comboBox)
                {
                    comboBox.SelectedIndex = -1;
                }
            }
        }

        private async Task EnviarMensagemZap(string telefone, string mensagem)
        {
#if DEBUG
            tabControl1.SelectTab(0);
#endif
            if (string.IsNullOrEmpty(telefone) || string.IsNullOrEmpty(mensagem))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Campos Vazios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string apenasNumeros = Regex.Replace(telefone, @"\D", "");

            string telefoneFormatado = "55" + apenasNumeros;

            string url = $"https://wa.me/{telefoneFormatado}?text={Uri.EscapeDataString(mensagem)}";

            SetSendingMessageState(true);
            //InjetarScriptWebview("window.onbeforeunload = null;");
            webView21.CoreWebView2.Navigate(url);

            while (isSendingMessage)
            {
                await Task.Delay(1000);
            }

            return;
        }

        private async void CoreWebView2_DOMContentLoaded(object sender, CoreWebView2DOMContentLoadedEventArgs e)
        {

            if (isSendingMessage)
            {
                try
                {
                    if (!iniciouConversa)
                    {
                        await webView21.ExecuteScriptAsync(WhatsAppWebScripts.ScriptIniciarConversa);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao executar script: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #region Tab Mensagens enviadas

        //private void dataGridViewMensagens_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == 0 && e.RowIndex >= 0) 
        //    {
        //        DataGridViewRow SelectedRow = dataGridViewMensagens.Rows[e.RowIndex];

        //        if (Convert.ToBoolean(SelectedRow.Cells["Selecionar"].Value))
        //        {
        //            foreach (DataGridViewRow row in dataGridViewMensagens.Rows)
        //            {
        //                row.Cells["Selecionar"].Value = false;
        //                var msgId = (long)row.Cells["ID"].Value;
        //                var contato = contatosList.First(c => c.Id == msgId); 
        //                contato.Selecionado = false;
        //                row.DefaultCellStyle.BackColor = Color.White;

        //            }
        //        }
        //        var mensagemId = (long)SelectedRow.Cells["ID"].Value;
        //        var mensagem = mensagensList.First(m => m.Id == mensagemId);

        //        if (Convert.ToBoolean(SelectedRow.Cells["Selecionar"].Value))
        //        {
        //            SelectedRow.DefaultCellStyle.BackColor = Color.LightGreen; 

        //        }
        //        else
        //        {                    
        //            SelectedRow.DefaultCellStyle.BackColor = Color.White; 
        //        }
        //    }
        //}

        //private void dataGridViewMensagens_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        //{
        //    if (dataGridViewMensagens.IsCurrentCellDirty)
        //    {
        //        dataGridViewMensagens.CommitEdit(DataGridViewDataErrorContexts.Commit);
        //    }
        //}

        private void dataGridViewMensagens_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= mensagensListVirtualFiltered.Count)
                return;

            var contato = mensagensListVirtualFiltered[e.RowIndex];

            switch (dataGridViewMensagens.Columns[e.ColumnIndex].Name)
            {
                case "ID":
                    e.Value = contato.Id;
                    break;
                case "Mensagem":
                    e.Value = contato.Mensagem;
                    break;
                case "DataEnvio":
                    e.Value = contato.DataEnvio.ToString("dd/MM/yyyy HH:mm") ?? "";
                    break;
                case "QtdContatosSelecionados":
                    e.Value = contato.QuantidadeContatosSolicitados;
                    break;
                case "QuantidadeContatosSucesso":
                    e.Value = contato.QuantidadeContatosSucesso;
                    break;
                case "VerMensagem":
                    e.Value = "Ver Mensagem";
                    break;
            }

            //if (dataGridViewMensagens.Columns[e.ColumnIndex].Name == "Selecionar")
            //{
            //    var row = dataGridViewMensagens.Rows[e.RowIndex];
            //    row.DefaultCellStyle.BackColor = contato.Selecionado ? Color.LightGreen : Color.White;
            //}
        }

        //private void DataGridViewMensagens_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        //{
        //    if (e.RowIndex >= mensagensListVirtualFiltered.Count)
        //        return;

        //    var contato = mensagensListVirtualFiltered[e.RowIndex];

        //    if (dataGridViewMensagens.Columns[e.ColumnIndex].Name == "Selecionar")
        //    {
        //        contato.Selecionado = Convert.ToBoolean(e.Value);

        //        dataGridViewMensagens.InvalidateRow(e.RowIndex);
        //    }
        //}

        //private void dataGridViewMensagens_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (dataGridViewMensagens.Columns[e.ColumnIndex].Name == "Selecionar")
        //    {
        //        var contato = mensagensList[e.RowIndex];
        //        e.Value = contato.Selecionado;
        //    }
        //}

        private async void dataGridViewMensagens_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dataGridViewMensagens.Columns[e.ColumnIndex].Name == "VerMensagem")
            {
                var mensagem = mensagensListVirtualFiltered[e.RowIndex];

                if (e.ColumnIndex == dataGridViewMensagens.Columns["VerMensagem"].Index && e.RowIndex >= 0)
                {
                    long mensagemId = mensagem.Id;

                    string query = "SELECT * FROM Mensagens WHERE Id = @Id";
                    SqlParameter[] sp = new SqlParameter[]
                    {
                        new SqlParameter("@Id",mensagemId)
                    };
                    ShowLoading();
                    DataTable dt = await dbHelper.ExecuteQueryAsync(query, sp);
                    HideLoading();
                    VerMensagemForm mensagemForm = new VerMensagemForm(this, dt);
                    mensagemForm.StartPosition = FormStartPosition.CenterParent;

                    mensagemForm.ShowDialog(this);
                }
            }
        }

        private void InitializeDataGridViewColumnsMessages()
        {
            dataGridViewMensagens.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Gray
            };

            //if (dataGridViewMensagens.Columns["Selecionar"] == null)
            //{
            //    DataGridViewCheckBoxColumn selectColumn = new DataGridViewCheckBoxColumn
            //    {
            //        Name = "Selecionar",
            //        HeaderText = "Selecionar",
            //        Width = 100,
            //        FalseValue = false,
            //        TrueValue = true,
            //        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            //    };
            //    dataGridViewMensagens.Columns.Add(selectColumn);
            //}

            dataGridViewMensagens.Columns.Add(new DataGridViewTextBoxColumn { Name = "ID", HeaderText = "ID", ReadOnly = true, Width = 100, AutoSizeMode = DataGridViewAutoSizeColumnMode.None, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, } });
            dataGridViewMensagens.Columns.Add(new DataGridViewTextBoxColumn { Name = "Mensagem", HeaderText = "Mensagem", ReadOnly = true, Width = 300 });
            dataGridViewMensagens.Columns.Add(new DataGridViewTextBoxColumn { Name = "DataEnvio", HeaderText = "Data de Envio", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dataGridViewMensagens.Columns.Add(new DataGridViewTextBoxColumn { Name = "QtdContatosSelecionados", HeaderText = "Contatos Selecionados", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, } });
            dataGridViewMensagens.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuantidadeContatosSucesso", HeaderText = "Envios com Sucesso", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, } });

            if (dataGridViewMensagens.Columns["VerMensagem"] == null)
            {
                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "VerMensagem",
                    HeaderText = "",
                    Text = "Ver Mensagem",
                    UseColumnTextForButtonValue = true,
                    Width = 120,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dataGridViewMensagens.Columns.Add(editButtonColumn);
            }
        }

        public async Task LoadMessages()
        {
            var query = $"SELECT " +
                 $"Id, Mensagem, DataEnvio, QuantidadeContatosSolicitados, QuantidadeContatosSucesso,QuantidadeContatosPendentes, IdEmpresa " +
                 $"FROM " +
                 $"Mensagens " +
                 $"WHERE IdEmpresa = @IdEmpresa";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@IdEmpresa", Evento.IdEmpresa)
            };

            try
            {
                var dataTable = await dbHelper.ExecuteQueryAsync(query, parameters);
                mensagensList.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    mensagensList.Add(new Mensagens
                    {
                        Id = (long)row["Id"],
                        Mensagem = row["Mensagem"].ToString(),
                        DataEnvio = (DateTime)row["DataEnvio"],
                        QuantidadeContatosSolicitados = (long)row["QuantidadeContatosSolicitados"],
                        QuantidadeContatosPendentes = (long)row["QuantidadeContatosPendentes"],
                        QuantidadeContatosSucesso = (long)row["QuantidadeContatosSucesso"],
                        IdEmpresa = (long)row["IdEmpresa"]
                    });
                }

                mensagensListVirtualFiltered = mensagensList.AsEnumerable().Reverse().ToList();

                if (dataGridViewMensagens.InvokeRequired)
                {
                    dataGridViewMensagens.Invoke(new Action(() =>
                    {
                        dataGridViewMensagens.SuspendDrawing();
                    }));
                }
                else
                {
                    dataGridViewMensagens.SuspendDrawing();
                }

                try
                {
                    dataGridViewMensagens.RowCount = mensagensListVirtualFiltered.Count;
                }
                finally
                {
                    dataGridViewMensagens.ResumeDrawing();
                    //lbQtdFiltrados.Text = $"{mensagensListVirtualFiltered.Count.ToString()} Mensagens";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar Mensagens: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ApplyFiltersAsyncMensagens()
        {
            try
            {

                string searchTerm = string.Empty;

                if (txtBuscaMsg.InvokeRequired)
                {
                    txtBuscaMsg.Invoke(new MethodInvoker(delegate
                    {
                        searchTerm = txtBuscaMsg.Text.Trim().ToLower();
                    }));
                }
                else
                {
                    searchTerm = txtBuscaMsg.Text.Trim().ToLower();
                }

                var filteredList = await Task.Run(() =>
                {
                    var query = mensagensList.AsQueryable();

                    if (chkMsgComFalha.Checked)
                    {
                        query = query.Where(m => m.QuantidadeContatosSolicitados > m.QuantidadeContatosSucesso);
                    }

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query = query.Where(m => m.Mensagem.ToLower().Contains(searchTerm));
                    }

                    return query.ToList();
                });

                dataGridViewMensagens.SuspendDrawing();

                try
                {
                    mensagensListVirtualFiltered = filteredList;
                    dataGridViewMensagens.RowCount = mensagensListVirtualFiltered.Count;
                }
                finally
                {
                    dataGridViewMensagens.ResumeDrawing();
                }

                dataGridViewMensagens.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao aplicar filtros: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateDataGridViewMensagens(List<Mensagens>? filteredMensages = null)
        {
            var dataSource = filteredMensages ?? mensagensList;

            var dt = new DataTable();

            dt.Columns.Add("ID", typeof(long));
            dt.Columns.Add("Mensagem");
            dt.Columns.Add("DataEnvio");
            dt.Columns.Add("QtdContatosSolicitados");
            dt.Columns.Add("QtdContatosSucesso");

            foreach (var contato in dataSource)
            {
                dt.Rows.Add(
                    contato.Id,
                    contato.Mensagem,
                    contato.DataEnvio.ToString("dd/MM/yyyy HH:mm"),
                    contato.QuantidadeContatosSolicitados,
                    contato.QuantidadeContatosSucesso
                );
            }

            dataGridViewMensagens.DataSource = dt;

            dataGridViewMensagens.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridViewMensagens.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);

            foreach (DataGridViewColumn column in dataGridViewMensagens.Columns)
            {
                if (column.Name != "Selecionar")
                {
                    column.ReadOnly = true;
                }
            }

            foreach (DataGridViewRow row in dataGridViewMensagens.Rows)
            {
                var messageId = (long)row.Cells["ID"].Value;
                var message = dataSource.First(c => c.Id == messageId);
                //row.Cells["Selecionar"].Value = message.Selecionado;

                //if (message.Selecionado)
                //{
                //    row.DefaultCellStyle.BackColor = Color.LightGreen; 
                //}
                //else
                //{
                //    row.DefaultCellStyle.BackColor = Color.White; 
                //}
            }
        }

        private void txtBuscaMsg_TextChanged(object sender, EventArgs e)
        {
            searchTimerMsg.Stop();
            searchTimerMsg.Start();
        }
        private void chkMsgComFalha_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFiltersAsyncMensagens();
        }

        #endregion
    }
}
