using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Utilidades.Generadores
{
    public readonly struct NumeroAleatorio
    {
        public NumeroAleatorio(int x, double numero)
        {
            Xm = x;
            Numero = numero;
        }

        public int Xm { get; }
        public double Numero { get; }
    }
}