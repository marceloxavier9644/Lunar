using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class BoletoConfigBO : BO
    {
        private BoletoConfigDAO dao;

        public BoletoConfigBO()
        {
            dao = new BoletoConfigDAO();
        }

        public void salvar(ObjetoPadrao boletoConfig)
        {
            Boolean excluido = boletoConfig.FlagExcluido;

            if (valida((BoletoConfig)boletoConfig))
            {
                if (((BoletoConfig)boletoConfig).Id == 0)
                    dao.incluir((BoletoConfig)boletoConfig);
                else
                    dao.alterar((BoletoConfig)boletoConfig);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((BoletoConfig)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Boleto Config não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new BoletoConfigDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Boleto Config!" + e.Message);
            }

        }

        private Boolean valida(BoletoConfig boletoConfig)
        {
            if (boletoConfig.ContaBancaria == null)
                throw new Exception("O campo \"Conta Bancaria\" é obrigatório!");
            return true;
        }

        public IList<BoletoConfig> selecionarTodosBoletoConfig()
        {
            try
            {
                return dao.selecionarTodosBoletosConfig();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Boleto Config! Erro: " + e.Message);
            }
        }

        public IList<BoletoConfig> selecionarBoletoConfigPorContaBancaria(int idContaBancaria)
        {
            try
            {
                return dao.selecionarBoletoConfigPorContaBancaria(idContaBancaria);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Boleto Config! Erro: " + e.Message);
            }
        }

        public BoletoConfig selecionarBoletoConfigPorContaBancariaUnica(ContaBancaria contaBancaria)
        {
            try
            {
                return dao.selecionarBoletoConfigPorContaBancariaUnica(contaBancaria);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar boleto config! Erro: " + e.Message);
            }
        }
    }
}