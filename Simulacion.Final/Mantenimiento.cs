using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Final
{
    public class Mantenimiento : ICloneable
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

        public object Clone()
        {
            Mantenimiento mantenimiento = new Mantenimiento();
            mantenimiento.id = id;
            mantenimiento.TiempoLlegada = TiempoLlegada;
            return mantenimiento;
        }
    }
}
