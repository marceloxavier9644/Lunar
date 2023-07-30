using FastReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.FastReportTestes
{
    public partial class FrmFastTest : Form
    {
        public FrmFastTest()
        {
            InitializeComponent();
        }

        private void gerarRelatorio()
        {
            Report report = new Report();
            report.Load("your_report.frx");
            report.RegisterData(dsOrdemServico);
            


        }
    }
}
