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


        //metodos Ernesto
        Cuadritos cuadrito = new Cuadritos();

        public void BarajaAcendnete(int[] A, FlowLayoutPanel panel)
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
                cuadroActual.BackColor = Color.Orange;
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500);

                while (K >= 0 && AUX < A[K])
                {
                    Panel cuadroMayor = cuadros[K];
                    cuadroMayor.BackColor = Color.Red;

                    A[K + 1] = A[K];

                    cuadros[K + 1].Controls[0].Text = cuadroMayor.Controls[0].Text;
                    cuadrito.AnimarCambioDeTamaño(cuadros[K + 1], A[K + 1]);
                    cuadros[K + 1].BackColor = cuadrito.GenerarColorUnico();
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500);

                    cuadroMayor.BackColor = cuadrito.GenerarColorUnico();
                    K--;
                }
                A[K + 1] = AUX;
                cuadros[K + 1].Controls[0].Text = AUX.ToString();
                cuadrito.AnimarCambioDeTamaño(cuadros[K + 1], AUX);
                cuadrito.GenerarColorUnico();
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500);
            }
        }


        public void HeapSortAcendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;

            // Obtener los cuadros como una lista
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            for (int I = 1; I < N; I++) // Adaptado a índices base 0
            {
                int K = I + 1; // Ajuste para representar la posición correcta
                bool BAND = true;

                // Propagación hacia arriba en el montículo
                while (K > 1 && BAND)
                {
                    BAND = false;
                    int padre = (K - 1) / 2; // Índice del nodo padre

                    // Validar que los índices son válidos
                    if (padre < 0 || padre >= A.Length || K - 1 >= A.Length)
                        break;

                    // Si el hijo es mayor que el padre
                    if (A[K - 1] > A[padre])
                    {
                        // Intercambiar A[K - 1] con A[padre]
                        int AUX = A[padre];
                        A[padre] = A[K - 1];
                        A[K - 1] = AUX;

                        // Actualizar textos en los cuadros
                        cuadros[K - 1].Controls[0].Text = A[K - 1].ToString();
                        cuadros[padre].Controls[0].Text = A[padre].ToString();

                        // Animar el cambio de tamaño de los cuadros
                        cuadrito.AnimarCambioDeTamaño(cuadros[K - 1], A[K - 1]);
                        cuadrito.AnimarCambioDeTamaño(cuadros[padre], A[padre]);

                        // Actualizar índice K y marcar BAND como true
                        K = padre + 1; // Ajuste en K para índices base 0
                        BAND = true;

                        // Pausa para hacer visible la animación
                        Thread.Sleep(500);
                    }
                }
            }
            EliminarMonticuloAnimado(A, panel);
        }
        public void EliminarMonticuloAnimado(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;
            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            for (int I = N - 1; I >= 1; I--) // Ajuste para índices base 0
            {
                int AUX = A[I];
                A[I] = A[0];

                // Actualizar textos
                cuadros[I].Controls[0].Text = A[I].ToString();
                cuadros[0].Controls[0].Text = AUX.ToString();

                int IZQ = 1, DER = 2, K = 0;
                bool BOOL = true;

                while ((IZQ < I) && BOOL)
                {
                    int MAYOR = A[IZQ];
                    int AP = IZQ;

                    if ((DER < I) && (MAYOR < A[DER]))
                    {
                        MAYOR = A[DER];
                        AP = DER;
                    }

                    if (AUX < MAYOR)
                    {
                        A[K] = A[AP];

                        cuadros[K].Controls[0].Text = A[K].ToString();
                        cuadros[AP].Controls[0].Text = MAYOR.ToString();

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
                cuadros[K].Controls[0].Text = AUX.ToString();

                // **Corregir tamaños al final**
                cuadrito.AnimarCambioDeTamaño(cuadros[K], A[K]);
                cuadrito.AnimarCambioDeTamaño(cuadros[I], A[I]); // Aplicar al último cuadro procesado
            }

            // Ajustar tamaño del cuadro inicial (A[0]) al final del proceso
            cuadrito.AnimarCambioDeTamaño(cuadros[0], A[0]);
        }

        public void BarajaDescendente(int[] A, FlowLayoutPanel panel)
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
                cuadroActual.BackColor = Color.Orange;  // Resaltar el cuadro actual
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500);

                // Desplazar elementos menores que AUX
                while (K >= 0 && AUX > A[K])
                {
                    Panel cuadroMayor = cuadros[K];
                    cuadroMayor.BackColor = Color.Red;  // Resaltar el cuadro mayor
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500);

                    // Actualizar el valor en el arreglo
                    A[K + 1] = A[K];

                    // Desplazar visualmente el cuadro hacia la derecha
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

        public void ShellAsendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Se comienza con una secuencia de gaps (puedes ajustar la secuencia de "h" según tu preferencia)
            int h = N / 2;

            while (h > 0)
            {
                // Realizamos una inserción con el gap actual
                for (int i = h; i < N; i++)
                {
                    int AUX = A[i];
                    int j = i;

                    Panel cuadroActual = cuadros[i];
                    cuadroActual.BackColor = cuadrito.GenerarColorUnico();
                    cuadroActual.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500); // Pausa para que se vea el cambio de color

                    // Desplazar los elementos hacia la derecha
                    while (j >= h && A[j - h] > AUX)
                    {
                        A[j] = A[j - h];

                        // Actualizamos el cuadro visualmente
                        cuadros[j].Controls[0].Text = A[j].ToString();
                        cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

                        cuadros[j].BackColor = cuadrito.GenerarColorUnico(); ; // Resaltamos el cuadro que está siendo desplazado
                        cuadros[j - h].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado

                        // Forzar actualización visual
                        panel.Refresh();
                        Application.DoEvents();
                        Thread.Sleep(500);

                        j -= h;
                    }

                    // Insertamos el elemento en la posición correcta
                    A[j] = AUX;
                    cuadros[j].Controls[0].Text = AUX.ToString();
                    cuadrito.AnimarCambioDeTamaño(cuadros[j], AUX);

                    cuadros[j].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que ha sido colocado en la posición correcta
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500); // Pausa para que se vea el cambio de color
                }

                // Reducir el gap según la secuencia
                h /= 2;
            }
        }
        public void ShellDescendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Se comienza con una secuencia de gaps (puedes ajustar la secuencia de "h" según tu preferencia)
            int h = N / 2;

            while (h > 0)
            {
                // Realizamos una inserción con el gap actual
                for (int i = h; i < N; i++)
                {
                    int AUX = A[i];
                    int j = i;

                    Panel cuadroActual = cuadros[i];
                    cuadroActual.BackColor = cuadrito.GenerarColorUnico();
                    cuadroActual.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500); // Pausa para que se vea el cambio de color

                    // Desplazar los elementos hacia la izquierda (para orden descendente)
                    while (j >= h && A[j - h] < AUX)  // Cambiado de > a <
                    {
                        A[j] = A[j - h];

                        // Actualizamos el cuadro visualmente
                        cuadros[j].Controls[0].Text = A[j].ToString();
                        cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

                        cuadros[j].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado
                        cuadros[j - h].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado

                        // Forzar actualización visual
                        panel.Refresh();
                        Application.DoEvents();
                        Thread.Sleep(500); // Pausa para que se vea el cambio de color

                        j -= h;
                    }

                    // Insertamos el elemento en la posición correcta
                    A[j] = AUX;
                    cuadros[j].Controls[0].Text = AUX.ToString();
                    cuadrito.AnimarCambioDeTamaño(cuadros[j], AUX);

                    cuadros[j].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que ha sido colocado en la posición correcta
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500); // Pausa para que se vea el cambio de color
                }

                // Reducir el gap según la secuencia
                h /= 2;
            }
        }
        public void InsertionDirecta(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            for (int i = 1; i < N; i++)
            {
                int AUX = A[i];
                int j = i - 1;

                Panel cuadroActual = cuadros[i];
                cuadroActual.BackColor = cuadrito.GenerarColorUnico();
                cuadroActual.Refresh();
                Application.DoEvents();
                Thread.Sleep(500); // Pausa para que se vea el cambio de color

                // Mover los elementos de A[0..i-1], que son mayores que AUX,
                // a una posición adelante de su posición actual
                while (j >= 0 && A[j] > AUX)
                {
                    A[j + 1] = A[j];

                    // Actualizamos el cuadro visualmente
                    cuadros[j + 1].Controls[0].Text = A[j].ToString();
                    cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], A[j]);

                    cuadros[j + 1].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado
                    cuadros[j].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado

                    // Forzar actualización visual
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500);

                    j--;
                }

                // Insertar el elemento en la posición correcta
                A[j + 1] = AUX;
                cuadros[j + 1].Controls[0].Text = AUX.ToString();
                cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], AUX);

                cuadros[j + 1].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que ha sido colocado en la posición correcta
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500); // Pausa para que se vea el cambio de color
            }
        }

        public void InsertionDescendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            for (int i = 1; i < N; i++)
            {
                int AUX = A[i];
                int j = i - 1;

                Panel cuadroActual = cuadros[i];
                cuadroActual.BackColor = cuadrito.GenerarColorUnico();
                cuadroActual.Refresh();
                Application.DoEvents();
                Thread.Sleep(500); // Pausa para que se vea el cambio de color

                // Mover los elementos de A[0..i-1], que son menores que AUX,
                // a una posición adelante de su posición actual
                while (j >= 0 && A[j] < AUX) // Cambiado de > a < para orden descendente
                {
                    A[j + 1] = A[j];

                    // Actualizamos el cuadro visualmente
                    cuadros[j + 1].Controls[0].Text = A[j].ToString();
                    cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], A[j]);

                    cuadros[j + 1].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado
                    cuadros[j].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado

                    // Forzar actualización visual
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500);

                    j--;
                }

                // Insertar el elemento en la posición correcta
                A[j + 1] = AUX;
                cuadros[j + 1].Controls[0].Text = AUX.ToString();
                cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], AUX);

                cuadros[j + 1].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que ha sido colocado en la posición correcta
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500); // Pausa para que se vea el cambio de color
            }
        }

        public void QuicksortAscendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Llamamos al método recursivo de Quicksort
            QuicksortRecursivo(A, cuadros, panel, 0, N - 1);
        }

        // Método recursivo de Quicksort
        private void QuicksortRecursivo(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int inicio, int fin)
        {
            if (inicio < fin)
            {
                // Particionamos el arreglo y obtenemos el índice del pivote
                int pivoteIndex = Particionar(A, cuadros, panel, inicio, fin);

                // Ordenamos los subarreglos de forma recursiva
                QuicksortRecursivo(A, cuadros, panel, inicio, pivoteIndex - 1);
                QuicksortRecursivo(A, cuadros, panel, pivoteIndex + 1, fin);
            }
        }

        // Método para particionar el arreglo
        private int Particionar(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int inicio, int fin)
        {
            int pivote = A[fin]; // Elegimos el último elemento como pivote
            int i = inicio - 1;

            Panel cuadroPivote = cuadros[fin];
            cuadroPivote.BackColor = Color.Red; // Resaltar el pivote
            cuadroPivote.Refresh();
            panel.Refresh();
            Application.DoEvents();
            Thread.Sleep(500);

            for (int j = inicio; j < fin; j++)
            {
                // Comparar elementos con el pivote
                if (A[j] <= pivote) // Condición ascendente
                {
                    i++;
                    IntercambiarElementos(A, cuadros, panel, i, j);
                }
            }

            // Colocar el pivote en su posición correcta
            IntercambiarElementos(A, cuadros, panel, i + 1, fin);
            cuadroPivote.BackColor = cuadrito.GenerarColorUnico(); // Restaurar el color del pivote
            return i + 1; // Retornar la posición final del pivote
        }

        // Método para intercambiar dos elementos en el arreglo y actualizar la interfaz
        private void IntercambiarElementos(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int i, int j)
        {
            // Intercambiar elementos en el arreglo
            int temp = A[i];
            A[i] = A[j];
            A[j] = temp;

            // Actualizar los cuadros visualmente
            cuadros[i].Controls[0].Text = A[i].ToString();
            cuadros[j].Controls[0].Text = A[j].ToString();

            cuadrito.AnimarCambioDeTamaño(cuadros[i], A[i]);
            cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

            cuadros[i].BackColor = cuadrito.GenerarColorUnico(); // Resaltar los cuadros intercambiados
            cuadros[j].BackColor = cuadrito.GenerarColorUnico();

            // Forzar actualización visual
            panel.Refresh();
            Application.DoEvents();
            Thread.Sleep(500); // Pausa para mostrar el intercambio

        }
        public void QuicksortDescendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Llamamos al método recursivo de Quicksort
            QuicksortRecursivoB(A, cuadros, panel, 0, N - 1);
        }

        // Método recursivo de Quicksort
        private void QuicksortRecursivoB(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int inicio, int fin)
        {
            if (inicio < fin)
            {
                // Particionamos el arreglo y obtenemos el índice del pivote
                int pivoteIndex = ParticionarB(A, cuadros, panel, inicio, fin);

                // Ordenamos los subarreglos de forma recursiva
                QuicksortRecursivoB(A, cuadros, panel, inicio, pivoteIndex - 1);
                QuicksortRecursivoB(A, cuadros, panel, pivoteIndex + 1, fin);
            }
        }

        // Método para particionar el arreglo
        private int ParticionarB(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int inicio, int fin)
        {
            int pivote = A[fin]; // Elegimos el último elemento como pivote
            int i = inicio - 1;

            Panel cuadroPivote = cuadros[fin];
            cuadroPivote.BackColor = Color.Red; // Resaltar el pivote
            cuadroPivote.Refresh();
            panel.Refresh();
            Application.DoEvents();
            Thread.Sleep(500);

            for (int j = inicio; j < fin; j++)
            {
                // Comparar elementos con el pivote (condición descendente)
                if (A[j] >= pivote) // Cambiar condición a descendente
                {
                    i++;
                    IntercambiarElementosB(A, cuadros, panel, i, j);
                }
            }

            // Colocar el pivote en su posición correcta
            IntercambiarElementosB(A, cuadros, panel, i + 1, fin);
            cuadroPivote.BackColor = cuadrito.GenerarColorUnico(); // Restaurar el color del pivote
            return i + 1; // Retornar la posición final del pivote
        }

        // Método para intercambiar dos elementos en el arreglo y actualizar la interfaz
        private void IntercambiarElementosB(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int i, int j)
        {
            // Intercambiar elementos en el arreglo
            int temp = A[i];
            A[i] = A[j];
            A[j] = temp;

            // Actualizar los cuadros visualmente
            cuadros[i].Controls[0].Text = A[i].ToString();
            cuadros[j].Controls[0].Text = A[j].ToString();

            cuadrito.AnimarCambioDeTamaño(cuadros[i], A[i]);
            cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

            cuadros[i].BackColor = cuadrito.GenerarColorUnico(); // Resaltar los cuadros intercambiados
            cuadros[j].BackColor = cuadrito.GenerarColorUnico();

            // Forzar actualización visual
            panel.Refresh();
            Application.DoEvents();
            Thread.Sleep(500); // Pausa para mostrar el intercambio
        }

    }
}
