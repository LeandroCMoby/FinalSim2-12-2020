using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Final
{
    public class Simulacion
    {
        public EstadoSimulacion estadoAnterior;
        public EstadoSimulacion estadoActual;

        public EstadoSimulacion GenerarSimulacion(Condiciones condiciones)
        {
            estadoActual = (EstadoSimulacion)estadoAnterior.Clone();
            estadoActual.tiempo = estadoAnterior.tiempoProximoEvento;
            estadoActual.eventoActual = estadoAnterior.proximoEvento;

            switch (estadoAnterior.eventoActual)
            {
                case Evento.LlegadaAlumno:
                    LlegoAlumno(condiciones);
                    break;
                case Evento.LlegadaMantenimiento:
                    LlegoMantenimiento(condiciones);
                    break;
                case Evento.FinAtencionEquipo1:
                    FinAtencion(estadoActual.equipo1);
                    break;
                case Evento.FinAtencionEquipo2:
                    FinAtencion(estadoActual.equipo2);
                    break;
                case Evento.FinAtencionEquipo3:
                    FinAtencion(estadoActual.equipo3);
                    break;
                case Evento.FinAtencionEquipo4:
                    FinAtencion(estadoActual.equipo4);
                    break;
                case Evento.FinAtencionEquipo5:
                    FinAtencion(estadoActual.equipo5);
                    break;
                default:
                    break;
            }

            return estadoAnterior;
        }

        private void FinAtencion(Equipo equipo1)
        {
            throw new NotImplementedException();
        }

        private void LlegoMantenimiento(Condiciones condiciones)
        {
            throw new NotImplementedException();
        }

        private void LlegoAlumno(Condiciones condiciones)
        {
            Alumno alumno = new Alumno(condiciones, estadoActual.tiempo, estadoActual.numeroAlumno);
            estadoActual.numeroAlumno++;
            estadoActual.ObtenerTiempoLlegadaProximoAlumno(estadoActual.tiempo);
            List<Equipo> equipos = new List<Equipo>
            {
                estadoActual.equipo1,
                estadoActual.equipo2,
                estadoActual.equipo3,
                estadoActual.equipo4,
                estadoActual.equipo5
            };
            Equipo proximoEquipo = equipos.FindAll(x => x.Libre).First();
            if(proximoEquipo != null)
            {
                proximoEquipo.Libre = false;
                proximoEquipo.TipoOcupacion = Ocupacion.Inscripcion;
                proximoEquipo.ObtenerTiempoAtencion();
                proximoEquipo.TiempoFinAtencion = proximoEquipo.TiempoEjecucion + estadoActual.tiempo;
                proximoEquipo.alumno = alumno;
            }
            else
            {
                estadoActual.colaAlumnos.Add(alumno);
            }

        }
    }
}
