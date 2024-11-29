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
        public void Shell(FlowLayoutPanel parent, bool ascendente)
        {
            {
                // Convertir los controles en una lista de paneles para facilitar la manipulación
                List<Panel> cuadros = parent.Controls.OfType<Panel>().ToList();

                int n = cuadros.Count;

                // Comenzamos con un gap grande y lo reducimos
                for (int gap = n / 2; gap > 0; gap /= 2)
                {
                    // Recorremos el arreglo según el gap
                    for (int i = gap; i < n; i++)
                    {
                        Panel cuadroA = cuadros[i - gap];
                        Panel cuadroB = cuadros[i];

                        // Obtener los valores de los cuadros
                        int valorA = int.Parse((cuadroA.Controls[0] as Label).Text);
                        int valorB = int.Parse((cuadroB.Controls[0] as Label).Text);

                        // Determinar si se debe intercambiar dependiendo de si es ascendente o descendente
                        bool intercambiar = ascendente ? valorA > valorB : valorA < valorB;

                        // Si es necesario intercambiar, hacerlo con animación
                        if (intercambiar)
                        {
                            // Animar el intercambio de los cuadros
                            Cuadritos.IntercambiarCuadrosAnimado(parent, i - gap, i);
                        }
                    }
                }
            }
        }
    }
}
