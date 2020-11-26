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
    public class DistribucionNormal : IDistribucion
    {
        private IGenerador generador;
        private double media;
        private double varianza;
        public DistribucionNormal(double media, double varianza,IGenerador generador)
        {
            this.generador = generador;
            this.media = media;
            this.varianza = varianza;
        }

        public DistribucionNormal(double media, double varianza)
        {
            this.generador = new GeneradorProvistoPorElLenguaje();
            this.media = media;
            this.varianza = varianza;
        }


        private double Z() {
            var r1 = generador.ObtenerValorAleatorio();
            var r2 = generador.ObtenerValorAleatorio();
            var z = Sqrt(-2 * Log(r1.Numero)) * Cos(2 * PI * r2.Numero);
            return z;
        }

        public double ObtenerVariableAleatoria()
        {
            return media + Z() * varianza;
        }

        public void ObtenerFrecuenciaEsperada(TablaDeFrecuencia tabla)
        {
            for (int i = 0; i < tabla.Intervalos.Length; i++)
            {
                tabla.Intervalos[i].FrecuenciaEsperada = (Normal.CDF(media, Math.Sqrt(varianza),tabla.Intervalos[i].Maximo) -
                    Normal.CDF(media, Math.Sqrt(varianza), tabla.Intervalos[i].Minimo) )* tabla.Cantidad;
            }
        }
    }
}
