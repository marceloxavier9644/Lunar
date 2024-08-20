using LunarBase.Classes;
using LunarBase.ConexaoBD;
using NHibernate.Transform;

namespace LunarBase.ClassesDAO
{
    public class NfeProdutoDAO : BaseDAO
    {
        public IList<NfeProduto> selecionarProdutosPorNfe(int idNfe)
        {
            Session = Conexao.GetSession();
            String sql = "FROM NfeProduto as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Nfe = " + idNfe;
            IList<NfeProduto> retorno = Session.CreateQuery(sql).List<NfeProduto>();
            return retorno;
        }

        public IList<NfeProduto> selecionarProdutosPorNumeroNfe(int numeroNfe)
        {
            Session = Conexao.GetSession();
            String sql = "Select * FROM NfeProduto as Tabela INNER JOIN Nfe on Tabela.Nfe = Nfe.Id WHERE " +
                         "Nfe.Nnf = '" + numeroNfe + "' and Nfe.TipoOperacao = 'E' and Nfe.FlagExcluido <> true";
            IList<NfeProduto> retorno = Session.CreateSQLQuery(sql).AddEntity(typeof(NfeProduto)).List<NfeProduto>();
            return retorno;
        }
        public IList<NfeProduto> selecionarProdutoPorCNPJeReferencia(string cnpjEmitente, string referencia)
        {
            Session = Conexao.GetSession();
            String sql = "FROM NfeProduto as Tabela " +
                "WHERE Tabela.Nfe.CnpjEmitente = '" + cnpjEmitente + "' and " +
                "Tabela.CProd like '" + referencia + "' and Tabela.FlagExcluido <> true";
            IList<NfeProduto> retorno = Session.CreateQuery(sql).List<NfeProduto>();
            return retorno;
        }

        public void excluirProdutosNfeParaAtualizar(string idNfe)
        {
            Session = Conexao.GetSession();
            String sql = "Update NfeProduto as Tabela Set Tabela.FlagExcluido = true WHERE Tabela.Nfe = " + idNfe + " and Tabela.FlagExcluido <> true";
            Session.CreateQuery(sql).ExecuteUpdate();
        }

        public IList<int> selecionarProdutoNFCe65PorNotaParaSintegra(int idNota)
        {
            Session = Conexao.GetSession();
            String sql = "Select Distinct(Tabela.Produto) FROM NfeProduto as Tabela WHERE Tabela.Nfe = " + idNota + " and Tabela.FlagExcluido <> true";
            IList<int> retorno = Session.CreateSQLQuery(sql).List<int>();
            return retorno;
        }

        public IList<RetProd> selecionarSomaItensNFCeParaSintegra(string dataInicial, string dataFinal, EmpresaFilial filial)
        {
            Session = Conexao.GetSession();
            String sql = "SELECT " +
                "cast(nfeproduto.PRODUTO as Char(255)) as codprod, " +
                "SUM(nfeproduto.VPROD - nfeproduto.VDESC) as valor, " +
                "SUM(nfeproduto.QuantidadeEntrada) as quantidade, " +
                "SUM(nfeproduto.VBC) as baseCalc, " +
                "ANY_VALUE(nfeProduto.Ncm) as ncm, " +
                "ANY_VALUE(nfeProduto.XProd) as descricao, " +
                "ANY_VALUE(nfeProduto.UComConvertida) as unidadeMedida, " +
                "ANY_VALUE(nfeProduto.AliqIpi) as aliquotaIpi, " +
                "ANY_VALUE(nfeProduto.PIcms) as aliquotaIcms " +
                "FROM `NfeProduto` " +
                "INNER JOIN nfe nf ON nfeproduto.NFE = nf.ID " +
                "WHERE nfeproduto.FLAGEXCLUIDO <> true " +
                "AND nf.NfeStatus = 1 " +
                "AND nf.EmpresaFilial = " + filial.Id + " " +
                "AND nf.FLAGEXCLUIDO <> true " +
                "AND nf.Modelo = '65' " +
                "AND nf.DATAEMISSAO BETWEEN '" + dataInicial + "' AND '" + dataFinal + "' " +
                "AND nf.Lancada = true " +
                "GROUP BY nfeproduto.Produto";

            IList<RetProd> retorno = Session.CreateSQLQuery(sql)
                .SetResultTransformer(Transformers.AliasToBean<RetProd>())
                .List<RetProd>()
                .ToList();

            return retorno;
        }

        //Erro group by
        //public IList<RetProd> selecionarSomaItensNFCeParaSintegra(string dataInicial, string dataFinal, EmpresaFilial filial)
        //{
        //    Session = Conexao.GetSession();
        //    String sql = "SELECT cast(nfeproduto.PRODUTO as Char(255)) as codprod, SUM(nfeproduto.VPROD - nfeproduto.VDESC) as valor, sum(nfeproduto.QuantidadeEntrada) as quantidade, " +
        //        "sum(nfeproduto.VBC) as baseCalc, nfeProduto.Ncm as ncm, nfeProduto.XProd as descricao, nfeProduto.UComConvertida as unidadeMedida, " +
        //        "nfeProduto.AliqIpi as aliquotaIpi, nfeProduto.PIcms as aliquotaIcms FROM `NfeProduto` INNER JOIN nfe nf ON nfeproduto.NFE = nf.ID " +
        //        "WHERE nfeproduto.FLAGEXCLUIDO <> true and nf.NfeStatus = 1 and nf.EmpresaFilial = " + filial.Id + " and nf.FLAGEXCLUIDO <> true and nf.Modelo = '65' and nf.DATAEMISSAO BETWEEN '" + dataInicial+"' and '"+dataFinal+ "' and nf.Lancada = true group by nfeproduto.Produto";
        //    IList<RetProd> retorno = Session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToBean<RetProd>()).List<RetProd>().ToList();
        //    return retorno;
        //}

        //Utiliza o campo quantidade de entrada pois qdo na entrada de uma nota o operador pode alterar de caixa pra unidade, e colocar uma entrada maior.
        //msm caso para UCOM Convertida - pegamos a unidade ja convertida
        public IList<RetProd> selecionarSomaItens55E65ParaSintegraReg75(string dataInicial, string dataFinal, EmpresaFilial filial)
        {
            Session = Conexao.GetSession();
            //String sql = "SELECT nfeproduto.CODIGOINTERNO as codprod, SUM(nfeproduto.VPROD) as valor, sum(nfeproduto.QuantidadeEntrada) as quantidade, " +
            //    "sum(nfeproduto.VBC) as baseCalc, nfeProduto.Ncm as ncm, nfeProduto.XProd as descricao, nfeProduto.UComConvertida as unidadeMedida, " +
            //    "nfeProduto.AliqIpi as aliquotaIpi, nfeProduto.PIcms as aliquotaIcms FROM `NfeProduto` " +
            //    "INNER JOIN nfe nf ON nfeproduto.NFE = nf.ID WHERE nfeproduto.FLAGEXCLUIDO <> true " +
            //    "and nf.FLAGEXCLUIDO <> true and nf.NfeStatus = 1 and nf.EmpresaFilial = " + filial.Id + " and nf.DATALANCAMENTO BETWEEN '" + dataInicial + "' and '" + dataFinal + "' and nf.Lancada = true group by nfeproduto.Produto";
            String sql = "SELECT MAX(nfeproduto.CODIGOINTERNO) as codprod, SUM(nfeproduto.VPROD) as valor, SUM(nfeproduto.QuantidadeEntrada) " +
                "as quantidade, SUM(nfeproduto.VBC) as baseCalc, MAX(nfeProduto.Ncm) as ncm, MAX(nfeProduto.XProd) as descricao, " +
                "MAX(nfeProduto.UComConvertida) as unidadeMedida, MAX(nfeProduto.AliqIpi) as aliquotaIpi, MAX(nfeProduto.PIcms) " +
                "as aliquotaIcms FROM `NfeProduto` INNER JOIN nfe nf ON nfeproduto.NFE = nf.ID WHERE nfeproduto.FLAGEXCLUIDO <> true " +
                "AND nf.FLAGEXCLUIDO<> true AND nf.NfeStatus = 1 AND nf.EmpresaFilial = "+ filial.Id + " AND nf.DATALANCAMENTO " +
                "BETWEEN '" + dataInicial + "' and '" + dataFinal + "' AND nf.Lancada = true GROUP BY nfeproduto.Produto";
            IList<RetProd> retorno = Session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToBean<RetProd>()).List<RetProd>().ToList();
            return retorno;
        }

        public IList<RetProdReg50Sintegra> selecionarRegistro50SintegraAgrupadoPorCfop(Nfe nfe)
        {
            Session = Conexao.GetSession();
            String sql = "";
            if (nfe.Fornecedor != null && nfe.TipoOperacao == "E")
            {
                //sql = "SELECT sum((nfeproduto.VUNCOM * Cast((nfeproduto.QCOM) as DECIMAL(7,2))) + nfeproduto.VICMSST + nfeproduto.VFRETE + nfeproduto.VALORIPI + nfeproduto.VOUTRO + nfeproduto.VIPIDEVOLVIDO - nfeproduto.VDESC) as ValorTotal, nfe.CnpjEmitente as cnpjRemetente, nfe.CnpjDestinatario as cnpjDestino, " +
                //    "pessoa.InscricaoEstadual as inscricaoEstadual, nfe.DataLancamento as data, nfe.Modelo as modelo,nfe.Serie as serie, " +
                //    "nfe.NNf as numero, nfeProduto.CfopEntrada as cfop, sum(nfeproduto.VBc) as baseCalcIcms, " +
                //    "sum(nfeproduto.VIcms) as valorIcms, sum(nfeproduto.ValorIsentoIcms) as valorIsentaNaoTributada, sum(nfeproduto.OutrosIcms) as valorOutras, nfeproduto.PIcms as aliquotaIcms " +
                //    "from `nfeproduto` " +
                //             "INNER JOIN nfe ON nfe.ID = nfeproduto.NFE " +
                //             "INNER JOIN pessoa ON nfe.Fornecedor = pessoa.Id " +
                //     "where nfe = " + nfe.Id + " and NfeProduto.FlagExcluido <> true and nfe.LANCADA = true " +
                //     "GROUP by nfeproduto.CFOPENTRADA";
                sql = @"
    SELECT 
        SUM((nfeproduto.VUNCOM * CAST(nfeproduto.QCOM AS DECIMAL(7,2))) + 
            nfeproduto.VICMSST + 
            nfeproduto.VFRETE + 
            nfeproduto.VALORIPI + 
            nfeproduto.VOUTRO + 
            nfeproduto.VIPIDEVOLVIDO - 
            nfeproduto.VDESC) AS ValorTotal, 
        MAX(nfe.CnpjEmitente) AS cnpjRemetente, 
        MAX(nfe.CnpjDestinatario) AS cnpjDestino, 
        MAX(pessoa.InscricaoEstadual) AS inscricaoEstadual, 
        MAX(nfe.DataLancamento) AS data, 
        MAX(nfe.Modelo) AS modelo, 
        MAX(nfe.Serie) AS serie, 
        MAX(nfe.NNf) AS numero, 
        nfeProduto.CfopEntrada AS cfop, 
        SUM(nfeproduto.VBc) AS baseCalcIcms, 
        SUM(nfeproduto.VIcms) AS valorIcms, 
        SUM(nfeproduto.ValorIsentoIcms) AS valorIsentaNaoTributada, 
        SUM(nfeproduto.OutrosIcms) AS valorOutras, 
        MAX(nfeproduto.PIcms) AS aliquotaIcms 
    FROM 
        nfeproduto 
    INNER JOIN 
        nfe ON nfe.ID = nfeproduto.NFE 
    INNER JOIN 
        pessoa ON nfe.Fornecedor = pessoa.Id 
    WHERE 
        nfe.ID = " + nfe.Id + @" 
        AND NfeProduto.FlagExcluido <> TRUE 
        AND nfe.LANCADA = TRUE 
    GROUP BY 
        nfeproduto.CfopEntrada
";

            }
            else if(nfe.Cliente != null && nfe.TipoOperacao == "E")
            {
                //sql = "SELECT nfe.VNf as valorTotal, nfe.CnpjEmitente as cnpjRemetente, nfe.CnpjDestinatario as cnpjDestino, " +
                //                   "pessoa.InscricaoEstadual as inscricaoEstadual, nfe.DataLancamento as data, nfe.Modelo as modelo,nfe.Serie as serie, " +
                //                   "nfe.NNf as numero, nfeProduto.CfopEntrada as cfop, sum(nfeproduto.VBc) as baseCalcIcms, " +
                //                   "sum(nfeproduto.VIcms) as valorIcms, sum(nfeproduto.ValorIsentoIcms) as valorIsentaNaoTributada, sum(nfeproduto.OutrosIcms) as valorOutras, nfeproduto.PIcms as aliquotaIcms " +
                //                   "from `nfeproduto` " +
                //                            "INNER JOIN nfe ON nfe.ID = nfeproduto.NFE " +
                //                            "INNER JOIN pessoa ON nfe.Cliente = pessoa.Id " +
                //                    "where nfe = " + nfe.Id + " and NfeProduto.FlagExcluido <> true and nfe.LANCADA = true " +
                //                    "GROUP by nfeproduto.CFOP";
                sql = @"
    SELECT 
        MAX(nfe.VNf) AS valorTotal, 
        MAX(nfe.CnpjEmitente) AS cnpjRemetente, 
        MAX(nfe.CnpjDestinatario) AS cnpjDestino, 
        MAX(pessoa.InscricaoEstadual) AS inscricaoEstadual, 
        MAX(nfe.DataLancamento) AS data, 
        MAX(nfe.Modelo) AS modelo, 
        MAX(nfe.Serie) AS serie, 
        MAX(nfe.NNf) AS numero, 
        nfeProduto.CfopEntrada AS cfop, 
        SUM(nfeproduto.VBc) AS baseCalcIcms, 
        SUM(nfeproduto.VIcms) AS valorIcms, 
        SUM(nfeproduto.ValorIsentoIcms) AS valorIsentaNaoTributada, 
        SUM(nfeproduto.OutrosIcms) AS valorOutras, 
        MAX(nfeproduto.PIcms) AS aliquotaIcms 
    FROM 
        nfeproduto 
    INNER JOIN 
        nfe ON nfe.ID = nfeproduto.NFE 
    INNER JOIN 
        pessoa ON nfe.Cliente = pessoa.Id 
    WHERE 
        nfe.ID = " + nfe.Id + @" 
        AND NfeProduto.FlagExcluido <> TRUE 
        AND nfe.LANCADA = TRUE 
    GROUP BY 
        nfeProduto.CfopEntrada
";

            }
            else if (nfe.Fornecedor != null && nfe.TipoOperacao == "S")
            {
                //sql = "SELECT SUM(((nfeproduto.VALORFINAL * Cast((nfeproduto.QCOM) as DECIMAL(7,2)) + nfeproduto.VFRETE + nfeproduto.VIPIDEVOLVIDO + nfeproduto.VOUTRO + nfeproduto.VICMSST + " +
                //    "nfeproduto.VALORIPI) - nfeproduto.VDESC)) as valorTotal, nfe.CnpjEmitente as cnpjRemetente, nfe.CnpjDestinatario as cnpjDestino, " +
                //           "pessoa.InscricaoEstadual as inscricaoEstadual, nfe.DataLancamento as data, nfe.Modelo as modelo,nfe.Serie as serie, " +
                //           "nfe.NNf as numero, nfeProduto.Cfop as cfop, sum(nfeproduto.VBc) as baseCalcIcms, " +
                //           "sum(nfeproduto.VIcms) as valorIcms, sum(nfeproduto.ValorIsentoIcms) as valorIsentaNaoTributada, sum(nfeproduto.OutrosIcms) as valorOutras, nfeproduto.PIcms as aliquotaIcms " +
                //           "from `nfeproduto` " +
                //                    "INNER JOIN nfe ON nfe.ID = nfeproduto.NFE " +
                //                    "INNER JOIN pessoa ON nfe.Fornecedor = pessoa.Id " +
                //            "where nfe = " + nfe.Id + " and NfeProduto.FlagExcluido <> true and nfe.LANCADA = true " +
                //            "GROUP by nfeproduto.CFOP";
                sql = @"
    SELECT 
        SUM(((nfeproduto.VALORFINAL * CAST(nfeproduto.QCOM AS DECIMAL(7,2))) + 
            nfeproduto.VFRETE + 
            nfeproduto.VIPIDEVOLVIDO + 
            nfeproduto.VOUTRO + 
            nfeproduto.VICMSST + 
            nfeproduto.VALORIPI) - 
            nfeproduto.VDESC) AS valorTotal, 
        MAX(nfe.CnpjEmitente) AS cnpjRemetente, 
        MAX(nfe.CnpjDestinatario) AS cnpjDestino, 
        MAX(pessoa.InscricaoEstadual) AS inscricaoEstadual, 
        MAX(nfe.DataLancamento) AS data, 
        MAX(nfe.Modelo) AS modelo, 
        MAX(nfe.Serie) AS serie, 
        MAX(nfe.NNf) AS numero, 
        nfeProduto.Cfop AS cfop, 
        SUM(nfeproduto.VBc) AS baseCalcIcms, 
        SUM(nfeproduto.VIcms) AS valorIcms, 
        SUM(nfeproduto.ValorIsentoIcms) AS valorIsentaNaoTributada, 
        SUM(nfeproduto.OutrosIcms) AS valorOutras, 
        MAX(nfeproduto.PIcms) AS aliquotaIcms 
    FROM 
        nfeproduto 
    INNER JOIN 
        nfe ON nfe.ID = nfeproduto.NFE 
    INNER JOIN 
        pessoa ON nfe.Fornecedor = pessoa.Id 
    WHERE 
        nfe.ID = " + nfe.Id + @" 
        AND NfeProduto.FlagExcluido <> TRUE 
        AND nfe.LANCADA = TRUE 
    GROUP BY 
        nfeProduto.Cfop
";

            }
            else if (nfe.Cliente != null && nfe.TipoOperacao == "S")
            {
                //sql = "SELECT SUM(((nfeproduto.VUNCOM * Cast((nfeproduto.QCOM) as DECIMAL(7,2)) + nfeproduto.VFRETE + nfeproduto.VIPIDEVOLVIDO + nfeproduto.VOUTRO + nfeproduto.VICMSST + " +
                //    "nfeproduto.VALORIPI) - nfeproduto.VDESC)) as valorTotal, nfe.CnpjEmitente as cnpjRemetente, nfe.CnpjDestinatario as cnpjDestino, " +
                //                   "pessoa.InscricaoEstadual as inscricaoEstadual, nfe.DataLancamento as data, nfe.Modelo as modelo,nfe.Serie as serie, " +
                //                   "nfe.NNf as numero, nfeProduto.Cfop as cfop, sum(nfeproduto.VBc) as baseCalcIcms, " +
                //                   "sum(nfeproduto.VIcms) as valorIcms, sum(nfeproduto.ValorIsentoIcms) as valorIsentaNaoTributada, sum(nfeproduto.OutrosIcms) as valorOutras, nfeproduto.PIcms as aliquotaIcms " +
                //                   "from `nfeproduto` " +
                //                            "INNER JOIN nfe ON nfe.ID = nfeproduto.NFE " +
                //                            "INNER JOIN pessoa ON nfe.Cliente = pessoa.Id " +
                //                    "where nfe = " + nfe.Id + " and NfeProduto.FlagExcluido <> true and nfe.LANCADA = true " +
                //                    "GROUP by nfeproduto.CFOP";
                sql = @"
    SELECT 
        SUM(((nfeproduto.VUNCOM * CAST(nfeproduto.QCOM AS DECIMAL(7,2))) + 
            nfeproduto.VFRETE + 
            nfeproduto.VIPIDEVOLVIDO + 
            nfeproduto.VOUTRO + 
            nfeproduto.VICMSST + 
            nfeproduto.VALORIPI) - 
            nfeproduto.VDESC) AS valorTotal, 
        MAX(nfe.CnpjEmitente) AS cnpjRemetente, 
        MAX(nfe.CnpjDestinatario) AS cnpjDestino, 
        MAX(pessoa.InscricaoEstadual) AS inscricaoEstadual, 
        MAX(nfe.DataLancamento) AS data, 
        MAX(nfe.Modelo) AS modelo, 
        MAX(nfe.Serie) AS serie, 
        MAX(nfe.NNf) AS numero, 
        nfeProduto.Cfop AS cfop, 
        SUM(nfeproduto.VBc) AS baseCalcIcms, 
        SUM(nfeproduto.VIcms) AS valorIcms, 
        SUM(nfeproduto.ValorIsentoIcms) AS valorIsentaNaoTributada, 
        SUM(nfeproduto.OutrosIcms) AS valorOutras, 
        MAX(nfeproduto.PIcms) AS aliquotaIcms 
    FROM 
        nfeproduto 
    INNER JOIN 
        nfe ON nfe.ID = nfeproduto.NFE 
    INNER JOIN 
        pessoa ON nfe.Cliente = pessoa.Id 
    WHERE 
        nfe.ID = " + nfe.Id + @" 
        AND NfeProduto.FlagExcluido <> TRUE 
        AND nfe.LANCADA = TRUE 
    GROUP BY 
        nfeProduto.Cfop
";


            }
            IList<RetProdReg50Sintegra> retorno = Session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToBean<RetProdReg50Sintegra>()).List<RetProdReg50Sintegra>().ToList();
            return retorno;
        }

        //Utilizado para gerar o registro 50 por CFOP ao invés de nota unica
        public class RetProdReg50Sintegra
        {
            public String cnpjDestino { get; set; }
            public String cnpjRemetente { get; set; }
            public String inscricaoEstadual { get; set; }
            public DateTime data { get; set; }
            public String modelo { get; set; }
            public String serie { get; set; }
            public String numero { get; set; }
            public String cfop { get; set; }
            public decimal valorTotal { get; set; }
            public decimal baseCalcIcms { get; set; }
            public decimal valorIcms { get; set; }
            public decimal valorIsentaNaoTributada { get; set; }
            public decimal valorOutras { get; set; }
            public string aliquotaIcms { get; set; }
         
        }

        public class RetProd
        {
            public string codProd { get; set; } // era string
            public decimal valor { get; set; }
            public double quantidade { get; set; }
            public decimal baseCalc { get; set; }

            public string ncm { get; set; }
            public string descricao { get; set; }
            public string unidadeMedida { get; set; }
            public string aliquotaIpi { get; set; }
            public string aliquotaIcms { get; set; }
            //public decimal reducaoBaseCalc { get; set; }
            //public decimal baseIcmsSt { get; set; }
        }
    }
}
