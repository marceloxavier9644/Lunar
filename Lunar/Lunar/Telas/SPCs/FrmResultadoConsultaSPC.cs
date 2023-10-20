using Lunar.consultaSPCBrasil;
using Lunar.Utils;
using Lunar.WSCorreios;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.SPCs
{
    public partial class FrmResultadoConsultaSPC : Form
    {
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        ResultadoConsulta resultadoConsultaSpc = new ResultadoConsulta();
        public FrmResultadoConsultaSPC(ResultadoConsulta resultadoConsultaSpc)
        {
            InitializeComponent();
            this.resultadoConsultaSpc = resultadoConsultaSpc;
            apresentarResultado();
        }

        private void apresentarResultado()
        {
            if (resultadoConsultaSpc.consumidor.consumidorpessoafisica != null)
            {
                lblCPF.Text = "CPF: " + genericaDesktop.FormatarCPF(resultadoConsultaSpc.consumidor.consumidorpessoafisica.cpf.numero);
                lblNome.Text = "NOME: " + resultadoConsultaSpc.consumidor.consumidorpessoafisica.nome;
                lblEndereco.Text = "ENDEREÇO: " + resultadoConsultaSpc.consumidor.consumidorpessoafisica.endereco.logradouro + ", " +
                    resultadoConsultaSpc.consumidor.consumidorpessoafisica.endereco.numero + " - " +
                    resultadoConsultaSpc.consumidor.consumidorpessoafisica.endereco.complemento + " " +
                    resultadoConsultaSpc.consumidor.consumidorpessoafisica.endereco.bairro;
                lblCidade.Text = "CIDADE: " +
                    resultadoConsultaSpc.consumidor.consumidorpessoafisica.endereco.cidade.nome + "-" +
                    resultadoConsultaSpc.consumidor.consumidorpessoafisica.endereco.cidade.estado.siglauf;
            }
            else if (resultadoConsultaSpc.consumidor.consumidorpessoajuridica != null)
            {
                lblCPF.Text = "CNPJ: " + genericaDesktop.FormatarCNPJ(resultadoConsultaSpc.consumidor.consumidorpessoajuridica.cnpj.numero);
                lblNome.Text = "NOME: " + resultadoConsultaSpc.consumidor.consumidorpessoajuridica.razaosocial + " (" + resultadoConsultaSpc.consumidor.consumidorpessoajuridica.nomecomercial + ")";
                lblEndereco.Text = "ENDEREÇO: " + resultadoConsultaSpc.consumidor.consumidorpessoajuridica.endereco.logradouro + ", " +
                    resultadoConsultaSpc.consumidor.consumidorpessoajuridica.endereco.numero + " - " +
                    resultadoConsultaSpc.consumidor.consumidorpessoajuridica.endereco.complemento + " " +
                    resultadoConsultaSpc.consumidor.consumidorpessoajuridica.endereco.bairro;
                lblCidade.Text = "CIDADE: " +
                    resultadoConsultaSpc.consumidor.consumidorpessoajuridica.endereco.cidade.nome + "-" +
                    resultadoConsultaSpc.consumidor.consumidorpessoajuridica.endereco.cidade.estado.siglauf;
            }

            //Consultas Realizadas
            if (resultadoConsultaSpc.consultarealizada != null)
            {
                if (resultadoConsultaSpc.consultarealizada.resumo != null)
                {
                    if (resultadoConsultaSpc.consultarealizada.resumo.quantidadetotal > 0)
                    {
                        lblQtdConsultaRealizada.Text = resultadoConsultaSpc.consultarealizada.resumo.quantidadetotal.ToString();
                        lblDataUltimaConsulta.Text = resultadoConsultaSpc.consultarealizada.resumo.dataultimaocorrencia.ToShortDateString();
                        if (resultadoConsultaSpc.consultarealizada.resumo.valorultimaocorrencia.ToString("C") != "R$ 0,00")
                            lblValorConsultaRealizada.Text = resultadoConsultaSpc.consultarealizada.resumo.valorultimaocorrencia.ToString("C");

                        IList<ConsultaRealizadaSpc> listaConsultasRealizadas = new List<ConsultaRealizadaSpc>();
                        lblConsultasRealizadasTitulo.Text = "Consultas Realizadas (" + resultadoConsultaSpc.consultarealizada.Items.Length + ")";
                        foreach (object item in resultadoConsultaSpc.consultarealizada.Items)
                        {
                            if (item is InsumoConsultaRealizada insumoConsultaRealizada)
                            {
                                ConsultaRealizadaSpc consultaRealizadaSpc = new ConsultaRealizadaSpc();
                                consultaRealizadaSpc.Data = insumoConsultaRealizada.dataconsulta;
                                consultaRealizadaSpc.CidadeOrigem = insumoConsultaRealizada.origemassociado.nome + "-" + insumoConsultaRealizada.origemassociado.estado.siglauf;
                                consultaRealizadaSpc.Associado = insumoConsultaRealizada.nomeassociado;
                                consultaRealizadaSpc.Origem = insumoConsultaRealizada.nomeentidadeorigem;
                                listaConsultasRealizadas.Add(consultaRealizadaSpc);
                            }
                        }
                        gridConsultasRealizadas.DataSource = listaConsultasRealizadas;
                    }
                }
            }
            //SPC
            if (resultadoConsultaSpc.spc != null)
            {
                if (resultadoConsultaSpc.spc.resumo != null)
                {
                    if (resultadoConsultaSpc.spc.resumo.quantidadetotal > 0)
                    {
                        lblRegistrosSPCTitulo.Visible = true;
                        gridRegistrosSPC.Visible = true;
                        lblRegistrosSPCTitulo.Text = lblRegistrosSPCTitulo.Text + " (" + resultadoConsultaSpc.spc.resumo.quantidadetotal + ")";
                        if (resultadoConsultaSpc.spc.resumo.dataultimaocorrencia.ToShortDateString() != "01/01/0001")
                            lblDataUltimaOcorrenciaSPC.Text = resultadoConsultaSpc.spc.resumo.dataultimaocorrencia.ToShortDateString();
                        if (resultadoConsultaSpc.spc.resumo.valorultimaocorrencia > 0)
                            lblValorUltimoRegistroSPC.Text = resultadoConsultaSpc.spc.resumo.valorultimaocorrencia.ToString("C");
                        lblQtdRegistroSPC.Text = resultadoConsultaSpc.spc.resumo.quantidadetotal.ToString();
                        lblQtdRegistroSPC.ForeColor = Color.Red;
                        gridRegistrosSPC.DataSource = resultadoConsultaSpc.spc.Items;
                        try
                        {
                            Pessoa pessoa = new Pessoa();
                            PessoaController pessoaController = new PessoaController();
                            pessoa = pessoaController.selecionarPessoaPorCPFCNPJ(GenericaDesktop.RemoveCaracteres(resultadoConsultaSpc.consumidor.consumidorpessoafisica.cpf.numero));
                            if (pessoa != null)
                            {
                                foreach (object item in resultadoConsultaSpc.spc.Items)
                                {
                                    if (item is InsumoSPC insumoSpc)
                                    {
                                        Spc spc = new Spc();
                                        spc.NomeUsuario = Sessao.usuarioLogado.Login;
                                        spc.DataRegistro = insumoSpc.datainclusao;
                                        spc.DataConsulta = DateTime.Now;
                                        spc.EmpresaFilial = Sessao.empresaFilialLogada;
                                        spc.LocalRegistro = insumoSpc.nomeassociado;
                                        spc.LoginWebService = Sessao.parametroSistema.UsuarioWebServiceSpcBrasil;
                                        spc.Pessoa = pessoa;
                                        spc.QuantidadeRegistro = resultadoConsultaSpc.spc.resumo.quantidadetotal;
                                        spc.ValorRegistro = insumoSpc.valor;
                                        spc.ProtocoloConsulta = resultadoConsultaSpc.protocolo.numero.ToString() +"-"+resultadoConsultaSpc.protocolo.digito.ToString();
                                        Controller.getInstance().salvar(spc);
                                    }
                                }
                            }
                        }
                        catch
                        {
                            //Apenas nao armazena os registros no banco de dados!
                        }
                    }
                }
            }
            //Pendencia Serasa
            if (resultadoConsultaSpc.pendenciafinanceira != null)
            {
                if (resultadoConsultaSpc.pendenciafinanceira.resumo != null)
                {
                    if(resultadoConsultaSpc.pendenciafinanceira.resumo.quantidadetotal > 0)    
                    {
                        lblQtdPendenciaFinanceira.Text = resultadoConsultaSpc.pendenciafinanceira.resumo.quantidadetotal.ToString();
                        lblDataUltimaOcorrenciaPendenciaFinanceira.Text = resultadoConsultaSpc.pendenciafinanceira.resumo.dataultimaocorrencia.ToShortDateString();
                        if (resultadoConsultaSpc.pendenciafinanceira.resumo.valorultimaocorrencia.ToString("C") != "R$ 0,00")
                            lblValorPendenciaFinanceira.Text = resultadoConsultaSpc.pendenciafinanceira.resumo.valorultimaocorrencia.ToString("C"); 
                    }
                }
            }
            //Cheque Lojista
            if (resultadoConsultaSpc.chequelojista != null)
            {
                if (resultadoConsultaSpc.chequelojista.resumo != null)
                {
                    if (resultadoConsultaSpc.chequelojista.resumo.quantidadetotal > 0)
                    {
                        lblQtdRegistroChequeLojista.Text = resultadoConsultaSpc.chequelojista.resumo.quantidadetotal.ToString();
                        lblDataUltimaOcorrenciaChequeLojista.Text = resultadoConsultaSpc.chequelojista.resumo.dataultimaocorrencia.ToShortDateString();
                        if (resultadoConsultaSpc.chequelojista.resumo.valorultimaocorrencia.ToString("C") != "R$ 0,00")
                            lblValorRegistroChequeLojista.Text = resultadoConsultaSpc.chequelojista.resumo.valorultimaocorrencia.ToString("C");
                    }
                }
            }
            //Cheque Varejo
            if (resultadoConsultaSpc.chequesemfundovarejo != null)
            {
                if (resultadoConsultaSpc.chequesemfundovarejo.resumo != null)
                {
                    if (resultadoConsultaSpc.chequesemfundovarejo.resumo.quantidadetotal > 0)
                    {
                        lblQtdChequeSemFundoVarejo.Text = resultadoConsultaSpc.chequesemfundovarejo.resumo.quantidadetotal.ToString();
                        lblDataUltimaOcorrenciaChequeSemFundoVarejo.Text = resultadoConsultaSpc.chequesemfundovarejo.resumo.dataultimaocorrencia.ToShortDateString();
                        if (resultadoConsultaSpc.chequesemfundovarejo.resumo.valorultimaocorrencia.ToString("C") != "R$ 0,00")
                            lblValorChequeSemFundoVarejo.Text = resultadoConsultaSpc.chequesemfundovarejo.resumo.valorultimaocorrencia.ToString("C");
                    }
                }
            }
            //Informacoes Pode Judiciario
            if (resultadoConsultaSpc.informacaopoderjudiciario != null)
            {
                if (resultadoConsultaSpc.informacaopoderjudiciario.resumo != null)
                {
                    if (resultadoConsultaSpc.informacaopoderjudiciario.resumo.quantidadetotal > 0)
                    {
                        lblQtdInformacoesPodeJudiciario.Text = resultadoConsultaSpc.informacaopoderjudiciario.resumo.quantidadetotal.ToString();
                        lblDataUltimaOcorrenciaInformacoesPodeJudiciario.Text = resultadoConsultaSpc.informacaopoderjudiciario.resumo.dataultimaocorrencia.ToShortDateString();
                        if (resultadoConsultaSpc.informacaopoderjudiciario.resumo.valorultimaocorrencia.ToString("C") != "R$ 0,00")
                            lblValorInformacoesPodeJudiciario.Text = resultadoConsultaSpc.informacaopoderjudiciario.resumo.valorultimaocorrencia.ToString("C");
                    }
                }
            }
            //Alerta de Documentos
            if (resultadoConsultaSpc.alertadocumento != null)
            {
                if (resultadoConsultaSpc.alertadocumento.resumo != null)
                {
                    if (resultadoConsultaSpc.alertadocumento.resumo.quantidadetotal > 0)
                    {
                        lblQtdAlertaDocumentos.Text = resultadoConsultaSpc.alertadocumento.resumo.quantidadetotal.ToString();
                        lblDataUltimaOcorrenciaAlertaDocumentos.Text = resultadoConsultaSpc.alertadocumento.resumo.dataultimaocorrencia.ToShortDateString();
                        if (resultadoConsultaSpc.alertadocumento.resumo.valorultimaocorrencia.ToString("C") != "R$ 0,00")
                            lblValorAlertaDocumentos.Text = resultadoConsultaSpc.alertadocumento.resumo.valorultimaocorrencia.ToString("C");
                    }
                }
            }
            //Alerta de Identidade
            if (resultadoConsultaSpc.alertaidentidade != null)
            {
                if (resultadoConsultaSpc.alertaidentidade.resumo != null)
                {
                    if (resultadoConsultaSpc.alertaidentidade.resumo.quantidadetotal > 0)
                    {
                        lblQtdAlertaIdentidade.Text = resultadoConsultaSpc.alertaidentidade.resumo.quantidadetotal.ToString();
                        lblDataUltimaOcorrenciaAlertaIdentidade.Text = resultadoConsultaSpc.alertaidentidade.resumo.dataultimaocorrencia.ToShortDateString();
                        if (resultadoConsultaSpc.alertaidentidade.resumo.valorultimaocorrencia.ToString("C") != "R$ 0,00")
                            lblValorAlertaIdentidade.Text = resultadoConsultaSpc.alertaidentidade.resumo.valorultimaocorrencia.ToString("C");
                    }
                }
            }
            //Credito Concedido
            if (resultadoConsultaSpc.creditoconcedido != null)
            {
                if (resultadoConsultaSpc.creditoconcedido.resumo != null)
                {
                    if (resultadoConsultaSpc.creditoconcedido.resumo.quantidadetotal > 0)
                    {
                        lblQtdCreditoConcedido.Text = resultadoConsultaSpc.creditoconcedido.resumo.quantidadetotal.ToString();
                        lblDataUltimaOcorrenciaCredidoConcedido.Text = resultadoConsultaSpc.creditoconcedido.resumo.dataultimaocorrencia.ToShortDateString();
                        if (resultadoConsultaSpc.creditoconcedido.resumo.valorultimaocorrencia.ToString("C") != "R$ 0,00")
                            lblValorCreditoConcedido.Text = resultadoConsultaSpc.creditoconcedido.resumo.valorultimaocorrencia.ToString("C");
                    }
                }
            }

         
          

        }

        public class ConsultaRealizadaSpc 
        {
            private DateTime data;
            private String associado;
            private string cidadeOrigem;
            private string origem;
            public DateTime Data
            {
                get { return data; }
                set { data = value; }
            }

            public string Associado
            {
                get { return associado; }
                set { associado = value; }
            }

            public string CidadeOrigem
            {
                get { return cidadeOrigem; }
                set { cidadeOrigem = value; }
            }

            public string Origem
            {
                get { return origem; }
                set { origem = value; }
            }
        }

        private void gridConsultasRealizadas_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void gridRegistrosSPC_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }
    }
}
