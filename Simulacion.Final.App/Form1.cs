using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simulacion.Final;
using Simulacion.Utilidades.Validaciones;

namespace Simulacion.Final.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private Condiciones ObtenerCondiciones()
        {
            Condiciones condiciones = new Condiciones
            {
                MediaLlegadaAlumno = Convert.ToInt32(txtMediaLlegadaAlumno.Text),
                DesvLlegadaAlumno = Convert.ToInt32(txtDesvLlegadaAlumnos.Text),
                AInscripcion = Convert.ToInt32(txtATiempoInscripcion.Text)*60,
                BInscripcion = Convert.ToInt32(txtBTiempoInscripcion.Text)*60,
                ALlegadaMantenimiento = Convert.ToInt32(txtAMantenimiento.Text)*60,
                BLlegadaMantenmiento = Convert.ToInt32(txtBMantenimiento.Text)*60,
                MediaMantenimiento = Convert.ToInt32(txtMediaMantenimiento.Text),
                DesvMantenimiento = Convert.ToInt32(txtDesvMantenimiento.Text),
                HorasSimulacion = Convert.ToInt32(txtHorasSimulacion.Text),
                MaxAlumnosCola = Convert.ToInt32(txtMaxAlumnos.Text)
            };

            return condiciones;
        }

        private void LimpiarLabels()
        {
            lblPorcentajeAbandono.Text = " ";
            lblPromedio1.Text = " ";
            lblPromedio2.Text = " ";
            lblPromedio3.Text = " ";
            lblPromedio4.Text = " ";
            lblPromedio5.Text = " ";
            lblPromedioSistema.Text = " ";
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            dvgSim.Rows.Clear();
            LimpiarLabels();

            if (Validar())
            {
                MessageBox.Show("Los parámetros ingresados tienen que ser mayo a cero, y los maximos deben ser mayor que los minimos");
            }
            else
            {
                Condiciones condiciones = ObtenerCondiciones();
                Simulacion simulacion = new Simulacion();
                EstadoSimulacion estado;
                bool condicion;
                progresBar.Visible = true;
                simulacion.estadoAnterior = new EstadoSimulacion(condiciones);
                AgregarFila(simulacion.estadoAnterior);
                do
                {

                    estado = simulacion.GenerarSimulacion(condiciones);

                    bool CambioHora = Math.Floor(((double)estado.tiempoProximoEvento / 3600)) > Math.Floor(((double)estado.tiempo / 3600));
                    if (CambioHora)
                    {
                        if (Math.Floor((double)(estado.tiempo / 3600)) < condiciones.HorasSimulacion  /10)
                        {
                            progresBar.Value = 10;
                        }else if(Math.Floor((double)(estado.tiempo / 3600)) < condiciones.HorasSimulacion /10 * 2)
                        {
                            progresBar.Value = 20;
                        }
                        else if (Math.Floor((double)(estado.tiempo / 3600)) < condiciones.HorasSimulacion / 10 * 3)
                        {
                            progresBar.Value = 30;
                        }
                        else if (Math.Floor((double)(estado.tiempo / 3600)) < condiciones.HorasSimulacion / 10 * 4)
                        {
                            progresBar.Value = 40;
                        }
                        else if (Math.Floor((double)(estado.tiempo / 3600)) < condiciones.HorasSimulacion / 10 * 5)
                        {
                            progresBar.Value = 50;
                        }
                        else if (Math.Floor((double)(estado.tiempo / 3600)) < condiciones.HorasSimulacion / 10 * 6)
                        {
                            progresBar.Value = 60;
                        }
                        else if (Math.Floor((double)(estado.tiempo / 3600)) < condiciones.HorasSimulacion / 10 * 7)
                        {
                            progresBar.Value = 70;
                        }
                        else if (Math.Floor((double)(estado.tiempo / 3600)) < condiciones.HorasSimulacion / 10 * 8)
                        {
                            progresBar.Value = 80;
                        }
                        else if (Math.Floor((double)(estado.tiempo / 3600)) < condiciones.HorasSimulacion / 10 * 9)
                        {
                            progresBar.Value = 90;
                        }
                        else if (Math.Floor((double)(estado.tiempo / 3600)) < condiciones.HorasSimulacion / 10 * 10)
                        {
                            progresBar.Value = 100;
                        }

                    }
                    AgregarFila(estado);

                    bool condicion1 = !estado.equipo1.Libre || !estado.equipo2.Libre || !estado.equipo3.Libre || !estado.equipo4.Libre || !estado.equipo5.Libre;
                    bool condicion2 = estado.colaAlumnos.Count > 0;
                    bool condicion4 = estado.eventoActual != Evento.Final;

                    condicion = condicion1 || condicion2 || condicion4;
                }
                while (condicion);

                double porcentajeAbandono = (double)estado.AlumnosAbandono / (double)estado.numeroAlumno * 100;
                estado.PromedioInscripcionesSistema = estado.PromedioInscripciones1 + estado.PromedioInscripciones2 + estado.PromedioInscripciones3 + estado.PromedioInscripciones4 + estado.PromedioInscripciones5;
                // lblPorcentajeAbandono.Text = porcentajeAbandono.ToString() + "%";
                lblPorcentajeAbandono.Text = StringifyCifra(Math.Round(porcentajeAbandono, 2)) + " %";
                lblPromedio1.Text = StringifyCifra(Math.Round(estado.PromedioInscripciones1, 2));
                lblPromedio2.Text = StringifyCifra(Math.Round(estado.PromedioInscripciones2,2));
                lblPromedio3.Text = StringifyCifra(Math.Round(estado.PromedioInscripciones3,2));
                lblPromedio4.Text = StringifyCifra(Math.Round(estado.PromedioInscripciones4,2));
                lblPromedio5.Text = StringifyCifra(Math.Round(estado.PromedioInscripciones5,2));
                lblPromedioSistema.Text = StringifyCifra(Math.Round(estado.PromedioInscripcionesSistema,2));

                progresBar.Visible = false;
            }

        }

        private bool Validar()
        {
            return (Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtMaxAlumnos.Text)) || Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtHorasSimulacion.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtAMantenimiento.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtATiempoInscripcion.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtBMantenimiento.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtBTiempoInscripcion.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtDesvLlegadaAlumnos.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtDesvMantenimiento.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtMediaLlegadaAlumno.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtMediaMantenimiento.Text))
                ||Convert.ToDouble(txtBMantenimiento.Text) < Convert.ToDouble(txtAMantenimiento.Text)
                ||Convert.ToDouble(txtBTiempoInscripcion.Text) < Convert.ToDouble(txtATiempoInscripcion.Text));
        }
        
        private void AgregarFila(EstadoSimulacion estado)
        {
            int NumeroEvento = estado.numeroEvento;
            Evento TipoEvento =estado.eventoActual;
            string reloj = StringifyHora(estado.tiempo);
            string LlegadaProximoAlumno = StringifyHora(estado.tiempoLlegadaProximoAlumno);
            string LlegadaProximoMantenimiento = StringifyHora(estado.tiempoLlegadaProximoMantenimiento);
            string colaAlumnos = estado.colaAlumnos.Count.ToString();
            string colaMantenimiento = estado.ColaMantenimientos.Count.ToString();
            string regreso = StringifyHora(estado.colaAbandono.Count > 0 ? estado.colaAbandono.First().TiempoRegreso : 0);

            #region Equipo1
            string e1Estado = estado.equipo1.Libre ? "Libre" : "Ocupado";
            Ocupacion e1TipoOcupacion = Ocupacion.Libre;
            string e1TiempoOcupacion = "-";
            string e1FinOcupacion = "-";
            string e1Alumno = "-";
            if (estado.equipo1.Libre == false)
            {
                e1TipoOcupacion = estado.equipo1.TipoOcupacion;
                e1TiempoOcupacion = StringifyHora(estado.equipo1.TiempoEjecucion);
                e1FinOcupacion = StringifyHora(estado.equipo1.TiempoFinAtencion);
                if(e1TipoOcupacion == Ocupacion.Inscripcion)
                {
                    e1Alumno = estado.equipo1.alumno.id.ToString();
                }

            }
            #endregion

            #region Equipo2
            string e2Estado = estado.equipo2.Libre ? "Libre" : "Ocupado";
            Ocupacion e2TipoOcupacion = Ocupacion.Libre;
            string e2TiempoOcupacion = "-";
            string e2FinOcupacion = "-";
            string e2Alumno = "-";
            if (estado.equipo2.Libre == false)
            {
                e2TipoOcupacion = estado.equipo2.TipoOcupacion;
                e2TiempoOcupacion = StringifyHora(estado.equipo2.TiempoEjecucion);
                e2FinOcupacion = StringifyHora(estado.equipo2.TiempoFinAtencion);
                if (e2TipoOcupacion == Ocupacion.Inscripcion)
                {
                    e2Alumno = estado.equipo2.alumno.id.ToString();
                }

            }
            #endregion

            #region Equipo3
            string e3Estado = estado.equipo3.Libre ? "Libre" : "Ocupado";
            Ocupacion e3TipoOcupacion = Ocupacion.Libre;
            string e3TiempoOcupacion = "-";
            string e3FinOcupacion = "-";
            string e3Alumno = "-";
            if (estado.equipo3.Libre == false)
            {
                e3TipoOcupacion = estado.equipo3.TipoOcupacion;
                e3TiempoOcupacion = StringifyHora(estado.equipo3.TiempoEjecucion);
                e3FinOcupacion = StringifyHora(estado.equipo3.TiempoFinAtencion);
                if (e3TipoOcupacion == Ocupacion.Inscripcion)
                {
                    e3Alumno = estado.equipo3.alumno.id.ToString();
                }

            }
            #endregion

            #region Equipo4
            string e4Estado = estado.equipo4.Libre ? "Libre" : "Ocupado";
            Ocupacion e4TipoOcupacion = Ocupacion.Libre;
            string e4TiempoOcupacion = "-";
            string e4FinOcupacion = "-";
            string e4Alumno = "-";
            if (estado.equipo4.Libre == false)
            {
                e4TipoOcupacion = estado.equipo4.TipoOcupacion;
                e4TiempoOcupacion = StringifyHora(estado.equipo4.TiempoEjecucion);
                e4FinOcupacion = StringifyHora(estado.equipo4.TiempoFinAtencion);
                if (e4TipoOcupacion == Ocupacion.Inscripcion)
                {
                    e4Alumno = estado.equipo4.alumno.id.ToString();
                }

            }
            #endregion

            #region Equipo5
            string e5Estado = estado.equipo5.Libre ? "Libre" : "Ocupado";
            Ocupacion e5TipoOcupacion = Ocupacion.Libre;
            string e5TiempoOcupacion = "-";
            string e5FinOcupacion = "-";
            string e5Alumno = "-";
            if (estado.equipo5.Libre == false)
            {
                e5TipoOcupacion = estado.equipo5.TipoOcupacion;
                e5TiempoOcupacion = StringifyHora(estado.equipo5.TiempoEjecucion);
                e5FinOcupacion = StringifyHora(estado.equipo5.TiempoFinAtencion);
                if (e5TipoOcupacion == Ocupacion.Inscripcion)
                {
                    e5Alumno = estado.equipo5.alumno.id.ToString();
                }
            }
            #endregion

            dvgSim.Rows.Add(NumeroEvento, TipoEvento, reloj, LlegadaProximoAlumno, LlegadaProximoMantenimiento, regreso, colaAlumnos, colaMantenimiento,
                e1Estado, e1TipoOcupacion, e1Alumno, e1TiempoOcupacion, e1FinOcupacion,
                e2Estado, e2TipoOcupacion, e2Alumno, e2TiempoOcupacion, e2FinOcupacion,
                e3Estado, e3TipoOcupacion, e3Alumno, e3TiempoOcupacion, e3FinOcupacion,
                e4Estado, e4TipoOcupacion, e4Alumno, e4TiempoOcupacion, e4FinOcupacion,
                e5Estado, e5TipoOcupacion, e5Alumno, e5TiempoOcupacion, e5FinOcupacion);
        }

        private string StringifyHora(int tiempo)
        {
            string horas = (tiempo / 3600).ToString();
            int restoHoras = tiempo % 3600;
            string minutos = (restoHoras / 60).ToString();
            string segundos = (restoHoras % 60).ToString();

            if (horas.Length < 2)
                horas = "0" + horas;

            if (minutos.Length < 2)
                minutos = "0" + minutos;

            if (segundos.Length < 2)
                segundos = "0" + segundos;

            return horas + ":" + minutos + ":" + segundos;
        }

        private string StringifyCifra(double cifra)
        {
            var nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = ",",
                NumberGroupSeparator = "."
            };

            string resultado = cifra.ToString("#,##0.00", nfi);

            return resultado;
        }
    }
}
