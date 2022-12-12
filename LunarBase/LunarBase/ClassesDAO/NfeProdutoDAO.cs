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
        public IList<NfeProduto> selecionarProdutoPorCNPJeReferencia(string cnpjEmitente, string referencia)
        {
            Session = Conexao.GetSession();
            String sql = "FROM NfeProduto as Tabela WHERE Tabela.Nfe.CnpjEmitente = '" + cnpjEmitente + "' and Tabela.CProd like '" + referencia + "' and Tabela.FlagExcluido <> true";
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
            String sql = "SELECT nfeproduto.PRODUTO as codprod, SUM(nfeproduto.VPROD) as valor, sum(nfeproduto.QuantidadeEntrada) as quantidade, " +
                "sum(nfeproduto.VBC) as baseCalc, nfeProduto.Ncm as ncm, nfeProduto.XProd as descricao, nfeProduto.UComConvertida as unidadeMedida, " +
                "nfeProduto.AliqIpi as aliquotaIpi, nfeProduto.PIcms as aliquotaIcms FROM `NfeProduto` INNER JOIN nfe nf ON nfeproduto.NFE = nf.ID " +
                "WHERE nfeproduto.FLAGEXCLUIDO <> true and nf.EmpresaFilial = " + filial.Id + " and nf.FLAGEXCLUIDO <> true and nf.Modelo = '65' and nf.DATAEMISSAO BETWEEN '" + dataInicial+"' and '"+dataFinal+ "' group by nfeproduto.Produto";
            IList<RetProd> retorno = Session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToBean<RetProd>()).List<RetProd>().ToList();
            return retorno;
        }

        //Utiliza o campo quantidade de entrada pois qdo na entrada de uma nota o operador pode alterar de caixa pra unidade, e colocar uma entrada maior.
        //msm caso para UCOM Convertida - pegamos a unidade ja convertida
        public IList<RetProd> selecionarSomaItens55E65ParaSintegraReg75(string dataInicial, string dataFinal, EmpresaFilial filial)
        {
            Session = Conexao.GetSession();
            String sql = "SELECT nfeproduto.CODIGOINTERNO as codprod, SUM(nfeproduto.VPROD) as valor, sum(nfeproduto.QuantidadeEntrada) as quantidade, " +
                "sum(nfeproduto.VBC) as baseCalc, nfeProduto.Ncm as ncm, nfeProduto.XProd as descricao, nfeProduto.UComConvertida as unidadeMedida, " +
                "nfeProduto.AliqIpi as aliquotaIpi, nfeProduto.PIcms as aliquotaIcms FROM `NfeProduto` " +
                "INNER JOIN nfe nf ON nfeproduto.NFE = nf.ID WHERE nfeproduto.FLAGEXCLUIDO <> true " +
                "and nf.FLAGEXCLUIDO <> true and nf.EmpresaFilial = " + filial.Id + " and nf.DATALANCAMENTO BETWEEN '" + dataInicial + "' and '" + dataFinal + "' group by nfeproduto.Produto";
            IList<RetProd> retorno = Session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToBean<RetProd>()).List<RetProd>().ToList();
            return retorno;
        }

        public class RetProd
        {
            public String codProd { get; set; }
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
