using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarImportador.ClassesImportadoras.SGBR_Sistemas
{
    public class ImportarProdutosSgbr
    {
        public event Action<string> OnStatusUpdate;
        public event Action<int> OnProgressUpdate;

    }
}
