using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Utils.Modal
{
    public static class FormModalHelper
    {
        public static DialogResult ShowModalWithBackground(Form modalForm)
        {
            using (var formBackground = new Form())
            {
                formBackground.StartPosition = FormStartPosition.Manual;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = true;
                formBackground.ShowInTaskbar = false;

                // Mostra o fundo e o formulário modal
                formBackground.Show();
                modalForm.StartPosition = FormStartPosition.CenterParent;
                modalForm.FormBorderStyle = FormBorderStyle.FixedDialog;

                // Exibe o modal e aguarda o resultado
                DialogResult result = modalForm.ShowDialog(formBackground);

                // Dispose do fundo depois que o modal for fechado
                formBackground.Dispose();

                return result;
            }
        }
    } 
}