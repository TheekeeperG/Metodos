using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MetNumBiseccion
{
    public partial class Form1 : Form
    {  
        private bool[] textoV=new bool[4]; 
        private double masa = 0;
        private double velocidad = 0;
        private double cons1 = .1;
        private double cons2 = 1;
        private double cons3 = 0;
        private double acons3 = 0; 
        private double errorEs = 0;
        private double errorEp = 100;
        private const double gravedad = 9.8;
        private double tiempo = 0;
        private double funcC1 = 0;
        private double funcC2 = 0;
        private double funcC3 = 0;
        private int i = 0;
        private bool cont = true;


  
        public Form1()
        {
            InitializeComponent();
            for (int m=0;i<4;i++) textoV[i] = false;
       
        }


        //Devolver a la configuracion predeterminada
        private void todoACero()
        {
            masa = 0;
            velocidad = 0;
            cons1 = .1;
            cons2 =1;
            cons3 = 0;
            acons3 = 0;
            errorEs = 0;
            errorEp = 100;
            tiempo = 0;
            funcC1 = 0;
            funcC2 = 0;
            funcC3 = 0;
            i = 0;
            cont = true;
        }
        //Encontrar el valor de c2
        public double EC(double cons1, double cons2, double g, double m, double v, double t)
        {
            funcC1 = Funciones.Paracaidista(gravedad, masa, cons1, velocidad, tiempo);
            funcC2 = Funciones.Paracaidista(gravedad, masa, cons2, velocidad, tiempo);
            while (funcC1 * funcC2 > 0)
            {
                cons2++;
                funcC2 = Funciones.Paracaidista(gravedad, masa, cons2, velocidad, tiempo);
            }
            return cons2;
        }
        private void button1_Click(object sender, EventArgs e)
        {
         
                //Valores iniciales
                masa = Convert.ToDouble(txbMasa.Text);
                velocidad = Convert.ToDouble(txbVelocidad.Text);
                errorEs = Convert.ToDouble(txbError.Text);
                tiempo = Convert.ToDouble(txbTiempo.Text);


            if (radioButton1.Checked == true)
            {
                cons2 = EC(cons1, cons2, gravedad, masa, velocidad, tiempo);
                cons3 = (cons1 + cons2) / 2;
                funcC1 = Funciones.Paracaidista(gravedad, masa, cons1, velocidad, tiempo);
                funcC2 = Funciones.Paracaidista(gravedad, masa, cons2, velocidad, tiempo);
                funcC3 = Funciones.Paracaidista(gravedad, masa, cons3, velocidad, tiempo);
                acons3 = cons2;

                //Analisís del par de datos correcto
                if ((funcC3 * funcC2) < 0) cons1 = cons3;
                else if (funcC3 == funcC2 || funcC3 == funcC1) cont = false;
                else cons2 = cons3;
                i++;

                while (errorEs < errorEp && cont == true && i < 500)
                {
                    acons3 = cons3;
                    cons3 = (cons1 + cons2) / 2;
                    errorEp = Math.Abs(((cons3 - acons3) / cons3) * 100);
                    funcC3 = Funciones.Paracaidista(gravedad, masa, cons3, velocidad, tiempo);

                    if ((funcC3 * funcC2) < 0) cons1 = cons3;
                    else if (funcC3 == funcC2 || funcC3 == funcC1) cont = false;
                    else cons2 = cons3;
                    i++;
                }
               
                if (!(i < 500)) MessageBox.Show("No se pudó encontrar la raíz a través de este método.", "Método incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    funcC3 = Funciones.Paracaidista(gravedad, masa, cons3, velocidad, tiempo);
                    Mostrar Fm = new Mostrar((Math.Truncate(funcC3 * 100000) / 100000), (Math.Truncate(errorEp * 100) / 100), i, (Math.Truncate(cons3 * 1000) / 1000), Titulo.Text);
                    Fm.ShowDialog();
                    todoACero();
                }
                todoACero();
            }
            else if (radioButton2.Checked == true)
            {
                while (errorEp > errorEs && i < 500 && errorEp!=0)
                {
                    acons3 = cons3;
                    funcC1 = Funciones.Paracaidista(gravedad, masa, cons1, velocidad, tiempo);
                    funcC2 = Funciones.Paracaidista(gravedad, masa, cons2, velocidad, tiempo);
                    cons3 = cons2 - (((funcC2) * (cons1 - cons2)) / (funcC1 - funcC2));
                    cons2 = cons1;
                    cons1 = cons3;
                    funcC3 = Funciones.Paracaidista(gravedad, masa, cons3, velocidad, tiempo);
                    errorEp = Math.Abs(funcC3 * 100);
                    i++;                  
                }
              
                if (!(i < 500))
                {
                    MessageBox.Show("No se pudó encontrar la raíz a través de este método.", "Método incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    todoACero();
                }
                else {
                    funcC3 = Funciones.Paracaidista(gravedad, masa, cons3, velocidad, tiempo);
                    Mostrar Fm = new Mostrar((Math.Truncate(funcC3 * 100000) / 100000), (Math.Truncate(errorEp * 100) / 100), i, (Math.Truncate(cons3 * 1000) / 1000), Titulo.Text);
                    Fm.ShowDialog();
                   
                }
                todoACero();
            }
            else {
                cons1 = 1;
                while (errorEp > errorEs && i < 1000 &&errorEp!=0) {
                    acons3 = cons1;
                    funcC1 = Funciones.Paracaidista(gravedad, masa, cons1, velocidad, tiempo)+cons1;
                    cons1 = funcC1;
                    errorEp = Math.Abs(((cons1 - acons3) / cons1) * 100);
                    i++;

                }
               
                if (!(i < 1000)) MessageBox.Show("No se pudó encontrar la raíz a través de este método.", "Método incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else {
                    funcC3 = Funciones.Paracaidista(gravedad, masa, cons1, velocidad, tiempo);

                    Mostrar Fm = new Mostrar((Math.Truncate(funcC3 * 100000) / 100000), (Math.Truncate(errorEp * 100) / 100), i, (Math.Truncate(cons1 * 1000) / 1000), Titulo.Text);
                    Fm.ShowDialog();
                   
                }
                todoACero();
            }
        }




        private void txbMasa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                return;
            }
           
           
        }

        private void txbMasa_TextChanged(object sender, EventArgs e)
        {
            string pattern = "(^([0-9]{1,20}[.]{0,1}[0-9]{0,20})?$)";
            string s = txbMasa.Text;
            if ((Regex.IsMatch(s, pattern)) && txbMasa.Text != "") { textoV[0] = true;  }
            else textoV[0] = false;
            validar();
        }

        private void txbTiempo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txbTiempo_TextChanged(object sender, EventArgs e)
        {
            string pattern = "(^([0-9]{0,20}[.]{0,1}[0-9]{0,20})?$)";
            string s = txbTiempo.Text;
            if ((Regex.IsMatch(s, pattern)) && txbTiempo.Text != "") { textoV[1] = true;  }
            else textoV[1] = false;
            validar();
        }

        private void txbVelocidad_TextChanged(object sender, EventArgs e)
        {
            string pattern = "(^([0-9]{0,20}[.]{0,1}[0-9]{0,20})?$)";
            string s = txbVelocidad.Text;
            if ((Regex.IsMatch(s, pattern)) && txbVelocidad.Text != "") { textoV[2] = true; }
            else textoV[2] = false;
            validar();
        }

        private void txbError_TextChanged(object sender, EventArgs e)
        {
            string pattern = "(^([0-9]{1,20}[.]{0,1}[0-9]{0,20})?$)";
            string s = txbError.Text;
            if ((Regex.IsMatch(s, pattern)) && txbError.Text != "") { textoV[3] = true;  }
            else textoV[3] = false;
            validar();
        }

        private void txbVelocidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txbError_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Enter) && (e.KeyChar != (char)Keys.Space) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                return;
            }
        }
        private void validar() {
            if (textoV[0] && textoV[1] && textoV[2] && textoV[3]) button1.Enabled = true;
            else button1.Enabled = false; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Titulo.Text = "METODOS NÚMERICOS: BISECCIÓN";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Titulo.Text = "METODOS NÚMERICOS: SECANTE";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Titulo.Text = "METODOS NÚMERICOS: PUNTO FIJO";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
