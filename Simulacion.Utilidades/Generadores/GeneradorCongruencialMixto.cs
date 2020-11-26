using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Utilidades.Generadores
{
    public class GeneradorCongruencialMixto : IGenerador
    {
        private readonly int a;
        private readonly int c;
        private readonly int m;
        public  int Xm;

        public GeneradorCongruencialMixto( int a, int c, int m, int x0) {
            this.a = a;
            this.c = c;
            this.m = m;
            this.Xm = x0; 
        }

        public NumeroAleatorio ObtenerValorAleatorio() {
            Xm = (a * Xm + c) % m;
            return new NumeroAleatorio(Xm, (double) Xm / m);
        }
       
    }
}