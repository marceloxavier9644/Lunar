using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;

namespace Lunar.Telas.OrdensDeServico.Servicos
{
    public partial class FrmServicoCadastro : Form
    {
        bool showModal = false;
        Servico servico = new Servico();
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        public DialogResult showModalNovo(ref object servico)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                servico = this.servico;
            }
            return DialogResult;
        }
        public FrmServicoCadastro()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            CarregarServicos();
        }

        public FrmServicoCadastro(Servico servico)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.servico = servico;
            CarregarServicos();
            get_Servico();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void set_Servico()
        {
            try
            {
                servico = new Servico();
                if (!String.IsNullOrEmpty(txtCodigo.Texts))
                    servico.Id = int.Parse(txtCodigo.Texts);
                else
                    servico.Id = 0;
                servico.Descricao = txtDescricao.Texts;
                servico.Filial = Sessao.empresaFilialLogada;
                if (!String.IsNullOrEmpty(txtValor.Texts))
                    servico.Valor = decimal.Parse(txtValor.Texts);
                else
                    servico.Valor = 0;

                if (comboItemServico.SelectedItem != null)
                {
                    var selectedItem = (ComboItemServico)comboItemServico.SelectedItem;
                    string codigoOriginal = selectedItem.Value;
                    if (codigoOriginal.Length > 4)
                        codigoOriginal = codigoOriginal.Substring(0, 5);
                    string codigoFormatado = codigoOriginal.Trim().Replace(".", "").PadLeft(4, '0');
                    servico.CodigoServicoNfse = codigoFormatado;
                }
                servico.AliquotaIss = txtAliquotaIss.Texts;

                Controller.getInstance().salvar(servico);
                GenericaDesktop.ShowInfo("Registrado com sucesso");
                txtDescricao.Texts = "";
                txtCodigo.Texts = "";
                txtValor.Texts = "";
                servico = new Servico();
                txtDescricao.Focus();
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }
        private string ConverterCodigoParaSalvo(string codigo)
        {
            // Verifica se o código tem o comprimento esperado (4 caracteres)
            if (codigo.Length == 4)
            {
                // Verifica se o primeiro dígito é 0
                if (codigo[0] == '0')
                {
                    // Remove o zero inicial e coloca o ponto
                    string parteAntes = codigo.Substring(1, 1); // O próximo dígito
                    string parteDepois = codigo.Substring(2); // Os dois últimos dígitos
                    return $"{parteAntes}.{parteDepois}";
                }
                else
                {
                    // Para códigos que não começam com 0, apenas adiciona o ponto
                    string parteAntes = codigo.Substring(0, 2); // Os dois primeiros dígitos
                    string parteDepois = codigo.Substring(2); // Os dois últimos dígitos
                    return $"{parteAntes}.{parteDepois}";
                }
            }

            // Caso o código não tenha o formato esperado, retorna o original
            return codigo;
        }
        private void get_Servico()
        {
            txtCodigo.Texts = servico.Id.ToString();
            txtDescricao.Texts = servico.Descricao;
            txtValor.Texts = string.Format("{0:0.00}",servico.Valor);
            txtAliquotaIss.Texts = servico.AliquotaIss;

            string codigoServicoCombo = ConverterCodigoParaSalvo(servico.CodigoServicoNfse);

            for (int i = 0; i < comboItemServico.Items.Count; i++)
            {
                var item = (ComboItemServico)comboItemServico.Items[i];
                if (item.Value.ToString().Contains(codigoServicoCombo)) 
                {
                    comboItemServico.SelectedIndex = i;
                    return;
                }
            }
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            try
            {
                txtValor.Texts = string.Format("{0:0.00}", decimal.Parse(txtValor.Texts));
            }
            catch
            {
           
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_Servico();
            //if (showModal)
            //{
            //    DialogResult = DialogResult.OK;
            //}
        }

        private void FrmServicoCadastro_KeyDown(object sender, KeyEventArgs e)
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

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                btnSalvar.PerformClick();
            }
        }

        private void txtDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtValor.Focus();
            }
        }

        private void CarregarServicos()
        {
            string exePath = Application.StartupPath;
            string jsonPath = Path.Combine(exePath, "codigo_servicos.json");

            string jsonContent = File.ReadAllText(jsonPath);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Deserializa diretamente em um Dictionary
            var categorias = JsonSerializer.Deserialize<Dictionary<string, ItemServicoNfse>>(jsonContent, options);

            // Adicionar os serviços no ComboBox
            if (categorias != null)
            {
                foreach (var categoria in categorias)
                {
                    foreach (var servico in categoria.Value.Servicos)
                    {
                        // Limitar o texto a 20 caracteres
                        string nomeServico = servico.Value.Length > 39 ? servico.Value.Substring(0, 39) + "..." : servico.Value;
                        // Concatenar código e texto
                        string itemTexto = $"{servico.Key} - {nomeServico}";
                        comboItemServico.Items.Add(new ComboItemServico(servico.Key, itemTexto));
                    }
                }
            }
            else
            {
                Console.WriteLine("Deserialização retornou null.");
            }
        }

        private void txtAliquotaIss_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtAliquotaIss.Texts, e);
        }
    }
}
