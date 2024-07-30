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
    public class OsComissaoBO /*: BO*/
    {
        private OsComissaoDAO dao;

        public OsComissaoBO()
        {
            dao = new OsComissaoDAO();
        }

        //public void salvar(ObjetoPadrao osComissao)
        //{
        //    Boolean excluido = osComissao.FlagExcluido;
        //        if (((OsComissao)osComissao).Id == 0)
        //            dao.incluir((OsComissao)osComissao);
        //        else
        //            dao.alterar((OsComissao)osComissao);
        //}

        //public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        //{
        //    try
        //    {
        //        objeto = dao.Selecionar(objeto, ((OsComissao)objeto).Id);
        //        if (objeto.FlagExcluido == true)
        //        {
        //            throw new Exception();
        //        }
        //        return objeto;
        //    }
        //    catch
        //    {
        //        throw new Exception("Os Comissao não encontrada!");
        //    }

        //}
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new OsComissaoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar OsComissao!" + e.Message);
            }

        }

       
    }
}