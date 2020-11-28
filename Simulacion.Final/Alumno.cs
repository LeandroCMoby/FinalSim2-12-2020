using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Final
{
    public class Alumno
    {
        public int id { get; set; }
        public int TiempoRegreso { get; set; }
        
        public Alumno()
        {

        }

        public Alumno(Condiciones condiciones, int acumulador)
        {
            id = acumulador + 1;
            TiempoRegreso = 0;

        }

    }
}
