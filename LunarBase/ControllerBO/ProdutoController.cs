using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ControllerBO
{
    public class ProdutoController : Controller
    {
        public Produto selecionarProdutoPorCodigoUnicoEFilial(int idProduto, EmpresaFilial empresaFilial)
        {
            ProdutoBO bo = new ProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutoPorCodigoUnicoEFilial(idProduto, empresaFilial);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
        public IList<Produto> selecionarTodosProdutorPorFilial(EmpresaFilial empresa)
        {
            ProdutoBO bo = new ProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosProdutorPorFilial(empresa);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Produto> selecionarProdutosComVariosFiltros(string valor, EmpresaFilial empresa)
        {
            ProdutoBO bo = new ProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosComVariosFiltros(valor, empresa);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Produto> selecionarProdutosPorSql(string sql)
        {
            ProdutoBO bo = new ProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosPorSql(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Produto> selecionarProdutoPorCodigoBarras(string valor)
        {
            ProdutoBO bo = new ProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutoPorCodigoBarras(valor);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
