using System;
using System.Windows.Forms;

namespace Lunar.RJ_UI.Classes
{
    public class UppercaseTextBox : UserControl
    {
        public Boolean Uppercase { get; set; }
        public override string Text
        {
            get
            {
                return Uppercase && !string.IsNullOrEmpty(base.Text) ? base.Text.ToUpper() : base.Text;
            }
            set
            {
                base.Text = Uppercase && !string.IsNullOrEmpty(value) ? value.ToUpper() : value;
            }
        }
    }
}