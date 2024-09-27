using Lunar.Telas.Vendas.Adicionais;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lunar.Utils.Modal
{
    public class ProdutoGradeHelper
    {
        private ProdutoGradeController _produtoGradeController;

        public ProdutoGradeHelper()
        {
            _produtoGradeController = new ProdutoGradeController();
        }

        public ProdutoGrade ObterOuSelecionarGrade(Produto produto)
        {
            IList<ProdutoGrade> listaGrade = _produtoGradeController.selecionarGradePorProduto(produto.Id);

            // Se há apenas uma grade, retornamos essa
            if (listaGrade.Count == 1)
            {
                return listaGrade.FirstOrDefault();
            }

            // Se há mais de uma grade, obrigamos o usuário a escolher
            else if (listaGrade.Count > 1)
            {
                ProdutoGrade gradeSelecionada = null;

                // Continua mostrando o formulário até que uma grade seja selecionada
                while (gradeSelecionada == null)
                {
                    using (var frmSelecionarGrade = new FrmSelecionarGrade(produto))
                    {
                        // Exibe o formulário modal
                        if (FormModalHelper.ShowModalWithBackground(frmSelecionarGrade) == DialogResult.OK)
                        {
                            gradeSelecionada = frmSelecionarGrade.GradeSelecionada; // Obtem a grade selecionada

                            // Se a grade for válida, saímos do loop
                            if (gradeSelecionada != null)
                            {
                                return gradeSelecionada;
                            }
                        }
                        else
                        {
                            // Se o usuário cancelar a operação (fechar o formulário), você pode definir a ação aqui.
                            MessageBox.Show("É necessário selecionar uma grade para continuar.", "Seleção Obrigatória", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }

            return null;
        }


    }
}
