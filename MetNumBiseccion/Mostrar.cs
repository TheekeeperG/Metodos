using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetNumBiseccion
{
    public partial class Mostrar : Form
    {
        public Mostrar(double f, double er , int ite, double c,string tit)
        {
            InitializeComponent();
            F.Text = f.ToString();
            E.Text = er.ToString()+" %";
            i.Text = ite.ToString();
            C.Text = c.ToString();
            Form1 Fp = new Form1();
            Fp.Enabled = false;
            TITULO1.Text = tit;
        }

        private void Mostrar_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
