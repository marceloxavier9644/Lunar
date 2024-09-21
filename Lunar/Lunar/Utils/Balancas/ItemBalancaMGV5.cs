using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.Balancas
{
    public class ItemBalancaMGV5
    {
        public string CodigoDepartamento { get; set; }  // DD - 2 bytes
        public string EtiquetaDepartamento { get; set; } // EE - 2 bytes
        public string TipoProduto { get; set; }  // O - 1 byte
        public string CodigoItem { get; set; }  // CCCCCCC - 6 bytes
        public decimal Preco { get; set; }  // PPPPPP - 6 bytes
        public int DiasValidade { get; set; }  // VVV - 3 bytes
        public string Descritivo1 { get; set; }  // D1 - 25 bytes
        public string Descritivo2 { get; set; }  // D2 - 25 bytes
        public string Receita1 { get; set; }  // R1 - 50 bytes
        public string Receita2 { get; set; }  // R2 - 50 bytes
        public string Receita3 { get; set; }  // R3 - 50 bytes
        public string Receita4 { get; set; }  // R4 - 50 bytes
        public string Receita5 { get; set; }
    }
}
