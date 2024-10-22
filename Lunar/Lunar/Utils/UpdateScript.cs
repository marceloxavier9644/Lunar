using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ConexaoBD;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using NHibernate;
using System;
using System.Diagnostics;
using System.IO;

namespace Lunar.Utils
{

    public class UpdateScript
    {
        private ISession session;

        public UpdateScript()
        {
            // Criar a conexão diretamente
            Conexao conexao = new Conexao();
            this.session = Conexao.GetSession();
        }
        public void ExecutarScript()
        {
            string diretorioAtual = AppDomain.CurrentDomain.BaseDirectory;
            string nomeArquivo = "update.txt";
            string caminhoArquivo = Path.Combine(diretorioAtual, nomeArquivo);
            ProdutoDAO produtoDAO = new ProdutoDAO();
            produtoDAO.CriarIndiceDescricao();
            if ((File.Exists(caminhoArquivo)))
            {
                Logger logger = new Logger();
                try
                {
                    session.BeginTransaction();
                    logger.WriteLog("INICIO SCRIPT ATUALIZACAO", "LOG");

                    // Verificar se a tabela ProdutoGrade existe
                    string verificarTabelaSQL = "SELECT COUNT(*) FROM information_schema.tables WHERE table_name = 'ProdutoGrade'";
                    IQuery verificarTabelaQuery = session.CreateSQLQuery(verificarTabelaSQL);
                    int tabelaExiste = Convert.ToInt32(verificarTabelaQuery.UniqueResult());

                    if (tabelaExiste == 0)
                    {
                        // Executar atualização se a tabela não existir
                        Controller.getInstanceAtualiza();
                    }

                    // Verificar se há registros na tabela ProdutoGrade, se a tabela já existir
                    string verificarSQL = "SELECT COUNT(*) FROM ProdutoGrade";
                    IQuery verificarQuery = session.CreateSQLQuery(verificarSQL);
                    int count = Convert.ToInt32(verificarQuery.UniqueResult());

                    if (count == 0)
                    {
                        logger.WriteLog("INSERIR REGISTROS NOVOS NO BANCO ATU280724", "LOG");

                        // Inserir registros na tabela ProdutoGrade
                        string inserirProdutoGradeSQL = @"
                INSERT INTO ProdutoGrade (Produto, ValorVenda, QuantidadeMedida, Principal, UnidadeMedida, OperadorCadastro, DataCadastro, Descricao, FlagExcluido)
                SELECT p.Id, p.ValorVenda, 1, true, p.UnidadeMedida, '1', NOW(), 'PRINCIPAL', false
                FROM Produto p
                LEFT JOIN UnidadeMedida um ON p.UnidadeMedida = um.Id
                WHERE p.Id NOT IN (SELECT Produto FROM ProdutoGrade);";

                        // Inserir registros na tabela ProdutoCodigoBarras
                        string inserirProdutoCodigoBarrasSQL = @"
                INSERT INTO ProdutoCodigoBarras (CodigoBarras, ProdutoGrade, OperadorCadastro, DataCadastro, FlagExcluido, Produto)
                SELECT p.Ean, pg.Id, '1', NOW(), false, p.Id
                FROM Produto p
                JOIN ProdutoGrade pg ON pg.Produto = p.Id
                WHERE p.Ean IS NOT NULL AND p.Ean != ''
                    AND NOT EXISTS (
                        SELECT 1 
                        FROM ProdutoCodigoBarras pcb
                        WHERE pcb.CodigoBarras = p.Ean
                    );";

                        // Atualizar a tabela Produto
                        string atualizarProdutoSQL = @"
                UPDATE Produto p
                JOIN ProdutoGrade pg ON p.Id = pg.Produto
                SET p.GradePrincipal = pg.Id
                WHERE pg.Principal = true;";

                        // Executar os comandos
                        session.CreateSQLQuery(inserirProdutoGradeSQL).ExecuteUpdate();
                        session.CreateSQLQuery(inserirProdutoCodigoBarrasSQL).ExecuteUpdate();
                        session.CreateSQLQuery(atualizarProdutoSQL).ExecuteUpdate();
                        // Adicionar seu script de atualização para ordemservicoproduto

                        string atualizarOrdemServicoProdutoSQL = @"
                        UPDATE ordemservicoproduto osp
                        JOIN produto p ON osp.PRODUTO = p.id
                        JOIN produtograde pg ON pg.id = p.gradeprincipal
                        SET osp.produtograde = pg.id
                        WHERE osp.produtograde IS NULL;";
                        session.CreateSQLQuery(atualizarOrdemServicoProdutoSQL).ExecuteUpdate();

                        string atualizarVendaItensProdutoSQL = @"
                        UPDATE vendaitens osp
                        JOIN produto p ON osp.PRODUTO = p.id
                        JOIN produtograde pg ON pg.id = p.gradeprincipal
                        SET osp.produtograde = pg.id
                        WHERE osp.produtograde IS NULL;";
                        session.CreateSQLQuery(atualizarOrdemServicoProdutoSQL).ExecuteUpdate();

                        session.Transaction.Commit();
                        logger.WriteLog("SCRIPT ATUALIZACAO EXECUTADO COM SUCESSO", "LOG");
                        File.Delete(caminhoArquivo);
                    }

                    if (Sessao.parametroSistema.TipoCaixa == null)
                    {
                        Sessao.parametroSistema.TipoCaixa = "INDIVIDUAL";
                        Controller.getInstance().salvar(Sessao.parametroSistema);
                    }
             
                    GenericaDesktop ge = new GenericaDesktop();
                    ge.enviarEmailPeloLunar("marcelo.xs@hotmail.com", "Atualização Lunar 1.0.0.32", Sessao.empresaFilialLogada.NomeFantasia, Environment.MachineName + " Sistema atualizado em " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " Pelo Usuário: " + Sessao.usuarioLogado.Login, null);
                }
                catch (Exception ex)
                {
                    logger.WriteLog("ERRO SCRIPT ATUALIZACAO " + ex.Message, "LOG");
                    session.Transaction.Rollback();
                }
            }
            // Verifica se a versão do sistema é 1.0.0.39 e executa as atualizações no caixa
            VerificarVersaoEAtualizar();
        }

        private void VerificarVersaoEAtualizar()
        {
            string versaoAtual = ObterVersaoSistema(); // Método que obtém a versão do sistema
            string pastaSistema = AppDomain.CurrentDomain.BaseDirectory; // Diretório do sistema
            string pastaScripts = Path.Combine(pastaSistema, "Script"); // Caminho da pasta Script
            string arquivoVersao = Path.Combine(pastaScripts, $"{versaoAtual}.txt"); // Arquivo com o número da versão

            // Verifica se a pasta "Script" existe, caso contrário, cria a pasta
            if (!Directory.Exists(pastaScripts))
            {
                Directory.CreateDirectory(pastaScripts);
            }

            // Verifica se o arquivo de versão já existe
            if (File.Exists(arquivoVersao))
            {
                // O arquivo já existe, portanto, o update já foi feito
                return;
            }

            // Caso o arquivo não exista, executa o script de atualização
            ISession session = Conexao.GetSession(); // Cria uma nova sessão
            try
            {
                session.BeginTransaction();

                // Comandos de atualização do caixa para versao 39
                string updateCartaoDebito = "UPDATE caixa SET CARTAODEBITO = FALSE WHERE CARTAODEBITO IS NULL;";
                string updateCartaoCredito = "UPDATE caixa SET CARTAOCREDITO = FALSE WHERE CARTAOCREDITO IS NULL;";
                string updateParcelasCartao = "UPDATE caixa SET PARCELASCARTAO = 0 WHERE PARCELASCARTAO IS NULL;";

                // Comandos de atualização dos parametros para versao 40 E 41
                string updateParametro = "UPDATE parametrosistema SET TIPOIMPRESSORARELATORIOCAIXA = 'A4' WHERE TIPOIMPRESSORARELATORIOCAIXA IS NULL;";

                session.CreateSQLQuery(updateCartaoDebito).ExecuteUpdate();
                session.CreateSQLQuery(updateCartaoCredito).ExecuteUpdate();
                session.CreateSQLQuery(updateParcelasCartao).ExecuteUpdate();
                session.CreateSQLQuery(updateParametro).ExecuteUpdate();

                session.Transaction.Commit();

                // Após o sucesso do script, cria o arquivo de versão
                File.WriteAllText(arquivoVersao, $"Versão {versaoAtual} aplicada em {DateTime.Now}");
            }
            catch (Exception ex)
            {
                Logger logger = new Logger();
                logger.WriteLog("ERRO AO ATUALIZAR PARAMETROS 1.0.0.41: " + ex.Message, "LOG");
                session.Transaction.Rollback();
            }
            finally
            {
                if (session.IsOpen)
                {
                    session.Close(); // Fechar a sessão se ela ainda estiver aberta
                }
            }
        }

        private string ObterVersaoSistema()
        {
            // Obtém o caminho do executável atual
            string caminhoExecutavel = AppDomain.CurrentDomain.BaseDirectory + "Lunar.exe"; // Substitua 'SeuExecutavel.exe' pelo nome do seu executável

            // Verifica se o arquivo existe
            if (File.Exists(caminhoExecutavel))
            {
                // Obtém as informações de versão do arquivo
                var versionInfo = FileVersionInfo.GetVersionInfo(caminhoExecutavel);
                return versionInfo.FileVersion; // Retorna a versão do arquivo
            }
            else
            {
                return "0";
            }
        }
    }
}