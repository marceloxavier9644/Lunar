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
    public class PessoaPerfilBO : BO
    {
        private PessoaPerfilDAO dao;

        public PessoaPerfilBO()
        {
            dao = new PessoaPerfilDAO();
        }

        public void salvar(ObjetoPadrao pessoaPerfil)
        {
            Boolean excluido = pessoaPerfil.FlagExcluido;

            if (valida((PessoaPerfil)pessoaPerfil))
            {
                if (((PessoaPerfil)pessoaPerfil).Id == 0)
                    dao.incluir((PessoaPerfil)pessoaPerfil);
                else
                    dao.alterar((PessoaPerfil)pessoaPerfil);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((PessoaPerfil)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Pessoa Perfil não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new PessoaPerfilDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Pessoa Perfil!" + e.Message);
            }

        }

        private Boolean valida(PessoaPerfil pessoaPerfil)
        {
            return true;
        }

        public IList<PessoaPerfil> selecionarPerfilPorPessoa(int idPessoa)
        {
            try
            {
                return dao.selecionarPerfilPorPessoa(idPessoa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Pessoa Perfil! Erro: " + e.Message);
            }
        }


    }
}