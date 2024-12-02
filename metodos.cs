using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    internal class Metodos
    {
        public async Task OrdenarInsercionBinariaConAnimacion(FlowLayoutPanel parent, bool ascendente)
        {
            int n = parent.Controls.Count;

            for (int i = 1; i < n; i++)
            {
                // Obtener el cuadro actual y su valor
                Panel cuadroActual = parent.Controls[i] as Panel;
                int valorActual = int.Parse((cuadroActual.Controls[0] as Label).Text);

                // Buscar la posición correcta usando búsqueda binaria
                int posicion = await BuscarPosicionBinariaAnimada(parent, valorActual, 0, i - 1, ascendente);

                // Mover el cuadro actual a la posición encontrada
                if (posicion < i)
                {
                    await MoverCuadroConIntercambio(parent, i, posicion);
                }
            }
        }

        private async Task<int> BuscarPosicionBinariaAnimada(FlowLayoutPanel parent, int valor, int inicio, int fin, bool ascendente)
        {
            while (inicio <= fin)
            {
                int medio = (inicio + fin) / 2;

                // Comparar con el cuadro en la posición `medio`
                Panel cuadroMedio = parent.Controls[medio] as Panel;
                int valorMedio = int.Parse((cuadroMedio.Controls[0] as Label).Text);

                // Resaltar los cuadros comparados
                cuadroMedio.BackColor = Color.Yellow;
                cuadroMedio.Refresh();
                await Task.Delay(500);

                // Comparación ajustada al orden
                if (ascendente ? (valor < valorMedio) : (valor > valorMedio))
                {
                    cuadroMedio.BackColor = Color.Red;
                    fin = medio - 1;
                }
                else
                {
                    cuadroMedio.BackColor = Color.Green;
                    inicio = medio + 1;
                }

                cuadroMedio.Refresh();
                await Task.Delay(500);

                // Restaurar el color original
                cuadroMedio.BackColor = Color.Black;
                cuadroMedio.Refresh();
            }

            return inicio;
        }
        private async Task MoverCuadroConIntercambio(FlowLayoutPanel parent, int origen, int destino)
        {
            // Mover los cuadros desde `origen` hasta `destino`
            for (int i = origen; i > destino; i--)
            {
                // Llamar al método animado para intercambiar cuadros
                await Cuadritos.IntercambiarCuadrosAnimado(parent, i, i - 1);
            }
        }
        public async Task OrdenarBurbujaConAnimacion(FlowLayoutPanel parent, bool ascendente)
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

                    // Resaltar cuadros
                    Color colorOriginalA = (Color)cuadroA.Tag;
                    Color colorOriginalB = (Color)cuadroB.Tag;

                    cuadroA.BackColor = Color.Yellow;
                    cuadroB.BackColor = Color.Yellow;
                    cuadroA.Refresh();
                    cuadroB.Refresh();

                    await Task.Delay(500);

                    // Determinar si intercambiar basado en el orden ascendente/descendente
                    bool intercambiar = ascendente ? valorA > valorB : valorA < valorB;
                    if (intercambiar)
                    {
                        // Intercambiar con animación
                        await Cuadritos.IntercambiarCuadrosAnimado(parent, j, j + 1);
                    }

                    // Restaurar colores originales
                    cuadroA.BackColor = colorOriginalA;
                    cuadroB.BackColor = colorOriginalB;
                    cuadroA.Refresh();
                    cuadroB.Refresh();
                }
            }
        }
        public async Task OrdenarBurbujaConAnimacionMejorado(FlowLayoutPanel parent, bool ascendente)
        {
            for (int i = 0; i < parent.Controls.Count - 1; i++)
            {
                bool intercambio = false;

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

                    // Resaltar cuadros
                    Color colorOriginalA = (Color)cuadroA.Tag;
                    Color colorOriginalB = (Color)cuadroB.Tag;

                    cuadroA.BackColor = Color.Yellow;
                    cuadroB.BackColor = Color.Yellow;
                    cuadroA.Refresh();
                    cuadroB.Refresh();

                    await Task.Delay(500);

                    // Determinar si intercambiar basado en el orden ascendente/descendente
                    bool intercambiar = ascendente ? valorA > valorB : valorA < valorB;
                    if (intercambiar)
                    {
                        // Intercambiar con animación
                        await Cuadritos.IntercambiarCuadrosAnimado(parent, j, j + 1);
                        intercambio = true;
                    }

                    // Restaurar colores originales
                    cuadroA.BackColor = colorOriginalA;
                    cuadroB.BackColor = colorOriginalB;
                    cuadroA.Refresh();
                    cuadroB.Refresh();
                }

                // Si no hubo intercambios, el arreglo ya está ordenado
                if (!intercambio) break;
            }
        }
        private Cuadritos creador = new Cuadritos();
        public async Task MergeSortConAnimacion(FlowLayoutPanel parent, bool ascendente)
        {
            // Convertir los bloques a una lista de valores enteros
            List<int> valores = new List<int>();
            foreach (Panel panel in parent.Controls)
            {
                int valor = int.Parse((panel.Controls[0] as Label).Text);
                valores.Add(valor);
            }

            // Llamar a la función recursiva con animación
            await MergeSortAnimado(parent, valores, 0, valores.Count - 1, ascendente);
        }
        private async Task MergeSortAnimado(FlowLayoutPanel parent, List<int> valores, int inicio, int fin, bool ascendente)
        {
            // Caso base: si la sublista tiene un solo elemento
            if (inicio >= fin) return;

            int medio = (inicio + fin) / 2;

            // Animar la división (resaltar la partición actual)
            ResaltarRango(parent, inicio, fin, System.Drawing.Color.Yellow);
            await Task.Delay(1000);

            // Llamar recursivamente a las mitades
            await MergeSortAnimado(parent, valores, inicio, medio, ascendente);
            await MergeSortAnimado(parent, valores, medio + 1, fin, ascendente);

            // Combinar las dos mitades
            await MergeAnimado(parent, valores, inicio, medio, fin, ascendente);
        }
        private async Task MergeAnimado(FlowLayoutPanel parent, List<int> valores, int inicio, int medio, int fin, bool ascendente)
        {
            int i = inicio, j = medio + 1;
            List<int> temp = new List<int>();

            // Animar las particiones
            ResaltarRango(parent, inicio, medio, Color.LightBlue);
            ResaltarRango(parent, medio + 1, fin, Color.LightGreen);
            await Task.Delay(1000);

            while (i <= medio && j <= fin)
            {
                if ((ascendente && valores[i] <= valores[j]) || (!ascendente && valores[i] >= valores[j]))
                {
                    temp.Add(valores[i]);
                    i++;
                }
                else
                {
                    temp.Add(valores[j]);
                    j++;
                }
            }

            // Agregar los elementos restantes
            while (i <= medio) temp.Add(valores[i++]);
            while (j <= fin) temp.Add(valores[j++]);

            // Actualizar los valores visualmente con animación
            for (int k = 0; k < temp.Count; k++)
            {
                valores[inicio + k] = temp[k];

                Panel cuadro = parent.Controls[inicio + k] as Panel;
                if (cuadro == null) continue;

                // Actualizamos visualmente el tamaño y el color
                int nuevoValor = temp[k];
                Size tamañoInicial = cuadro.Size;
                Size tamañoFinal = new Size(nuevoValor * 10, nuevoValor * 10);

                // Animar el cambio de tamaño
                int pasos = 20;
                for (int paso = 0; paso <= pasos; paso++)
                {
                    cuadro.Invoke((MethodInvoker)(() =>
                    {
                        cuadro.Size = new Size(
                            Interpolar(tamañoInicial.Width, tamañoFinal.Width, paso, pasos),
                            Interpolar(tamañoInicial.Height, tamañoFinal.Height, paso, pasos)
                        );
                        cuadro.BackColor = Color.Green;
                        (cuadro.Controls[0] as Label).Text = nuevoValor.ToString();
                        cuadro.Refresh();
                    }));

                    await Task.Delay(20); // Pausa para que la animación sea visible
                }
            }

            // Restaurar el color después de la animación
            RestaurarColor(parent, inicio, fin, Color.Black);
        }
        private static int Interpolar(int inicio, int fin, int pasoActual, int totalPasos)
        {
            return inicio + (fin - inicio) * pasoActual / totalPasos;
        }
        private void ResaltarRango(FlowLayoutPanel parent, int inicio, int fin, System.Drawing.Color color)
        {
            for (int i = inicio; i <= fin; i++)
            {
                Panel panel = parent.Controls[i] as Panel;
                if (panel != null)
                    panel.BackColor = color;
            }
        }
        private void RestaurarColor(FlowLayoutPanel parent, int inicio, int fin, System.Drawing.Color color)
        {
            for (int i = inicio; i <= fin; i++)
            {
                Panel panel = parent.Controls[i] as Panel;
                if (panel != null)
                    panel.BackColor = color;
            }
        }
    }
}
