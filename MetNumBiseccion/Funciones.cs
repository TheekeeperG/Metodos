using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetNumBiseccion
{
    class Funciones
    {
        public static double Paracaidista(double g, double m, double c, double v, double t)
        {

            double Eu = Math.Exp(-1 * ((c / m) * t));
            double resultado = 0;
            resultado = ((g * m) / c) * (1 - Eu) - v;
            return resultado;
        }/*
        public static Biseccion ParConstBisec(double c1, double c2, double c3,double f1,double f2, double f3) {
            Biseccion cons = new Biseccion();
            if ((f1 * f3) < 0) {
                cons
            }
            else {

            }
             
            return cons; 
        }
        */
    }
}
