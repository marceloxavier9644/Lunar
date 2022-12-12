using Lunar.Utils;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.ArquivosContabilidade
{
    public partial class FrmEnviarArquivosContabilidade : Form
    {
        DateTime data;
        public FrmEnviarArquivosContabilidade()
        {
            InitializeComponent();
            data = DateTime.Today;
            data = data.AddMonths(-1);
            txtMes.Texts = data.Month.ToString().PadLeft(2, '0');
            if (data.Month.Equals("12"))
                data = data.AddYears(-1);
            txtAno.Texts = data.Year.ToString();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            envioArquivos();
        }

        private async void envioArquivos()
        {
            string localFilePath = txtPasta.Texts + @"\";
            string fileName = txtMes.Texts + txtAno.Texts + ".zip";
            if (!String.IsNullOrEmpty(txtPasta.Texts))
            {
                LunarApiNotas lunarApiNotas = new LunarApiNotas();
                var retor = await lunarApiNotas.coletarArquivosContabeis(Sessao.empresaFilialLogada.Cnpj, txtMes.Texts, txtAno.Texts);
                if (File.Exists(retor.ToString()))
                {
                    GenericaDesktop genericaDesktop = new GenericaDesktop();
                    if (!String.IsNullOrEmpty(txtEmail.Texts.Trim()))
                    {

                        List<string> listaAnexo = new List<string>();
                        listaAnexo.Add(localFilePath + fileName);
                        if (!String.IsNullOrEmpty(Sessao.parametroSistema.Email) && !String.IsNullOrEmpty(Sessao.parametroSistema.NomeRemetenteEmail))
                            genericaDesktop.enviarEmail(txtEmail.Texts.Trim(), "Arquivos Fiscais " + Sessao.empresaFilialLogada.NomeFantasia, txtMes.Texts + "/" + txtAno.Texts + "    " + Sessao.empresaFilialLogada.NomeFantasia + " CNPJ: " + Sessao.empresaFilialLogada.Cnpj, "Olá, segue arquivos em anexo. Este e-mail foi disparado pelo sistema Lunar Software, qualquer dúvida entre em contato com o responsável da empresa.", listaAnexo);
                        GenericaDesktop.ShowInfo("E-mail enviado com sucesso!");
                    }
                    GenericaDesktop.ShowInfo("Arquivo salvo com sucesso!");
                }
            }
            else
                GenericaDesktop.ShowAlerta("Selecione uma pasta para salvar os arquivos");
        }

        private void btnPesquisaPasta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                txtPasta.Texts = folder.SelectedPath;
            }
        }
    }
}
