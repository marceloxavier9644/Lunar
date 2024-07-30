using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class NfeBO : BO
    {
        private NfeDAO dao;

        public NfeBO()
        {
            dao = new NfeDAO();
        }

        public void salvar(ObjetoPadrao nfe)
        {
            Boolean excluido = nfe.FlagExcluido;

            if (valida((Nfe)nfe))
            {
                if (((Nfe)nfe).Id == 0)
                    dao.incluir((Nfe)nfe);
                else
                    dao.alterar((Nfe)nfe);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Nfe)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Nfe não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new NfeDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Nfe!" + e.Message);
            }

        }

        private Boolean valida(Nfe nfe)
        {
            return true;
        }

        public Nfe selecionarNotaPorChave(string chave)
        {
            try
            {
                return dao.selecionarNotaPorChave(chave);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Nota Fiscal Com Chave! Erro: " + e.Message);
            }
        }
        public int selecionarUltimoNsu()
        {
            try
            {
                return dao.selecionarUltimoNsu();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Ultimo NSU de NFe! Erro: " + e.Message);
            }
        }

        public string selecionarUltimaDataNotaBaixada()
        {
            try
            {
                return dao.selecionarUltimaDataNotaBaixada();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Ultima data de NFe! Erro: " + e.Message);
            }
        }

        public IList<Nfe> selecionarNotasEntradaPorPeriodo(string dataInicial, string dataFinal)
        {
            try
            {
                return dao.selecionarNotasEntradaPorPeriodo(dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar notas! Erro: " + e.Message);
            }
        }

        public IList<Nfe> selecionarNotasSaidaPorPeriodo(string dataInicial, string dataFinal)
        {
            try
            {
                return dao.selecionarNotasSaidaPorPeriodo(dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar notas! Erro: " + e.Message);
            }
        }
        public IList<Nfe> selecionarNotasSaidaModelo65PorPeriodo(string dataInicial, string dataFinal)
        {
            try
            {
                return dao.selecionarNotasSaidaModelo65PorPeriodoReg61(dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar notas! Erro: " + e.Message);
            }
        }
        public IList<Nfe> selecionarNotasEntradaESaidaPorPeriodoParaSintegraReg5054(string dataInicial, string dataFinal)
        {
            try
            {
                return dao.selecionarNotasEntradaESaidaPorPeriodoParaSintegraReg5054(dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar notas! Erro: " + e.Message);
            }
        }

        public IList<Nfe> selecionarNotasEmitidasPorPeriodo(string dataInicial, string dataFinal)
        {
            try
            {
                return dao.selecionarNotasEmitidasPorPeriodo(dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar notas! Erro: " + e.Message);
            }
        }

        public IList<Nfe> selecionarNotasSaidaEmContigenciaPorPeriodo(string dataInicial, string dataFinal)
        {
            try
            {
                return dao.selecionarNotasSaidaEmContigenciaPorPeriodo(dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar notas! Erro: " + e.Message);
            }
        }

        public IList<Nfe> selecionarNotaEntradaComVariosFiltros(string dataInicial, string dataFinal, string valorPesquisa)
        {
            try
            {
                return dao.selecionarNotaEntradaComVariosFiltros(dataInicial, dataFinal, valorPesquisa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar notas! Erro: " + e.Message);
            }
        }

        public IList<Nfe> selecionarNotaSaidaComVariosFiltros(string dataInicial, string dataFinal, string valorPesquisa, string modeloNota, string sqlAdicional)
        {
            try
            {
                return dao.selecionarNotaSaidaComVariosFiltros(dataInicial, dataFinal, valorPesquisa, modeloNota, sqlAdicional);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar notas! Erro: " + e.Message);
            }
        }

        public Nfe selecionarNFCePorNumeroESerie(string numero, string serie)
        {
            try
            {
                return dao.selecionarNFCePorNumeroESerie(numero, serie);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Nota Fiscal Com Numero e Serie! Erro: " + e.Message);
            }
        }

        public Nfe selecionarNFePorNumeroESerie(string numero, string serie)
        {
            try
            {
                return dao.selecionarNFePorNumeroESerie(numero, serie);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Nota Fiscal Com Numero e Serie! Erro: " + e.Message);
            }
        }

        public Nfe selecionarUltimoNumeroNota(string modelo)
        {
            try
            {
                return dao.selecionarUltimoNumeroNota(modelo);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar ultimo numero de nf! Erro: " + e.Message);
            }
        }
    }
}