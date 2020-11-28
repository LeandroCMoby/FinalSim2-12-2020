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

        private void FinAtencion(Equipo equipo)
        {
            if (equipo.TipoOcupacion == Ocupacion.Inscripcion)
            {
                equipo.CantidadInscripciones++;
            }
            if(equipo.TipoOcupacion == Ocupacion.Mantenimiento)
            {
                List<Equipo> equipos = new List<Equipo>
                {
                   estadoActual.equipo1,
                   estadoActual.equipo2,
                   estadoActual.equipo3,
                   estadoActual.equipo4,
                   estadoActual.equipo5,
                };

                Equipo equipoAux = equipos.FindAll(x => x.Mantenido = false).First();
                if(equipoAux == null)
                {
                    estadoActual.equipo1.Mantenido = false;
                    estadoActual.equipo2.Mantenido = false;
                    estadoActual.equipo3.Mantenido = false;
                    estadoActual.equipo4.Mantenido = false;
                    estadoActual.equipo5.Mantenido = false;
                }
                else
                {
                    estadoActual.ColaMantenimientos.Add(equipo.mantenimiento);
                }
                equipo.mantenimiento = null;
            }
            if (estadoActual.ColaMantenimientos.Count > 0 && equipo.Mantenido==false)
            {
                equipo.alumno = null;
                equipo.Libre = false;
                equipo.TipoOcupacion = Ocupacion.Mantenimiento;
                equipo.ObtenerTiempoAtencion();
                equipo.TiempoFinAtencion = equipo.TiempoEjecucion + estadoActual.tiempo;
                equipo.Mantenido = true;
                equipo.mantenimiento = estadoActual.ColaMantenimientos.First();
                estadoActual.ColaMantenimientos.RemoveAt(0);
            }else if(estadoActual.colaAlumnos.Count> 0)
            {
                equipo.alumno = estadoAnterior.colaAlumnos.First();
                estadoActual.colaAlumnos.RemoveAt(0);
                equipo.Libre = false;
                equipo.TipoOcupacion = Ocupacion.Inscripcion;
                equipo.ObtenerTiempoAtencion();
                equipo.TiempoFinAtencion = equipo.TiempoEjecucion + estadoActual.tiempo;
            }
            else
            {
                equipo.alumno = null;
                equipo.Libre = true;
                equipo.TipoOcupacion = Ocupacion.Libre;
                equipo.TiempoEjecucion = 0;
                equipo.TiempoFinAtencion = 0;
                equipo.mantenimiento = null; 
            }
        }

        private void LlegoMantenimiento(Condiciones condiciones)
        {
            throw new NotImplementedException();
        }

        private void LlegoAlumno(Condiciones condiciones)
        {
            Alumno alumno;
            if(estadoActual.colaAbandono.First() != null && estadoActual.tiempo == estadoActual.colaAbandono.First().TiempoRegreso)
            {
                alumno = estadoActual.colaAbandono.First();
                estadoActual.colaAbandono.RemoveAt(0);
            }
            else
            {
                alumno = new Alumno(condiciones, estadoActual.numeroAlumno);
            }
           
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
                if (estadoActual.colaAlumnos.Count < 4)
                {
                    estadoActual.colaAlumnos.Add(alumno);
                }
                else
                {
                    alumno.TiempoRegreso = estadoActual.tiempo + 30 * 60;
                    estadoActual.AlumnosAbandono++;
                    estadoActual.colaAbandono.Add(alumno);
                }
                
            }

        }
    }
}
