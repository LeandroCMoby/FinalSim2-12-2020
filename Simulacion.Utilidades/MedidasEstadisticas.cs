using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Utilidades
{
    public class MedidasEstadisticas
    {
        public double ValorMaximo { get; set; }
        public double ValorMinimo { get; set; }

        public double RangoEntreValores { get { return ValorMaximo - ValorMinimo; } }
        public double Media { get; set; }

        public double Desviacion { get; set; }

        public int TamanioDeLaMuestra { get; set; }
    }
}
