using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Final
{
    public class Equipo : ICloneable
    {
        public String Nombre { get; set; }
        public int TiempoFinAtencion { get; set; }
        public bool Mantenido { get; set; }
        public bool Libre { get; set; }
        public Evento EventoFin { get; set; }
        public int CantidadInscripciones { get; set; }

        public Equipo()
        {

        }

        public Equipo(string nombre, Evento eventoFin)
        {
            Nombre = nombre;
            TiempoFinAtencion = 0;
            Libre = true;
            Mantenido = false;
            EventoFin = eventoFin;
            CantidadInscripciones = 0;
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
            return equipo;
        }
    }
}
