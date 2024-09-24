using LunarBase.Classes;
using LunarBase.ConexaoBD;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using NHibernate;
using System;
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

            if (!Properties.Settings.Default.Script || (File.Exists(caminhoArquivo)))
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
                    if (!Properties.Settings.Default.Script)
                    {
                        if (Sessao.usuarioLogado.GrupoUsuario.Id == 1)
                        {
                            // Faz a atualização das permissões
                            // Sessao.usuarioLogado.GrupoUsuario.Permissoes = "OzE7MjszOzQ7NTs2Ozc7ODs5OzEwOzExOzEyOzEzOzE0OzE1OzE2OzE3OzMwOzMxOzMyOzMzOzM0OzM1OzM2OzM3OzM4OzM5OzQwOzQxOzQzOzQ0OzQ1OzYwOzYxOzYyOzYzOzY0OzY1OzY4OzY5OzcwOzcxOzcyOzEwMDsxMDE7MTAyOzEwMzsxMDQ7MTA1OzEwNjsxMDc7MTA4OzIwMDsyMDE7MjAyOzIwMzsyMDQ7MjA1OzIwNjsyMDc7MzAwOzMwMQ==";
                            Sessao.usuarioLogado.GrupoUsuario.Permissoes = "OzE7MjszOzQ7NTs2Ozc7ODs5OzEwOzExOzEyOzEzOzE0OzE1OzE2OzE3OzMwOzMxOzMyOzMzOzM0OzM1OzM2OzM3OzM4OzM5OzQwOzQxOzQzOzQ0OzQ1OzYwOzYxOzYyOzYzOzY0OzY1OzY2OzY3OzY4OzY5OzcwOzcxOzcyOzEwMDsxMDE7MTAyOzEwMzsxMDQ7MTA1OzEwNjsxMDc7MTA4OzIwMDsyMDE7MjAyOzIwMzsyMDQ7MjA1OzIwNjsyMDc7MzAwOzMwMQ==";
                            // Marca a configuração como atualizada
                            Properties.Settings.Default.Script = true;
                            Properties.Settings.Default.Save();

                            // Salva a mudança no banco de dados
                            Controller.getInstance().salvar(Sessao.usuarioLogado.GrupoUsuario);
                        }
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
        }
    }
}