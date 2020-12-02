using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulacion.Utilidades.Distribuciones;

namespace Simulacion.Final
{
    public class EstadoSimulacion : ICloneable
    {
        public int tiempoProximoEvento { get; set; }
        public int tiempoLlegadaProximoAlumno { get; set; }
        public int tiempoLlegadaProximoMantenimiento { get; set; }
        public int tiempo { get; set; }
        public Evento proximoEvento { get; set; }
        public Evento eventoActual { get; set; }
        public Equipo equipo1 { get; set; }
        public Equipo equipo2 { get; set; }
        public Equipo equipo3 { get; set; }
        public Equipo equipo4 { get; set; }
        public Equipo equipo5 { get; set; }
        public int numeroEvento { get; set; }
        public int numeroAlumno { get; set; }
        public List<Alumno> colaAlumnos { get; set; }
        public List<Alumno> colaAbandono { get; set; }
        public List<Mantenimiento> ColaMantenimientos { get; set; }
        public int AlumnosAbandono { get; set; }
        public double Inscripciones1 { get; set; }
        public double Inscripciones2 { get; set; }
        public double Inscripciones3 { get; set; }
        public double Inscripciones4 { get; set; }
        public double Inscripciones5 { get; set; }
        public double PromedioInscripciones1 { get; set; }
        public double PromedioInscripciones2 { get; set; }
        public double PromedioInscripciones3 { get; set; }
        public double PromedioInscripciones4 { get; set; }
        public double PromedioInscripciones5 { get; set; }
        public double PromedioInscripcionesSistema { get; set; }
        public Condiciones condicionesIniciales { get; set; }

        public EstadoSimulacion(Condiciones condiciones)
        {
            tiempo = 0;
            eventoActual = Evento.Inicio;

            equipo1 = new Equipo("Equipo1", Evento.FinAtencionEquipo1, condiciones);
            equipo2 = new Equipo("Equipo2", Evento.FinAtencionEquipo2, condiciones);
            equipo3 = new Equipo("Equipo3", Evento.FinAtencionEquipo3, condiciones);
            equipo4 = new Equipo("Equipo4", Evento.FinAtencionEquipo4, condiciones);
            equipo5 = new Equipo("Equipo5", Evento.FinAtencionEquipo5, condiciones);

            numeroEvento = 0;
            numeroAlumno = 0;

            colaAlumnos = new List<Alumno>();
            ColaMantenimientos = new List<Mantenimiento>();
            colaAbandono = new List<Alumno>();
            AlumnosAbandono = 0;
            Inscripciones1 = 0.0;
            Inscripciones2 = 0.0;
            Inscripciones3 = 0.0;
            Inscripciones4 = 0.0;
            Inscripciones5 = 0.0;
            PromedioInscripciones1 = 0.0;
            PromedioInscripciones2 = 0.0;
            PromedioInscripciones3 = 0.0;
            PromedioInscripciones4 = 0.0;
            PromedioInscripciones5 = 0.0;
            PromedioInscripcionesSistema = 0.0;

            condicionesIniciales = condiciones;
            ObtenerTiempoLlegadaProximoMantenimiento(tiempo);
            ObtenerTiempoLlegadaProximoAlumno(tiempo);
            if(tiempoLlegadaProximoAlumno < tiempoLlegadaProximoMantenimiento)
            {
                tiempoProximoEvento = tiempoLlegadaProximoAlumno;
                proximoEvento = Evento.LlegadaAlumno;
            }
            else
            {
                tiempoProximoEvento = tiempoLlegadaProximoMantenimiento;
                proximoEvento = Evento.LlegadaMantenimiento;
            }
           

        }

        public void ObtenerTiempoLlegadaProximoAlumno(int tiempo)
        {
            double lambda = 1.0 / (condicionesIniciales.MediaLlegadaAlumno );
            //double lambda = 1.0 / 2.0;
            DistribucionExponencialNegativa distribucion = new DistribucionExponencialNegativa(lambda);
            tiempoLlegadaProximoAlumno = (int)(distribucion.ObtenerVariableAleatoria());
            tiempoLlegadaProximoAlumno += tiempo;
        }

        public void CalcularTiempoProximoEvento()
        {
            List<Equipo> ListaEquipos = new List<Equipo>
            {
                equipo1,
                equipo2,
                equipo3,
                equipo4,
                equipo5,
            };

            Equipo proximoEquipo;
            proximoEquipo = ListaEquipos.FindAll(x => !x.Libre ).OrderBy(x => x.TiempoFinAtencion).FirstOrDefault();

            if(proximoEquipo != null)
            {
                tiempoProximoEvento = proximoEquipo.TiempoFinAtencion;
                proximoEvento = proximoEquipo.EventoFin;

                if(tiempoLlegadaProximoAlumno < condicionesIniciales.HorasSimulacion * 3600)
                {
                    if (tiempoProximoEvento > tiempoLlegadaProximoAlumno)
                    {
                        tiempoProximoEvento = tiempoLlegadaProximoAlumno;
                        proximoEvento = Evento.LlegadaAlumno;
                    }
                }
                if(tiempoLlegadaProximoMantenimiento < condicionesIniciales.HorasSimulacion * 3600)
                {
                    if(tiempoProximoEvento > tiempoLlegadaProximoMantenimiento)
                    {
                        tiempoProximoEvento = tiempoLlegadaProximoMantenimiento;
                        proximoEvento = Evento.LlegadaMantenimiento;
                    }

                }
                //Con el siguiente codigo, compruebo el regreso de un alumno que habia abandonado la fila
                if (colaAbandono.Count > 0 && colaAbandono.First().TiempoRegreso < condicionesIniciales.HorasSimulacion * 3600)
                {
                    if(tiempoProximoEvento > colaAbandono.First().TiempoRegreso)
                    {
                        tiempoProximoEvento = colaAbandono.First().TiempoRegreso;
                        proximoEvento = Evento.LlegadaAlumno;
                    }
                }
            }
            else
            {
                tiempoProximoEvento = (condicionesIniciales.HorasSimulacion+1) * 3600;
                if (tiempoLlegadaProximoAlumno < condicionesIniciales.HorasSimulacion * 3600)
                {
                    if (tiempoProximoEvento > tiempoLlegadaProximoAlumno)
                    {
                        tiempoProximoEvento = tiempoLlegadaProximoAlumno;
                        proximoEvento = Evento.LlegadaAlumno;
                    }
                }
                if (tiempoLlegadaProximoMantenimiento < condicionesIniciales.HorasSimulacion * 3600)
                {
                    if (tiempoProximoEvento > tiempoLlegadaProximoMantenimiento)
                    {
                        tiempoProximoEvento = tiempoLlegadaProximoMantenimiento;
                        proximoEvento = Evento.LlegadaMantenimiento;
                    }

                }
                if (colaAbandono.Count > 0 && colaAbandono.First().TiempoRegreso < condicionesIniciales.HorasSimulacion * 3600)
                {
                    if (tiempoProximoEvento > colaAbandono.First().TiempoRegreso)
                    {
                        tiempoProximoEvento = colaAbandono.First().TiempoRegreso;
                        proximoEvento = Evento.LlegadaAlumno;
                    }
                }
                if (tiempoLlegadaProximoAlumno > condicionesIniciales.HorasSimulacion*3600 && tiempoLlegadaProximoMantenimiento > condicionesIniciales.HorasSimulacion * 3600)
                {
                    proximoEvento = Evento.Final;
                    tiempoProximoEvento = tiempo;
                }
            }
            numeroEvento++;
        }


        public void ObtenerTiempoLlegadaProximoMantenimiento(int tiempo)
        {
            DistribucionUniforme distribucion = new DistribucionUniforme(condicionesIniciales.ALlegadaMantenimiento, condicionesIniciales.BLlegadaMantenmiento);
            tiempoLlegadaProximoMantenimiento = (int)distribucion.ObtenerVariableAleatoria();
            tiempoLlegadaProximoMantenimiento += tiempo;
        }

        public object Clone()
        {
            EstadoSimulacion estado = new EstadoSimulacion(condicionesIniciales);
            estado.AlumnosAbandono = AlumnosAbandono;
            estado.colaAlumnos = colaAlumnos;
            estado.colaAbandono = colaAbandono;
            estado.ColaMantenimientos = ColaMantenimientos;
            estado.equipo1 = (Equipo)equipo1.Clone();
            estado.equipo2 = (Equipo)equipo2.Clone();
            estado.equipo3 = (Equipo)equipo3.Clone();
            estado.equipo4 = (Equipo)equipo4.Clone();
            estado.equipo5 = (Equipo)equipo5.Clone();
            estado.eventoActual = eventoActual;
            estado.Inscripciones1 = Inscripciones1;
            estado.Inscripciones2 = Inscripciones2;
            estado.Inscripciones3 = Inscripciones3;
            estado.Inscripciones4 = Inscripciones4;
            estado.Inscripciones5 = Inscripciones5;
            estado.numeroAlumno = numeroAlumno;
            estado.numeroEvento = numeroEvento;
            estado.PromedioInscripciones1 = PromedioInscripciones1;
            estado.PromedioInscripciones2 = PromedioInscripciones2;
            estado.PromedioInscripciones3 = PromedioInscripciones3;
            estado.PromedioInscripciones4 = PromedioInscripciones4;
            estado.PromedioInscripciones5 = PromedioInscripciones5;
            estado.PromedioInscripcionesSistema = PromedioInscripcionesSistema;
            estado.proximoEvento = proximoEvento;
            estado.tiempo = tiempo;
            estado.tiempoLlegadaProximoAlumno = tiempoLlegadaProximoAlumno;
            estado.tiempoLlegadaProximoMantenimiento = tiempoLlegadaProximoMantenimiento;
            estado.tiempoProximoEvento = tiempoProximoEvento;
            return estado;
        }
    
        public void CalcularPromedios()
        {
            int hora = (int)Math.Floor((double) (tiempo / 3600));
            PromedioInscripciones1 = (1.0 / (double)hora) * (((double)hora - 1.0) * PromedioInscripciones1 + (double)equipo1.CantidadInscripciones);
            PromedioInscripciones2 = (1.0 / (double)hora) * (((double)hora - 1.0) * PromedioInscripciones2 + (double)equipo2.CantidadInscripciones);
            PromedioInscripciones3 = (1.0 / (double)hora) * (((double)hora - 1.0) * PromedioInscripciones3 + (double)equipo3.CantidadInscripciones);
            PromedioInscripciones4 = (1.0 / (double)hora) * (((double)hora - 1.0) * PromedioInscripciones4 + (double)equipo4.CantidadInscripciones);
            PromedioInscripciones5 = (1.0 / (double)hora) * (((double)hora - 1.0) * PromedioInscripciones5 + (double)equipo5.CantidadInscripciones);
            PromedioInscripcionesSistema = PromedioInscripciones1 + PromedioInscripciones2 + PromedioInscripciones3 + PromedioInscripciones4 + PromedioInscripciones5;

            equipo1.CantidadInscripciones = 0;
            equipo2.CantidadInscripciones = 0;
            equipo3.CantidadInscripciones = 0;
            equipo4.CantidadInscripciones = 0;
            equipo5.CantidadInscripciones = 0;

        }
    }
}
