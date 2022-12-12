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
    public class ParcelamentoBO : BO
    {
        private ParcelamentoDAO dao;

        public ParcelamentoBO()
        {
            dao = new ParcelamentoDAO();
        }

        public void salvar(ObjetoPadrao parcelamento)
        {
            Boolean excluido = parcelamento.FlagExcluido;

            if (valida((Parcelamento)parcelamento))
            {
                if (((Parcelamento)parcelamento).Id == 0)
                    dao.incluir((Parcelamento)parcelamento);
                else
                    dao.alterar((Parcelamento)parcelamento);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Parcelamento)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Parcelamento não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ParcelamentoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Parcelamento!" + e.Message);
            }

        }

        private Boolean valida(Parcelamento parcelamento)
        {
            if (string.IsNullOrWhiteSpace(parcelamento.Descricao))
            {
                throw new Exception("O campo \"Descrição do Parcelamento\" é obrigatório!");
            }
            if (parcelamento.Parcelas <= 0)
            {
                throw new Exception("O campo \"Parcelas\" é obrigatório!");
            }
            return true;
        }

        public void salvarSeNaoExistir(Parcelamento parcelamento)
        {
            try
            {
                Parcelamento parcelamentoAux = (Parcelamento)dao.Selecionar(parcelamento, ((Parcelamento)parcelamento).Id);
                if (parcelamentoAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(parcelamento.Descricao))
                {
                    throw new Exception("O campo \"Descrição Parcelamento\" é obrigatório!");
                }
                dao.incluir((Parcelamento)parcelamento);
            }
        }
        public IList<Parcelamento> selecionarTodasCondicoesCredito()
        {
            try
            {
                return dao.selecionarTodasCondicoesCredito();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar condicoes! Erro: " + e.Message);
            }
        }

        public IList<Parcelamento> selecionarTodasCondicoesDebito()
        {
            try
            {
                return dao.selecionarTodasCondicoesDebito();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar condicoes! Erro: " + e.Message);
            }
        }
    }
}