using Simulacion.Utilidades.TablaDeFrecuencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Utilidades.Distribuciones
{
    public interface IDistribucion
    {
         double ObtenerVariableAleatoria();
         void ObtenerFrecuenciaEsperada(TablaDeFrecuencia tabla);
    }
}
