using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.Balancas
{
    public class ItemMGV6
    {
        public string CodigoDepartamento { get; set; }  // DD - 2 bytes
        public string EtiquetaDepartamento { get; set; } // EE - 2 bytes
        public string TipoProduto { get; set; }  // O - 1 byte
        public string CodigoItem { get; set; }  // CCCCCCC - 6 bytes
        public decimal Preco { get; set; }  // PPPPPP - 6 bytes
        public string DiasValidade { get; set; }  // VVV - 3 bytes
        public string Descritivo { get; set; }  // Nome do produto - 25 bytes (ou mais)
        public string OutrosCampos { get; set; }  // Placeholder para os zeros e valores não especificados.
        public string Separador { get; set; }  // Separador do final, por exemplo, "|01|"}

    }
}
