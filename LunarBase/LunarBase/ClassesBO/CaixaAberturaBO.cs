using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesBO
{
    public class CaixaAberturaBO : BO
    {
        private CaixaAberturaDAO dao;

        public CaixaAberturaBO()
        {
            dao = new CaixaAberturaDAO();
        }

        public void salvar(ObjetoPadrao caixaAbertura)
        {
            Boolean excluido = caixaAbertura.FlagExcluido;

            if (valida((CaixaAbertura)caixaAbertura))
            {
                if (((CaixaAbertura)caixaAbertura).Id == 0)
                    dao.incluir((CaixaAbertura)caixaAbertura);
                else
                    dao.alterar((CaixaAbertura)caixaAbertura);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((CaixaAbertura)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Caixa Abertura não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CaixaAberturaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Caixa Abertura!" + e.Message);
            }

        }

        private Boolean valida(CaixaAbertura caixaAbertura)
        {

            return true;
        }

        public IList<CaixaAbertura> selecionarAberturaCaixaPorUsuario(int idUsuario)
        {
            try
            {
                return dao.selecionarAberturaCaixaPorUsuario(idUsuario);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar abertura de caixa! Erro: " + e.Message);
            }
        }

        public IList<CaixaAbertura> selecionarAberturaCaixaPorUsuarioEData(int idUsuario, string dataInicial, string dataFinal)
        {
            try
            {
                return dao.selecionarAberturaCaixaPorUsuarioEData(idUsuario, dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar abertura de caixa! Erro: " + e.Message);
            }
        }
        public IList<CaixaAbertura> selecionarTodosCaixasAbertos()
        {
            try
            {
                return dao.selecionarTodosCaixasAbertos();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar abertura de caixa! Erro: " + e.Message);
            }
        }
    }
}