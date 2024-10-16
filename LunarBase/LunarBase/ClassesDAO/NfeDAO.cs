using LunarBase.Classes;
using LunarBase.ConexaoBD;
using LunarBase.Utilidades;
using NHibernate.Mapping;
using System.Reflection;

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
                         "Tabela.DataEmissao Between '" + dataInicial + " 00:00:00' and '" + dataFinal + " 23:59:59' and Tabela.CnpjEmitente = '"+Sessao.empresaFilialLogada.Cnpj+ "' order by Tabela.DataEmissao desc";
            IList<Nfe> retorno = Session.CreateQuery(sql).List<Nfe>();
            return retorno;
        }

        public IList<Nfe> selecionarNotasEntradaESaidaPorPeriodoParaSintegraReg5054(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.Lancada = true and " +
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
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.TipoOperacao = 'S' and Tabela.Modelo = '65' and Tabela.NfeStatus = 1 order by Tabela.DataEmissao";
            IList<Nfe> retorno = Session.CreateQuery(sql).List<Nfe>();
            return retorno;
        }

        public int? selecionarMenorNota65Dia(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "Select MIN(CAST(Tabela.NNf AS UNSIGNED)) FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.TipoOperacao = 'S' and Tabela.Modelo = '65' and Tabela.NfeStatus = 1";

            // Retorna como objeto e converte manualmente
            var resultado = Session.CreateSQLQuery(sql).UniqueResult<object>();

            // Verifica se o resultado é nulo e tenta converter para int?
            return resultado != null ? Convert.ToInt32(resultado) : (int?)null;
        }

        public int? selecionarMaiorNota65Dia(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "Select MAX(CAST(Tabela.NNf AS UNSIGNED)) FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.DataEmissao Between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.TipoOperacao = 'S' and Tabela.Modelo = '65' and Tabela.NfeStatus = 1 order by CAST(Tabela.NNf AS UNSIGNED)";
            // Retorna como objeto e converte manualmente
            var resultado = Session.CreateSQLQuery(sql).UniqueResult<object>();

            // Verifica se o resultado é nulo e tenta converter para int?
            return resultado != null ? Convert.ToInt32(resultado) : (int?)null;
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
                         "and Tabela.Modelo = '"+ modelo + "' and Tabela.CnpjEmitente = '"+Sessao.empresaFilialLogada.Cnpj+"' order by ABS(NNF) desc";
            Session = Conexao.GetSession();
            return Session.CreateSQLQuery(sql).AddEntity(typeof(Nfe)).SetMaxResults(1).UniqueResult<Nfe>();
        }
        public Nfe selecionarNFePorNumeroESerie(string numeroNFCe, string serie)
        {
            String sql = "FROM Nfe as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.NNf = '" + numeroNFCe + "' and Tabela.Serie = '" + serie + "' and Tabela.CnpjEmitente = '"+Sessao.empresaFilialLogada.Cnpj+"' and Tabela.Modelo = '55' order by Tabela.DataEmissao desc";

            Session = Conexao.GetSession();
            return Session.CreateQuery(sql).UniqueResult<Nfe>();
        }

        public void updateNotasAutorizadasComoLancadas(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "UPDATE Nfe as Tabela SET Tabela.Lancada = true where Tabela.Modelo = '55' and Tabela.Lancada = false and Tabela.Status = 'Autorizado o uso da NF-e' and Tabela.CnpjEmitente = '" + Sessao.empresaFilialLogada.Cnpj+"' " +
                         "and Tabela.DataLancamento Between '" + dataInicial + "' and '" + dataFinal + "'";
            Session.CreateQuery(sql).ExecuteUpdate();
        }
    }
}
