using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Final
{
    public class Alumno
    {
        private int id { get; set; }
        private int TiempoLlegada { get; set; }

        public Alumno()
        {

        }

        public Alumno(Condiciones condiciones, int tiempo, int acumulador)
        {
            id = acumulador + 1;
            TiempoLlegada = tiempo;

        }

    }
}
