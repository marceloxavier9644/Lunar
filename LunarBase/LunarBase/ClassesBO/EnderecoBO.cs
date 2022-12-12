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
    public class EnderecoBO : BO
    {
        private EnderecoDAO dao;

        public EnderecoBO()
        {
            dao = new EnderecoDAO();
        }

        public void salvar(ObjetoPadrao endereco)
        {
            Boolean excluido = endereco.FlagExcluido;

            if (valida((Endereco)endereco))
            {
                if (((Endereco)endereco).Id == 0)
                    dao.incluir((Endereco)endereco);
                else
                    dao.alterar((Endereco)endereco);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Endereco)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Endereco não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new EnderecoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Endereco!" + e.Message);
            }

        }

        private Boolean valida(Endereco endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco.Logradouro))
            {
                throw new Exception("O campo \"Logradouro\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(endereco.Numero))
            {
                throw new Exception("O campo \"Número\" é obrigatório se nao existir escreva S/N!");
            }
            if (endereco.Cidade == null)
            {
                throw new Exception("O campo \"Cidade\" é obrigatório!");
            }
            return true;
        }

        public IList<Endereco> selecionarEnderecoPorPessoa(int idPessoa)
        {
            try
            {
                return dao.selecionarEnderecoPorPessoa(idPessoa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Endereco! Erro: " + e.Message);
            }
        }
    }
}