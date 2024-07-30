using LunarBase.Classes;
using LunarBase.ConexaoBD;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class TrocoFixoDAO : BaseDAO
    {
        public IList<TrocoFixo> selecionarTodosTrocoFixoPorEmpresaFilial()
        {
            Session = Conexao.GetSession();
            String sql = "FROM TrocoFixo as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.EmpresaFilial = " + Sessao.empresaFilialLogada.Id;
            IList<TrocoFixo> retorno = Session.CreateQuery(sql).List<TrocoFixo>();
            return retorno;
        }
    }
}
