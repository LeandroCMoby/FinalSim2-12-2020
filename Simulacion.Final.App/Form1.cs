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
                DesvLlegadaAlumno = Convert.ToInt32(txtDesvLlegadaAlumnos),
                AInscripcion = Convert.ToInt32(txtATiempoInscripcion),
                BInscripcion = Convert.ToInt32(txtBTiempoInscripcion),
                ALlegadaMantenimiento = Convert.ToInt32(txtAMantenimiento),
                BLlegadaMantenmiento = Convert.ToInt32(txtBMantenimiento),
                MediaMantenimiento = Convert.ToInt32(txtMediaMantenimiento),
                DesvMantenimiento = Convert.ToInt32(txtDesvMantenimiento),
                HorasSimulacion = Convert.ToInt32(txtHorasSimulacion),
                MaxAlumnosCola = Convert.ToInt32(txtMaxAlumnos)
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

            }

        }

        private bool Validar()
        {
            return (Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtMaxAlumnos.Text)) || Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtHorasSimulacion.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtAMantenimiento.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtATiempoInscripcion.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtBMantenimiento.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtBTiempoInscripcion.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtDesvLlegadaAlumnos))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtDesvMantenimiento.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtMediaLlegadaAlumno.Text))
                ||Validaciones.ValidarMayoraCeroInt(Convert.ToDouble(txtMediaMantenimiento.Text))
                ||Convert.ToDouble(txtBMantenimiento.Text) < Convert.ToDouble(txtAMantenimiento.Text)
                ||Convert.ToDouble(txtBTiempoInscripcion.Text) < Convert.ToDouble(txtATiempoInscripcion.Text));
        }
    }
}
