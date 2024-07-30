using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class AtendimentoMesaDAO : BaseDAO
    {
        //Nao puxa as comandas
        public IList<AtendimentoMesa> selecionarTodasMesas()
        {
            Session = Conexao.GetSession();
            string sql = "From AtendimentoMesa as Tabela Where Tabela.Tipo = 'MESA' and Tabela.FlagExcluido <> true";
            IList<AtendimentoMesa> retorno = Session.CreateQuery(sql).List<AtendimentoMesa>();
            return retorno;
        }
    }
}
