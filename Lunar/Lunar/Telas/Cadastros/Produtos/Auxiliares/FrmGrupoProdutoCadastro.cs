using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.IO;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    public partial class FrmGrupoProdutoCadastro : Form
    {
        bool showModal = false;
        ProdutoGrupo grupo = new ProdutoGrupo();

        public DialogResult showModalNovo(ref object grupo)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                grupo = this.grupo;
            }
            return DialogResult;
        }
        public FrmGrupoProdutoCadastro()
        {
            InitializeComponent();
        }
        public FrmGrupoProdutoCadastro(ProdutoGrupo grupo)
        {
            InitializeComponent();
            this.grupo = grupo;
            get_Grupo();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void set_Grupo()
        {
            try
            {
                grupo = new ProdutoGrupo();
                if (!String.IsNullOrEmpty(txtCodigo.Texts))
                    grupo.Id = int.Parse(txtCodigo.Texts);
                else
                    grupo.Id = 0;
                grupo.Descricao = txtGrupo.Texts;
                grupo.Empresa = Sessao.empresaFilialLogada.Empresa;
                grupo.Food = false;
                if (chkFood.Checked == true)
                    grupo.Food = true;
                grupo.CaminhoImagem = txtCaminhoImagem.Texts;

                Controller.getInstance().salvar(grupo);
                GenericaDesktop.ShowInfo("Registrado com sucesso");
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void get_Grupo()
        {
            txtCodigo.Texts = grupo.Id.ToString();
            txtGrupo.Texts = grupo.Descricao;
            txtCaminhoImagem.Texts = grupo.CaminhoImagem;
            if (grupo.Food == true)
                chkFood.Checked = true;
            else
                chkFood.Checked = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_Grupo();
            if (showModal)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void FrmGrupoProdutoCadastro_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnSalvar.PerformClick();
                    break;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro?"))
            {
                this.Close();
            }
        }

        private void btnPesquisarImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Selecione uma Imagem",
                Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string caminhoImagemSelecionada = openFileDialog.FileName;
                txtCaminhoImagem.Texts = caminhoImagemSelecionada;
                string diretorioDestino = "";

                if (!String.IsNullOrEmpty(Sessao.parametroSistema.CaminhoAnexo))
                {
                    diretorioDestino = Sessao.parametroSistema.CaminhoAnexo;

                    if (!Directory.Exists(diretorioDestino))
                    {
                        Directory.CreateDirectory(diretorioDestino);
                    }

                    string nomeArquivoSequencial = GerarNomeArquivoSequencial(diretorioDestino);
                    string extensaoArquivo = Path.GetExtension(caminhoImagemSelecionada);
                    string caminhoDestino = Path.Combine(diretorioDestino, nomeArquivoSequencial + extensaoArquivo);
                    File.Copy(caminhoImagemSelecionada, caminhoDestino, true);
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Configure nos parâmetros do sistema um caminho para os anexos do sistema!");
                }
            }
        }
        private string GerarNomeArquivoSequencial(string diretorioDestino)
        {
            // Obtém todos os arquivos no diretório de destino que começam com "Grupo_"
            var arquivosExistentes = Directory.GetFiles(diretorioDestino, "Grupo_*.*");

            int maiorNumero = 0;

            foreach (var arquivo in arquivosExistentes)
            {
                string nomeArquivo = Path.GetFileNameWithoutExtension(arquivo);
                if (nomeArquivo.StartsWith("Grupo_"))
                {
                    string numeroStr = nomeArquivo.Substring(6); // Pega a parte depois de "Grupo_"
                    if (int.TryParse(numeroStr, out int numero))
                    {
                        if (numero > maiorNumero)
                        {
                            maiorNumero = numero;
                        }
                    }
                }
            }
            return $"Grupo_{maiorNumero + 1}";
        }


    }
}
