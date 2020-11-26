using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Utilidades
{
    public class Estadistica
    {

        public static MedidasEstadisticas DeterminarMedidasEstadisticas(IEnumerable<double> valores) {
            var medidas = new MedidasEstadisticas();
            medidas.ValorMaximo = Statistics.Maximum(valores);
            medidas.ValorMinimo = Statistics.Minimum(valores);
            medidas.Media = Statistics.Mean(valores);
            medidas.TamanioDeLaMuestra = valores.Count();
            medidas.Desviacion = Statistics.StandardDeviation(valores);

            var desc = new DescriptiveStatistics(samples);
            return medidas;
        }
    }
}
