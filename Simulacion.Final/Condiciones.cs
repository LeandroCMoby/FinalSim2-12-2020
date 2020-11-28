using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Final
{
    public class Condiciones: ICloneable
    {

        public int MediaLlegadaAlumno { get; set; }
        public int DesvLlegadaAlumno { get; set; }
        public int AInscripcion { get; set; }
        public int BInscripcion { get; set; }
        public int ALlegadaMantenimiento { get; set; }
        public int BLlegadaMantenmiento { get; set; }
        public int MediaMantenimiento { get; set; }
        public int DesvMantenimiento { get; set; }
        public int MaxAlumnosCola { get; set; }
        public int HorasSimulacion { get; set; }

        public object Clone()
        {
            Condiciones condiciones = new Condiciones();
            condiciones.AInscripcion = AInscripcion;
            condiciones.ALlegadaMantenimiento = ALlegadaMantenimiento;
            condiciones.BInscripcion = BInscripcion;
            condiciones.BLlegadaMantenmiento = BLlegadaMantenmiento;
            condiciones.DesvLlegadaAlumno = DesvLlegadaAlumno;
            condiciones.DesvMantenimiento = DesvMantenimiento;
            condiciones.HorasSimulacion = HorasSimulacion;
            condiciones.MaxAlumnosCola = MaxAlumnosCola;
            condiciones.MediaLlegadaAlumno = MediaLlegadaAlumno;
            condiciones.MediaMantenimiento = MediaMantenimiento;
            return condiciones;
        }
    }
}
