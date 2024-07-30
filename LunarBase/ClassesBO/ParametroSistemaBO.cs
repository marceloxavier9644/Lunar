using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ParametroSistemaBO : BO
    {
        private ParametroSistemaDAO dao;

        public ParametroSistemaBO()
        {
            dao = new ParametroSistemaDAO();
        }

        public void salvar(ObjetoPadrao parametroSistema)
        {
            Boolean excluido = parametroSistema.FlagExcluido;

            if (valida((ParametroSistema)parametroSistema))
            {
                if (((ParametroSistema)parametroSistema).Id == 0)
                    dao.incluir((ParametroSistema)parametroSistema);
                else
                    dao.alterar((ParametroSistema)parametroSistema);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ParametroSistema)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Parametro Sistema não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ParametroSistemaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Parametro Sistema!" + e.Message);
            }

        }

        private Boolean valida(ParametroSistema parametroSistema)
        {
            if (string.IsNullOrWhiteSpace(parametroSistema.UltNsu))
            {
                throw new Exception("O campo \"Ult NSU\" é obrigatório!");
            }
            return true;
        }

        public void salvarSeNaoExistir(ParametroSistema parametro)
        {
            try
            {
                ParametroSistema parametroAux = (ParametroSistema)dao.Selecionar(parametro, ((ParametroSistema)parametro).Id);
                if (parametroAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(parametro.UltNsu))
                {
                    throw new Exception("O campo \"Ult NSU\" é obrigatório!");
                }

                dao.incluir((ParametroSistema)parametro);
            }
        }
    }
}