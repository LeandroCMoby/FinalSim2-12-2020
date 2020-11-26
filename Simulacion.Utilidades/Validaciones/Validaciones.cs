using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Simulacion.Utilidades.Validaciones
{
    public static class Validaciones
    {
        public static bool ValidarVarios(int s, int c, int a, int m)
        {
            return (s >= 0 && s < m && a > 0 && a < m && c >= 0 && c < m && m > 0);
        }
        public static bool ValidarMayoraCeroInt(double Num)
        {
            return !(Num > 0 && Math.Floor(Num) == Num );
        }
        public static bool ValidarMayoraCero(double Num)
        {
            return !(Num > 0 && Num.GetType() == Type.GetType("System.Double")); ;
        }

        public static bool ValidarProbabilidad(double probSandwich, double probPizza, double probEmp, double probLomitoHamb)
        {
            return !(probEmp + probLomitoHamb + probPizza + probSandwich == 1.0);
            
        }
    }
}