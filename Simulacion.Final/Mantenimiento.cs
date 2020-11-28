using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Final
{
    class Mantenimiento
    {
        public int id { get; set; }
        public int TiempoLlegada { get; set; }

        public Mantenimiento()
        {

        }

        public Mantenimiento(Condiciones condiciones, int tiempo, int acumulador)
        {
            id = acumulador + 1;
            TiempoLlegada = tiempo;
        }
    }
}
