using MathNet.Numerics.Distributions;
using Simulacion.Utilidades.TablaDeFrecuencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion.Utilidades.PruebasDeBondad
{
    public static class ChiCuadrado
    {

        public static ChiCuadradoResultado ObtenerChiCuadrado(TablaDeFrecuencia tabla)
        {
            ChiCuadradoResultado resultado = new ChiCuadradoResultado();
            
            resultado.ValorTeorico=Math.Round(ChiSquared.InvCDF(tabla.Intervalos.Length - 1, 0.9),4);

            foreach (var item in tabla.Intervalos)
            {
                resultado.ValorCalculado += Math.Pow(item.FrecuenciaEsperada - item.FrecuenciaAbsoluta, 2) / item.FrecuenciaEsperada;
            }
            resultado.ValorCalculado = Math.Round(resultado.ValorCalculado, 4);
            resultado.Aprueba = resultado.ValorCalculado < resultado.ValorTeorico;
            
            return resultado;
        }

    }

    public class ChiCuadradoResultado
    {
        public double ValorCalculado { get; set; }
        public double ValorTeorico { get; set; }
        public Boolean Aprueba { get; set; }


    }
}
