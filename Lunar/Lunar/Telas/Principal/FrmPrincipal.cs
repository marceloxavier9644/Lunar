using FontAwesome.Sharp;
using Lunar.Telas.ArquivosContabilidade;
using Lunar.Telas.Cadastros.Bancos;
using Lunar.Telas.Cadastros.BandeirasCartao;
using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.Cadastros.Cliente.PessoaAdicionais;
using Lunar.Telas.Cadastros.Empresas;
using Lunar.Telas.Cadastros.Financeiro.Cartoes;
using Lunar.Telas.Cadastros.Financeiro.PlanoContas.PlanosPorGrupos;
using Lunar.Telas.Cadastros.Produtos;
using Lunar.Telas.Cadastros.Produtos.Auxiliares;
using Lunar.Telas.CaixaConferencia;
using Lunar.Telas.CaixaConferencia.Reports;
using Lunar.Telas.Compras.Manifestos;
using Lunar.Telas.Condicionais;
using Lunar.Telas.ContasPagar;
using Lunar.Telas.ContasReceber;
using Lunar.Telas.Estoques;
using Lunar.Telas.Fiscal;
using Lunar.Telas.Fiscal.Adicionais;
using Lunar.Telas.Food;
using Lunar.Telas.IntegracoesEcommerce.NuvemShopIntegration;
using Lunar.Telas.Mensagens;
using Lunar.Telas.Orcamentos;
using Lunar.Telas.OrdensDeServico;
using Lunar.Telas.OrdensDeServico.Servicos;
using Lunar.Telas.ParametroDoSistema;
using Lunar.Telas.PermissoesUsuarios;
using Lunar.Telas.ReciboAvulso1;
using Lunar.Telas.RelatoriosDiversos;
using Lunar.Telas.Sintegra;
using Lunar.Telas.TransferenciaEstoques;
using Lunar.Telas.UsuarioRegistro;
using Lunar.Telas.ValeFuncionarios;
using Lunar.Telas.Vendas;
using Lunar.Telas.Vendas.Relatorios;
using Lunar.Utils;
using Lunar.Utils.ClassesRepeticoes;
using Lunar.Utils.ImportadorSistemas;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Principal
{
    public partial class FrmPrincipal : Form
    {
        bool passou = false;
        //Fields
        private int borderSize = 2;
        private Size formSize; //Keep form size when it is minimized and restored.Since the form is resized because it takes into account the size of the title bar and borders.
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        Logger logger = new Logger();
        //Novo

        private DragControl dragControl; // Lets you drag the form.
        private List<Form> listChildForms; // Gets or sets the child forms open on the form's desktop panel.
        private Form activeChildForm; // Gets or sets the currently displayed child form.

        public FrmPrincipal()
        {
            InitializeComponent();

            CollapseMenu();
            this.Padding = new Padding(borderSize);//Border size
            this.BackColor = Color.FromArgb(98, 102, 244);//Border color

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);

            btnMaximize.PerformClick();
            CollapseMenu();

            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            dragControl = new DragControl(panelTitleBar, this);
            listChildForms = new List<Form>();
            consultaCEP();
            lblTitleChildForm.Text = "Home";

            Sessao.empresaFilialLogada = new EmpresaFilial();
            Sessao.empresaFilialLogada.Id = 1;
            Sessao.empresaFilialLogada = (EmpresaFilial)Controller.getInstance().selecionar(Sessao.empresaFilialLogada);

            if (File.Exists(Sessao.parametroSistema.Logo))
            {
                ajustarImagemFundo();
            }

            //lblCaption.Text = "Página Inicial - " + Sessao.usuarioLogado.Login;
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void logarCaixa()
        {
            Sessao.caixaLogado = new CaixaAbertura();
            CaixaAberturaController caixaAberturaController = new CaixaAberturaController();
            if (Sessao.parametroSistema.TipoCaixa == "GERAL")
            {
                IList<CaixaAbertura> listaCaixa = caixaAberturaController.selecionarTodosCaixasAbertos();
                if(listaCaixa.Count >= 1)
                {
                    foreach(CaixaAbertura caixaAbertura in listaCaixa)
                    {
                        if (caixaAbertura.Logado == true)
                        {
                            Sessao.caixaLogado = caixaAbertura;
                            if (Sessao.caixaLogado.DataAbertura.ToShortDateString() != DateTime.Now.ToShortDateString())
                                GenericaDesktop.ShowAlerta("O caixa que está aberto tem uma data diferente da de hoje. Suas vendas serão registradas com a data do caixa: " + Sessao.caixaLogado.DataAbertura.ToShortDateString());
                        }
                    }
                }
            }
            //Caixa Individual
            else
            {
                IList<CaixaAbertura> listaCaixa = caixaAberturaController.selecionarAberturaCaixaPorUsuario(Sessao.usuarioLogado.Id);
                if (listaCaixa.Count >= 1)
                {
                    foreach (CaixaAbertura caixaAbertura in listaCaixa)
                    {
                        if (caixaAbertura.Logado == true)
                            Sessao.caixaLogado = caixaAbertura;
                        if (Sessao.caixaLogado.DataAbertura.ToShortDateString() != DateTime.Now.ToShortDateString())
                            GenericaDesktop.ShowAlerta("O caixa que está aberto tem uma data diferente da de hoje. Suas vendas serão registradas com a data do caixa: " + Sessao.caixaLogado.DataAbertura.ToShortDateString());
                    }
                }
            }
            verificaUsuarioLogado();
        }
        private Image RedimensionarImagemPadrao(Image imagemOriginal, int larguraPadrao = 500, int alturaPadrao = 300)
        {
            var proporcao = Math.Min((float)larguraPadrao / imagemOriginal.Width, (float)alturaPadrao / imagemOriginal.Height);
            var larguraNova = (int)(imagemOriginal.Width * proporcao);
            var alturaNova = (int)(imagemOriginal.Height * proporcao);

            var imagemRedimensionada = new Bitmap(larguraNova, alturaNova);
            using (var g = Graphics.FromImage(imagemRedimensionada))
            {
                g.DrawImage(imagemOriginal, 0, 0, larguraNova, alturaNova);
            }

            return imagemRedimensionada;
        }
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //Overridden methods
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;//Standar Title Bar - Snap Window
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020; //Minimize form (Before)
            const int SC_RESTORE = 0xF120; //Restore form (Before)
            const int WM_NCHITTEST = 0x0084;//Win32, Mouse Input Notification: Determine what part of the window corresponds to a point, allows to resize the form.
            const int resizeAreaSize = 10;
            #region Form Resize
            // Resize/WM_NCHITTEST values
            const int HTCLIENT = 1; //Represents the client area of the window
            const int HTLEFT = 10;  //Left border of a window, allows resize horizontally to the left
            const int HTRIGHT = 11; //Right border of a window, allows resize horizontally to the right
            const int HTTOP = 12;   //Upper-horizontal border of a window, allows resize vertically up
            const int HTTOPLEFT = 13;//Upper-left corner of a window border, allows resize diagonally to the left
            const int HTTOPRIGHT = 14;//Upper-right corner of a window border, allows resize diagonally to the right
            const int HTBOTTOM = 15; //Lower-horizontal border of a window, allows resize vertically down
            const int HTBOTTOMLEFT = 16;//Lower-left corner of a window border, allows resize diagonally to the left
            const int HTBOTTOMRIGHT = 17;//Lower-right corner of a window border, allows resize diagonally to the right
            ///<Doc> More Information: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest </Doc>
            if (m.Msg == WM_NCHITTEST)
            { //If the windows m is WM_NCHITTEST
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)//Resize the form if it is in normal state
                {
                    if ((int)m.Result == HTCLIENT)//If the result of the m (mouse pointer) is in the client area of the window
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32()); //Gets screen point coordinates(X and Y coordinate of the pointer)                           
                        Point clientPoint = this.PointToClient(screenPoint); //Computes the location of the screen point into client coordinates                          
                        if (clientPoint.Y <= resizeAreaSize)//If the pointer is at the top of the form (within the resize area- X coordinate)
                        {
                            if (clientPoint.X <= resizeAreaSize) //If the pointer is at the coordinate X=0 or less than the resizing area(X=10) in 
                                m.Result = (IntPtr)HTTOPLEFT; //Resize diagonally to the left
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))//If the pointer is at the coordinate X=11 or less than the width of the form(X=Form.Width-resizeArea)
                                m.Result = (IntPtr)HTTOP; //Resize vertically up
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize)) //If the pointer is inside the form at the Y coordinate(discounting the resize area size)
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize horizontally to the left
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))//Resize horizontally to the right
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize diagonally to the left
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize)) //Resize vertically down
                                m.Result = (IntPtr)HTBOTTOM;
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            #endregion
            //Remove border and keep snap window
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            //Keep form size when it is minimized and restored. Since the form is resized because it takes into account the size of the title bar and borders.
            if (m.Msg == WM_SYSCOMMAND)
            {
                /// <see cref="https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand"/>
                /// Quote:
                /// In WM_SYSCOMMAND messages, the four low - order bits of the wParam parameter 
                /// are used internally by the system.To obtain the correct result when testing 
                /// the value of wParam, an application must combine the value 0xFFF0 with the 
                /// wParam value by using the bitwise AND operator.
                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                if (wParam == SC_MINIMIZE)  //Before
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)// Restored form(Before)
                    this.Size = formSize;
            }
            base.WndProc(ref m);
        }

        private void CollapseMenu()
        {
            if (this.panelMenu.Width > 200) //Collapse menu
            {
                panelMenu.Width = 100;
                pictureBox1.Visible = false;
                btnMenu.Dock = DockStyle.Top;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            }
            else
            { //Expand menu
                panelMenu.Width = 230;
                pictureBox1.Visible = true;
                btnMenu.Dock = DockStyle.None;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "   " + menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
            }
        } 
        
        private void FrmPrincipal_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized: //Maximized form (After)
                    this.Padding = new Padding(8, 8, 8, 0);
                    break;
                case FormWindowState.Normal: //Restored form (After)
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                formSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = formSize;
            }
        }

        private void btnClose_Click_Click(object sender, EventArgs e)
        {
            CloseApp();
        }

        public static void CloseApp()
        {
            if (MessageBox.Show("Tem certeza que deseja fechar o aplicativo?", "Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                Application.Exit();//Close the entire application ending all processes.
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                //DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //Current Child Form Icon
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
                lblTitleChildForm.Text = currentBtn.Tag.ToString();
            }
        }
        private void DisableButton(Button menuButton)
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void btnDashboards_Click(object sender, EventArgs e)
        {
            //OpenChildForm(() => new FrmDashboard1(), sender);
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmClienteLista(), sender);
        }

        private void btnProduto_Click(object sender, EventArgs e)
        {
            //OpenChildForm(() => new FrmProdutoLista(), sender);

            dropMenuProduto.Show(btnProduto, btnProduto.Width, 0);
        }

        //private void OpenChildForm(Form childForm)
        //{
        //    //open only form
        //    if (currentChildForm != null)
        //    {
        //        currentChildForm.Close();
        //    }
        //    currentChildForm = childForm;
        //    //End
        //    childForm.TopLevel = false;
        //    childForm.FormBorderStyle = FormBorderStyle.None;
        //    childForm.Dock = DockStyle.Fill;
        //    panelDesktop.Controls.Add(childForm);
        //    panelDesktop.Tag = childForm;
        //    childForm.BringToFront();
        //    childForm.Show();
        //    lblTitleChildForm.Text = childForm.Text;
        //}

        //private void Reset()
        //{
        //    DisableButton();
        //    leftBorderBtn.Visible = false;
        //    iconCurrentChildForm.IconChar = IconChar.Home;
        //    iconCurrentChildForm.IconColor = Color.MediumPurple;
        //    lblTitleChildForm.Text = "Dashboards";
        //}

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //currentChildForm.Close();
            //ResetDefaults();
            System.Diagnostics.Process.Start("https://lunarsoftware.com.br");
        }

        private void btnMenuVenda_Click(object sender, EventArgs e)
        {
            //ActivateButton(sender, RGBColors.color6);
            //OpenChildForm(() => new FrmVendas02(), sender);
            dropMenuVendas.Show(btnMenuVenda, btnMenuVenda.Width, 0);
        }


        //Layout novo
        public void OpenChildForm<childForm>(Func<childForm> _delegate, object senderMenuButton) where childForm : Form
        {
            Button menuButton = (Button)senderMenuButton;
            Form form = listChildForms.OfType<childForm>().FirstOrDefault();

            if (activeChildForm != null && form == activeChildForm)
                return;

            if (form == null)
            {
                form = _delegate();
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; 
                form.TopLevel = false; // Indicate that the form is not top-level
                form.Dock = DockStyle.Fill; // Set the dock style to full (Fill the desktop panel)
                listChildForms.Add(form); // Add child form to the list of forms.

                if (menuButton != null) // If the menu button is other than null:
                {
                    //ActivateButton(senderMenuButton, RGBColors.color6);
                    ////ActivateButton(menuButton); // Activate / Highlight the button.
                    //form.FormClosed += (s, e) =>
                    //{// When the form closes, deactivate the button.
                    // //DeactivateButton(menuButton);
                    //    DisableButton(menuButton);
                    //};
                }
                btnChildFormClose.Visible = true; // Show the child form close button.
            }
            CleanDesktop(); // Remove the current child form from the desktop panel
            panelDesktop.Controls.Add(form); // add child form to desktop panel
            panelDesktop.Tag = form; // Store the form
            form.Show(); // Show the form
            form.BringToFront(); // Bring to front
            form.Focus(); // Focus the form
            lblCaption.Text = form.Text; // Set the title of the form.
            activeChildForm = form; // Set as active form.


            /// <Note>
            /// You can use the Func <TResult> delegate with anonymous methods or lambda expression,
            /// For example, we can call this method in the following way: Suppose we are in the click event method of some button.
            /// With anonymous method:
            /// <see cref = "OpenChildForm (delegate () {return new MyForm ('MyParameter');});" />
            /// With lambda expression (My favorite)
            /// <see cref = "OpenChildForm (() => new MyForm ('id', 'username'));" />
            /// </Note>
        }

        private void CloseChildForm()
        {//Close active child form.

            if (activeChildForm != null)
            {
                listChildForms.Remove(activeChildForm); // Remove from the list of forms.
                panelDesktop.Controls.Remove(activeChildForm); // Remove from the desktop panel.
                activeChildForm.Close(); // Close the form.
                RefreshDesktop(); // Refresh the desktop panel (Show the previous form if it is the case, otherwise restore the main form)
            }
        }

        private void ActivateButton(Button menuButton)
        {
            menuButton.ForeColor = Color.RoyalBlue;
            //menuButton.BackColor = panelMenuHeader.BackColor;
        }
        private void DeactivateButton(Button menuButton)
        {
            menuButton.ForeColor = Color.DarkGray;
            //menuButton.BackColor = panelSideMenu.BackColor;
        }

        private void CleanDesktop()
        {
            if (activeChildForm != null)
            {
                activeChildForm.Hide();
                panelDesktop.Controls.Remove(activeChildForm);
            }
        }
        private void RefreshDesktop()
        {
            var childForm = listChildForms.LastOrDefault();
            if (childForm != null)
            {
                activeChildForm = childForm;
                panelDesktop.Controls.Add(childForm);
                panelDesktop.Tag = childForm;
                childForm.Show();
                childForm.BringToFront();
                lblCaption.Text = childForm.Text;
            }
            else 
            {
                ResetDefaults();
            }
        }
        private void ResetDefaults()
        {
            activeChildForm = null;
            lblCaption.Text = "   Home";
            btnChildFormClose.Visible = false;
        }

        private void btnChildFormClose_Click(object sender, EventArgs e)
        {
            CloseChildForm();
        }

        private CNPJResponse consultaEmpresa(string cnpj)
        {
            var requisicaoWeb = WebRequest.CreateHttp("https://api-publica.speedio.com.br/buscarcnpj?cnpj=" + cnpj);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "ConsultaCNPJ";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var empresa = JsonConvert.DeserializeObject<CNPJResponse>(objResponse.ToString());
                streamDados.Close();
                resposta.Close();
                return empresa;
            }
        }

        private void consultaCEP()
        {
            try
            {
                var ws = new WSCorreios.AtendeClienteClient();
                var resposta = ws.consultaCEP("38613302", "marcelo.xs@hotmail.com", "@Aranhamxs11");
                
                //MessageBox.Show(resposta.end);
                //MessageBox.Show(resposta.bairro);
                //MessageBox.Show(resposta.cidade);
                //MessageBox.Show(resposta.uf);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Erro ao efetuar busca do CEP: {0}", ex.Message);
            }
        }

        //private void gerarNfe()
        //{
        //    NFe nfe = new NFe();
        //    nfe.infNFe = new NFeInfNFe();
        //    nfe.infNFe.dest = new NFeInfNFeDest();
        //    nfe.infNFe.dest.enderDest = new NFeInfNFeDestEnderDest();
        //    nfe.infNFe.emit = new NFeInfNFeEmit();

        //}

        private void btnCompra_Click(object sender, EventArgs e)
        {
            dropMenuServico.Show(btnServicos, btnServicos.Width, 0);
        }

        private void btnUtilitarios_Click(object sender, EventArgs e)
        {
            dropMenuUtilitarios.Show(btnUtilitarios, btnUtilitarios.Width, 0);
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            dropMenuUtilitarios.IsMainMenu = true;
            dropMenuUtilitarios.PrimaryColor = Color.FromArgb(37, 36, 81);
            dropMenuUtilitarios.MenuItemTextColor = Color.White;
            dropMenuUtilitarios.Cursor = Cursors.Hand;

            dropMenuFinanceiro.IsMainMenu = true;
            dropMenuFinanceiro.PrimaryColor = Color.FromArgb(37, 36, 81);
            dropMenuFinanceiro.MenuItemTextColor = Color.White;
            dropMenuFinanceiro.Cursor = Cursors.Hand;

            dropMenuFiscal.IsMainMenu = true;
            dropMenuFiscal.PrimaryColor = Color.FromArgb(37, 36, 81);
            dropMenuFiscal.MenuItemTextColor = Color.White;
            dropMenuFiscal.Cursor = Cursors.Hand;

            dropMenuServico.IsMainMenu = true;
            dropMenuServico.PrimaryColor = Color.FromArgb(37, 36, 81);
            dropMenuServico.MenuItemTextColor = Color.White;
            dropMenuServico.Cursor = Cursors.Hand;

            dropMenuRelatorios.IsMainMenu = true;
            dropMenuRelatorios.PrimaryColor = Color.FromArgb(37, 36, 81);
            dropMenuRelatorios.MenuItemTextColor = Color.White;
            dropMenuRelatorios.Cursor = Cursors.Hand;

            dropMenuVendas.IsMainMenu = true;
            dropMenuVendas.PrimaryColor = Color.FromArgb(37, 36, 81);
            dropMenuVendas.MenuItemTextColor = Color.White;
            dropMenuVendas.Cursor = Cursors.Hand;

            dropMenuProduto.IsMainMenu = true;
            dropMenuProduto.PrimaryColor = Color.FromArgb(37, 36, 81);
            dropMenuProduto.MenuItemTextColor = Color.White;
            dropMenuProduto.Cursor = Cursors.Hand;

            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            if (w == 1024 && h == 768)
            {
                CollapseMenu();
            }
            if (h < 768)
            {
                GenericaDesktop.ShowAlerta("A resolução do seu monitor não atende os requisitos mínimos do sistema Lunar, " +
                    "verifique com um técnico a possibilidade da resolução de tela ficar em 1366x768 ou superior");
            }

            abrirNsCloud();
            logarCaixa();

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lblVersaoSistema.Text = "Versão do Sistema: " + version;
            UpdateScript updateScript = new UpdateScript();
            updateScript.ExecutarScript();
        }

        private void verificaUsuarioLogado()
        {
            if (Sessao.caixaLogado != null)
            {
                if(Sessao.caixaLogado.Id > 0)
                    lblUserLogado.Text = Sessao.usuarioLogado.Login + " - CAIXA ABERTO: " + Sessao.caixaLogado.DataAbertura.ToShortDateString();
                else
                    lblUserLogado.Text = "Usuário Logado: " + Sessao.usuarioLogado.Login;
            }
            else
            {
                lblUserLogado.Text = "Usuário Logado: " + Sessao.usuarioLogado.Login;
            }
        }
        private async Task abrirNsCloud()
        {
            await GenericaDesktop.VerificaProgramaContigenciaEstaEmExecucao();
        }
        private void EmpresaMenu_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("106"))
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (106)!");
            else
                OpenChildForm(() => new FrmEmpresaLista(), btnUtilitarios);
        }

        private void UsuariosMenu_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("101"))
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (101)!");
            else
                OpenChildForm(() => new FrmUsuarioLista(), btnUtilitarios);
        }

        private void FrmPrincipal_Paint(object sender, PaintEventArgs e)
        {
            if (passou == false)
            {
                if (String.IsNullOrEmpty(Sessao.usuarioLogado.Senha))
                {
                    GenericaDesktop.ShowAlerta("Usuário com senha em branco, coloque uma senha para sua segurança!");
                    FrmUsuarioCadastro frmUser = new FrmUsuarioCadastro(Sessao.usuarioLogado);
                    frmUser.ShowDialog();
                }
                passou = true;
            }
        }

        private void adquirenteMaquinaCartãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("104"))
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (104)!");
            else
                OpenChildForm(() => new FrmAdquirenteCartaoLista(), btnUtilitarios);
        }

        private void bandeirasDeCartãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("104"))
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (104)!");
            else
                OpenChildForm(() => new FrmBandeiraCartaoLista(), btnUtilitarios);
        }

        private void contaBancáriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("105"))
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (105)!");
            else
                OpenChildForm(() => new FrmContaBancariaLista(), btnUtilitarios);
        }

        private void btnPlanoDecontas1_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmPlanoConta(), btnFinanceiro);       
        }

        private void btnFinanceiro_Click(object sender, EventArgs e)
        {
            dropMenuFinanceiro.Show(btnFinanceiro, btnFinanceiro.Width, 0);
        }

        private void ParametrosMenu_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("100"))
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (100)!");
            else
            {
                Form formBackground = new Form();
                formBackground.StartPosition = FormStartPosition.Manual;
                //formBackground.FormBorderStyle = FormBorderStyle.None;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.Left = Top = 0;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = false;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                FrmParametroSistema fr = new FrmParametroSistema();
                fr.Owner = formBackground;
                fr.ShowDialog();
                formBackground.Dispose();
                fr.Dispose();
   
            }
        }

        private void btnDepartamentoFiscal_Click(object sender, EventArgs e)
        {
            dropMenuFiscal.Show(btnDepartamentoFiscal, btnDepartamentoFiscal.Width, 0);
        }

        private void ajustarImagemFundo()
        {
            if (File.Exists(Sessao.parametroSistema.Logo))
            {
                panelDesktop.BackColor = Color.White;
                var imagemOriginal = Image.FromFile(Sessao.parametroSistema.Logo);
                //var imagemRedimensionada = RedimensionarImagemPadrao(imagemOriginal);
                panelDesktop.BackgroundImageLayout = ImageLayout.Center; // Pode ser Center ou Zoom dependendo da necessidade
                panelDesktop.BackgroundImage = imagemOriginal;

                //panelDesktop.BackColor = Color.White;
                //panelDesktop.BackgroundImageLayout = ImageLayout.Center;
                //panelDesktop.BackgroundImage = Image.FromFile(Sessao.parametroSistema.Logo);
            }
        }
        private void btnContaReceberLista_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmContaReceberLista(), btnFinanceiro);
        }

        private void btnMonitoramentoFiscal_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmControleNotas(), btnDepartamentoFiscal);
        }

        private void btnEmitirNfe_Click(object sender, EventArgs e)
        {
            // OpenChildForm(() => new FrmNfe(), btnDepartamentoFiscal);
            Form formBackground = new Form();
            FrmNfe uu = new FrmNfe();
            formBackground.StartPosition = FormStartPosition.Manual;
            //formBackground.FormBorderStyle = FormBorderStyle.None;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            //formBackground.Left = Top = 0;
            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
            formBackground.WindowState = FormWindowState.Maximized;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();
            uu.Owner = formBackground;
            uu.ShowDialog();
            formBackground.Dispose();
            uu.Dispose();
        }

        private void btnNaturezaOperacao_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmNaturezaOperacaoLista(), btnDepartamentoFiscal);
        }

        private void btnComprasFiscal_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmManifesto(), btnDepartamentoFiscal);
        }

        private void btnOrdemServico_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmOrdemServicoLista(), btnServicos);
        }

        private void btnServico_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmServicoLista(), btnServicos);
        }

        private void btnRelatorios_Click(object sender, EventArgs e)
        {
            dropMenuRelatorios.Show(btnRelatorios, btnRelatorios.Width, 0);
        }

        private void caixaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmContaPagarLista(), btnFinanceiro);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //OpenChildForm(() => new FrmRelatorioCaixa(), btnRelatorios);
            if (Sessao.permissoes.Count > 0)
            {
                // Habilitar ou desabilitar os controles com base nas permissões
                if (Sessao.permissoes.Contains("200"))
                {
                    Form formBackground = new Form();
                    FrmRelatorioCaixa uu = new FrmRelatorioCaixa();
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    //formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    uu.ShowDialog();
                    formBackground.Dispose();
                    uu.Dispose();
                }
                else
                    GenericaDesktop.ShowAlerta("Usuário não possui permissão para visualizar esta tela! (200)");
            }
        }

        private void btnConsultaVendasRelatorio_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("71"))
            {
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (71)!");
            }
            else if (!Sessao.permissoes.Contains("72"))
            {
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (72)!");
            }
            else
                OpenChildForm(() => new FrmConsultaVendas(), btnRelatorios);
        }

        private void orçamentoAvulsoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            FrmOrcamentoAvulso uu = new FrmOrcamentoAvulso();
            formBackground.StartPosition = FormStartPosition.Manual;
            //formBackground.FormBorderStyle = FormBorderStyle.None;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            //formBackground.Left = Top = 0;
            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
            formBackground.WindowState = FormWindowState.Maximized;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();
            uu.Owner = formBackground;
            uu.ShowDialog();
            formBackground.Dispose();
            uu.Dispose();
            //FrmOrcamentoAvulso frmOrcamento = new FrmOrcamentoAvulso();
            //frmOrcamento.ShowDialog();
        }

        private void btnGerarInventario_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmGerarInventario(), btnRelatorios);
        }

        private void saldoDeEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {           //OpenChildForm(() => new FrmRelatorioCaixa(), btnRelatorios);
            if (Sessao.permissoes.Count > 0)
            {
                // Habilitar ou desabilitar os controles com base nas permissões
                if (Sessao.permissoes.Contains("301"))
                {
                    OpenChildForm(() => new FrmSaldoEstoque(), btnRelatorios);
                }
                else
                    GenericaDesktop.ShowAlerta("Usuário sem Permissão para este relatório! (301)");
            }
        }

        private void btnLembreteVencimento_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmEnviarLembreteVencimento(), btnFinanceiro);
        }

        private void btnBalancoEstoque_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("103"))
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (103)!");
            else
                OpenChildForm(() => new FrmBalancoEstoqueLista(), btnUtilitarios);
        }

        private void btnVendaPDV_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmVendas02(), btnMenuVenda);
        }

        private void btnCondicionalMenu_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmCondicionalLista(), btnMenuVenda);
        }

        private void enviarArquivosContabilidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("107"))
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (107)!");
            else
            {
                Form formBackground = new Form();
                FrmEnviarArquivosContabilidade uu = new FrmEnviarArquivosContabilidade();
                formBackground.StartPosition = FormStartPosition.Manual;
                //formBackground.FormBorderStyle = FormBorderStyle.None;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                //formBackground.Left = Top = 0;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = false;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                uu.Owner = formBackground;
                uu.ShowDialog();
                formBackground.Dispose();
                uu.Dispose();
            }
        }

        private void gerarSintegraTool_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("107"))
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (107)!");
            else
            {
                Form formBackground = new Form();
                formBackground.StartPosition = FormStartPosition.Manual;
                //formBackground.FormBorderStyle = FormBorderStyle.None;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.Left = Top = 0;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = false;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                FrmGerarSintegra fr = new FrmGerarSintegra();
                fr.Owner = formBackground;
                fr.ShowDialog();
                formBackground.Dispose();
                fr.Dispose();
            }
        }

        private void imprimirDuplicataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("108"))
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (108)!");
            else
            {
                Form formBackground = new Form();
                FrmImportarCSV uu = new FrmImportarCSV();
                formBackground.StartPosition = FormStartPosition.Manual;
                //formBackground.FormBorderStyle = FormBorderStyle.None;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                //formBackground.Left = Top = 0;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = false;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                uu.Owner = formBackground;
                uu.ShowDialog();
                formBackground.Dispose();
                uu.Dispose();
            }
        }

        private void btnMovimentoCaixa_Click(object sender, EventArgs e)
        {
            if (Sessao.permissoes.Count > 0)
            {
                // Habilitar ou desabilitar os controles com base nas permissões
                if (Sessao.permissoes.Contains("202"))
                {
                    OpenChildForm(() => new FrmMovimentoCaixa(), btnFinanceiro);
                }
                else
                    GenericaDesktop.ShowAlerta("Usuário não possui permissão para visualizar esta tela! (202)");
            }
        }

        private void btnComissoes_Click(object sender, EventArgs e)
        {           //OpenChildForm(() => new FrmRelatorioCaixa(), btnRelatorios);
            if (Sessao.permissoes.Count > 0)
            {
                // Habilitar ou desabilitar os controles com base nas permissões
                if (Sessao.permissoes.Contains("300"))
                {
                    DateTime dataInicial = DateTime.Now;
                    DateTime dataFinal = DateTime.Now;
                    Form formBackground = new Form();
                    using (FrmSelecionarData uu = new FrmSelecionarData("COMISSAO"))
                    {
                        formBackground.StartPosition = FormStartPosition.Manual;
                        //formBackground.FormBorderStyle = FormBorderStyle.None;
                        formBackground.Opacity = .50d;
                        formBackground.BackColor = Color.Black;
                        //formBackground.Left = Top = 0;
                        formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                        formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                        formBackground.WindowState = FormWindowState.Maximized;
                        formBackground.TopMost = false;
                        formBackground.Location = this.Location;
                        formBackground.ShowInTaskbar = false;
                        formBackground.Show();
                        uu.Owner = formBackground;
                        switch (uu.showModalNovo(ref dataInicial, ref dataFinal))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                break;
                            case DialogResult.OK:
                                try
                                {
                                    FrmComissaoRelatorio01 frmRelComissao = new FrmComissaoRelatorio01(dataInicial.ToString("yyyy-MM-dd"), dataFinal.ToString("yyyy-MM-dd"));
                                    frmRelComissao.ShowDialog();
                                }
                                catch
                                {
                                    GenericaDesktop.ShowErro("Data Inválida");
                                }
                                break;
                        }
                        formBackground.Dispose();
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Usuário sem Permissão para Visualizar Comissões! (300)");

            }
        }

        private void etiquetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmImprimirEtiquetasOtica frm = new FrmImprimirEtiquetasOtica();
            Form formBackground = new Form();
            FrmEtiquetas uu = new FrmEtiquetas();
            formBackground.StartPosition = FormStartPosition.Manual;
            //formBackground.FormBorderStyle = FormBorderStyle.None;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            //formBackground.Left = Top = 0;
            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
            formBackground.WindowState = FormWindowState.Maximized;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();
            uu.Owner = formBackground;
            uu.ShowDialog();
            formBackground.Dispose();
            uu.Dispose();
            //FrmEtiquetas frm = new FrmEtiquetas();
            //frm.ShowDialog();
        }

        private void transferênciaFilialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmTransferenciaEstoqueLista(), btnMenuVenda);
        }

        private void adiantamentoValeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmImprimirEtiquetasOtica frm = new FrmImprimirEtiquetasOtica();
            Form formBackground = new Form();
            FrmVale uu = new FrmVale();
            formBackground.StartPosition = FormStartPosition.Manual;
            //formBackground.FormBorderStyle = FormBorderStyle.None;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            //formBackground.Left = Top = 0;
            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
            formBackground.WindowState = FormWindowState.Maximized;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();
            uu.Owner = formBackground;
            uu.ShowDialog();
            formBackground.Dispose();
            uu.Dispose();
        }

        private void reciboAvulsoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            FrmReciboAvulso uu = new FrmReciboAvulso();
            formBackground.StartPosition = FormStartPosition.Manual;
            //formBackground.FormBorderStyle = FormBorderStyle.None;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            //formBackground.Left = Top = 0;
            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
            formBackground.WindowState = FormWindowState.Maximized;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();
            uu.Owner = formBackground;
            uu.ShowDialog();
            formBackground.Dispose();
            uu.Dispose();
        }

        private void notaFiscalAgrupadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            FrmNotaAgrupada uu = new FrmNotaAgrupada();
            formBackground.StartPosition = FormStartPosition.Manual;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
            formBackground.WindowState = FormWindowState.Maximized;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();
            uu.Owner = formBackground;
            uu.ShowDialog();
            formBackground.Dispose();
            uu.Dispose();
        }

        private void usuáriosPermissõesPorGrupoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("102"))
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (102)!");
            else
            {
                Form formBackground = new Form();
                formBackground.StartPosition = FormStartPosition.Manual;
                //formBackground.FormBorderStyle = FormBorderStyle.None;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.Left = Top = 0;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = false;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                FrmPermissoesUsuario fr = new FrmPermissoesUsuario();
                fr.Owner = formBackground;
                fr.ShowDialog();
                formBackground.Dispose();
                fr.Dispose();
            }
            //Se é o usuario do suporte, pode
            if (Sessao.usuarioLogado.Id == 1 && !Sessao.permissoes.Contains("102"))
            {
                Form formBackground = new Form();
                formBackground.StartPosition = FormStartPosition.Manual;
                //formBackground.FormBorderStyle = FormBorderStyle.None;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.Left = Top = 0;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = false;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                FrmPermissoesUsuario fr = new FrmPermissoesUsuario();
                fr.Owner = formBackground;
                fr.ShowDialog();
                formBackground.Dispose();
                fr.Dispose();
            }
        }

        private void mensagensAgendadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmMensagemPosVendas(), btnRelatorios);
        }

        private void integraçõesEcommerceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            FrmIntegracaoNuvemShop uu = new FrmIntegracaoNuvemShop();
            formBackground.StartPosition = FormStartPosition.Manual;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
            formBackground.WindowState = FormWindowState.Maximized;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();
            uu.Owner = formBackground;
            uu.ShowDialog();
            formBackground.Dispose();
            uu.Dispose();
        }

        private void foodServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmControleFood(), btnMenuVenda);
        }

        private void inutilizarNúmeraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmInutilizar uu = new FrmInutilizar())
                {
                    // Configuração do fundo semi-transparente
                    formBackground.StartPosition = FormStartPosition.Manual;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    uu.ShowDialog();

                    switch (uu.DialogResult)
                    {
                        case DialogResult.Ignore:
                            break;
                        case DialogResult.OK:
                            break;
                    }
                }
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void btnCadastroProduto_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmProdutoLista(), btnProduto);
        }

        private void btnGrupo_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmGrupoLista(), btnProduto);
        }

        private void btnSetor_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new FrmSetorLista(), btnProduto);
        }

        private void variaçõesCaracterísticasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmCadastroVariacoes uu = new FrmCadastroVariacoes())
                {
                    // Configuração do fundo semi-transparente
                    formBackground.StartPosition = FormStartPosition.Manual;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    uu.ShowDialog();

                    switch (uu.DialogResult)
                    {
                        case DialogResult.Ignore:
                            break;
                        case DialogResult.OK:
                            break;
                    }
                }
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void abrirCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("206"))
            {
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (206)!");
            }
            else
            {
                Form formBackground = new Form();
                formBackground.StartPosition = FormStartPosition.Manual;
                //formBackground.FormBorderStyle = FormBorderStyle.None;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.Left = Top = 0;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = false;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                FrmAbrirCaixa fr = new FrmAbrirCaixa();
                fr.Owner = formBackground;
                fr.ShowDialog();
                formBackground.Dispose();
                fr.Dispose();
                logarCaixa();
            }
        }

        private void vendasPorProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("71"))
            {
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (71)!");
            }
            else if (!Sessao.permissoes.Contains("72"))
            {
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (72)!");
            }
            else
                OpenChildForm(() => new FrmVendaProdutos(), btnRelatorios);
        }

        private void consultaVendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("71"))
            {
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (71)!");
            }
            else if (!Sessao.permissoes.Contains("72"))
            {
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (72)!");
            }
            else
                OpenChildForm(() => new FrmConsultaVendas(), btnMenuVenda);
        }

        private void aniversariantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            formBackground.StartPosition = FormStartPosition.Manual;
            //formBackground.FormBorderStyle = FormBorderStyle.None;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            formBackground.Left = Top = 0;
            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
            formBackground.WindowState = FormWindowState.Maximized;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();
            FrmAniversariantes fr = new FrmAniversariantes();
            fr.Owner = formBackground;
            fr.ShowDialog();
            formBackground.Dispose();
            fr.Dispose();
        }

        private void vendasEOrdensDeServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("71"))
            {
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (71)!");
            }
            else if (!Sessao.permissoes.Contains("72"))
            {
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (72)!");
            }
            else
                OpenChildForm(() => new FrmVendaEOrdemPorPeriodo(), btnRelatorios);
        }

        private void saldoContasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.permissoes.Contains("207"))
            {
                GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (207)!");
            }
            else
            {
                Form formBackground = new Form();
                formBackground.StartPosition = FormStartPosition.Manual;
                //formBackground.FormBorderStyle = FormBorderStyle.None;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.Left = Top = 0;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = false;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                FrmSaldoContas fr = new FrmSaldoContas();
                fr.Owner = formBackground;
                fr.ShowDialog();
                formBackground.Dispose();
                fr.Dispose();
            }
        }

        private void reativaçãoDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            formBackground.StartPosition = FormStartPosition.Manual;
            //formBackground.FormBorderStyle = FormBorderStyle.None;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            formBackground.Left = Top = 0;
            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
            formBackground.WindowState = FormWindowState.Maximized;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();
            FrmRelatorioReativarClientes fr = new FrmRelatorioReativarClientes();
            fr.Owner = formBackground;
            fr.ShowDialog();
            formBackground.Dispose();
            fr.Dispose();
        }

        private void aniversariantesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            formBackground.StartPosition = FormStartPosition.Manual;
            //formBackground.FormBorderStyle = FormBorderStyle.None;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            formBackground.Left = Top = 0;
            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
            formBackground.WindowState = FormWindowState.Maximized;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();
            FrmAniversariantes fr = new FrmAniversariantes();
            fr.Owner = formBackground;
            fr.ShowDialog();
            formBackground.Dispose();
            fr.Dispose();
        }

        private void enviarProdutosBalançaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            formBackground.StartPosition = FormStartPosition.Manual;
            //formBackground.FormBorderStyle = FormBorderStyle.None;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            formBackground.Left = Top = 0;
            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
            formBackground.WindowState = FormWindowState.Maximized;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();
            FrmCargaBalanca fr = new FrmCargaBalanca();
            fr.Owner = formBackground;
            fr.ShowDialog();
            formBackground.Dispose();
            fr.Dispose();
        }
    }
}
