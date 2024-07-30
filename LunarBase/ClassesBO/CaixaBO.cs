using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CaixaBO : BO
    {
        private CaixaDAO dao;

        public CaixaBO()
        {
            dao = new CaixaDAO();
        }

        public void salvar(ObjetoPadrao caixa)
        {
            Boolean excluido = caixa.FlagExcluido;

            if (valida((Caixa)caixa))
            {
                if (((Caixa)caixa).Id == 0)
                    dao.incluir((Caixa)caixa);
                else
                    dao.alterar((Caixa)caixa);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Caixa)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Caixa não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CaixaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Caixa!" + e.Message);
            }

        }

        private Boolean valida(Caixa caixa)
        {
            if (string.IsNullOrWhiteSpace(caixa.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            return true;
        }

        public IList<Caixa> selecionarCaixaPorOrigem(string tabelaOrigem, string idOrigem)
        {
            try
            {
                return dao.selecionarCaixaPorOrigem(tabelaOrigem, idOrigem);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Caixa! Erro: " + e.Message);
            }
        }

        public IList<Caixa> selecionarCaixaPorUsuarioEDataCadastro(int idUsuario, string dataInicial, string dataFinal)
        {
            try
            {
                return dao.selecionarCaixaPorUsuarioEDataCadastro(idUsuario, dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Caixa! Erro: " + e.Message);
            }
        }

        public IList<Caixa> selecionarCaixaPorSql(string sql)
        {
            try
            {
                return dao.selecionarCaixaPorSql(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Caixa! Erro: " + e.Message);
            }
        }

        public IList<Caixa> selecionarCaixaPorSqlNativo(string sql)
        {
            try
            {
                return dao.selecionarCaixaPorSqlNativo(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Caixa! Erro: " + e.Message);
            }
        }
    }
}