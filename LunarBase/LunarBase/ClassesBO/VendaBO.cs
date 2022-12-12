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
    public class VendaBO : BO
    {
        private VendaDAO dao;

        public VendaBO()
        {
            dao = new VendaDAO();
        }

        public void salvar(ObjetoPadrao venda)
        {
            Boolean excluido = venda.FlagExcluido;

            if (valida((Venda)venda))
            {
                if (((Venda)venda).Id == 0)
                    dao.incluir((Venda)venda);
                else
                    dao.alterar((Venda)venda);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Venda)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Venda não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new VendaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Venda!" + e.Message);
            }

        }

        private Boolean valida(Venda venda)
        {
            if (venda.ValorFinal <= 0)
            {
                throw new Exception("O valor da venda deve ser maior que 0,00");
            }
            return true;
        }

        public Venda selecionarVendaPorNF(int idNFe)
        {
            try
            {
                return dao.selecionarVendaPorNF(idNFe);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar venda por NF! Erro: " + e.Message);
            }
        }

        public IList<Venda> selecionarTop5VendaPorVendedores(EmpresaFilial filial, string dataInicial, string dataFinal)
        {
            try
            {
                return dao.selecionarTop5VendaPorVendedores(filial, dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar venda por vendedor top 5! Erro: " + e.Message);
            }
        }

        public IList<Venda> selecionarVendaPorPeriodo(EmpresaFilial filial, string dataInicial, string dataFinal)
        {
            try
            {
                return dao.selecionarVendaPorPeriodo(filial, dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar venda por periodo! Erro: " + e.Message);
            }
        }

        public IList<Venda> selecionarVendaPorSql(string sql)
        {
            try
            {
                return dao.selecionarVendaPorSql(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Venda! Erro: " + e.Message);
            }
        }
    }
}