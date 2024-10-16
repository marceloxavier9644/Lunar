using Lunar.Telas.Principal;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Lunar.Telas.Login
{
    public partial class WelcomeForm : Form
    {
        bool carregarProgressBarLentamente = false;
        Generica generica = new Generica();
        EstadoController estadoController = new EstadoController();
        CidadeController cidadeController = new CidadeController();
        ValoresPadraoBO valoresPadraoBO = new ValoresPadraoBO();
        Thread th;
        ParametroSistema parametroSistema = new ParametroSistema();
        public WelcomeForm(string nomeUsuario)
        {
            InitializeComponent();
            this.Opacity = 0.0;
            circularProgressBar1.Value = 0;
            circularProgressBar1.Minimum = 0;
            circularProgressBar1.Maximum = 100;

            lblUserName.Text = nomeUsuario;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void cadastrarEstadoCidades()
        {
            try
            {
              
                Estado est = new Estado();
                IList<ObjetoPadrao> lisEst = estadoController.selecionarTodos(est);

                int i = 0;

                if (lisEst.Count == 0)
                {
                    lblStatus.Visible = true;
                    circularProgressBar1.Visible = true;
                    var listaEstados = Estados.BuscarEstados();
                    foreach (Estados estados in listaEstados)
                    {
                        //btnLogin.Enabled = false;
                        lblStatus.Text = "Cadastro de estados: " + generica.RemoverAcentos(estados.Nome);
                        i++;
                        backgroundWorker1.ReportProgress(1);
                        Estado estado = new Estado();
                        estado.Descricao = generica.RemoverAcentos(estados.Nome.ToUpper());
                        estado.Uf = estados.Sigla.ToUpper();
                        estado.Ibge = estados.Id.ToString();
                        Controller.getInstance().salvar(estado);
                    }
                }
                Cidade cid = new Cidade();
                IList<ObjetoPadrao> listCid = cidadeController.selecionarTodos(cid);
                if (listCid.Count == 0)
                {
                    lblStatus.Visible = true;
                    circularProgressBar1.Visible = true;
                    var listaMunicipios = Municipios.BuscarMunicipios();
                    foreach (Municipios municipio in listaMunicipios)
                    {
                        lblStatus.Text = "Cadastro de cidades: " + generica.RemoverAcentos(municipio.Nome);
                        i++;
                        if (i >= 54)
                            backgroundWorker1.ReportProgress(i / 56);
                        Cidade cidade = new Cidade();
                        cidade.Descricao = generica.RemoverAcentos(municipio.Nome.ToUpper());
                        cidade.Estado = estadoController.selecionarEstadoPorUF(municipio.Microrregiao.Mesorregiao.Uf.Sigla);
                        cidade.Ibge = municipio.Id.ToString();
                        Controller.getInstance().salvar(cidade);
                    }
                }
                else
                {
                    carregarProgressBarLentamente = true;
                }
                //Se ja tem os cadastros no banco apenas gira o tempo para chegar nos 100%
                lblStatus.Text = "Verificando dados...";
                lblStatus.Visible = false;
                Conexao.FechaConexaoBD();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.0;
            circularProgressBar1.Value = 0;
            circularProgressBar1.Minimum = 0;
            circularProgressBar1.Maximum = 100;
            timer1.Start();
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
            else
                MessageBox.Show("Erro na atualização (E445564)");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.20;
            lblStatus.Visible = true;
            //lblStatus.Text = "Aguarde...";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.1;
           
            if (this.Opacity == 0)
            {
                timer2.Stop();
                this.Close();
                th = new Thread(opennewform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        private void WelcomeForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Gray), 0, 0, this.Width - 1, this.Height - 1);
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //cadastrarEstadoCidades();
            //valoresPadraoBO.gerarValoresPadrao();
            //cadastrarNCM();
            //cadastrarCEST();
            //cadastrarCFOP();
            //cadastrarCstPisCofins();
            //cadastrarCstIPI();
            //cadastrarANP();
            //cadastrarBancos();

            //Selecionar o Parametro padrão e coloca em Sessão
            try
            {
                parametroSistema.Id = 1;
                parametroSistema = (ParametroSistema)Controller.getInstance().selecionar(parametroSistema);
                Sessao.parametroSistema = parametroSistema;
            }
            catch(Exception erro)
            {
                GenericaDesktop.ShowErro("Erro ao selecionar parâmetro do sistema " + erro.Message);
            }
        }

        private void abrirTelaPrincipal()
        {
            Form mainForm;
            mainForm = new FrmPrincipal();
            this.Hide();

            mainForm.FormClosed += new FormClosedEventHandler(MainForm_SessionClosed);//Associate the closed event, to clean the login form when the session is closed from the main form.
            mainForm.Show();//Show the main form.
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            circularProgressBar1.Value = e.ProgressPercentage;
            circularProgressBar1.Text = circularProgressBar1.Value.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            timer1.Stop();
            if (carregarProgressBarLentamente == false)
                timer2.Start();
            else
                timer3.Start();
        }

        private void MainForm_SessionClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (circularProgressBar1.Value < 99)
                circularProgressBar1.Value += 2;
            else
                circularProgressBar1.Value = 100;
            circularProgressBar1.Text = circularProgressBar1.Value.ToString();
            if (circularProgressBar1.Value == 100)
            {
                timer3.Stop();
                timer4.Start();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.1;
            if (this.Opacity == 0)
            {
                timer4.Stop();
                this.Close();
                th = new Thread(opennewform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }
        private void opennewform()
        {
            Application.Run(new FrmPrincipal());
        }

        private void cadastrarNCM()
        {
            try
            {
                NcmController ncmController = new NcmController();
                IList<Ncm> listaNCMCadastrado = ncmController.selecionarTodosNCM();
                if (listaNCMCadastrado.Count < 50)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    //List<Ncm_Json.Rootobject> ListaNCM = null;
                    var ListaNCM = generica.consultaNCM();
                    for (int i = 0; i < ListaNCM.Nomenclaturas.Length; i++)
                    {
                        Ncm ncm = new Ncm();

                        ncm.Id = 0;
                        ncm.NumeroNcm = GenericaDesktop.RemoveCaracteres(ListaNCM.Nomenclaturas[i].Codigo.ToString()).Trim();
                        ncm.DescricaoNcm = ListaNCM.Nomenclaturas[i].Descricao.ToUpper();
                        ncmController.salvar(ncm);

                        lblStatus.Text = "Cadastro de NCM: " + ncm.NumeroNcm;
                        int cal = i * 100 / ListaNCM.Nomenclaturas.Length;
                        if (cal <= 100)
                            backgroundWorker1.ReportProgress(cal);
                        else
                        {
                            circularProgressBar1.Value = 100;
                            backgroundWorker1.ReportProgress(100);
                        }
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarCEST()
        {
            try
            {
                CestController cestController = new CestController();
                IList<Cest> listaCESTCadastrado = cestController.selecionarTodosCest();
                if (listaCESTCadastrado.Count < 50)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    IList<Cest_Json.TabelaCest> ListaCEST = generica.consultaCEST();
                    int i = 0;
                    foreach (Cest_Json.TabelaCest cestSel in ListaCEST)
                    {
                        Cest cest = new Cest();

                        cest.Id = 0;
                        cest.NumeroCest = GenericaDesktop.RemoveCaracteres(cestSel.CEST.ToString()).Trim();
                        cest.DescricaoCest = cestSel.DESCRICAO.ToUpper();
                        cest.Item = cestSel.ITEM.ToString();
                        //String ncm = cestSel.NCM.ToString().Substring("", "ou"));
                        try { cest.Ncm = GenericaDesktop.RemoveCaracteres(cestSel.NCM.ToString()); } catch { }
                        cest.Segmento = cestSel.SEGMENTO.Trim();
                        cestController.salvar(cest);

                        lblStatus.Text = "Cadastro de CEST: " + cest.NumeroCest;
                        int cal = i * 100 / ListaCEST.Count;
                        if (cal <= 100)
                            backgroundWorker1.ReportProgress(cal);
                        else
                        {
                            circularProgressBar1.Value = 100;
                            backgroundWorker1.ReportProgress(100);
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarOrigemICMS()
        {
            try
            {
                OrigemIcmsController origemIcmsController = new OrigemIcmsController();
   
                IList<OrigemIcms> listaOrigemIcmsCadastrado = origemIcmsController.selecionarTodasOrigemIcms();
                if (listaOrigemIcmsCadastrado.Count < 7)
                {
                    lblStatus.Text = "Cadastro de Origem";
                    valoresPadraoBO.gerarOrigemIcmsPadrao();
                    circularProgressBar1.Value = 100;
                    backgroundWorker1.ReportProgress(100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cadastrarCSTICMS()
        {
            try
            {
                CstIcmsController cstIcmsController = new CstIcmsController();
                IList<CstIcms> listaCstIcmsCadastrado = cstIcmsController.selecionarTodosCstIcms();
                if (listaCstIcmsCadastrado.Count < 2)
                {
                    lblStatus.Text = "Cadastro de CST ICMS";
                    valoresPadraoBO.gerarCstIcmsPadrao();
                    circularProgressBar1.Value = 100;
                    backgroundWorker1.ReportProgress(100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarCSOSN()
        {
            try
            {
                CsosnController csosnController = new CsosnController();
                IList<Csosn> listaCsosnCadastrado = csosnController.selecionarTodosCSOSN();
                if (listaCsosnCadastrado.Count < 2)
                {
                    lblStatus.Text = "Cadastro de Csosn";
                    valoresPadraoBO.gerarCSOSNPadrao();
                    circularProgressBar1.Value = 100;
                    backgroundWorker1.ReportProgress(100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarCFOP()
        {
            try
            {
                CfopController cfopController = new CfopController();
                IList<Cfop> listaCFOPCadastrado = cfopController.selecionarTodosCfop();
                if (listaCFOPCadastrado.Count < 50)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    IList<CfopAux.Class1> listaCFOP = generica.consultarCFOP_JSON();
                    int i = 0;
                    foreach (CfopAux.Class1 cfopSel in listaCFOP)
                    {
                        Cfop cfop = new Cfop();

                        cfop.Id = 0;
                        cfop.CfopNumero = GenericaDesktop.RemoveCaracteres(cfopSel.CFOP.ToString()).Trim();
                        cfop.Descricao = cfopSel.Descrição.ToUpper();
                        IList<Cfop> listaCFOP2 = cfopController.selecionarCfopPorCfop(cfop.CfopNumero);
                        if (listaCFOP2.Count < 1)
                            cfopController.salvar(cfop);

                        lblStatus.Text = "Cadastro de CFOP: " + cfop.CfopNumero;
                        int cal = i * 100 / listaCFOP.Count;
                        if (cal <= 100)
                            backgroundWorker1.ReportProgress(cal);
                        else
                        {
                            circularProgressBar1.Value = 100;
                            backgroundWorker1.ReportProgress(100);
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarCstPisCofins()
        {
            try
            {
                CstPisCofinsController cstPisCofinsController = new CstPisCofinsController();
                IList<CstPisCofins> listaCstCadastrado = cstPisCofinsController.selecionarTodosCST();
                if (listaCstCadastrado.Count < 10)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    var listaCstA = generica.consultarCSTPISCOFINS_JSON();
                    for (int i = 0; i < listaCstA.CSTPISCOFINS.Length; i++)
                    {
                        CstPisCofins cstPisCofins = new CstPisCofins();
                        cstPisCofins.Id = 0;
                        cstPisCofins.Cst = listaCstA.CSTPISCOFINS[i].Código.ToString();
                        cstPisCofins.Descricao = listaCstA.CSTPISCOFINS[i].Descrição.ToUpper();
                        IList<CstPisCofins> listaCST2 = cstPisCofinsController.selecionarCstPorCst(cstPisCofins.Cst);
                        if (listaCST2.Count < 1)
                            cstPisCofinsController.salvar(cstPisCofins);

                        lblStatus.Text = "Cadastro de CST Pis/Cofins: " + cstPisCofins.Cst;
                        int cal = i * 100 / listaCstA.CSTPISCOFINS.Length;
                        if (cal <= 100)
                            backgroundWorker1.ReportProgress(cal);
                        else
                        {
                            circularProgressBar1.Value = 100;
                            backgroundWorker1.ReportProgress(100);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarCstIPI()
        {
            try
            {
                CstIpiController cstIpiController = new CstIpiController();
                IList<CstIpi> listaCstCadastrado = cstIpiController.selecionarTodosCST();
                if (listaCstCadastrado.Count < 10)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    IList<CstAuxIPI.CSTIPIAUX> listaCstA = generica.consultarCSTIPI_JSON();
                    int i = 0;
                    foreach (CstAuxIPI.CSTIPIAUX cstSel in listaCstA)
                    {
                        CstIpi cstIPI = new CstIpi();

                        cstIPI.Id = 0;
                        cstIPI.Cst = GenericaDesktop.RemoveCaracteres(cstSel.CST.ToString()).Trim();
                        cstIPI.Descricao = cstSel.DESCRICAO.ToUpper();
                        IList<CstIpi> listaCST2 = cstIpiController.selecionarCstPorCst(cstIPI.Cst);
                        if (listaCST2.Count < 1)
                            cstIpiController.salvar(cstIPI);

                        lblStatus.Text = "Cadastro de CST IPI: " + cstIPI.Cst;
                        int cal = i * 100 / listaCstA.Count;
                        if (cal <= 100)
                            backgroundWorker1.ReportProgress(cal);
                        else
                        {
                            circularProgressBar1.Value = 100;
                            backgroundWorker1.ReportProgress(100);
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarANP()
        {
            try
            {
                AnpController anpController = new AnpController();
                IList<Anp> listaAnpCadastrado = anpController.selecionarTodosCodigosANP();
                if (listaAnpCadastrado.Count < 10)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    IList <AnpAux.AnpAuxiliar> listaCstA = generica.consultarANP_JSON();
                    int i = 0;
                    foreach (AnpAux.AnpAuxiliar anpSel in listaCstA)
                    {
                        Anp anp = new Anp();

                        anp.Id = 0;
                        anp.Codigo = GenericaDesktop.RemoveCaracteres(anpSel.Código.ToString()).Trim();
                        anp.Descricao = anpSel.Produto.ToUpper();
                        IList<Anp> listaANP2 = anpController.selecionarCodigoAnpPorCodigo(anp.Codigo);
                        if (listaANP2.Count < 1)
                            anpController.salvar(anp);

                        lblStatus.Text = "Cadastro de Código ANP: " + anp.Codigo;
                        int cal = i * 100 / listaCstA.Count;
                        if (cal <= 100)
                            backgroundWorker1.ReportProgress(cal);
                        else
                        {
                            circularProgressBar1.Value = 100;
                            backgroundWorker1.ReportProgress(100);
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarBancos()
        {
            try
            {
                BancoController bancoController = new BancoController();
                IList<Banco> listaBancos = bancoController.selecionarTodosBancos();
                if (listaBancos.Count < 2)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    var bancoJson = generica.consultaBancos();
                    for (int i = 0; i < bancoJson.Bancos.Length; i++)
                    {
                        Banco banco = new Banco();

                        banco.Id = 0;
                        banco.Descricao = bancoJson.Bancos[i].Descricao.ToUpper();
                        banco.CodBanco = bancoJson.Bancos[i].CodBanco.ToString();
                        banco.CodIspb = bancoJson.Bancos[i].CodIspb.ToString();
                        bancoController.salvar(banco);

                        lblStatus.Text = "Cadastro de Bancos: " + banco.Descricao;
                        int cal = i * 100 / bancoJson.Bancos.Length;
                        if (cal <= 100)
                            backgroundWorker1.ReportProgress(cal);
                        else
                        {
                            circularProgressBar1.Value = 100;
                            backgroundWorker1.ReportProgress(100);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
