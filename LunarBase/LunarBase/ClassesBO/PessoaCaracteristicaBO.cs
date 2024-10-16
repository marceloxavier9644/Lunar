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
    public class PessoaCaracteristicaBO : BO
    {
        private PessoaCaracteristicaDAO dao;

        public PessoaCaracteristicaBO()
        {
            dao = new PessoaCaracteristicaDAO();
        }

        public void salvar(ObjetoPadrao pessoaCaracteristica)
        {
            Boolean excluido = pessoaCaracteristica.FlagExcluido;

            if (valida((PessoaCaracteristica)pessoaCaracteristica))
            {
                if (((PessoaCaracteristica)pessoaCaracteristica).Id == 0)
                    dao.incluir((PessoaCaracteristica)pessoaCaracteristica);
                else
                    dao.alterar((PessoaCaracteristica)pessoaCaracteristica);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((PessoaCaracteristica)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Pessoa Caracteristica não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new PessoaCaracteristicaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Pessoa Caracteristica!" + e.Message);
            }

        }

        private Boolean valida(PessoaCaracteristica pessoaCaracteristica)
        {
            return true;
        }
        public IList<PessoaCaracteristica> selecionarTodasPessoaCaracteristica()
        {
            try
            {
                return dao.selecionarTodasPessoaCaracteristica();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar caracteristicas! Erro: " + e.Message);
            }
        }
    }
}