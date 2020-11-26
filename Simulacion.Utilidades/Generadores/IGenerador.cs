using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Utilidades.Generadores
{
    public interface IGenerador
    {
        NumeroAleatorio ObtenerValorAleatorio();
    }
}