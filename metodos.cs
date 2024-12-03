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

        //public void ShellAsendente(int[] A, FlowLayoutPanel panel)
        //{
        //    int N = A.Length;

        //    if (panel.Controls.Count != N)
        //    {
        //        throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
        //    }

        //    List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

        //    // Se comienza con una secuencia de gaps (puedes ajustar la secuencia de "h" según tu preferencia)
        //    int h = N / 2;

        //    while (h > 0)
        //    {
        //        // Realizamos una inserción con el gap actual
        //        for (int i = h; i < N; i++)
        //        {
        //            int AUX = A[i];
        //            int j = i;

        //            Panel cuadroActual = cuadros[i];
        //            cuadroActual.BackColor = cuadrito.GenerarColorUnico();
        //            cuadroActual.Refresh();
        //            Application.DoEvents();
        //            Thread.Sleep(500); // Pausa para que se vea el cambio de color

        //            // Desplazar los elementos hacia la derecha
        //            while (j >= h && A[j - h] > AUX)
        //            {
        //                A[j] = A[j - h];

        //                // Actualizamos el cuadro visualmente
        //                cuadros[j].Controls[0].Text = A[j].ToString();
        //                cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

        //                cuadros[j].BackColor = cuadrito.GenerarColorUnico(); ; // Resaltamos el cuadro que está siendo desplazado
        //                cuadros[j - h].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado

        //                // Forzar actualización visual
        //                panel.Refresh();
        //                Application.DoEvents();
        //                Thread.Sleep(500);

        //                j -= h;
        //            }

        //            // Insertamos el elemento en la posición correcta
        //            A[j] = AUX;
        //            cuadros[j].Controls[0].Text = AUX.ToString();
        //            cuadrito.AnimarCambioDeTamaño(cuadros[j], AUX);

        //            cuadros[j].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que ha sido colocado en la posición correcta
        //            panel.Refresh();
        //            Application.DoEvents();
        //            Thread.Sleep(500); // Pausa para que se vea el cambio de color
        //        }

        //        // Reducir el gap según la secuencia
        //        h /= 2;
        //    }
        //}

        //public void ShellAsendente(int[] A, FlowLayoutPanel panel)
        //{
        //    int N = A.Length;

        //    if (panel.Controls.Count != N)
        //    {
        //        throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
        //    }

        //    List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

        //    // Se comienza con una secuencia de gaps (puedes ajustar la secuencia de "h" según tu preferencia)
        //    int h = N / 2;

        //    while (h > 0)
        //    {
        //        // Realizamos una inserción con el gap actual
        //        for (int i = h; i < N; i++)
        //        {
        //            int AUX = A[i];
        //            int j = i;

        //            Panel cuadroActual = cuadros[i];
        //            cuadroActual.BackColor = cuadrito.GenerarColorUnico();
        //            cuadroActual.Refresh();
        //            Application.DoEvents();
        //            Thread.Sleep(700); // Aumentamos la pausa para que se vea el cambio de color

        //            // Desplazar los elementos hacia la derecha
        //            while (j >= h && A[j - h] > AUX)
        //            {
        //                A[j] = A[j - h];

        //                // Actualizamos el cuadro visualmente
        //                cuadros[j].Controls[0].Text = A[j].ToString();
        //                cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

        //                // Resaltamos el cuadro que está siendo desplazado
        //                cuadros[j].BackColor = cuadrito.GenerarColorUnico();
        //                cuadros[j - h].BackColor = Color.Red; // Usamos un color fijo para destacar el cuadro que se está comparando

        //                // Forzar actualización visual
        //                panel.Refresh();
        //                Application.DoEvents();
        //                Thread.Sleep(700); // Aumentamos la pausa para que se vea el cambio de color

        //                j -= h;
        //            }

        //            // Insertamos el elemento en la posición correcta
        //            A[j] = AUX;
        //            cuadros[j].Controls[0].Text = AUX.ToString();
        //            cuadrito.AnimarCambioDeTamaño(cuadros[j], AUX);

        //            // Resaltamos el cuadro que ha sido colocado en la posición correcta
        //            cuadros[j].BackColor = Color.Green; // Usamos un color fijo para indicar que este cuadro está en su posición final
        //            panel.Refresh();
        //            Application.DoEvents();
        //            Thread.Sleep(700); // Aumentamos la pausa para que se vea el cambio de color
        //        }

        //        // Reducir el gap según la secuencia
        //        h /= 2;
        //    }
        //}

        public async Task OrdenarShellConAnimacion(FlowLayoutPanel parent, bool ascendente)
        {
            int n = parent.Controls.Count;

            // Inicializar la distancia (gap)
            int gap = n / 2;

            // Realizar el ordenamiento Shell
            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    // Obtener el cuadro actual
                    Panel cuadroActual = parent.Controls[i] as Panel;

                    // Validar que el cuadro existe
                    if (cuadroActual == null) continue;

                    // Obtener el valor del cuadro actual
                    int valorActual = int.Parse((cuadroActual.Controls[0] as Label).Text);
                    int j = i;

                    // Comparar e insertar en la posición correcta
                    while (j >= gap)
                    {
                        // Obtener el cuadro en la posición j - gap
                        Panel cuadroComparar = parent.Controls[j - gap] as Panel;

                        // Validar que el cuadro a comparar existe
                        if (cuadroComparar == null) break;

                        // Obtener el valor del cuadro a comparar
                        int valorComparar = int.Parse((cuadroComparar.Controls[0] as Label).Text);

                        // Resaltar cuadros
                        Color colorOriginalActual = (Color)cuadroActual.Tag;
                        Color colorOriginalComparar = (Color)cuadroComparar.Tag;

                        cuadroActual.BackColor = Color.Yellow;
                        cuadroComparar.BackColor = Color.Yellow;
                        cuadroActual.Refresh();
                        cuadroComparar.Refresh();

                        await Task.Delay(500);

                        // Determinar si intercambiar basado en el orden ascendente/descendente
                        bool intercambiar = ascendente ? valorActual < valorComparar : valorActual > valorComparar;
                        if (intercambiar)
                        {
                            // Intercambiar con animación
                            await Cuadritos.IntercambiarCuadrosAnimado(parent, j, j - gap);
                            j -= gap; // Mover hacia atrás
                        }
                        else
                        {
                            break; // Si no se necesita intercambiar, salir del bucle
                        }

                        // Restaurar colores originales
                        cuadroActual.BackColor = colorOriginalActual;
                        cuadroComparar.BackColor = colorOriginalComparar;
                        cuadroActual.Refresh();
                        cuadroComparar.Refresh();
                    }
                }

                // Reducir la distancia (gap)
                gap /= 2;
            }
        }

        //public void ShellDescendente(int[] A, FlowLayoutPanel panel)
        //{
        //    int N = A.Length;

        //    if (panel.Controls.Count != N)
        //    {
        //        throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
        //    }

        //    List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

        //     Se comienza con una secuencia de gaps (puedes ajustar la secuencia de "h" según tu preferencia)
        //    int h = N / 2;

        //    while (h > 0)
        //    {
        //         Realizamos una inserción con el gap actual
        //        for (int i = h; i < N; i++)
        //        {
        //            int AUX = A[i];
        //            int j = i;

        //            Panel cuadroActual = cuadros[i];
        //            cuadroActual.BackColor = cuadrito.GenerarColorUnico();
        //            cuadroActual.Refresh();
        //            Application.DoEvents();
        //            Thread.Sleep(500); // Pausa para que se vea el cambio de color

        //             Desplazar los elementos hacia la izquierda (para orden descendente)
        //            while (j >= h && A[j - h] < AUX)  // Cambiado de > a <
        //            {
        //                A[j] = A[j - h];

        //                 Actualizamos el cuadro visualmente
        //                cuadros[j].Controls[0].Text = A[j].ToString();
        //                cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

        //                cuadros[j].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado
        //                cuadros[j - h].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado

        //                 Forzar actualización visual
        //                panel.Refresh();
        //                Application.DoEvents();
        //                Thread.Sleep(500); // Pausa para que se vea el cambio de color

        //                j -= h;
        //            }

        //             Insertamos el elemento en la posición correcta
        //            A[j] = AUX;
        //            cuadros[j].Controls[0].Text = AUX.ToString();
        //            cuadrito.AnimarCambioDeTamaño(cuadros[j], AUX);

        //            cuadros[j].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que ha sido colocado en la posición correcta
        //            panel.Refresh();
        //            Application.DoEvents();
        //            Thread.Sleep(500); // Pausa para que se vea el cambio de color
        //        }

        //         Reducir el gap según la secuencia
        //        h /= 2;
        //    }
        //}

        public void ShellDescendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Hacer que todos los cuadros sean blancos al inicio
            foreach (var cuadro in cuadros)
            {
                cuadro.BackColor = Color.White; // Establecer el color de fondo a blanco
                cuadro.Refresh();
            }

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

                        // Resaltamos el cuadro que está siendo desplazado
                        cuadros[j].BackColor = cuadrito.GenerarColorUnico();
                        cuadros[j - h].BackColor = cuadrito.GenerarColorUnico();

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

                    // Resaltamos el cuadro que ha sido colocado en la posición correcta
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
