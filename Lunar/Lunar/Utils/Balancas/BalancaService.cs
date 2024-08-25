using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.Balancas
{
    public class BalancaService
    {
        public void GerarArquivoItens(List<ItemBalanca> itens)
        {
            string caminhoAreaDeTrabalho = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string caminhoArquivo = Path.Combine(caminhoAreaDeTrabalho, "Itensmgv.txt");

            using (StreamWriter sw = new StreamWriter(caminhoArquivo))
            {
                foreach (var item in itens)
                {
                    string linha = item.CodigoDepartamento.PadLeft(2, '0') +
                                   item.TipoProduto.PadLeft(1, '0') +
                                   item.CodigoItem.PadLeft(6, '0') +
                                   item.Preco.PadLeft(6, '0') +
                                   item.DiasValidade.PadLeft(3, '0') +
                                   item.Descritivo1.PadRight(25) +
                                   item.Descritivo2.PadRight(25) +
                                   item.CodigoInfoExtra.PadLeft(6, '0') +
                                   item.CodigoImagem.PadLeft(4, '0') +
                                   item.CodigoInfoNutricional.PadLeft(6, '0') +
                                   item.DataValidade.PadLeft(1, '0') +
                                   item.DataEmbalagem.PadLeft(1, '0') +
                                   item.CodigoFornecedor.PadLeft(4, '0') +
                                   item.Lote.PadLeft(12, ' ') +
                                   item.CodigoEANEspecial.PadLeft(11, '0') +
                                   item.VersaoPreco.PadLeft(1, '0') +
                                   item.CodigoSom.PadLeft(4, '0') +
                                   item.CodigoTara.PadLeft(4, '0') +
                                   item.CodigoFracionador.PadLeft(4, '0') +
                                   item.CodigoCampoExtra1.PadLeft(4, '0') +
                                   item.CodigoCampoExtra2.PadLeft(4, '0') +
                                   item.CodigoConservacao.PadLeft(4, '0') +
                                   item.EANFornecedor.PadLeft(12, '0') +
                                   item.PercentualGlaciamento.PadLeft(6, '0') +
                                   "|DA|" + // Marcação de início dos departamentos associados
                                   item.Descritivo3.PadRight(35) +
                                   item.Descritivo4.PadRight(35) +
                                   item.CodigoCampoExtra3.PadLeft(6, '0') +
                                   item.CodigoCampoExtra4.PadLeft(6, '0') +
                                   item.CodigoMidia.PadLeft(6, '0') +
                                   item.PrecoPromocional.PadLeft(6, '0') +
                                   item.SolicitacaoFornecedor.PadLeft(1, '0') +
                                   "|" + item.CodigoFornecedorAssociado.PadLeft(4, '0') + "|" + // Associações de fornecedores
                                   item.SolicitacaoTara.PadLeft(1, '0') +
                                   "|" + item.BalançasInativas + "|" + // Sequência de balanças inativas
                                   item.CodigoEANEspecialG1.PadLeft(12, '0') +
                                   item.PercentualGlaciamentoPG.PadLeft(4, '0');

                    sw.WriteLine(linha);
                }
            }
        }
    }
}
