using LunarBase.Classes;
using LunarBase.ConexaoBD;
using LunarBase.ControllerBO;
using NHibernate;
using System;

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
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog("ERRO SCRIPT ATUALIZACAO " + ex.Message, "LOG");
                session.Transaction.Rollback();
            }
        }
    }
}