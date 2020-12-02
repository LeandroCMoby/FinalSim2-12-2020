using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulacion.Utilidades.Distribuciones;

namespace Simulacion.Final
{
    public class Equipo : ICloneable
    {
        public String Nombre { get; set; }
        public int TiempoEjecucion { get; set; }
        public int TiempoFinAtencion { get; set; }
        public bool Mantenido { get; set; }
        public bool Libre { get; set; }
        public Evento EventoFin { get; set; }
        public int CantidadInscripciones { get; set; }
        public Ocupacion TipoOcupacion { get; set; }
        public Alumno alumno { get; set; }
        public Condiciones condicionesIniciales { get; set; }
        public Mantenimiento mantenimiento { get; set; }
        

        public Equipo()
        {

        }

        public Equipo(string nombre, Evento eventoFin , Condiciones condiciones)
        {
            condicionesIniciales = condiciones;
            Nombre = nombre;
            TiempoFinAtencion = 0;
            Libre = true;
            Mantenido = false;
            EventoFin = eventoFin;
            CantidadInscripciones = 0;
            alumno = null;
            mantenimiento = null;
        }

        public object Clone()
        {
            Equipo equipo = new Equipo();
            equipo.CantidadInscripciones = CantidadInscripciones;
            equipo.EventoFin = EventoFin;
            equipo.Libre = Libre;
            equipo.Mantenido = Mantenido;
            equipo.Nombre = Nombre;
            equipo.TiempoFinAtencion = TiempoFinAtencion;
            equipo.TipoOcupacion = TipoOcupacion;
            equipo.alumno = alumno;
            equipo.TiempoEjecucion = TiempoEjecucion;
            equipo.condicionesIniciales = condicionesIniciales;
            equipo.mantenimiento = mantenimiento;
            return equipo;
        }

        public void ObtenerTiempoAtencion()
        {
            if(TipoOcupacion == Ocupacion.Inscripcion)
            {
                DistribucionUniforme distribucion = new DistribucionUniforme(condicionesIniciales.AInscripcion, condicionesIniciales.BInscripcion);
                TiempoEjecucion = (int)distribucion.ObtenerVariableAleatoria();
            }
            if(TipoOcupacion == Ocupacion.Mantenimiento)
            {
                DistribucionNormal distribucion = new DistribucionNormal(condicionesIniciales.MediaMantenimiento, Math.Pow(condicionesIniciales.DesvMantenimiento, 2.0));
                TiempoEjecucion = (int)distribucion.ObtenerVariableAleatoria();
            }
        }
    }
}
