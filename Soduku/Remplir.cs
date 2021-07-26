using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soduku
{
    public partial class Remplir : Form
    {
        Button b;
        public Remplir(Button b)
        {
            this.b = b;
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int convert =0;
            try
            {
                if(textBox1.Text!="")
                {
                    convert = int.Parse(textBox1.Text);
                    if (convert < 1 || convert > 9)
                        throw new Exception("");
                }
            }
            catch (Exception)
            {

                return;
            }

            if(e.KeyChar==13)
            {
                b.Text = textBox1.Text;
                this.Close();
            }
        }
    }
}
