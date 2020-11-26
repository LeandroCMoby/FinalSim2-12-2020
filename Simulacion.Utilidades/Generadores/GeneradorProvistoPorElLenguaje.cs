using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Utilidades.Generadores
{
    public class GeneradorProvistoPorElLenguaje : IGenerador
    {
        private Random random;

        public GeneradorProvistoPorElLenguaje() {
            random = new Random();
        }

        public NumeroAleatorio ObtenerValorAleatorio()
        {
            return new NumeroAleatorio(0, random.NextDouble());
        }

     
    }
}