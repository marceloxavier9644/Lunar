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
    public class BandeiraCartaoBO : BO
    {
        private BandeiraCartaoDAO dao;

        public BandeiraCartaoBO()
        {
            dao = new BandeiraCartaoDAO();
        }

        public void salvar(ObjetoPadrao bandeiraCartao)
        {
            Boolean excluido = bandeiraCartao.FlagExcluido;

            if (valida((BandeiraCartao)bandeiraCartao))
            {
                if (((BandeiraCartao)bandeiraCartao).Id == 0)
                    dao.incluir((BandeiraCartao)bandeiraCartao);
                else
                    dao.alterar((BandeiraCartao)bandeiraCartao);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((BandeiraCartao)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Bandeira de Cartao não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new BandeiraCartaoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Bandeira de Cartao!" + e.Message);
            }

        }

        private Boolean valida(BandeiraCartao bandeiraCartao)
        {
            if (string.IsNullOrWhiteSpace(bandeiraCartao.Descricao))
            {
                throw new Exception("O campo \"Bandeira de Cartão\" é obrigatório!");
            }
            return true;
        }

        public IList<BandeiraCartao> selecionarTodasBandeiras()
        {
            try
            {
                return dao.selecionarTodasBandeiras();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Bandeira! Erro: " + e.Message);
            }
        }
        public void salvarSeNaoExistir(BandeiraCartao bandeiraCartao)
        {
            try
            {
                BandeiraCartao bandeiraCartaoAux = (BandeiraCartao)dao.Selecionar(bandeiraCartao, ((BandeiraCartao)bandeiraCartao).Id);
                if (bandeiraCartaoAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(bandeiraCartao.Descricao))
                {
                    throw new Exception("O campo \"Bandeira de Cartão\" é obrigatório!");
                }
                dao.incluir((BandeiraCartao)bandeiraCartao);
            }
        }

        public IList<BandeiraCartao> selecionarBandeiraCartaoComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarBandeiraCartaoComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar bandeiras de cartão! Erro: " + e.Message);
            }
        }
    }
}