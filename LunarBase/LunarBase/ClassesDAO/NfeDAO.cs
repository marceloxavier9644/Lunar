using LunarBase.Classes;
using LunarBase.ConexaoBD;
using LunarBase.Utilidades;

namespace LunarBase.ClassesDAO
{
    public class NfeDAO : BaseDAO
    {
        public Nfe selecionarNotaPorChave(string chave)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Nfe as Tabela where Tabela.Chave = '" + chave + "' and Tabela.FlagExcluido <> true").UniqueResult<Nfe>();
        }
        public int selecionarUltimoNsu()
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("select Tabela.Nsu from Nfe as Tabela where Tabela.FlagExcluido <> true order by Tabela.Nsu desc").SetMaxResults(1).UniqueResult<int>();
        }
        public string selecionarUltimaDataNotaBaixada()
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("select Tabela.DhEmi from Nfe as Tabela where Tabela.FlagExcluido <> true and Tabela.TipoOperacao = 'E' and Tabela.Nsu > 0 order by Tabela.DhEmi desc").SetMaxResults(1).UniqueResult<string>();
        }

        //Nota entrada
        public IList<Nfe> selecionarNotasEntradaPorPeriodo(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.TipoOperacao = 'E' and Tabela.CnpjEmitente <> '"+Sessao.empresaFilialLogada.Cnpj+"' order by Tabela.DataEmissao desc";
            IList<Nfe> retorno = Session.CreateQuery(sql).List<Nfe>();
            return retorno;
        }
        public IList<Nfe> selecionarNotasEmitidasPorPeriodo(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.CnpjEmitente = '"+Sessao.empresaFilialLogada.Cnpj+ "' order by Tabela.DataEmissao desc";
            IList<Nfe> retorno = Session.CreateQuery(sql).List<Nfe>();
            return retorno;
        }

        public IList<Nfe> selecionarNotasEntradaESaidaPorPeriodoParaSintegraReg5054(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.DataLancamento Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.Modelo = '55' and Tabela.NfeStatus = 1 order by Tabela.DataLancamento";
            IList<Nfe> retorno = Session.CreateQuery(sql).List<Nfe>();
            return retorno;
        }
        //Nota saida
        public IList<Nfe> selecionarNotasSaidaPorPeriodo(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.TipoOperacao = 'S' order by Tabela.DataEmissao desc";
            IList<Nfe> retorno = Session.CreateQuery(sql).List<Nfe>();
            return retorno;
        }

        public IList<Nfe> selecionarNotasSaidaModelo65PorPeriodoReg61(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.TipoOperacao = 'S' and Tabela.Modelo = '65' and Tabela.NfeStatus = 1 order by Tabela.NNf";
            IList<Nfe> retorno = Session.CreateQuery(sql).List<Nfe>();
            return retorno;
        }

        public string selecionarMenorNota65Dia(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "Select MIN(Tabela.NNf) FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.TipoOperacao = 'S' and Tabela.Modelo = '65' and Tabela.NfeStatus = 1 order by Tabela.NNf";
            return Session.CreateSQLQuery(sql).UniqueResult<string>();
        }

        public string selecionarMaiorNota65Dia(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "Select MAX(Tabela.NNf) FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.TipoOperacao = 'S' and Tabela.Modelo = '65' and Tabela.NfeStatus = 1 order by Tabela.NNf";
            return Session.CreateSQLQuery(sql).UniqueResult<string>();
        }

        public decimal selecionarSomaValorNota65Dia(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "Select SUM(Tabela.VNf) FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.TipoOperacao = 'S' and Tabela.Modelo = '65' and Tabela.NfeStatus = 1 order by Tabela.NNf";
            return Session.CreateSQLQuery(sql).UniqueResult<decimal>();
        }

        public decimal selecionarSomaBaseCalcIcmsNota65Dia(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "Select SUM(Tabela.VBc) FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.TipoOperacao = 'S' and Tabela.Modelo = '65' and Tabela.NfeStatus = 1 order by Tabela.NNf";
            return Session.CreateSQLQuery(sql).UniqueResult<decimal>();
        }
        public IList<Nfe> selecionarNotasSaidaEmContigenciaPorPeriodo(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.TipoOperacao = 'S' and Tabela.NfeStatus = 6 order by Tabela.DataEmissao desc";
            IList<Nfe> retorno = Session.CreateQuery(sql).List<Nfe>();
            return retorno;
        }
        public IList<Nfe> selecionarNotaEntradaComVariosFiltros(string dataInicial, string dataFinal, string valorPesquisa)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Nfe as Tabela WHERE CONCAT(Tabela.NNf, ' ', Tabela.Chave, ' ', Tabela.CnpjEmitente, ' ', Tabela.RazaoEmitente, ' ', Tabela.VNf) like '%" + valorPesquisa + "%' and Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.TipoOperacao = 'E' order by Tabela.DataEmissao desc";
            IList<Nfe> retorno = Session.CreateQuery(sql).List<Nfe>();
            return retorno;
        }

        public IList<Nfe> selecionarNotaSaidaComVariosFiltros(string dataInicial, string dataFinal, string valorPesquisa, string modeloNota, string sqlAdicional)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Nfe as Tabela WHERE CONCAT(Tabela.NNf, ' ', Tabela.Chave, ' ', Tabela.CnpjEmitente, ' ', Tabela.RazaoEmitente, ' ', Tabela.Destinatario, ' ', Tabela.CnpjDestinatario, ' ', Tabela.VNf) like '%" + valorPesquisa + "%' and Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.Modelo like '%" + modeloNota + "%' " + sqlAdicional+ " and Tabela.CnpjEmitente = '" + Sessao.empresaFilialLogada.Cnpj + "' order by Tabela.DataEmissao desc";
            IList<Nfe> retorno = Session.CreateQuery(sql).List<Nfe>();
            return retorno;
        }

        public Nfe selecionarNFCePorNumeroESerie(string numeroNFCe, string serie)
        {
            String sql = "FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.NNf = '" + numeroNFCe+"' and Tabela.Serie = '"+serie+"' and Tabela.Modelo = '65' order by Tabela.DataEmissao desc";
            
            Session = Conexao.GetSession();
            return Session.CreateQuery(sql).UniqueResult<Nfe>();
        }

        public Nfe selecionarUltimoNumeroNota(string modelo)
        {
            String sql = "SELECT * FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true " +
                         "and Tabela.Modelo = '"+ modelo + "' and Tabela.Volume <> 'ERRO' order by ABS(NNF) desc";
            Session = Conexao.GetSession();
            return Session.CreateSQLQuery(sql).AddEntity(typeof(Nfe)).SetMaxResults(1).UniqueResult<Nfe>();
        }
        public Nfe selecionarNFePorNumeroESerie(string numeroNFCe, string serie)
        {
            String sql = "FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.NNf = '" + numeroNFCe + "' and Tabela.Serie = '" + serie + "' and Tabela.Modelo = '55' order by Tabela.DataEmissao desc";

            Session = Conexao.GetSession();
            return Session.CreateQuery(sql).UniqueResult<Nfe>();
        }
    }
}
