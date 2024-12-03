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

        //
        //
        //
        //Inicio de métodos erika

        Cuadritos cuadrito = new Cuadritos();

        public void Baraja(int[] A, FlowLayoutPanel panel, bool esAscendente)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            for (int I = 1; I < N; I++)
            {
                int AUX = A[I];
                int K = I - 1;

                Panel cuadroActual = cuadros[I];
                cuadroActual.BackColor = Color.Red;  // Resaltar el cuadro actual
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500);

                // Ajustar la comparación según el orden ascendente o descendente
                while (K >= 0 && (esAscendente ? AUX < A[K] : AUX > A[K]))  // Dependiendo del booleano
                {
                    Panel cuadroMayor = cuadros[K];
                    cuadroMayor.BackColor = Color.Red;  // Resaltar el cuadro mayor
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500);

                    // Actualizar el valor en el arreglo
                    A[K + 1] = A[K];

                    // Desplazar visualmente el cuadro hacia la derecha o izquierda según el orden
                    cuadros[K + 1].Controls[0].Text = cuadros[K].Controls[0].Text;

                    // Animar el tamaño del cuadro desplazado
                    cuadrito.AnimarCambioDeTamaño(cuadros[K + 1], A[K + 1]);
                    cuadros[K + 1].BackColor = cuadrito.GenerarColorUnico();
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500);

                    // Restaurar color original del cuadro mayor
                    cuadroMayor.BackColor = cuadrito.GenerarColorUnico();
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500);

                    K--;
                }

                // Insertar el elemento en la posición correcta
                A[K + 1] = AUX;
                cuadros[K + 1].Controls[0].Text = AUX.ToString();

                // Animar el tamaño del cuadro insertado
                cuadrito.AnimarCambioDeTamaño(cuadros[K + 1], AUX);
                cuadros[K + 1].BackColor = Color.LightBlue;  // Restaurar color
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500);
            }
        }
        public void HeapSort(int[] A, FlowLayoutPanel panel, bool esAscendente)
        {
            int N = A.Length;
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            for (int I = (N / 2) - 1; I >= 0; I--)
            {
                int K = I;
                bool BAND = true;

                while (K < N)
                {
                    int IZQ = 2 * K + 1;
                    int DER = IZQ + 1;
                    int AP = K;

                    if (IZQ < N && (esAscendente ? A[IZQ] > A[AP] : A[IZQ] < A[AP]))
                    {
                        AP = IZQ;
                    }

                    if (DER < N && (esAscendente ? A[DER] > A[AP] : A[DER] < A[AP]))
                    {
                        AP = DER;
                    }

                    // Si el nodo es mayor/menor que el hijo, intercambiar
                    if (AP != K)
                    {
                        // Resaltar los cuadros para animación
                        cuadros[K].BackColor = Color.Red;
                        cuadros[AP].BackColor = Color.Red;
                        Thread.Sleep(200);

                        int AUX = A[K];
                        A[K] = A[AP];
                        A[AP] = AUX;

                        // Actualizar el texto de los cuadros
                        cuadros[K].Controls[0].Text = A[K].ToString();
                        cuadros[AP].Controls[0].Text = A[AP].ToString();
                        cuadrito.AnimarCambioDeTamaño(cuadros[K], A[K]);
                        cuadrito.AnimarCambioDeTamaño(cuadros[AP], A[AP]);

                        // Restaurar colores a aleatorio
                        cuadros[K].BackColor = cuadrito.GenerarColorUnico();
                        cuadros[AP].BackColor = cuadrito.GenerarColorUnico();

                        K = AP; // Avanzar el índice
                    }
                    else
                    {
                        break;
                    }

                    // Forzar actualización visual
                    cuadros[K].Refresh();
                    Thread.Sleep(500); // Pausa para visualizar el cambio
                }
            }

            // Llamar a la función de eliminación del montículo
            EliminarMonticulo(A, panel, esAscendente);
        }

        public void EliminarMonticulo(int[] A, FlowLayoutPanel panel, bool esAscendente)
        {
            int N = A.Length;
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            for (int I = N - 1; I >= 1; I--)
            {
                int AUX = A[I];
                A[I] = A[0];

                // Resaltar en rojo los cuadros que van a cambiar
                cuadros[I].BackColor = Color.Red;
                cuadros[0].BackColor = Color.Red;

                // Forzar actualización visual
                cuadros[I].Refresh();
                cuadros[0].Refresh();

                Thread.Sleep(500); // Pausa para visualizar el cambio

                // Actualizar visualmente los cuadros
                cuadros[I].Controls[0].Text = A[I].ToString();
                cuadros[0].Controls[0].Text = AUX.ToString();

                // Cambiar a un color aleatorio después de la actualización
                cuadros[I].BackColor = cuadrito.GenerarColorUnico();
                cuadros[0].BackColor = cuadrito.GenerarColorUnico();

                cuadros[I].Refresh();
                cuadros[0].Refresh();

                int IZQ = 1, DER = 2, K = 0;
                bool BOOL = true;

                while ((IZQ < I) && BOOL)
                {
                    int valorComparado = esAscendente ? A[IZQ] : A[IZQ];
                    int AP = IZQ;

                    if ((DER < I) && (esAscendente ? valorComparado < A[DER] : valorComparado > A[DER]))
                    {
                        valorComparado = A[DER];
                        AP = DER;
                    }

                    // Resaltar en rojo los cuadros que van a cambiar
                    cuadros[K].BackColor = Color.Red;
                    cuadros[AP].BackColor = Color.Red;

                    // Forzar actualización visual
                    cuadros[K].Refresh();
                    cuadros[AP].Refresh();

                    Thread.Sleep(500); // Pausa para visualizar el cambio

                    if ((esAscendente && AUX < valorComparado) || (!esAscendente && AUX > valorComparado))
                    {
                        A[K] = A[AP];

                        // Actualizar visualmente los cuadros
                        cuadros[K].Controls[0].Text = A[K].ToString();
                        cuadros[AP].Controls[0].Text = valorComparado.ToString();

                        // Cambiar a un color aleatorio después de la actualización
                        cuadros[K].BackColor = cuadrito.GenerarColorUnico();
                        cuadros[AP].BackColor = cuadrito.GenerarColorUnico();

                        cuadros[K].Refresh();
                        cuadros[AP].Refresh();

                        K = AP;
                    }
                    else
                    {
                        BOOL = false;
                    }

                    IZQ = 2 * K + 1;
                    DER = IZQ + 1;
                }

                A[K] = AUX;

                // Resaltar en rojo el cuadro final que va a cambiar
                cuadros[K].BackColor = Color.Red;
                cuadros[K].Refresh();

                Thread.Sleep(500); // Pausa para visualizar el cambio

                cuadros[K].Controls[0].Text = AUX.ToString();

                // Cambiar a un color aleatorio después de la actualización
                cuadros[K].BackColor = cuadrito.GenerarColorUnico();
                cuadros[K].Refresh();

                // Animar el cambio de tamaño
                cuadrito.AnimarCambioDeTamaño(cuadros[K], A[K]);
                cuadrito.AnimarCambioDeTamaño(cuadros[I], A[I]);
            }

            // Animar el último cuadro restante
            cuadrito.AnimarCambioDeTamaño(cuadros[0], A[0]);
        }


    }
}
