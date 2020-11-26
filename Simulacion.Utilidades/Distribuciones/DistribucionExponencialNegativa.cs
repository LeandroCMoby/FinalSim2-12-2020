using MathNet.Numerics.Distributions;
using Simulacion.Utilidades.Generadores;
using Simulacion.Utilidades.TablaDeFrecuencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Simulacion.Utilidades.Distribuciones
{
    public class DistribucionExponencialNegativa: IDistribucion
    {
        private double lambda;
        private IGenerador generador;
        public DistribucionExponencialNegativa(double lambda, IGenerador generador) {
            this.generador = generador;
            this.lambda = lambda;
        }

        public DistribucionExponencialNegativa(double lambda)
        {
            this.lambda = lambda;
            this.generador = new GeneradorProvistoPorElLenguaje();
        }

        public double ObtenerVariableAleatoria(double x)
        {
            var u = 1 - Pow(E, -(lambda * x));
            var x1 = -(1 / lambda) * Log(1 - u);
            return x1;
        }

        public double ObtenerVariableAleatoria()
        {
            var u = generador.ObtenerValorAleatorio();
            var x1 = -(1 / lambda) * Log(1.0 - u.Numero);
            return x1;
        }

        public void ObtenerFrecuenciaEsperada(TablaDeFrecuencia tabla)
        {
            for (int i = 0; i < tabla.Intervalos.Length; i++)
            {
                tabla.Intervalos[i].FrecuenciaEsperada = (Exponential.CDF(lambda, tabla.Intervalos[i].Maximo) -
                    Exponential.CDF(lambda, tabla.Intervalos[i].Minimo)) * tabla.Cantidad;
            }
        }

        
    }
}
