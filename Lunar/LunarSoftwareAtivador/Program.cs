using System;
using System.Windows.Forms;

namespace LunarSoftwareAtivador
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzA1NDk1QDMxMzkyZTM0MmUzMEpUTFdHNW03NHVveVltU0wzdzhsMDRzOTgwdGx0M3pPZmlMc3QydE9hb2M9");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmAtivador());
        }
    }
}
