namespace ZapEnvioSeguro
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControl1 = new TabControl();
            tabWhatsApp = new TabPage();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            tabContatos = new TabPage();
            panel2 = new Panel();
            dataGridViewContatos = new DataGridView();
            lbQtdFiltrados = new Label();
            lbCliqueImportar = new Label();
            lbNovosContatos = new Label();
            btnEnviarMensagem = new Button();
            btnSincronizarContatos = new Button();
            btnAdicionarContato = new Button();
            chkSelecionarTodos = new CheckBox();
            panel1 = new Panel();
            chkFiltroBusiness = new CheckBox();
            chkFiltroSemConversa = new CheckBox();
            txtDDD = new TextBox();
            btnRemoverFiltros = new Button();
            chkFiltroDDD = new CheckBox();
            chkFiltroDias = new CheckBox();
            btnAplicarFiltros = new Button();
            label5 = new Label();
            txtDias = new TextBox();
            label2 = new Label();
            label1 = new Label();
            txtBusca = new TextBox();
            tabMensagem = new TabPage();
            btConfirmarOpcoesMensagem = new Button();
            lbQuantidadeContatos = new Label();
            label3 = new Label();
            btCancelarOpcoesMensagem = new Button();
            panelFiltroMensagem = new Panel();
            chkBusinessMensagem = new CheckBox();
            chkSemConversaMensagem = new CheckBox();
            ckTodosContatosMensagem = new CheckBox();
            chkGeneroMensagem = new CheckBox();
            cmbGeneroMensagem = new ComboBox();
            txtDddMensagem = new TextBox();
            chkDddMensagem = new CheckBox();
            chkDiasMensagem = new CheckBox();
            label4 = new Label();
            txtDiasMensagem = new TextBox();
            btnEnviar = new Button();
            label6 = new Label();
            label7 = new Label();
            txtMensagem = new RichTextBox();
            tabControl1.SuspendLayout();
            tabWhatsApp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            tabContatos.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewContatos).BeginInit();
            panel1.SuspendLayout();
            tabMensagem.SuspendLayout();
            panelFiltroMensagem.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabWhatsApp);
            tabControl1.Controls.Add(tabContatos);
            tabControl1.Controls.Add(tabMensagem);
            tabControl1.Cursor = Cursors.Hand;
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControl1.HotTrack = true;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1264, 681);
            tabControl1.TabIndex = 1;
            tabControl1.Selecting += tabControl1_Selecting;
            // 
            // tabWhatsApp
            // 
            tabWhatsApp.Controls.Add(webView21);
            tabWhatsApp.Location = new Point(4, 26);
            tabWhatsApp.Name = "tabWhatsApp";
            tabWhatsApp.Padding = new Padding(3);
            tabWhatsApp.Size = new Size(1256, 651);
            tabWhatsApp.TabIndex = 0;
            tabWhatsApp.Text = "WhatsApp Web";
            tabWhatsApp.UseVisualStyleBackColor = true;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Fill;
            webView21.Location = new Point(3, 3);
            webView21.Name = "webView21";
            webView21.Size = new Size(1250, 645);
            webView21.TabIndex = 0;
            webView21.ZoomFactor = 1D;
            webView21.NavigationStarting += webView21_NavigationStarting;
            webView21.NavigationCompleted += webView21_NavigationCompleted;
            webView21.WebMessageReceived += webView21_WebMessageReceived;
            // 
            // tabContatos
            // 
            tabContatos.Controls.Add(panel2);
            tabContatos.Controls.Add(lbQtdFiltrados);
            tabContatos.Controls.Add(lbCliqueImportar);
            tabContatos.Controls.Add(lbNovosContatos);
            tabContatos.Controls.Add(btnEnviarMensagem);
            tabContatos.Controls.Add(btnSincronizarContatos);
            tabContatos.Controls.Add(btnAdicionarContato);
            tabContatos.Controls.Add(chkSelecionarTodos);
            tabContatos.Controls.Add(panel1);
            tabContatos.Controls.Add(label2);
            tabContatos.Controls.Add(label1);
            tabContatos.Controls.Add(txtBusca);
            tabContatos.Location = new Point(4, 26);
            tabContatos.Name = "tabContatos";
            tabContatos.Padding = new Padding(3);
            tabContatos.Size = new Size(1256, 651);
            tabContatos.TabIndex = 1;
            tabContatos.Text = "Contatos";
            tabContatos.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(dataGridViewContatos);
            panel2.Location = new Point(0, 198);
            panel2.Name = "panel2";
            panel2.Size = new Size(1260, 445);
            panel2.TabIndex = 19;
            // 
            // dataGridViewContatos
            // 
            dataGridViewContatos.AllowUserToAddRows = false;
            dataGridViewContatos.AllowUserToDeleteRows = false;
            dataGridViewContatos.AllowUserToResizeColumns = false;
            dataGridViewContatos.AllowUserToResizeRows = false;
            dataGridViewContatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewContatos.BackgroundColor = SystemColors.ActiveCaption;
            dataGridViewContatos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewContatos.Dock = DockStyle.Fill;
            dataGridViewContatos.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dataGridViewContatos.Location = new Point(0, 0);
            dataGridViewContatos.MultiSelect = false;
            dataGridViewContatos.Name = "dataGridViewContatos";
            dataGridViewContatos.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewContatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewContatos.ShowEditingIcon = false;
            dataGridViewContatos.Size = new Size(1260, 445);
            dataGridViewContatos.TabIndex = 5;
            dataGridViewContatos.CellContentClick += dataGridViewContatos_CellContentClick;
            dataGridViewContatos.CellValueNeeded += dataGridViewContatos_CellValueNeeded;
            dataGridViewContatos.CellValuePushed += DataGridViewContatos_CellValuePushed;
            dataGridViewContatos.CurrentCellDirtyStateChanged += dataGridViewContatos_CurrentCellDirtyStateChanged;
            // 
            // lbQtdFiltrados
            // 
            lbQtdFiltrados.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbQtdFiltrados.AutoSize = true;
            lbQtdFiltrados.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbQtdFiltrados.ForeColor = SystemColors.MenuHighlight;
            lbQtdFiltrados.Location = new Point(917, 172);
            lbQtdFiltrados.Name = "lbQtdFiltrados";
            lbQtdFiltrados.Size = new Size(72, 17);
            lbQtdFiltrados.TabIndex = 18;
            lbQtdFiltrados.Text = "0 contatos";
            lbQtdFiltrados.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbCliqueImportar
            // 
            lbCliqueImportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbCliqueImportar.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lbCliqueImportar.Location = new Point(670, 30);
            lbCliqueImportar.Name = "lbCliqueImportar";
            lbCliqueImportar.Size = new Size(300, 17);
            lbCliqueImportar.TabIndex = 17;
            lbCliqueImportar.Text = "Clique em Importar para sincronizar";
            lbCliqueImportar.TextAlign = ContentAlignment.MiddleCenter;
            lbCliqueImportar.Visible = false;
            // 
            // lbNovosContatos
            // 
            lbNovosContatos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbNovosContatos.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbNovosContatos.ForeColor = SystemColors.MenuHighlight;
            lbNovosContatos.Location = new Point(670, 13);
            lbNovosContatos.Name = "lbNovosContatos";
            lbNovosContatos.Size = new Size(300, 17);
            lbNovosContatos.TabIndex = 16;
            lbNovosContatos.Text = "Foram encontrados 0 novos contatos";
            lbNovosContatos.TextAlign = ContentAlignment.MiddleCenter;
            lbNovosContatos.Visible = false;
            // 
            // btnEnviarMensagem
            // 
            btnEnviarMensagem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEnviarMensagem.BackColor = SystemColors.MenuHighlight;
            btnEnviarMensagem.Cursor = Cursors.Hand;
            btnEnviarMensagem.FlatAppearance.BorderSize = 0;
            btnEnviarMensagem.FlatStyle = FlatStyle.Flat;
            btnEnviarMensagem.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEnviarMensagem.ForeColor = SystemColors.ButtonFace;
            btnEnviarMensagem.Location = new Point(1055, 144);
            btnEnviarMensagem.Name = "btnEnviarMensagem";
            btnEnviarMensagem.Size = new Size(193, 48);
            btnEnviarMensagem.TabIndex = 14;
            btnEnviarMensagem.Text = "Enviar Mensagem";
            btnEnviarMensagem.UseVisualStyleBackColor = false;
            btnEnviarMensagem.Click += btnEnviarMensagem_Click;
            // 
            // btnSincronizarContatos
            // 
            btnSincronizarContatos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSincronizarContatos.Cursor = Cursors.Hand;
            btnSincronizarContatos.Enabled = false;
            btnSincronizarContatos.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSincronizarContatos.Location = new Point(976, 6);
            btnSincronizarContatos.Name = "btnSincronizarContatos";
            btnSincronizarContatos.Size = new Size(134, 52);
            btnSincronizarContatos.TabIndex = 13;
            btnSincronizarContatos.Text = "Importar Contatos";
            btnSincronizarContatos.UseVisualStyleBackColor = true;
            btnSincronizarContatos.Click += btnSincronizarContatos_Click;
            // 
            // btnAdicionarContato
            // 
            btnAdicionarContato.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdicionarContato.Cursor = Cursors.Hand;
            btnAdicionarContato.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdicionarContato.Location = new Point(1116, 6);
            btnAdicionarContato.Name = "btnAdicionarContato";
            btnAdicionarContato.Size = new Size(134, 52);
            btnAdicionarContato.TabIndex = 12;
            btnAdicionarContato.Text = "Adicionar Contato";
            btnAdicionarContato.UseVisualStyleBackColor = true;
            btnAdicionarContato.Click += btnAdicionarContato_Click;
            // 
            // chkSelecionarTodos
            // 
            chkSelecionarTodos.AutoSize = true;
            chkSelecionarTodos.BackColor = Color.Transparent;
            chkSelecionarTodos.FlatAppearance.BorderColor = Color.Red;
            chkSelecionarTodos.FlatAppearance.BorderSize = 5;
            chkSelecionarTodos.FlatStyle = FlatStyle.System;
            chkSelecionarTodos.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkSelecionarTodos.Location = new Point(15, 170);
            chkSelecionarTodos.Name = "chkSelecionarTodos";
            chkSelecionarTodos.Size = new Size(137, 22);
            chkSelecionarTodos.TabIndex = 12;
            chkSelecionarTodos.Text = "Selecionar Todos";
            chkSelecionarTodos.UseVisualStyleBackColor = false;
            chkSelecionarTodos.CheckedChanged += chkSelecionarTodos_CheckedChanged;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(chkFiltroBusiness);
            panel1.Controls.Add(chkFiltroSemConversa);
            panel1.Controls.Add(txtDDD);
            panel1.Controls.Add(btnRemoverFiltros);
            panel1.Controls.Add(chkFiltroDDD);
            panel1.Controls.Add(chkFiltroDias);
            panel1.Controls.Add(btnAplicarFiltros);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(txtDias);
            panel1.Location = new Point(0, 84);
            panel1.Name = "panel1";
            panel1.Size = new Size(1256, 51);
            panel1.TabIndex = 3;
            // 
            // chkFiltroBusiness
            // 
            chkFiltroBusiness.Cursor = Cursors.Hand;
            chkFiltroBusiness.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkFiltroBusiness.Location = new Point(514, 13);
            chkFiltroBusiness.Name = "chkFiltroBusiness";
            chkFiltroBusiness.Size = new Size(86, 25);
            chkFiltroBusiness.TabIndex = 15;
            chkFiltroBusiness.Text = "Business?";
            chkFiltroBusiness.UseVisualStyleBackColor = true;
            // 
            // chkFiltroSemConversa
            // 
            chkFiltroSemConversa.Cursor = Cursors.Hand;
            chkFiltroSemConversa.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkFiltroSemConversa.Location = new Point(669, 13);
            chkFiltroSemConversa.Name = "chkFiltroSemConversa";
            chkFiltroSemConversa.Size = new Size(248, 25);
            chkFiltroSemConversa.TabIndex = 14;
            chkFiltroSemConversa.Text = "Não incluir contatos sem conversa";
            chkFiltroSemConversa.UseVisualStyleBackColor = true;
            // 
            // txtDDD
            // 
            txtDDD.Cursor = Cursors.IBeam;
            txtDDD.Enabled = false;
            txtDDD.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDDD.Location = new Point(389, 13);
            txtDDD.Name = "txtDDD";
            txtDDD.Size = new Size(59, 25);
            txtDDD.TabIndex = 13;
            txtDDD.KeyPress += txtDDD_KeyPress;
            // 
            // btnRemoverFiltros
            // 
            btnRemoverFiltros.Cursor = Cursors.Hand;
            btnRemoverFiltros.Enabled = false;
            btnRemoverFiltros.Location = new Point(1127, 13);
            btnRemoverFiltros.Name = "btnRemoverFiltros";
            btnRemoverFiltros.Size = new Size(107, 25);
            btnRemoverFiltros.TabIndex = 12;
            btnRemoverFiltros.Text = "Remover Filtros";
            btnRemoverFiltros.UseVisualStyleBackColor = true;
            btnRemoverFiltros.Click += btnRemoverFiltros_Click;
            // 
            // chkFiltroDDD
            // 
            chkFiltroDDD.Cursor = Cursors.Hand;
            chkFiltroDDD.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkFiltroDDD.Location = new Point(337, 13);
            chkFiltroDDD.Name = "chkFiltroDDD";
            chkFiltroDDD.Size = new Size(61, 25);
            chkFiltroDDD.TabIndex = 11;
            chkFiltroDDD.Text = "DDD:";
            chkFiltroDDD.UseVisualStyleBackColor = true;
            chkFiltroDDD.CheckedChanged += chkFiltroDDD_CheckedChanged;
            // 
            // chkFiltroDias
            // 
            chkFiltroDias.Cursor = Cursors.Hand;
            chkFiltroDias.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkFiltroDias.Location = new Point(11, 13);
            chkFiltroDias.Name = "chkFiltroDias";
            chkFiltroDias.Size = new Size(167, 25);
            chkFiltroDias.TabIndex = 10;
            chkFiltroDias.Text = "Não entra em contato a";
            chkFiltroDias.UseVisualStyleBackColor = true;
            chkFiltroDias.CheckedChanged += chkFiltroDias_CheckedChanged;
            // 
            // btnAplicarFiltros
            // 
            btnAplicarFiltros.Cursor = Cursors.Hand;
            btnAplicarFiltros.Location = new Point(1012, 13);
            btnAplicarFiltros.Name = "btnAplicarFiltros";
            btnAplicarFiltros.Size = new Size(107, 25);
            btnAplicarFiltros.TabIndex = 8;
            btnAplicarFiltros.Text = "Aplicar Filtros";
            btnAplicarFiltros.UseVisualStyleBackColor = true;
            btnAplicarFiltros.Click += btnAplicarFiltros_Click;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(238, 13);
            label5.Name = "label5";
            label5.Size = new Size(32, 25);
            label5.TabIndex = 7;
            label5.Text = "dias";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDias
            // 
            txtDias.Cursor = Cursors.IBeam;
            txtDias.Enabled = false;
            txtDias.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDias.Location = new Point(180, 13);
            txtDias.Name = "txtDias";
            txtDias.Size = new Size(56, 25);
            txtDias.TabIndex = 5;
            txtDias.KeyPress += txtDias_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(15, 60);
            label2.Name = "label2";
            label2.Size = new Size(57, 21);
            label2.TabIndex = 4;
            label2.Text = "Filtros";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 22);
            label1.Name = "label1";
            label1.Size = new Size(119, 17);
            label1.TabIndex = 1;
            label1.Text = "Procurar Contatos";
            // 
            // txtBusca
            // 
            txtBusca.Cursor = Cursors.IBeam;
            txtBusca.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBusca.Location = new Point(139, 19);
            txtBusca.Name = "txtBusca";
            txtBusca.PlaceholderText = "Digite algum texto.. ";
            txtBusca.Size = new Size(367, 25);
            txtBusca.TabIndex = 0;
            txtBusca.TextChanged += txtBusca_TextChanged;
            // 
            // tabMensagem
            // 
            tabMensagem.Controls.Add(btConfirmarOpcoesMensagem);
            tabMensagem.Controls.Add(lbQuantidadeContatos);
            tabMensagem.Controls.Add(label3);
            tabMensagem.Controls.Add(btCancelarOpcoesMensagem);
            tabMensagem.Controls.Add(panelFiltroMensagem);
            tabMensagem.Controls.Add(btnEnviar);
            tabMensagem.Controls.Add(label6);
            tabMensagem.Controls.Add(label7);
            tabMensagem.Controls.Add(txtMensagem);
            tabMensagem.Location = new Point(4, 26);
            tabMensagem.Name = "tabMensagem";
            tabMensagem.Size = new Size(1256, 651);
            tabMensagem.TabIndex = 2;
            tabMensagem.Text = "Enviar Mensagem";
            // 
            // btConfirmarOpcoesMensagem
            // 
            btConfirmarOpcoesMensagem.Cursor = Cursors.Hand;
            btConfirmarOpcoesMensagem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btConfirmarOpcoesMensagem.Location = new Point(1073, 394);
            btConfirmarOpcoesMensagem.Name = "btConfirmarOpcoesMensagem";
            btConfirmarOpcoesMensagem.Size = new Size(126, 35);
            btConfirmarOpcoesMensagem.TabIndex = 19;
            btConfirmarOpcoesMensagem.Text = "Confirmar";
            btConfirmarOpcoesMensagem.UseVisualStyleBackColor = true;
            btConfirmarOpcoesMensagem.Click += btConfirmarOpcoesMensagem_Click;
            // 
            // lbQuantidadeContatos
            // 
            lbQuantidadeContatos.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbQuantidadeContatos.ForeColor = Color.Green;
            lbQuantidadeContatos.Location = new Point(863, 569);
            lbQuantidadeContatos.Name = "lbQuantidadeContatos";
            lbQuantidadeContatos.Size = new Size(152, 30);
            lbQuantidadeContatos.TabIndex = 16;
            lbQuantidadeContatos.Text = "0 Contatos";
            lbQuantidadeContatos.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(715, 32);
            label3.Name = "label3";
            label3.Size = new Size(484, 25);
            label3.TabIndex = 15;
            label3.Text = "Opções disponíves ao entrar sem selecionar contatos:";
            // 
            // btCancelarOpcoesMensagem
            // 
            btCancelarOpcoesMensagem.Cursor = Cursors.Hand;
            btCancelarOpcoesMensagem.Enabled = false;
            btCancelarOpcoesMensagem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btCancelarOpcoesMensagem.Location = new Point(941, 394);
            btCancelarOpcoesMensagem.Name = "btCancelarOpcoesMensagem";
            btCancelarOpcoesMensagem.Size = new Size(126, 35);
            btCancelarOpcoesMensagem.TabIndex = 20;
            btCancelarOpcoesMensagem.Text = "Cancelar";
            btCancelarOpcoesMensagem.UseVisualStyleBackColor = true;
            btCancelarOpcoesMensagem.Click += btCancelarOpcoesMensagem_Click;
            // 
            // panelFiltroMensagem
            // 
            panelFiltroMensagem.BackColor = SystemColors.Window;
            panelFiltroMensagem.BorderStyle = BorderStyle.FixedSingle;
            panelFiltroMensagem.Controls.Add(chkBusinessMensagem);
            panelFiltroMensagem.Controls.Add(chkSemConversaMensagem);
            panelFiltroMensagem.Controls.Add(ckTodosContatosMensagem);
            panelFiltroMensagem.Controls.Add(chkGeneroMensagem);
            panelFiltroMensagem.Controls.Add(cmbGeneroMensagem);
            panelFiltroMensagem.Controls.Add(txtDddMensagem);
            panelFiltroMensagem.Controls.Add(chkDddMensagem);
            panelFiltroMensagem.Controls.Add(chkDiasMensagem);
            panelFiltroMensagem.Controls.Add(label4);
            panelFiltroMensagem.Controls.Add(txtDiasMensagem);
            panelFiltroMensagem.Location = new Point(750, 70);
            panelFiltroMensagem.Name = "panelFiltroMensagem";
            panelFiltroMensagem.Size = new Size(449, 359);
            panelFiltroMensagem.TabIndex = 14;
            // 
            // chkBusinessMensagem
            // 
            chkBusinessMensagem.Cursor = Cursors.Hand;
            chkBusinessMensagem.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkBusinessMensagem.Location = new Point(45, 190);
            chkBusinessMensagem.Name = "chkBusinessMensagem";
            chkBusinessMensagem.Size = new Size(86, 25);
            chkBusinessMensagem.TabIndex = 20;
            chkBusinessMensagem.Text = "Business?";
            chkBusinessMensagem.UseVisualStyleBackColor = true;
            // 
            // chkSemConversaMensagem
            // 
            chkSemConversaMensagem.Cursor = Cursors.Hand;
            chkSemConversaMensagem.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkSemConversaMensagem.Location = new Point(45, 234);
            chkSemConversaMensagem.Name = "chkSemConversaMensagem";
            chkSemConversaMensagem.Size = new Size(228, 25);
            chkSemConversaMensagem.TabIndex = 19;
            chkSemConversaMensagem.Text = "Não incluir contatos sem conversa";
            chkSemConversaMensagem.UseVisualStyleBackColor = true;
            // 
            // ckTodosContatosMensagem
            // 
            ckTodosContatosMensagem.AutoSize = true;
            ckTodosContatosMensagem.Cursor = Cursors.Hand;
            ckTodosContatosMensagem.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ckTodosContatosMensagem.Location = new Point(26, 332);
            ckTodosContatosMensagem.Name = "ckTodosContatosMensagem";
            ckTodosContatosMensagem.Size = new Size(141, 21);
            ckTodosContatosMensagem.TabIndex = 18;
            ckTodosContatosMensagem.Text = "Todos os Contatos";
            ckTodosContatosMensagem.UseVisualStyleBackColor = true;
            ckTodosContatosMensagem.CheckedChanged += ckTodosContatosMensagem_CheckedChanged;
            // 
            // chkGeneroMensagem
            // 
            chkGeneroMensagem.AutoSize = true;
            chkGeneroMensagem.Cursor = Cursors.Hand;
            chkGeneroMensagem.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkGeneroMensagem.Location = new Point(45, 143);
            chkGeneroMensagem.Name = "chkGeneroMensagem";
            chkGeneroMensagem.Size = new Size(70, 21);
            chkGeneroMensagem.TabIndex = 17;
            chkGeneroMensagem.Text = "Gênero";
            chkGeneroMensagem.UseVisualStyleBackColor = true;
            chkGeneroMensagem.CheckedChanged += chkGeneroMensagem_CheckedChanged;
            // 
            // cmbGeneroMensagem
            // 
            cmbGeneroMensagem.Cursor = Cursors.Hand;
            cmbGeneroMensagem.Enabled = false;
            cmbGeneroMensagem.FormattingEnabled = true;
            cmbGeneroMensagem.Items.AddRange(new object[] { "M", "F", "O" });
            cmbGeneroMensagem.Location = new Point(121, 141);
            cmbGeneroMensagem.Name = "cmbGeneroMensagem";
            cmbGeneroMensagem.Size = new Size(50, 25);
            cmbGeneroMensagem.TabIndex = 16;
            // 
            // txtDddMensagem
            // 
            txtDddMensagem.Cursor = Cursors.IBeam;
            txtDddMensagem.Enabled = false;
            txtDddMensagem.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDddMensagem.Location = new Point(108, 85);
            txtDddMensagem.Name = "txtDddMensagem";
            txtDddMensagem.Size = new Size(59, 25);
            txtDddMensagem.TabIndex = 15;
            // 
            // chkDddMensagem
            // 
            chkDddMensagem.AutoSize = true;
            chkDddMensagem.Cursor = Cursors.Hand;
            chkDddMensagem.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkDddMensagem.Location = new Point(45, 88);
            chkDddMensagem.Name = "chkDddMensagem";
            chkDddMensagem.Size = new Size(57, 21);
            chkDddMensagem.TabIndex = 14;
            chkDddMensagem.Text = "DDD:";
            chkDddMensagem.UseVisualStyleBackColor = true;
            chkDddMensagem.CheckedChanged += chkDddMensagem_CheckedChanged;
            // 
            // chkDiasMensagem
            // 
            chkDiasMensagem.AutoSize = true;
            chkDiasMensagem.Cursor = Cursors.Hand;
            chkDiasMensagem.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkDiasMensagem.Location = new Point(45, 33);
            chkDiasMensagem.Name = "chkDiasMensagem";
            chkDiasMensagem.Size = new Size(167, 21);
            chkDiasMensagem.TabIndex = 13;
            chkDiasMensagem.Text = "Não entra em contato a";
            chkDiasMensagem.UseVisualStyleBackColor = true;
            chkDiasMensagem.CheckedChanged += chkDiasMensagem_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(276, 35);
            label4.Name = "label4";
            label4.Size = new Size(32, 17);
            label4.TabIndex = 12;
            label4.Text = "dias";
            // 
            // txtDiasMensagem
            // 
            txtDiasMensagem.Cursor = Cursors.IBeam;
            txtDiasMensagem.Enabled = false;
            txtDiasMensagem.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDiasMensagem.Location = new Point(214, 31);
            txtDiasMensagem.Name = "txtDiasMensagem";
            txtDiasMensagem.Size = new Size(59, 25);
            txtDiasMensagem.TabIndex = 11;
            // 
            // btnEnviar
            // 
            btnEnviar.BackColor = SystemColors.MenuHighlight;
            btnEnviar.Cursor = Cursors.Hand;
            btnEnviar.FlatStyle = FlatStyle.Popup;
            btnEnviar.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEnviar.ForeColor = SystemColors.Window;
            btnEnviar.Location = new Point(1058, 544);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(141, 55);
            btnEnviar.TabIndex = 13;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = false;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(29, 32);
            label6.Name = "label6";
            label6.Size = new Size(263, 25);
            label6.TabIndex = 11;
            label6.Text = "Escreva a mensagem abaixo:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(710, 544);
            label7.Name = "label7";
            label7.Size = new Size(314, 25);
            label7.TabIndex = 12;
            label7.Text = "Essa mensagem será enviada para:";
            // 
            // txtMensagem
            // 
            txtMensagem.AcceptsTab = true;
            txtMensagem.AutoWordSelection = true;
            txtMensagem.Cursor = Cursors.IBeam;
            txtMensagem.Location = new Point(29, 70);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtMensagem.Size = new Size(455, 529);
            txtMensagem.TabIndex = 9;
            txtMensagem.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            RightToLeft = RightToLeft.No;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ZapEnvio Seguro";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            tabControl1.ResumeLayout(false);
            tabWhatsApp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            tabContatos.ResumeLayout(false);
            tabContatos.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewContatos).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabMensagem.ResumeLayout(false);
            tabMensagem.PerformLayout();
            panelFiltroMensagem.ResumeLayout(false);
            panelFiltroMensagem.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabWhatsApp;
        private TabPage tabContatos;
        private Label label1;
        private TextBox txtBusca;
        private Panel panel1;
        private Label label2;
        private Label label5;
        private TextBox txtDias;
        private Button btnAplicarFiltros;
        private CheckBox chkFiltroDias;
        private CheckBox chkFiltroDDD;
        private DataGridView dataGridViewContatos;
        private CheckBox chkSelecionarTodos;
        private Button btnSincronizarContatos;
        private Button btnAdicionarContato;
        private Button btnRemoverFiltros;
        private Button btnEnviarMensagem;
        private TextBox txtDDD;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        public TabPage tabMensagem;
        private Label lbQuantidadeContatos;
        private Label label3;
        private Panel panelFiltroMensagem;
        private Button btConfirmarOpcoesMensagem;
        private CheckBox ckTodosContatosMensagem;
        private CheckBox chkGeneroMensagem;
        private ComboBox cmbGeneroMensagem;
        private TextBox txtDddMensagem;
        private CheckBox chkDddMensagem;
        private CheckBox chkDiasMensagem;
        private Label label4;
        private TextBox txtDiasMensagem;
        private Button btnEnviar;
        private Label label6;
        private Label label7;
        private RichTextBox txtMensagem;
        private Button btCancelarOpcoesMensagem;
        private Label lbNovosContatos;
        private Label lbCliqueImportar;
        private Label lbQtdFiltrados;
        private CheckBox chkFiltroSemConversa;
        private CheckBox chkFiltroBusiness;
        private CheckBox chkBusinessMensagem;
        private CheckBox chkSemConversaMensagem;
        private Panel panel2;
    }
}
