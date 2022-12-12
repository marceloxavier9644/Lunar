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
    public class ProdutoSetorBO : BO
    {
        private ProdutoSetorDAO dao;

        public ProdutoSetorBO()
        {
            dao = new ProdutoSetorDAO();
        }

        public void salvar(ObjetoPadrao produtoSetor)
        {
            Boolean excluido = produtoSetor.FlagExcluido;

            if (valida((ProdutoSetor)produtoSetor))
            {
                if (((ProdutoSetor)produtoSetor).Id == 0)
                    dao.incluir((ProdutoSetor)produtoSetor);
                else
                    dao.alterar((ProdutoSetor)produtoSetor);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ProdutoSetor)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Produto Setor não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ProdutoSetorDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Produto Setor!" + e.Message);
            }

        }

        private Boolean valida(ProdutoSetor produtoSetor)
        {
            if (string.IsNullOrWhiteSpace(produtoSetor.Descricao))
            {
                throw new Exception("O campo \"Setor/Localização\" é obrigatório!");
            }
            return true;
        }

        public IList<ProdutoSetor> selecionarProdutoSetorComVariosFiltros(string valor, EmpresaFilial empresaFilial)
        {
            try
            {
                return dao.selecionarProdutoSetorComVariosFiltros(valor, empresaFilial);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar setor! Erro: " + e.Message);
            }
        }
    }
}