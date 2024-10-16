using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    public partial class FrmCaracteristicaPessoa : Form
    {
        private IList<PessoaCaracteristica> listaVariacoes = new List<PessoaCaracteristica>();
        bool editarVariacao = false;
        GenericaDesktop generica = new GenericaDesktop();
        public FrmCaracteristicaPessoa()
        {
            InitializeComponent();
            CarregarVariacoesNoGrid();
        }

        private void btnAdicionarVariacao_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDescricaoVariacao.Texts) && (!string.IsNullOrEmpty(txtOrdem.Texts)))
            {
                var caracteristicaPessoa = new PessoaCaracteristica
                {
                    Id = 0,
                    Descricao = txtDescricaoVariacao.Texts.Trim(),
                    Ordenacao = int.Parse(txtOrdem.Texts)
                };

                if (editarVariacao)
                {
                    // Atualiza o item existente na lista
                    var itemToUpdate = txtDescricaoVariacao.Tag as PessoaCaracteristica;
                    if (itemToUpdate != null)
                    {
                        var index = listaVariacoes.IndexOf(itemToUpdate);
                        if (index >= 0)
                        {
                            // Copia o Id do item original para o novo objeto
                            caracteristicaPessoa.Id = itemToUpdate.Id;

                            // Salva no banco de dados (atualização)
                            Controller.getInstance().salvar(caracteristicaPessoa);

                            // Atualiza a lista local
                            listaVariacoes[index] = caracteristicaPessoa;
                        }
                    }
                    txtDescricaoVariacao.Focus();
                    txtDescricaoVariacao.SelectAll();
                    editarVariacao = false; // Reseta o estado de edição
                }
                else
                {
                    // Salva no banco de dados (inserção)
                    Controller.getInstance().salvar(caracteristicaPessoa);

                    // Adiciona o novo item na lista
                    listaVariacoes.Add(caracteristicaPessoa);
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
            PessoaCaracteristicaController caracteristicaController = new PessoaCaracteristicaController();
            try
            {
                ObjetoPadrao caract = new PessoaCaracteristica();
                IList<ObjetoPadrao> listaVariacoesPadrao = caracteristicaController.selecionarTodos(caract);
                listaVariacoes = listaVariacoesPadrao.Cast<PessoaCaracteristica>().ToList();

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

            if (selectedRow is PessoaCaracteristica caracteristica)
            {
                txtDescricaoVariacao.Texts = caracteristica.Descricao;
                txtCodVariacao.Texts = caracteristica.Id.ToString();
                txtOrdem.Texts = caracteristica.Ordenacao.ToString();
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (gridVariacao.SelectedItem != null)
            {
                var selectedRow = gridVariacao.SelectedItem;

                if (selectedRow is PessoaCaracteristica caracteristica)
                {
                    if (GenericaDesktop.ShowConfirmacao("Deseja realmente excluir a caracteristica " + caracteristica.Descricao + "?"))
                    {
                        Controller.getInstance().excluir(caracteristica);
                        CarregarVariacoesNoGrid();
                    }
                }
            }
            else
                GenericaDesktop.ShowAlerta("Primeiro você deve clicar na característica que deseja excluir!");
        }
    }
}
