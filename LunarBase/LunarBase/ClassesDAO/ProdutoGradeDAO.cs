using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class ProdutoGradeDAO : BaseDAO
    {
        public IList<ProdutoGrade> selecionarGradePorProduto(int idProduto)
        {
            Session = Conexao.GetSession();
            String sql = "FROM ProdutoGrade as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.Produto = " + idProduto;
            IList<ProdutoGrade> retorno = Session.CreateQuery(sql).List<ProdutoGrade>();
            return retorno;
        }

        public IList<ProdutoGradeDTO> selecionarGradeComBarrasPorProduto(int idProduto)
        {
            using (var session = Conexao.GetSession())
            {
                string hql = @"
            select distinct pg.Id, pg.Descricao, pg.UnidadeMedida, pg.QuantidadeMedida, pg.ValorVenda, pcb.CodigoBarras, pg.Principal 
            from ProdutoCodigoBarras pcb
            join pcb.ProdutoGrade pg
            where pg.FlagExcluido <> true and pg.Produto.Id = :idProduto";

                var query = session.CreateQuery(hql);
                query.SetParameter("idProduto", idProduto);

                var result = query.List<object[]>();

                var listaGrade = new List<ProdutoGradeDTO>();

                foreach (var item in result)
                {
                    var dto = new ProdutoGradeDTO
                    {
                        Id = (int)item[0],
                        Descricao = (string)item[1],
                        UnidadeMedida = (UnidadeMedida)item[2],
                        QuantidadeMedida = (double)item[3],
                        ValorVenda = (decimal)item[4],
                        CodigoBarras = (string)item[5],
                        Principal = (bool)item[6]
                    };
                    listaGrade.Add(dto);
                }

                return listaGrade;
            }
        }

        public class ProdutoGradeDTO
        {
            public int Id { get; set; }
            public string Descricao { get; set; }
            public string CodigoBarras { get; set; }
            public UnidadeMedida UnidadeMedida { get; set; }
            public double QuantidadeMedida { get; set; }
            public decimal ValorVenda { get; set; }
            public bool Principal { get; set; }
        }
    }
}
