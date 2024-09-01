namespace LunarSoftwareAtivador
{
    partial class FrmDemonstracao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDemonstracao));
            this.txtRazaoSocial = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCnpj = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNomeFantasia = new System.Windows.Forms.TextBox();
            this.txtCep = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtComplemento = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtUf = new System.Windows.Forms.TextBox();
            this.txtCelular = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFoneFixo = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSegmento = new System.Windows.Forms.TextBox();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCnpjRepresentante = new System.Windows.Forms.MaskedTextBox();
            this.chkTermosUso = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label16 = new System.Windows.Forms.Label();
            this.txtIe = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtResponsavel = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtCpfResponsavel = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnPesquisarCidade = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblLoading = new System.Windows.Forms.Label();
            this.chkTerminal = new System.Windows.Forms.CheckBox();
            this.chkServidor = new System.Windows.Forms.CheckBox();
            this.lblServidor = new System.Windows.Forms.Label();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtRazaoSocial
            // 
            this.txtRazaoSocial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRazaoSocial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazaoSocial.Location = new System.Drawing.Point(194, 93);
            this.txtRazaoSocial.Name = "txtRazaoSocial";
            this.txtRazaoSocial.Size = new System.Drawing.Size(276, 26);
            this.txtRazaoSocial.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "CNPJ";
            // 
            // txtCnpj
            // 
            this.txtCnpj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtCnpj.Location = new System.Drawing.Point(16, 93);
            this.txtCnpj.Mask = "00,000,000/0000-00";
            this.txtCnpj.Name = "txtCnpj";
            this.txtCnpj.Size = new System.Drawing.Size(172, 26);
            this.txtCnpj.TabIndex = 0;
            this.txtCnpj.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtCnpj.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCnpj_KeyPress);
            this.txtCnpj.Leave += new System.EventHandler(this.txtCnpj_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(190, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Razão Social";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(472, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nome Fantasia";
            // 
            // txtNomeFantasia
            // 
            this.txtNomeFantasia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeFantasia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeFantasia.Location = new System.Drawing.Point(476, 93);
            this.txtNomeFantasia.Name = "txtNomeFantasia";
            this.txtNomeFantasia.Size = new System.Drawing.Size(312, 26);
            this.txtNomeFantasia.TabIndex = 2;
            // 
            // txtCep
            // 
            this.txtCep.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtCep.Location = new System.Drawing.Point(621, 150);
            this.txtCep.Mask = "00,000-000";
            this.txtCep.Name = "txtCep";
            this.txtCep.Size = new System.Drawing.Size(127, 26);
            this.txtCep.TabIndex = 5;
            this.txtCep.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(617, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "CEP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Endereço";
            // 
            // txtEndereco
            // 
            this.txtEndereco.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEndereco.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndereco.Location = new System.Drawing.Point(16, 203);
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.Size = new System.Drawing.Size(387, 26);
            this.txtEndereco.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(405, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Número";
            // 
            // txtNumero
            // 
            this.txtNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumero.Location = new System.Drawing.Point(409, 203);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(91, 26);
            this.txtNumero.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(502, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Complemento";
            // 
            // txtComplemento
            // 
            this.txtComplemento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComplemento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComplemento.Location = new System.Drawing.Point(506, 203);
            this.txtComplemento.Name = "txtComplemento";
            this.txtComplemento.Size = new System.Drawing.Size(282, 26);
            this.txtComplemento.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 232);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "Bairro";
            // 
            // txtBairro
            // 
            this.txtBairro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBairro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBairro.Location = new System.Drawing.Point(16, 255);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(324, 26);
            this.txtBairro.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(342, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "Cidade";
            // 
            // txtCidade
            // 
            this.txtCidade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCidade.Enabled = false;
            this.txtCidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCidade.Location = new System.Drawing.Point(346, 255);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Size = new System.Drawing.Size(324, 26);
            this.txtCidade.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(709, 232);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 20);
            this.label10.TabIndex = 20;
            this.label10.Text = "UF";
            // 
            // txtUf
            // 
            this.txtUf.Enabled = false;
            this.txtUf.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUf.Location = new System.Drawing.Point(713, 255);
            this.txtUf.Name = "txtUf";
            this.txtUf.Size = new System.Drawing.Size(75, 26);
            this.txtUf.TabIndex = 13;
            // 
            // txtCelular
            // 
            this.txtCelular.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtCelular.Location = new System.Drawing.Point(488, 330);
            this.txtCelular.Mask = "(99) 0 0000-0000";
            this.txtCelular.Name = "txtCelular";
            this.txtCelular.Size = new System.Drawing.Size(147, 26);
            this.txtCelular.TabIndex = 15;
            this.txtCelular.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(484, 307);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(135, 20);
            this.label11.TabIndex = 22;
            this.label11.Text = "Celular Whatsapp";
            // 
            // txtFoneFixo
            // 
            this.txtFoneFixo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtFoneFixo.Location = new System.Drawing.Point(641, 330);
            this.txtFoneFixo.Mask = "(99) 0000-0000";
            this.txtFoneFixo.Name = "txtFoneFixo";
            this.txtFoneFixo.Size = new System.Drawing.Size(147, 26);
            this.txtFoneFixo.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(637, 307);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 20);
            this.label12.TabIndex = 24;
            this.label12.Text = "Fone Fixo";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(12, 307);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(425, 20);
            this.label13.TabIndex = 27;
            this.label13.Text = "Segmento da Empresa (Exemplo: Ótica, Loja de Roupas...)";
            // 
            // txtSegmento
            // 
            this.txtSegmento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSegmento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSegmento.Location = new System.Drawing.Point(16, 330);
            this.txtSegmento.Name = "txtSegmento";
            this.txtSegmento.Size = new System.Drawing.Size(466, 26);
            this.txtSegmento.TabIndex = 14;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnConfirmar.Location = new System.Drawing.Point(624, 581);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(164, 44);
            this.btnConfirmar.TabIndex = 22;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.label14.Location = new System.Drawing.Point(294, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(212, 31);
            this.label14.TabIndex = 29;
            this.label14.Text = "Lunar Software";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(12, 576);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(236, 20);
            this.label15.TabIndex = 31;
            this.label15.Text = "CNPJ Representante (Opcional)";
            // 
            // txtCnpjRepresentante
            // 
            this.txtCnpjRepresentante.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtCnpjRepresentante.Location = new System.Drawing.Point(16, 599);
            this.txtCnpjRepresentante.Mask = "00,000,000/0000-00";
            this.txtCnpjRepresentante.Name = "txtCnpjRepresentante";
            this.txtCnpjRepresentante.Size = new System.Drawing.Size(232, 26);
            this.txtCnpjRepresentante.TabIndex = 19;
            this.txtCnpjRepresentante.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtCnpjRepresentante.Leave += new System.EventHandler(this.txtCnpjRepresentante_Leave);
            // 
            // chkTermosUso
            // 
            this.chkTermosUso.AutoSize = true;
            this.chkTermosUso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTermosUso.Location = new System.Drawing.Point(368, 594);
            this.chkTermosUso.Name = "chkTermosUso";
            this.chkTermosUso.Size = new System.Drawing.Size(163, 20);
            this.chkTermosUso.TabIndex = 21;
            this.chkTermosUso.Text = "Confirmo que aceito os";
            this.chkTermosUso.UseVisualStyleBackColor = true;
            this.chkTermosUso.CheckedChanged += new System.EventHandler(this.chkTermosUso_CheckedChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(527, 595);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(92, 16);
            this.linkLabel1.TabIndex = 34;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "termos de uso";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(12, 127);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 20);
            this.label16.TabIndex = 36;
            this.label16.Text = "I.E";
            // 
            // txtIe
            // 
            this.txtIe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIe.Location = new System.Drawing.Point(16, 150);
            this.txtIe.Name = "txtIe";
            this.txtIe.Size = new System.Drawing.Size(172, 26);
            this.txtIe.TabIndex = 3;
            this.txtIe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIe_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(190, 127);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 20);
            this.label17.TabIndex = 38;
            this.label17.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(194, 150);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(421, 26);
            this.txtEmail.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(12, 366);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(215, 20);
            this.label18.TabIndex = 41;
            this.label18.Text = "Nome Responsável Empresa";
            // 
            // txtResponsavel
            // 
            this.txtResponsavel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtResponsavel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResponsavel.Location = new System.Drawing.Point(16, 389);
            this.txtResponsavel.Name = "txtResponsavel";
            this.txtResponsavel.Size = new System.Drawing.Size(581, 26);
            this.txtResponsavel.TabIndex = 17;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(599, 366);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(136, 20);
            this.label20.TabIndex = 45;
            this.label20.Text = "CPF Responsável";
            // 
            // txtCpfResponsavel
            // 
            this.txtCpfResponsavel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCpfResponsavel.Location = new System.Drawing.Point(603, 389);
            this.txtCpfResponsavel.Name = "txtCpfResponsavel";
            this.txtCpfResponsavel.Size = new System.Drawing.Size(185, 26);
            this.txtCpfResponsavel.TabIndex = 18;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(366, 617);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 46;
            this.lblStatus.Text = "Status";
            this.lblStatus.Visible = false;
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::LunarSoftwareAtivador.Properties.Resources.Lupa1;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(320, 598);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 30);
            this.button2.TabIndex = 47;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnPesquisarCidade
            // 
            this.btnPesquisarCidade.BackgroundImage = global::LunarSoftwareAtivador.Properties.Resources.Lupa1;
            this.btnPesquisarCidade.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPesquisarCidade.Location = new System.Drawing.Point(676, 253);
            this.btnPesquisarCidade.Name = "btnPesquisarCidade";
            this.btnPesquisarCidade.Size = new System.Drawing.Size(31, 30);
            this.btnPesquisarCidade.TabIndex = 12;
            this.btnPesquisarCidade.UseVisualStyleBackColor = true;
            this.btnPesquisarCidade.Click += new System.EventHandler(this.btnPesquisarCidade_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::LunarSoftwareAtivador.Properties.Resources.Lupa1;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(754, 149);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 30);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoading.ForeColor = System.Drawing.Color.Red;
            this.lblLoading.Location = new System.Drawing.Point(13, 45);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(42, 16);
            this.lblLoading.TabIndex = 48;
            this.lblLoading.Text = "CNPJ";
            this.lblLoading.Visible = false;
            // 
            // chkTerminal
            // 
            this.chkTerminal.AutoSize = true;
            this.chkTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTerminal.Location = new System.Drawing.Point(110, 421);
            this.chkTerminal.Name = "chkTerminal";
            this.chkTerminal.Size = new System.Drawing.Size(88, 24);
            this.chkTerminal.TabIndex = 49;
            this.chkTerminal.Text = "Terminal";
            this.chkTerminal.UseVisualStyleBackColor = true;
            this.chkTerminal.CheckedChanged += new System.EventHandler(this.chkTerminal_CheckedChanged);
            // 
            // chkServidor
            // 
            this.chkServidor.AutoSize = true;
            this.chkServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkServidor.Location = new System.Drawing.Point(16, 421);
            this.chkServidor.Name = "chkServidor";
            this.chkServidor.Size = new System.Drawing.Size(86, 24);
            this.chkServidor.TabIndex = 50;
            this.chkServidor.Text = "Servidor";
            this.chkServidor.UseVisualStyleBackColor = true;
            this.chkServidor.CheckedChanged += new System.EventHandler(this.chkServidor_CheckedChanged);
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = true;
            this.lblServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServidor.Location = new System.Drawing.Point(12, 448);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(176, 20);
            this.lblServidor.TabIndex = 52;
            this.lblServidor.Text = "Nome ou IP do Servidor";
            this.lblServidor.Visible = false;
            // 
            // txtServidor
            // 
            this.txtServidor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServidor.Location = new System.Drawing.Point(16, 471);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(772, 26);
            this.txtServidor.TabIndex = 51;
            this.txtServidor.Visible = false;
            // 
            // FrmDemonstracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 637);
            this.Controls.Add(this.lblServidor);
            this.Controls.Add(this.txtServidor);
            this.Controls.Add(this.chkServidor);
            this.Controls.Add(this.chkTerminal);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtCpfResponsavel);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtResponsavel);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtIe);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.chkTermosUso);
            this.Controls.Add(this.txtCnpjRepresentante);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtSegmento);
            this.Controls.Add(this.txtFoneFixo);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtCelular);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnPesquisarCidade);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtUf);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCidade);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtBairro);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtComplemento);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEndereco);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCep);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNomeFantasia);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCnpj);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRazaoSocial);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDemonstracao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Empresa";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmDemonstracao_FormClosed);
            this.Load += new System.EventHandler(this.FrmDemonstracao_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmDemonstracao_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDemonstracao_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRazaoSocial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtCnpj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNomeFantasia;
        private System.Windows.Forms.MaskedTextBox txtCep;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEndereco;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtComplemento;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtUf;
        private System.Windows.Forms.Button btnPesquisarCidade;
        private System.Windows.Forms.MaskedTextBox txtCelular;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MaskedTextBox txtFoneFixo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSegmento;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MaskedTextBox txtCnpjRepresentante;
        private System.Windows.Forms.CheckBox chkTermosUso;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtIe;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtResponsavel;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtCpfResponsavel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.CheckBox chkTerminal;
        private System.Windows.Forms.CheckBox chkServidor;
        private System.Windows.Forms.Label lblServidor;
        private System.Windows.Forms.TextBox txtServidor;
    }
}