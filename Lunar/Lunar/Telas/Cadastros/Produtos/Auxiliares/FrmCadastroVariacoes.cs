using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    public partial class FrmCadastroVariacoes : Form
    {
        private IList<Caracteristica> listaVariacoes = new List<Caracteristica>();
        bool editarVariacao = false;
        GenericaDesktop generica = new GenericaDesktop();
        public FrmCadastroVariacoes()
        {
            InitializeComponent();
            CarregarVariacoesNoGrid();
        }

        private void btnAdicionarVariacao_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDescricaoVariacao.Texts) && (!string.IsNullOrEmpty(txtOrdem.Texts)))
                {
                var caracteristicaProduto = new Caracteristica
                {
                    Id = 0,
                    Descricao = txtDescricaoVariacao.Texts.Trim(),
                    Ordem = int.Parse(txtOrdem.Texts)
                };

                if (editarVariacao)
                {
                    // Atualiza o item existente na lista
                    var itemToUpdate = txtDescricaoVariacao.Tag as Caracteristica;
                    if (itemToUpdate != null)
                    {
                        var index = listaVariacoes.IndexOf(itemToUpdate);
                        if (index >= 0)
                        {
                            // Copia o Id do item original para o novo objeto
                            caracteristicaProduto.Id = itemToUpdate.Id;

                            // Salva no banco de dados (atualização)
                            Controller.getInstance().salvar(caracteristicaProduto);

                            // Atualiza a lista local
                            listaVariacoes[index] = caracteristicaProduto;
                        }
                    }
                    txtDescricaoVariacao.Focus();
                    txtDescricaoVariacao.SelectAll();
                    editarVariacao = false; // Reseta o estado de edição
                }
                else
                {
                    // Salva no banco de dados (inserção)
                    Controller.getInstance().salvar(caracteristicaProduto);

                    // Adiciona o novo item na lista
                    listaVariacoes.Add(caracteristicaProduto);
                }

                // Atualiza o DataSource do grid
                gridVariacao.DataSource = null;
                gridVariacao.DataSource = listaVariacoes;
                gridVariacao.Refresh();

                // Limpa os campos de texto
                txtCodVariacao.Texts = "";
                txtDescricaoVariacao.Texts = "";
                txtOrdem.Texts = "";
            }
            else
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
            private void CarregarVariacoesNoGrid()
            {
            CaracteristicaController caracteristicaController = new CaracteristicaController();
            try
            {
                ObjetoPadrao caract = new Caracteristica();
                IList<ObjetoPadrao> listaVariacoesPadrao = caracteristicaController.selecionarTodos(caract);
                listaVariacoes = listaVariacoesPadrao.Cast<Caracteristica>().ToList();

                gridVariacao.DataSource = listaVariacoes;
                gridVariacao.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar variações: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gridVariacao_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            var selectedRow = gridVariacao.SelectedItem;

            if (selectedRow is Caracteristica caracteristica)
            {
                txtDescricaoVariacao.Texts = caracteristica.Descricao;
                txtCodVariacao.Texts = caracteristica.Id.ToString();

                txtDescricaoVariacao.Tag = caracteristica;
                editarVariacao = true;
                txtDescricaoVariacao.Focus();
                txtDescricaoVariacao.SelectAll();
            }
        }

        private void txtOrdem_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtOrdem.Texts, e);
        }

        private void gridVariacao_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }
    }
}
