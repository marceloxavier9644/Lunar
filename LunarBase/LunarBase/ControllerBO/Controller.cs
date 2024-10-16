using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;
using LunarBase.Interface;
using LunarBase.Utilidades;
using Newtonsoft.Json.Linq;
using NHibernate;
using System.Windows;

namespace LunarBase.ControllerBO
{
    public class Controller
    {
        private Lazy<Conexao> conexaoLazy;
        private Conexao conexao;
        private ISession session;
        private static Controller instancia;
        public Controller()
        {
            conexao = new Conexao();
            session = Conexao.GetSession();
        }
        public Controller(Boolean atualizaBanco)
        {
            conexao = new Conexao(atualizaBanco);
            session = Conexao.GetSession();
        }

        public static Controller getInstanceAtualiza(Boolean atualizaBanco = true)
        {
            instancia = new Controller(atualizaBanco);
            return instancia;
        }
        public static async Task<Controller> getInstanceAtualizaAsync(Boolean atualizaBanco = true)
        {
            // Executa o código de forma assíncrona
            instancia = await Task.Run(() => new Controller(atualizaBanco));
            return instancia;
        }
        public static Controller getInstance(Boolean atualizaBanco = false)
        {
            if (instancia == null)
                instancia = new Controller(atualizaBanco);

            return instancia;
        }

        public void salvar(ObjetoPadrao objeto)
        {
            try
            {
                Conexao.IniciaTransacao();
                BO bo = (BO)Type.GetType("LunarBase.ClassesBO." + objeto.GetType().Name + "BO").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                ObjetoPadrao objetoAuxiliar = null;
                try
                {
                    //objetoAuxiliar = bo.selecionar(objeto);
                    objetoAuxiliar = objeto;
                    objeto.DataCadastro = objetoAuxiliar.DataCadastro;
                    objeto.DataAlteracao = objetoAuxiliar.DataAlteracao;
                    objeto.DataExclusao = objetoAuxiliar.DataExclusao;

                    objeto.OperadorCadastro = objetoAuxiliar.OperadorCadastro;
                    objeto.OperadorAlteracao = objetoAuxiliar.OperadorAlteracao;
                    objeto.OperadorExclusao = objetoAuxiliar.OperadorExclusao;
                }
                catch (Exception)
                {
                    //Não precisa fazer nada
                }
                valoresPadraoObjeto(ref objeto);
                bo.salvar(objeto);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                //Removido 20/01/18
                //MessageBox.Show(e.ToString());
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + objeto.GetType().Name + ":" + Environment.NewLine + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public async Task salvarAsync(ObjetoPadrao objeto)
        {
            await Task.Run(() => salvar(objeto));
        }

        public void atualizar(ObjetoPadrao objeto)
        {
            try
            {
                Conexao.IniciaTransacao();
                valoresPadraoObjeto(ref objeto);
                BO bo = (BO)Type.GetType("LunarBase.ClassesBO." + objeto.GetType().Name + "BO").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                bo.salvar(objeto);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + objeto.GetType().Name + ":" + Environment.NewLine + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public void excluir(ObjetoPadrao objeto)
        {
            try
            {
                BO bo = (BO)Type.GetType("LunarBase.ClassesBO." + objeto.GetType().Name + "BO").GetConstructor(System.Type.EmptyTypes).Invoke(null);
                Conexao.IniciaTransacao();
                objeto = bo.selecionar(objeto);
                objeto.FlagExcluido = true;
                valoresPadraoObjeto(ref objeto);
                bo.salvar(objeto);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao excluir " + objeto.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ":" + Environment.NewLine + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            BO bo = (BO)Type.GetType("LunarBase.ClassesBO." + objeto.GetType().Name + "BO").GetConstructor(System.Type.EmptyTypes).Invoke(null);
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionar(objeto);
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao selecionar " + objeto.GetType().Name + ":" + Environment.NewLine + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public ObjetoPadrao selecionarViaApi(ObjetoPadrao objeto)
        {
            BO bo = (BO)Type.GetType("LunarBase.ClassesBO." + objeto.GetType().Name + "BO").GetConstructor(System.Type.EmptyTypes).Invoke(null);

            bool isNewTransaction = false;
            if (Conexao.Transaction == null || !Conexao.Transaction.IsActive)
            {
                Console.WriteLine("Iniciando nova transação no selecionar.");
                Conexao.IniciaTransacao();
                isNewTransaction = true;
            }
            else
            {
                Console.WriteLine("Usando transação existente no selecionar.");
            }

            try
            {
                Console.WriteLine($"Selecionando objeto do tipo {objeto.GetType().Name}.");
                return bo.selecionar(objeto);
            }
            catch (Exception e)
            {
                if (isNewTransaction)
                {
                    Conexao.RollBack();
                }
                Console.WriteLine($"Erro ao selecionar {objeto.GetType().Name}: {e.Message}");
                throw new Exception("Erro ao selecionar " + objeto.GetType().Name + ":" + Environment.NewLine + e.Message);
            }
            finally
            {
                if (isNewTransaction)
                {
                    Conexao.FechaConexaoBD();
                }
            }
        }

        public IList<ObjetoPadrao> selecionarTodos(ObjetoPadrao objeto)
        {
            BO bo = (BO)Type.GetType("LunarBase.ClassesBO." + objeto.GetType().Name + "BO").GetConstructor(System.Type.EmptyTypes).Invoke(null);
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodos(objeto.GetType().Name);
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao selecionar " + objeto.GetType().Name + "s:" + Environment.NewLine + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }

        }


        private void valoresPadraoObjeto(ref ObjetoPadrao objeto)
        {
            if (objeto.DataCadastro < Convert.ToDateTime("01/01/1900 00:00:00"))
            {
                objeto.DataCadastro = DateTime.Now;
                if (Sessao.usuarioLogado != null) objeto.OperadorCadastro = Sessao.usuarioLogado.Id.ToString();
            }

            else if (objeto.FlagExcluido != true)
            {
                objeto.DataAlteracao = DateTime.Now;
                if (Sessao.usuarioLogado != null) objeto.OperadorAlteracao = Sessao.usuarioLogado.Id.ToString();
            }

            if (objeto.FlagExcluido == true)
            {
                objeto.DataExclusao = DateTime.Now;
                if (Sessao.usuarioLogado != null) objeto.OperadorExclusao = Sessao.usuarioLogado.Id.ToString();
            }
        }

        public void geraValoresPadrao()
        {
            ValoresPadraoBO valoresPadraoBO = new ValoresPadraoBO();
            try
            {
                valoresPadraoBO.gerarValoresPadrao();
            }
            catch (Exception erro)
            {
                //MessageBox.Show(erro.Message);
            }
        }
    }
}