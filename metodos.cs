using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ProyectoFinal
{
    internal class Metodos
    {
        public void OrdenarBurbujaConAnimacion(FlowLayoutPanel parent, bool ascendente)
        {
            for (int i = 0; i < parent.Controls.Count - 1; i++)
            {
                for (int j = 0; j < parent.Controls.Count - 1 - i; j++)
                {
                    // Obtener los cuadros actuales y adyacentes
                    Panel cuadroA = parent.Controls[j] as Panel;
                    Panel cuadroB = parent.Controls[j + 1] as Panel;

                    // Validar que los cuadros existen
                    if (cuadroA == null || cuadroB == null) continue;

                    // Obtener los números de los cuadros
                    int valorA = int.Parse((cuadroA.Controls[0] as Label).Text);
                    int valorB = int.Parse((cuadroB.Controls[0] as Label).Text);

                    // Determinar si intercambiar basado en el orden ascendente/descendente
                    bool intercambiar = ascendente ? valorA > valorB : valorA < valorB;
                    if (intercambiar)
                    {
                        Cuadritos.IntercambiarCuadrosAnimado(parent, j, j + 1);
                    }
                }
            }
        }


    }
}
