﻿using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LunarBase.ClassesDAO.NfeProdutoDAO;

namespace Lunar.Utils.Sintegra
{
    public class GeradorSintegra
    {
        GenericaDesktop generica = new GenericaDesktop();
        NfeController nfeController = new NfeController();
        public void gerarSintegra(DateTime dataInicial, DateTime dataFinal, EmpresaFilial filial)
        {
            IList<NfeProduto> listaProdutos = new List<NfeProduto>();
            string cfop = "";
            List<string> listaSintegra = new List<string>();
            StreamWriter x;
            string folderPath = @"SINTEGRA\" + dataInicial.Year + dataInicial.Month.ToString().PadLeft(2, '0');
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine(folderPath);
            }
            string CaminhoNome = folderPath + @"\Sintegra" + dataInicial.Day.ToString().PadLeft(2, '0') +
                dataInicial.Month.ToString().PadLeft(2, '0') + dataInicial.Year + "_" +
                dataFinal.Day.ToString().PadLeft(2, '0') + dataFinal.Month.ToString().PadLeft(2, '0') + dataFinal.Year + ".txt";

            if (!File.Exists(CaminhoNome))
            {
                x = File.CreateText(CaminhoNome);
            }
            else
            {
                File.Delete(CaminhoNome);
                x = File.CreateText(CaminhoNome);
            }

            var codEstruturaArquivo = new FiscalBr.Sintegra.CodEstruturaArquivo();
            codEstruturaArquivo = FiscalBr.Sintegra.CodEstruturaArquivo.Cod3;

            var codNaturezaOperacoes = new FiscalBr.Sintegra.CodNaturezaOperacoes();
            codNaturezaOperacoes = FiscalBr.Sintegra.CodNaturezaOperacoes.Cod3;

            var codFinalidadeArquivo = new FiscalBr.Sintegra.CodFinalidadeArquivo();
            codFinalidadeArquivo = FiscalBr.Sintegra.CodFinalidadeArquivo.Cod1;

            var registro10 = new FiscalBr.Sintegra.Registro10
                (filial.Cnpj, filial.InscricaoEstadual,
                filial.RazaoSocial, filial.Endereco.Cidade.Descricao,
                filial.Endereco.Cidade.Estado.Uf, "0000000000", dataInicial, dataFinal,
                codFinalidadeArquivo, codEstruturaArquivo, codNaturezaOperacoes
                );

            var registro11 = new FiscalBr.Sintegra.Registro11(filial.Endereco.Logradouro, filial.Endereco.Numero,
                filial.Endereco.Complemento, filial.Endereco.Bairro,
                GenericaDesktop.RemoveCaracteres(filial.Endereco.Cep),
                filial.Empresa.Responsavel, GenericaDesktop.RemoveCaracteres(filial.DddPrincipal + filial.TelefonePrincipal));

            var arquivo = FiscalBr.Common.Sintegra.EscreverCamposSintegra.EscreverCampos(registro10);
            listaSintegra.Add(arquivo);
            //Criar metodo de armazenar arquivo txt de linha em linha para cada registro
            x.Write(arquivo);
            arquivo = FiscalBr.Common.Sintegra.EscreverCamposSintegra.EscreverCampos(registro11);
            listaSintegra.Add(arquivo);
            x.Write(arquivo);
            //x.Close();

            var registro50 = new FiscalBr.Sintegra.Registro50();
            IList<Nfe> listaNotas = new List<Nfe>();
            listaNotas = nfeController.selecionarNotasEntradaESaidaPorPeriodoParaSintegraReg5054(dataInicial.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), dataFinal.ToString("yyyy'-'MM'-'dd' '23':'59':'59"));
            if (listaNotas.Count > 0)
            {
                NfeProdutoController nfeProdutoController = new NfeProdutoController();
                foreach (Nfe nf in listaNotas)
                {
                    string emitente = "P";
                    if (GenericaDesktop.RemoveCaracteres(nf.CnpjEmitente) != GenericaDesktop.RemoveCaracteres(filial.Cnpj.Trim()))
                        emitente = "T";
                    string aliq = "0";
                    string situacaoNota = "N";
                    if (nf.Cancelada == true)
                        situacaoNota = "S";

                    listaProdutos = nfeProdutoController.selecionarProdutosPorNfe(nf.Id);
                    foreach (NfeProduto nfProduto in listaProdutos)
                    {
                        cfop = GenericaDesktop.RemoveCaracteres(nfProduto.CfopEntrada.Trim());
                        aliq = nfProduto.PICMS.Substring(0,2);
                        if (String.IsNullOrEmpty(aliq))
                            aliq = "0";
                        if (String.IsNullOrEmpty(cfop))
                            cfop = GenericaDesktop.RemoveCaracteres(nfProduto.Cfop.Trim());
                    }
                    registro50.Tipo = "50";
                    if (emitente.Equals("P"))
                        registro50.Cnpj = GenericaDesktop.RemoveCaracteres(nf.CnpjDestinatario.Trim());
                    else
                        registro50.Cnpj = GenericaDesktop.RemoveCaracteres(nf.CnpjEmitente.Trim());

                    if (emitente.Equals("P"))
                    {
                        if (!String.IsNullOrEmpty(nf.Cliente.InscricaoEstadual))
                            registro50.InscrEstadual = GenericaDesktop.RemoveCaracteres(nf.Cliente.InscricaoEstadual.Trim());
                        else
                            registro50.InscrEstadual = "ISENTO";
                    }
                    else
                    {
                        if (nf.Fornecedor != null)
                        {
                            if (!String.IsNullOrEmpty(nf.Fornecedor.InscricaoEstadual))
                                registro50.InscrEstadual = GenericaDesktop.RemoveCaracteres(nf.Fornecedor.InscricaoEstadual);
                            else
                                registro50.InscrEstadual = "ISENTO";
                        }
                        else
                            registro50.InscrEstadual = "ISENTO";
                    }

                    registro50.DataEmissaoRecebimento = nf.DataLancamento;
                    string uf = retornaUF(nf.CUf);
                    if (String.IsNullOrEmpty(uf))
                        uf = nf.CUf;
                    registro50.Uf = uf;
                    registro50.Modelo = int.Parse(nf.Modelo);
                    registro50.Serie = nf.Serie;
                    registro50.Numero = int.Parse(GenericaDesktop.RemoveCaracteres(nf.NNf));
                    registro50.Cfop = int.Parse(cfop);
                    registro50.Emitente = emitente;
                    registro50.ValorTotal = nf.VNf;
                    registro50.BaseCalculoIcms = nf.VBc;
                    registro50.ValorIcms = nf.VIcms;
                    registro50.ValorIsentaOuNaoTributadas = 0;
                    registro50.ValorOutras = nf.VOutro;
                    registro50.AliquotaIcms = decimal.Parse(aliq);
                    registro50.SituacaoNotaFiscal = situacaoNota;
                    arquivo = FiscalBr.Common.Sintegra.EscreverCamposSintegra.EscreverCampos(registro50);
                    listaSintegra.Add(arquivo);
                    x.Write(arquivo);
                }
            }

            var registro54 = new FiscalBr.Sintegra.Registro54();
            listaNotas = nfeController.selecionarNotasEntradaESaidaPorPeriodoParaSintegraReg5054(dataInicial.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), dataFinal.ToString("yyyy'-'MM'-'dd' '23':'59':'59"));
            if (listaNotas.Count > 0)
            {
                NfeProdutoController nfeProdutoController = new NfeProdutoController();
                foreach (Nfe nf in listaNotas)
                {
                    string emitente = "P";
                    if (GenericaDesktop.RemoveCaracteres(nf.CnpjEmitente) != GenericaDesktop.RemoveCaracteres(filial.Cnpj.Trim()))
                        emitente = "T";

                    registro54.Tipo = "54";
                    if (emitente.Equals("P"))
                        registro54.Cnpj = GenericaDesktop.RemoveCaracteres(nf.CnpjDestinatario.Trim());
                    else
                        registro54.Cnpj = GenericaDesktop.RemoveCaracteres(nf.CnpjEmitente.Trim());

                    registro54.Modelo = int.Parse(nf.Modelo);
                    registro54.Serie = nf.Serie;
                    registro54.Numero = int.Parse(GenericaDesktop.RemoveCaracteres(nf.NNf.Trim()));
                    listaProdutos = nfeProdutoController.selecionarProdutosPorNfe(nf.Id);
                    foreach (NfeProduto nfProduto in listaProdutos)
                    {
                        cfop = GenericaDesktop.RemoveCaracteres(nfProduto.CfopEntrada.Trim());
                        if (String.IsNullOrEmpty(cfop))
                            cfop = GenericaDesktop.RemoveCaracteres(nfProduto.Cfop.Trim());
                        registro54.Cfop = int.Parse(GenericaDesktop.RemoveCaracteres(cfop.Trim()));
                        registro54.Cst = nfProduto.CstIcms.PadLeft(3, '0');
                        registro54.NumeroItem = int.Parse(nfProduto.Item);
                        registro54.CodProdutoServico = nfProduto.CodigoInterno;
                        registro54.Quantidade = decimal.Parse(nfProduto.QuantidadeEntrada.ToString());
                        registro54.VlProdutoServico = nfProduto.VProd;
                        registro54.VlDescontoDespesaAc = nfProduto.VDesc;
                        registro54.BaseCalculoIcms = nfProduto.VBC;
                        registro54.BaseCalculoIcmsSt = nfProduto.VBCST;
                        registro54.VlIpi = nfProduto.ValorIpi;
                        decimal aliq = 0;
                        if (!string.IsNullOrEmpty(nfProduto.PICMS))
                            aliq = decimal.Parse(nfProduto.PICMS.Substring(0, 2));
                        registro54.AliquotaIcms = aliq;

                        arquivo = FiscalBr.Common.Sintegra.EscreverCamposSintegra.EscreverCampos(registro54);
                        listaSintegra.Add(arquivo);
                        x.Write(arquivo);
                    }
                }
            }

            var registro61 = new FiscalBr.Sintegra.Registro61();
            IList<Nfe> listaNFCe = nfeController.selecionarNotasSaidaModelo65PorPeriodo(dataInicial.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), dataFinal.ToString("yyyy'-'MM'-'dd' '23':'59':'59"));
            DateTime dataInserida = DateTime.Now.AddYears(-100);
            if (listaNFCe.Count > 0)
            {
                NfeDAO nfeDAO = new NfeDAO();
                foreach (Nfe nf65 in listaNFCe)
                {
                    if (!nf65.DataEmissao.ToShortDateString().Equals(dataInserida.ToShortDateString()))
                    {
                        registro61.Tipo = "61";
                        registro61.DataEmissao = nf65.DataEmissao;
                        registro61.Modelo = nf65.Modelo;
                        registro61.Serie = nf65.Serie;
                        registro61.Subserie = "";
                        registro61.NumInicial = int.Parse(nfeDAO.selecionarMenorNota65Dia(nf65.DataEmissao.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), nf65.DataEmissao.ToString("yyyy'-'MM'-'dd' '23':'59':'59")));
                        registro61.NumFinal = int.Parse(nfeDAO.selecionarMaiorNota65Dia(nf65.DataEmissao.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), nf65.DataEmissao.ToString("yyyy'-'MM'-'dd' '23':'59':'59")));
                        registro61.ValorTotal = nfeDAO.selecionarSomaValorNota65Dia(nf65.DataEmissao.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), nf65.DataEmissao.ToString("yyyy'-'MM'-'dd' '23':'59':'59").Replace(".", ","));
                        registro61.ValorIcms = nfeDAO.selecionarSomaBaseCalcIcmsNota65Dia(nf65.DataEmissao.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), nf65.DataEmissao.ToString("yyyy'-'MM'-'dd' '23':'59':'59"));
                        registro61.IsentaNaoTrib = 0;
                        registro61.Outras = nfeDAO.selecionarSomaValorNota65Dia(nf65.DataEmissao.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), nf65.DataEmissao.ToString("yyyy'-'MM'-'dd' '23':'59':'59").Replace(".", ","));
                        registro61.Aliquota = 0;
                        arquivo = FiscalBr.Common.Sintegra.EscreverCamposSintegra.EscreverCampos(registro61);
                        listaSintegra.Add(arquivo);
                        x.Write(arquivo);
                    }
                    dataInserida = nf65.DataEmissao;
                }
            }

            var registro61R = new FiscalBr.Sintegra.Registro61R();
            NfeProdutoDAO nfeProdutoDAO = new NfeProdutoDAO();
            IList<RetProd> listaCodigos = new List<RetProd>();
            listaCodigos = nfeProdutoDAO.selecionarSomaItensNFCeParaSintegra(dataInicial.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), dataFinal.ToString("yyyy'-'MM'-'dd' '23':'59':'59"), filial);
            if (listaCodigos.Count > 0)
            {
                foreach (RetProd retProd in listaCodigos)
                {
                    decimal valorProdutosSemDesconto = retProd.valor;
                    decimal quantidadeProdutos = decimal.Parse(retProd.quantidade.ToString());
                    decimal somaBaseCalc = retProd.baseCalc;
                    registro61R.Tipo = "61R";
                    registro61R.DataEmissao = dataInicial;
                    registro61R.CodItem = retProd.codProd.ToString();/*.PadLeft(14, '0')*/
                    registro61R.Quantidade = quantidadeProdutos;
                    registro61R.ValorItem = valorProdutosSemDesconto;
                    registro61R.BaseCalculoIcms = somaBaseCalc;
                    registro61R.AliquotaIcms = 0;
                    arquivo = FiscalBr.Common.Sintegra.EscreverCamposSintegra.EscreverCampos(registro61R);
                    listaSintegra.Add(arquivo);
                    x.Write(arquivo);
                }
            }

            var registro75 = new FiscalBr.Sintegra.Registro75();
            listaCodigos = nfeProdutoDAO.selecionarSomaItens55E65ParaSintegraReg75(dataInicial.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), dataFinal.ToString("yyyy'-'MM'-'dd' '23':'59':'59"), filial);
            foreach (RetProd retProd in listaCodigos)
            {
                registro75.Tipo = "75";
                registro75.DataInicial = dataInicial;
                registro75.DataFinal = dataFinal;
                registro75.CodItem = retProd.codProd.ToString();
                registro75.CodNcm = GenericaDesktop.RemoveCaracteres(retProd.ncm.Trim());
                registro75.Descricao = retProd.descricao.Trim();
                registro75.UnidadeMedida = retProd.unidadeMedida;

                decimal aliqIpi = 0;
                //if (!String.IsNullOrEmpty(retProd.aliquotaIpi.ToString()))
                //    aliqIpi = decimal.Parse(retProd.aliquotaIpi);
                registro75.AliquotaIpi = aliqIpi;

                decimal aliqIcm = 0;
                //if (!String.IsNullOrEmpty(retProd.aliquotaIcms))
                //    aliqIcm = decimal.Parse(retProd.aliquotaIcms);
                registro75.AliquotaIcms = aliqIcm;
                registro75.ReducaoBaseIcms = 100;
                registro75.BaseCalculoSt = 0;
                arquivo = FiscalBr.Common.Sintegra.EscreverCamposSintegra.EscreverCampos(registro75);
                listaSintegra.Add(arquivo);
                x.Write(arquivo);
            }

            arquivo = new FiscalBr.Sintegra.Registro90(GenericaDesktop.RemoveCaracteres(filial.Cnpj), GenericaDesktop.RemoveCaracteres(filial.InscricaoEstadual), listaSintegra).EscreverRegistro90();
            x.Write(arquivo);
            x.Close();

            GenericaDesktop.ShowInfo("Arquivo Gerado com Sucesso no Caminho: \n" + System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\" + CaminhoNome);
            if (GenericaDesktop.ShowConfirmacao("Deseja abrir a pasta que o arquivo foi gerado?"))
                generica.AbrirPastaExplorer(CaminhoNome);
        }

        private string retornaUF(string valor)
        {
            if (valor.Equals("31"))
                return "MG";
            else if (valor.Equals("11"))
                return "RO";
            else if (valor.Equals("12"))
                return "AC";
            else if (valor.Equals("13"))
                return "AM";
            else if (valor.Equals("14"))
                return "";
            else if (valor.Equals("15"))
                return "PA";
            else if (valor.Equals("16"))
                return "AP";
            else if (valor.Equals("17"))
                return "TO";
            else if (valor.Equals("21"))
                return "MA";
            else if (valor.Equals("22"))
                return "PI";
            else if (valor.Equals("23"))
                return "CE";
            else if (valor.Equals("24"))
                return "RN";
            else if (valor.Equals("25"))
                return "PB";
            else if (valor.Equals("26"))
                return "PE";
            else if (valor.Equals("27"))
                return "AL";
            else if (valor.Equals("28"))
                return "SE";
            else if (valor.Equals("29"))
                return "BA";
            else if (valor.Equals("32"))
                return "ES";
            else if (valor.Equals("33"))
                return "RJ";
            else if (valor.Equals("35"))
                return "SP";
            else if (valor.Equals("41"))
                return "PR";
            else if (valor.Equals("42"))
                return "SC";
            else if (valor.Equals("43"))
                return "RS";
            else if (valor.Equals("50"))
                return "MS";
            else if (valor.Equals("51"))
                return "MT";
            else if (valor.Equals("52"))
                return "GO";
            else if (valor.Equals("53"))
                return "DF";
            else return "";
        }
    }
}
