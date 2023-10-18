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
    public class SpcBO : BO
    {
        private SpcDAO dao;

        public SpcBO()
        {
            dao = new SpcDAO();
        }

        public void salvar(ObjetoPadrao spc)
        {
            Boolean excluido = spc.FlagExcluido;

            if (valida((Spc)spc))
            {
                if (((Spc)spc).Id == 0)
                    dao.incluir((Spc)spc);
                else
                    dao.alterar((Spc)spc);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Spc)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Spc não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new SpcDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Spc!" + e.Message);
            }

        }

        private Boolean valida(Spc spc)
        {
            if (string.IsNullOrWhiteSpace(spc.NomeUsuario))
            {
                throw new Exception("O campo \"Usuario\" é obrigatório!");
            }
            return true;
        }

        public IList<Spc> selecionarRegistrosPorCliente(Pessoa pessoa)
        {
            try
            {
                return dao.selecionarRegistrosPorCliente(pessoa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar registros SPC! Erro: " + e.Message);
            }
        }


    }
}