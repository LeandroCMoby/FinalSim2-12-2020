using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulacion.App.Componentes
{
    public partial class IntegerBox : UserControl
    {
        public IntegerBox()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public int Integer {
            get
            {
                var result = 0;
                int.TryParse(this.textBox1.Text, out result);
                return result;
            } 
            
            set {
                this.textBox1.Text = value.ToString();
            }
        }

       
    }
}
