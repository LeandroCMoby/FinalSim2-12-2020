using MathNet.Numerics.Statistics;
using Simulacion.Utilidades.Distribuciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Utilidades.TablaDeFrecuencias
{
    public class TablaDeFrecuencia
    {
        private IntervaloDeFrecuencia[] intervalos;
        public double Rango { get; set; }
        private IDistribucion distribucion;
        private int N;
        public double ValorMaximo { get; set; }
        public double ValorMinimo { get; set; }
        public TablaDeFrecuencia(int numeroIntervalos, IList<double> datos) {

            var histograma = new Histogram(datos, numeroIntervalos);
            this.intervalos = new IntervaloDeFrecuencia[numeroIntervalos];
            this.N = datos.Count;
            this.ValorMaximo = histograma.UpperBound;
            this.ValorMinimo = histograma.LowerBound;

            for (int i = 0; i < histograma.BucketCount; i++)
            {
                intervalos[i] = new IntervaloDeFrecuencia();
                intervalos[i].Numero = i;
                intervalos[i].Minimo = Math.Round(histograma[i].LowerBound, 4);
                intervalos[i].Maximo = Math.Round(histograma[i].UpperBound, 4);
                intervalos[i].FrecuenciaAbsoluta = histograma[i].Count;
                intervalos[i].MarcaDeClase = Math.Round((intervalos[i].Maximo + intervalos[i].Minimo) / 2,4);
            }
           
        }

        public int Cantidad { get { return N; } }
        public int NumeroDeIntervalos { get { return intervalos.Length; } }

        public IntervaloDeFrecuencia[] Intervalos { get { return this.intervalos; } }
    }

    public class IntervaloDeFrecuencia
    {
        public int Numero { get; set; }
        public double Minimo { get; set; }
        public double Maximo { get; set; }

        public double MarcaDeClase { get; set; }

        public double  FrecuenciaAbsoluta { get; set; }
        public double FrecuenciaEsperada { get; set; }
    }
}
