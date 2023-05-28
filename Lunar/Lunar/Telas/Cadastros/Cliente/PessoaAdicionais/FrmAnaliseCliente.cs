using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    public partial class FrmAnaliseCliente : Form
    {
        Pessoa pessoa = new Pessoa();
        VendaController vendaController = new VendaController();
        ContaReceberController contaReceberController = new ContaReceberController();
        OrdemServicoController ordemServicoController = new OrdemServicoController();
        CondicionalController condicionalController = new CondicionalController();
        CondicionalProdutoController condicionalProdutoController = new CondicionalProdutoController();
        public FrmAnaliseCliente(Pessoa pessoa)
        {
            InitializeComponent();
            this.pessoa = pessoa;
            analisarCliente();
            txtDataPrimeiraCompra.TextAlign = HorizontalAlignment.Center;
            txtCPF.TextAlign = HorizontalAlignment.Center;
            txtQuantidadeCondicional.TextAlign = HorizontalAlignment.Center;
            txtQuantidadeOrdemServico.TextAlign = HorizontalAlignment.Center;
            txtQuantidadePecasDevolvidas.TextAlign = HorizontalAlignment.Center;
            txtQuantidadePecasLevadas.TextAlign = HorizontalAlignment.Center;
            txtTotalAReceber.TextAlign = HorizontalAlignment.Center;
            txtTotalGeral.TextAlign = HorizontalAlignment.Center;
            txtTotalPago.TextAlign = HorizontalAlignment.Center;
            txtValorCondicionalLevado.TextAlign = HorizontalAlignment.Center;
            txtValorOrdemServicoAberta.TextAlign = HorizontalAlignment.Center;
            txtValorOrdemServicoEncerrada.TextAlign = HorizontalAlignment.Center;
            txtValorTotalOrdemServico.TextAlign = HorizontalAlignment.Center;
        }
        private void analisarCliente()
        {
            DateTime primeiraCompra = DateTime.Parse("1900-01-01 00:00:00");
            txtCliente.Texts = pessoa.RazaoSocial;
            txtCodCliente.Texts = pessoa.Id.ToString();
            txtCPF.Texts = pessoa.Cnpj;
            //Venda
            IList<Venda> listaVenda = vendaController.selecionarVendaPorSql("From Pessoa as Tabela where Tabela.FlagExcluido <> true and Tabela.Cliente = " + pessoa.Id.ToString());
            decimal valorTotalVendas = 0;
            int qtdVenda = 0;
            if (listaVenda.Count > 0)
            {
                foreach (Venda venda in listaVenda)
                {
                    valorTotalVendas = valorTotalVendas + venda.ValorFinal;
                    primeiraCompra = venda.DataVenda;
                    qtdVenda++;
                }
            }

            //Condicional 
            decimal valorTotalCondicional = 0;
            decimal valorTotalCondicionalAberto = 0;
            decimal valorTotalCondicionalEncerrada = 0;
            double quantidadePecasLevadas = 0;
            double quantidadePecasDevolvidas = 0;
            decimal valorDevolvido = 0;
            int qtdCondicional = 0;
            IList<Condicional> listaCondicional = condicionalController.selecionarCondicionalPorSql("From Condicional as Tabela Where Tabela.FlagExcluido <> true and Tabela.Cliente = " + pessoa.Id.ToString());
            if(listaCondicional.Count > 0)
            {
                foreach(Condicional condicional in listaCondicional)
                {
                    qtdCondicional++;
                    valorTotalCondicional = valorTotalCondicional + condicional.ValorTotal;
                    IList<CondicionalProduto> listaCondicionalProduto = condicionalProdutoController.selecionarProdutosPorCondicional(condicional.Id);
                    foreach (CondicionalProduto condicionalProduto in listaCondicionalProduto)
                    {
                        quantidadePecasLevadas = quantidadePecasLevadas + condicionalProduto.Quantidade;
                    }
                    CondicionalDevolucaoController condicionalDevolucaoController = new CondicionalDevolucaoController();
                    IList<CondicionalDevolucao> listaDev = condicionalDevolucaoController.selecionarProdutosDevolvidosPorCondicional(condicional.Id);
                    foreach (CondicionalDevolucao condicionalDevolucao in listaDev)
                    {
                        quantidadePecasDevolvidas = quantidadePecasDevolvidas + condicionalDevolucao.Quantidade;
                    }

                }
                txtQuantidadeCondicional.Texts = qtdCondicional.ToString();
                txtValorCondicionalLevado.Texts = valorTotalCondicional.ToString("C");
                txtQuantidadePecasLevadas.Texts = quantidadePecasLevadas.ToString();
                txtQuantidadePecasDevolvidas.Texts = quantidadePecasDevolvidas.ToString();
            }
            //Ordem de Serviço
            IList<OrdemServico> listaOrdemServico = ordemServicoController.selecionarOrdemServicoPorSQL("Select * From OrdemServico as Tabela Where Tabela.FlagExcluido <> true and Tabela.Cliente = "+pessoa.Id.ToString());
            decimal valorTotalOrdemServico = 0;
            decimal valorTotalOrdemServicoEncerrada = 0;
            decimal valorTotalOrdemServicoAberta = 0;
            int qtdOrdemServico = 0;
            if (listaOrdemServico.Count > 0)
            {
                foreach (OrdemServico ordemServico in listaOrdemServico)
                {
                    valorTotalOrdemServico = valorTotalOrdemServico + ordemServico.ValorTotal;
                    if (primeiraCompra.Year != 1900)
                    {
                        if (ordemServico.DataAbertura < primeiraCompra)
                            primeiraCompra = ordemServico.DataAbertura;
                    }
                    else
                    {
                        primeiraCompra = ordemServico.DataAbertura;
                    }
                    qtdOrdemServico++;
                    if (ordemServico.Status.Equals("ENCERRADA"))
                        valorTotalOrdemServicoEncerrada = valorTotalOrdemServicoEncerrada + ordemServico.ValorTotal;
                    else if (ordemServico.Status.Equals("ABERTA"))
                        valorTotalOrdemServicoAberta = valorTotalOrdemServicoAberta + ordemServico.ValorTotal;
                }
            }

            //Contas a Receber
            IList<ContaReceber> listaReceber = contaReceberController.selecionarContaReceberPorSql("From ContaReceber as Tabela where Tabela.FlagExcluido <> true and Tabela.Cliente = " + pessoa.Id.ToString() + " and Tabela.Concluido = true");
            decimal valorTotalReceber = 0;
            decimal valorTotalReceberRecebido = 0;
            if (listaReceber.Count > 0)
            {
                foreach (ContaReceber contaReceber in listaReceber)
                {
                    if (contaReceber.Recebido == false)
                        valorTotalReceber = ((valorTotalReceber + contaReceber.ValorTotal) - contaReceber.ValorRecebimentoParcial);
                    if (contaReceber.Recebido == true)
                        valorTotalReceberRecebido = valorTotalReceberRecebido + contaReceber.ValorTotal;
                }
            }

            txtTotalGeral.Texts = (valorTotalOrdemServico + valorTotalVendas).ToString("C");
            txtTotalPago.Texts = ((valorTotalOrdemServicoEncerrada + valorTotalVendas) - valorTotalReceber).ToString("C");
            txtTotalAReceber.Texts = valorTotalReceber.ToString("C");
            if (primeiraCompra.Year != 1900)
                txtDataPrimeiraCompra.Texts = primeiraCompra.ToShortDateString();
            else
                txtDataPrimeiraCompra.Texts = "";
            txtQuantidadeOrdemServico.Texts = qtdOrdemServico.ToString();
            txtValorTotalOrdemServico.Texts = valorTotalOrdemServico.ToString("C");
            txtValorOrdemServicoEncerrada.Texts = valorTotalOrdemServicoEncerrada.ToString("C");
            txtValorOrdemServicoAberta.Texts = valorTotalOrdemServicoAberta.ToString("C");
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
