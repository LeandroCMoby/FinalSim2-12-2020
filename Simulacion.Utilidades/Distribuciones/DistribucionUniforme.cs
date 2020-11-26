using Simulacion.Utilidades.Generadores;
using Simulacion.Utilidades.TablaDeFrecuencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Utilidades.Distribuciones
{
    public class DistribucionUniforme : IDistribucion
    {
        private double a;
        private double b;
        private IGenerador generador;

        public DistribucionUniforme(double a, double b, IGenerador generador) {
            this.a = a;
            this.b = b;
            this.generador = generador;
        }

        public DistribucionUniforme(double a, double b)
        {
            this.a = a;
            this.b = b;
            this.generador = new GeneradorProvistoPorElLenguaje();
        }

        public double ObtenerVariableAleatoria(double x) {
            var u = (x - a) / (b - a);
            var x1 = a + u * (b - a);
            return x1;
        }

        public double ObtenerVariableAleatoria()
        {
            var u = generador.ObtenerValorAleatorio();
            var x1 = a + u.Numero * (b - a);
            return x1;
        }

        public void ObtenerFrecuenciaEsperada(TablaDeFrecuencia tabla) {
            double frecuenciaEsperada =tabla.Cantidad / tabla.Intervalos.Length;

            foreach (var item in tabla.Intervalos)
            {
                item.FrecuenciaEsperada = frecuenciaEsperada;
            }
        }

    }
}
