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
    public class BalancoEstoqueBO : BO
    {
        private BalancoEstoqueDAO dao;

        public BalancoEstoqueBO()
        {
            dao = new BalancoEstoqueDAO();
        }

        public void salvar(ObjetoPadrao balancoEstoque)
        {
            Boolean excluido = balancoEstoque.FlagExcluido;

            if (valida((BalancoEstoque)balancoEstoque))
            {
                if (((BalancoEstoque)balancoEstoque).Id == 0)
                    dao.incluir((BalancoEstoque)balancoEstoque);
                else
                    dao.alterar((BalancoEstoque)balancoEstoque);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((BalancoEstoque)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Balanco de Estoque não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new BalancoEstoqueDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Balanco de Estoque!" + e.Message);
            }

        }

        private Boolean valida(BalancoEstoque balancoEstoque)
        {
            if (string.IsNullOrWhiteSpace(balancoEstoque.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            return true;
        }

        public IList<BalancoEstoque> selecionarTodosBalancoEstoque(EmpresaFilial empresaFilial)
        {
            try
            {
                return dao.selecionarTodosBalancoEstoque(empresaFilial);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Balanco de Estoque! Erro: " + e.Message);
            }
        }

        public IList<BalancoEstoque> selecionarBalancoEstoquePorSql(string sql)
        {
            try
            {
                return dao.selecionarBalancoEstoquePorSql(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Balanco de Estoque! Erro: " + e.Message);
            }
        }

        public void salvarBalancoEstoqueComItens(BalancoEstoque balancoEstoque, IList<BalancoEstoqueProduto> listaProdutos)
        {
            try
            {
                salvar(balancoEstoque);
                BalancoEstoqueProdutoBO balancoEstoqueProdutoBO = new BalancoEstoqueProdutoBO();

                if (listaProdutos.Count > 0)
                {
                    foreach (BalancoEstoqueProduto balancoEstoqueProduto in listaProdutos)
                    {
                        balancoEstoqueProduto.BalancoEstoque = balancoEstoque;
                        balancoEstoqueProdutoBO.salvar(balancoEstoqueProduto);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}