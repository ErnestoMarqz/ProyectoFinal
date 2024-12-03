using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    internal class metodos
    {

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

        public void BucketSortAcendente1(int[] arr, FlowLayoutPanel panel)
        {
            // Crear lista de cuadros
            if (panel.Controls.Count != arr.Length)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Encontrar el valor máximo para determinar el número de cubos
            int maxValue = arr.Max();

            // Crear los cubos visuales
            int bucketCount = maxValue / 10 + 1; // Ajustar tamaño del cubo
            List<List<int>> buckets = new List<List<int>>(bucketCount);
            List<FlowLayoutPanel> cubosVisuales = new List<FlowLayoutPanel>();
            FlowLayoutPanel cubosPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                BackColor = Color.LightGray,
                Margin = new Padding(10)
            };
            panel.Parent.Controls.Add(cubosPanel);

            for (int i = 0; i < bucketCount; i++)
            {
                buckets.Add(new List<int>());
                FlowLayoutPanel cuboVisual = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.TopDown,
                    AutoSize = true,
                    BackColor = Color.AliceBlue,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle
                };
                cubosPanel.Controls.Add(cuboVisual);
                cubosVisuales.Add(cuboVisual);
            }

            // Animar distribución de elementos en los cubos
            foreach (int num in arr)
            {
                int bucketIndex = num / 10; // Ajustar tamaño del cubo
                buckets[bucketIndex].Add(num);

                // Mover visualmente el cuadro al cubo correspondiente
                Panel cuadro = cuadros.First(c => c.Controls[0].Text == num.ToString());
                cuadros.Remove(cuadro);
                AnimarMovimiento(cuadro, cubosVisuales[bucketIndex]);
            }

            // Ordenar cada cubo y animar
            int index = 0;
            foreach (List<int> bucket in buckets)
            {
                bucket.Sort(); // Ordenar cubo
                FlowLayoutPanel cuboVisual = cubosVisuales[buckets.IndexOf(bucket)];

                foreach (int num in bucket)
                {
                    Panel cuadro = cuboVisual.Controls.Cast<Panel>().First(c => c.Controls[0].Text == num.ToString());
                    cuadros.Add(cuadro);

                    // Combinar visualmente los elementos en el panel original
                    AnimarMovimiento(cuadro, panel);
                    arr[index++] = num;
                }
            }
        }

        private void AnimarMovimiento1(Panel cuadro, FlowLayoutPanel destino)
        {
            // Coordenadas iniciales y finales dentro del destino
            Point startLocation = cuadro.Location;
            Point endLocation = destino.PointToClient(cuadro.Parent.PointToScreen(cuadro.Location));

            endLocation = new Point(
                Clamp(endLocation.X, 0, destino.ClientSize.Width - cuadro.Width),
                Clamp(endLocation.Y, 0, destino.ClientSize.Height - cuadro.Height)
            );

            int deltaX = (endLocation.X - startLocation.X) / 10;
            int deltaY = (endLocation.Y - startLocation.Y) / 10;

            // Animar movimiento
            for (int i = 0; i < 10; i++)
            {
                cuadro.Location = new Point(
                    Clamp(cuadro.Location.X + deltaX, 0, destino.ClientSize.Width - cuadro.Width),
                    Clamp(cuadro.Location.Y + deltaY, 0, destino.ClientSize.Height - cuadro.Height)
                                                       );

                cuadro.Refresh();
                Application.DoEvents();
                Thread.Sleep(50);


                // Asegurarse de agregar el cuadro al destino al final
                destino.Controls.Add(cuadro);
                destino.Refresh();
            }

            
        }

        public int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }


        public void BucketSortAcendente(int[] arr, FlowLayoutPanel panel, int bucketCount)
        {
            // Crear lista de cuadros
            if (panel.Controls.Count != arr.Length)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Encontrar el valor máximo para determinar el número de cubos
            int maxValue = arr.Max();

            // Crear los cubos visuales
            //int bucketCount = maxValue / 10 + 1; // Ajustar tamaño del cubo

            int bucketSize = (maxValue / bucketCount) + 1;

            // Distribuir los números en las cubetas

            List<List<int>> buckets = new List<List<int>>(bucketCount);
            List<FlowLayoutPanel> cubosVisuales = new List<FlowLayoutPanel>();

            FlowLayoutPanel cubosPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true, // Permitirá que los cubos se ajusten si hay muchos
                AutoSize = true,
                BackColor = Color.LightGray,
                Margin = new Padding(10),
                Dock = DockStyle.Top // Aseguramos que esté en la parte superior del contenedor
            };

            // Asegurarse de que el panel de cubos se añada al contenedor correcto
            panel.Parent.Controls.Add(cubosPanel);

            // Crear los cubos visuales
            for (int i = 0; i < bucketCount; i++)
            {
                buckets.Add(new List<int>());
                FlowLayoutPanel cuboVisual = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.TopDown,
                    AutoSize = true,
                    BackColor = Color.AliceBlue,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(5)
                };
                cubosPanel.Controls.Add(cuboVisual);
                cubosVisuales.Add(cuboVisual);
            }

            // Animar distribución de elementos en los cubos
            foreach (int num in arr)
            {
                int bucketIndex = num / (bucketCount); // Ajustar tamaño del cubo
                buckets[bucketIndex].Add(num);

                // Mover visualmente el cuadro al cubo correspondiente
                Panel cuadro = cuadros.First(c => c.Controls[0].Text == num.ToString());
                cuadros.Remove(cuadro);
                AnimarMovimiento(cuadro, cubosVisuales[bucketIndex]);
            }

            // Ordenar cada cubo y animar
            int index = 0;
            foreach (List<int> bucket in buckets)
            {
                bucket.Sort(); // Ordenar cubo
                FlowLayoutPanel cuboVisual = cubosVisuales[buckets.IndexOf(bucket)];

                foreach (int num in bucket)
                {
                    Panel cuadro = cuboVisual.Controls.Cast<Panel>().First(c => c.Controls[0].Text == num.ToString());
                    cuadros.Add(cuadro);

                    // Combinar visualmente los elementos en el panel original
                    AnimarMovimiento(cuadro, panel);
                    arr[index++] = num;
                }
            }
            panel.Parent.Controls.Remove(cubosPanel);


        }

        private void AnimarMovimiento(Panel cuadro, FlowLayoutPanel destino)
        {
            // Coordenadas iniciales y finales dentro del destino
            Point startLocation = cuadro.Location;
            Point endLocation = destino.PointToClient(cuadro.Parent.PointToScreen(cuadro.Location));

            endLocation = new Point(
                Clamp(endLocation.X, 0, destino.ClientSize.Width - cuadro.Width),
                Clamp(endLocation.Y, 0, destino.ClientSize.Height - cuadro.Height)
            );

            int deltaX = (endLocation.X - startLocation.X) / 10;
            int deltaY = (endLocation.Y - startLocation.Y) / 10;

            // Animar movimiento
            for (int i = 0; i < 10; i++)
            {
                cuadro.Location = new Point(
                    Clamp(cuadro.Location.X + deltaX, 0, destino.ClientSize.Width - cuadro.Width),
                    Clamp(cuadro.Location.Y + deltaY, 0, destino.ClientSize.Height - cuadro.Height)
                );

                cuadro.Refresh();
                Application.DoEvents();
                Thread.Sleep(50);
            }

            // Asegurarse de agregar el cuadro al destino al final
            destino.Controls.Add(cuadro);
            destino.Refresh();
        }

        




    }
}




    

