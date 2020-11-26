using MathNet.Numerics.Distributions;
using Simulacion.Utilidades.Generadores;
using Simulacion.Utilidades.TablaDeFrecuencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simulacion.Utilidades.Distribuciones
{
    public class DistribucionPoisson : IDistribucion
    {
        private double lambda;
        private IGenerador generador;
        private int xMax;
        private double[] intervalos;
        private int num;
        public DistribucionPoisson(double lambda, int xMax)
        {
            this.generador = new GeneradorProvistoPorElLenguaje();
            this.lambda = lambda;
            this.xMax = xMax;
            this.intervalos = new double[xMax+1];

            for (int i = 0; i <= xMax; i++)
            {
                intervalos[i] = Poisson.CDF(lambda, i);
            }
            
        }
        public int ObtenerNum()
        {
            return num;
        }

        public DistribucionPoisson(double lambda)
        {
            this.generador = new GeneradorProvistoPorElLenguaje();
            this.lambda = lambda;
            var entero = new Poisson(lambda);
            num = entero.Sample();
        }

        public void ObtenerFrecuenciaEsperada(TablaDeFrecuencia tabla)
        {
            for (int i = 0; i < tabla.Intervalos.Length; i++)
            {
                tabla.Intervalos[i].FrecuenciaEsperada = (Poisson.CDF(lambda,tabla.Intervalos[i].Maximo) -
                    Poisson.CDF(lambda, tabla.Intervalos[i].Minimo)) * tabla.Cantidad;
            }
        }

        public double ObtenerVariableAleatoria()
        {
            double x = 0.0;
            double r = generador.ObtenerValorAleatorio().Numero;

            for (int i = 0; i < intervalos.Length; i++)
            {
                if (r < intervalos[i])
                {
                    x = i;
                    break;
                }
            }

            return x;
        }
    }
}
