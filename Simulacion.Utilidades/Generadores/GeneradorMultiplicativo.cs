using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Utilidades.Generadores
{
    public class GeneradorMultiplicativo : IGenerador
    {
        private readonly int a;
        private readonly int m;
        public int Xm  { get;set; }

        public GeneradorMultiplicativo(int a, int m, int x0 = 0)
        {
            this.a = a;
            this.m = m;
            this.Xm = x0;
        }

        public NumeroAleatorio ObtenerValorAleatorio()
        {
            Xm = (a * Xm) % m;
            return new NumeroAleatorio(Xm, (double)Xm / m);

        }

    }
}