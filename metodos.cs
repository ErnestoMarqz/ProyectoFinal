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

        public async Task OrdenarShellConAnimacion(FlowLayoutPanel parent, bool ascendente)
        {
            int n = parent.Controls.Count;

            // Inicializar el intervalo
            int intervalo = n / 2;

            while (intervalo > 0)
            {
                // Hacer una pasada con el intervalo actual
                for (int i = intervalo; i < n; i++)
                {
                    int j = i;
                    while (j >= intervalo)
                    {
                        // Obtener los cuadros actuales y adyacentes
                        Panel cuadroA = parent.Controls[j - intervalo] as Panel;
                        Panel cuadroB = parent.Controls[j] as Panel;

                        // Validar que los cuadros existen
                        if (cuadroA == null || cuadroB == null) break;

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
                            await Cuadritos.IntercambiarCuadrosAnimado(parent, j - intervalo, j);
                        }

                        // Restaurar colores originales
                        cuadroA.BackColor = colorOriginalA;
                        cuadroB.BackColor = colorOriginalB;
                        cuadroA.Refresh();
                        cuadroB.Refresh();

                        j -= intervalo; // Avanzar a la siguiente posición
                    }
                }

                intervalo /= 2; // Reducir el intervalo
            }
        }
       
        public async Task OrdenarShellConAnimacionDescendente(FlowLayoutPanel parent)
        {
            int n = parent.Controls.Count;

            // Inicializar el intervalo
            int intervalo = n / 2;

            while (intervalo > 0)
            {
                // Hacer una pasada con el intervalo actual
                for (int i = intervalo; i < n; i++)
                {
                    int j = i;
                    while (j >= intervalo)
                    {
                        // Obtener los cuadros actuales y adyacentes
                        Panel cuadroA = parent.Controls[j - intervalo] as Panel;
                        Panel cuadroB = parent.Controls[j] as Panel;

                        // Validar que los cuadros existen
                        if (cuadroA == null || cuadroB == null) break;

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

                        // Determinar si intercambiar basado en el orden descendente
                        bool intercambiar = valorA < valorB;
                        if (intercambiar)
                        {
                            // Intercambiar con animación
                            await Cuadritos.IntercambiarCuadrosAnimado(parent, j - intervalo, j);
                        }

                        // Restaurar colores originales
                        cuadroA.BackColor = colorOriginalA;
                        cuadroB.BackColor = colorOriginalB;
                        cuadroA.Refresh();
                        cuadroB.Refresh();

                        j -= intervalo; // Avanzar a la siguiente posición
                    }
                }

                intervalo /= 2; // Reducir el intervalo
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

            // Inicializar todos los cuadros a blanco
            foreach (var cuadro in cuadros)
            {
                cuadro.BackColor = Color.White;
            }

            for (int i = 1; i < N; i++)
            {
                int AUX = A[i];
                int j = i - 1;

                // Resaltar el cuadro actual en amarillo
                Panel cuadroActual = cuadros[i];
                cuadroActual.BackColor = Color.Yellow;
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

                    // Resaltar el cuadro que está siendo desplazado en amarillo
                    cuadros[j + 1].BackColor = Color.Yellow; // Color amarillo para el cuadro que se desplaza

                    // Forzar actualización visual
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500); // Pausa para que se vea el cambio de color

                    j--;
                }

                // Insertar el elemento en la posición correcta
                A[j + 1] = AUX;
                cuadros[j + 1].Controls[0].Text = AUX.ToString();
                cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], AUX);

                // Resaltar el cuadro que ha sido colocado en la posición correcta en amarillo
                cuadros[j + 1].BackColor = Color.Yellow;
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500); // Pausa para que se vea el cambio de color

                // Cambiar el color de los cuadros procesados a verde
                for (int k = 0; k <= j + 1; k++) // Cambiar todos los cuadros hasta la posición actual a verde
                {
                    cuadros[k].BackColor = Color.Green; // Cambiar a verde
                }
            }

            // Al final, aseguramos que todos los cuadros estén en verde
            for (int k = 0; k < N; k++)
            {
                if (cuadros[k].BackColor != Color.Green)
                {
                    cuadros[k].BackColor = Color.Green; // Aseguramos que todos los cuadros procesados estén en verde
                }
            }
        }

        //public void InsertionDescendente(int[] A, FlowLayoutPanel panel)
        //{
        //    int N = A.Length;

        //    if (panel.Controls.Count != N)
        //    {
        //        throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
        //    }

        //    List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

        //    for (int i = 1; i < N; i++)
        //    {
        //        int AUX = A[i];
        //        int j = i - 1;

        //        Panel cuadroActual = cuadros[i];
        //        cuadroActual.BackColor = cuadrito.GenerarColorUnico();
        //        cuadroActual.Refresh();
        //        Application.DoEvents();
        //        Thread.Sleep(500); // Pausa para que se vea el cambio de color

        //        // Mover los elementos de A[0..i-1], que son menores que AUX,
        //        // a una posición adelante de su posición actual
        //        while (j >= 0 && A[j] < AUX) // Cambiado de > a < para orden descendente
        //        {
        //            A[j + 1] = A[j];

        //            // Actualizamos el cuadro visualmente
        //            cuadros[j + 1].Controls[0].Text = A[j].ToString();
        //            cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], A[j]);

        //            cuadros[j + 1].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado
        //            cuadros[j].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado

        //            // Forzar actualización visual
        //            panel.Refresh();
        //            Application.DoEvents();
        //            Thread.Sleep(500);

        //            j--;
        //        }

        //        // Insertar el elemento en la posición correcta
        //        A[j + 1] = AUX;
        //        cuadros[j + 1].Controls[0].Text = AUX.ToString();
        //        cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], AUX);

        //        cuadros[j + 1].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que ha sido colocado en la posición correcta
        //        panel.Refresh();
        //        Application.DoEvents();
        //        Thread.Sleep(500); // Pausa para que se vea el cambio de color
        //    }
        //}
        public void InsertionDescendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Inicializar todos los cuadros a un color único (por ejemplo, azul claro)
            Color colorInicial = Color.LightBlue; // Color único para los cuadros iniciales
            foreach (var cuadro in cuadros)
            {
                cuadro.BackColor = colorInicial;
            }

            for (int i = 1; i < N; i++)
            {
                int AUX = A[i];
                int j = i - 1;

                // Resaltar el cuadro actual en amarillo
                Panel cuadroActual = cuadros[i];
                cuadroActual.BackColor = Color.Yellow;
                cuadroActual.Refresh();
                Application.DoEvents();
                Thread.Sleep(500); // Pausa para que se vea el cambio de color

                // Mover los elementos de A[0..i-1], que son menores que AUX,
                // a una posición adelante de su posición actual
                while (j >= 0 && A[j] < AUX) // Cambiar la condición a menor
                {
                    A[j + 1] = A[j];

                    // Actualizamos el cuadro visualmente
                    cuadros[j + 1].Controls[0].Text = A[j].ToString();
                    cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], A[j]);

                    // Resaltar el cuadro que está siendo desplazado en amarillo
                    cuadros[j + 1].BackColor = Color.Yellow; // Color amarillo para el cuadro que se desplaza

                    // Forzar actualización visual
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500); // Pausa para que se vea el cambio de color

                    j--;
                }

                // Insertar el elemento en la posición correcta
                A[j + 1] = AUX;
                cuadros[j + 1].Controls[0].Text = AUX.ToString();
                cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], AUX);

                // Resaltar el cuadro que ha sido colocado en la posición correcta en amarillo
                cuadros[j + 1].BackColor = Color.Yellow;
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500); // Pausa para que se vea el cambio de color

                // Cambiar el color de los cuadros procesados a verde
                for (int k = 0; k <= j + 1; k++) // Cambiar todos los cuadros hasta la posición actual a verde
                {
                    cuadros[k].BackColor = Color.Green; // Cambiar a verde
                }
            }

            // Al final, aseguramos que todos los cuadros estén en verde
            for (int k = 0; k < N; k++)
            {
                if (cuadros[k].BackColor != Color.Green)
                {
                    cuadros[k].BackColor = Color.Green; // Aseguramos que todos los cuadros procesados estén en verde
                }
            }
        }


        //public void QuicksortAscendente(int[] A, FlowLayoutPanel panel)
        //{
        //    int N = A.Length;

        //    if (panel.Controls.Count != N)
        //    {
        //        throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
        //    }

        //    List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

        //    // Llamamos al método recursivo de Quicksort
        //    QuicksortRecursivo(A, cuadros, panel, 0, N - 1);
        //}

        //private void QuicksortRecursivo(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int inicio, int fin)
        //{
        //    if (inicio < fin)
        //    {
        //        int pivote = A[fin]; // Elegimos el último elemento como pivote
        //        int i = inicio - 1;

        //        Panel cuadroPivote = cuadros[fin];
        //        cuadroPivote.BackColor = Color.Red; // Resaltar el pivote
        //        cuadroPivote.Refresh();
        //        panel.Refresh();
        //        Application.DoEvents();
        //        Thread.Sleep(500);

        //        for (int j = inicio; j < fin; j++)
        //        {
        //            // Comparar elementos con el pivote
        //            if (A[j] <= pivote) // Condición ascendente
        //            {
        //                i++;
        //                // Intercambiar elementos en el arreglo
        //                int temp = A[i];
        //                A[i] = A[j];
        //                A[j] = temp;

        //                // Actualizar los cuadros visualmente
        //                cuadros[i].Controls[0].Text = A[i].ToString();
        //                cuadros[j].Controls[0].Text = A[j].ToString();

        //                cuadrito.AnimarCambioDeTamaño(cuadros[i], A[i]);
        //                cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

        //                cuadros[i].BackColor = cuadrito.GenerarColorUnico(); // Resaltar los cuadros intercambiados
        //                cuadros[j].BackColor = cuadrito.GenerarColorUnico();

        //                // Forzar actualización visual
        //                panel.Refresh();
        //                Application.DoEvents();
        //                Thread.Sleep(500); // Pausa para mostrar el intercambio
        //            }
        //        }

        //        // Colocar el pivote en su posición correcta
        //        int pivoteFinalIndex = i + 1;
        //        int tempPivote = A[pivoteFinalIndex];
        //        A[pivoteFinalIndex] = A[fin];
        //        A[fin] = tempPivote;

        //        // Actualizar los cuadros visualmente
        //        cuadros[pivoteFinalIndex].Controls[0].Text = A[pivoteFinalIndex].ToString();
        //        cuadros[fin].Controls[0].Text = A[fin].ToString();

        //        cuadrito.AnimarCambioDeTamaño(cuadros[pivoteFinalIndex], A[pivoteFinalIndex]);
        //        cuadrito.AnimarCambioDeTamaño(cuadros[fin], A[fin]);

        //        cuadros[pivoteFinalIndex].BackColor = cuadrito.GenerarColorUnico(); // Restaurar el color del pivote
        //        cuadroPivote.BackColor = cuadrito.GenerarColorUnico(); // Restaurar el color del pivote

        //        // Llamadas recursivas
        //        QuicksortRecursivo(A, cuadros, panel, inicio, pivoteFinalIndex - 1);
        //        QuicksortRecursivo(A, cuadros, panel, pivoteFinalIndex + 1, fin);
        //    }
        //}

     public void QuicksortAscendente(int[] A, FlowLayoutPanel panel)
{
    int N = A.Length;

    if (panel.Controls.Count != N)
    {
        throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
    }

    List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

    // Establecer todos los cuadros en azul al inicio
    foreach (var cuadro in cuadros)
    {
        cuadro.BackColor = Color.Blue;
    }

    // Llamamos al método recursivo de Quicksort
    QuicksortRecursivo(A, cuadros, panel, 0, N - 1);

    // Al final, aseguramos que todos los cuadros procesados estén en verde
    foreach (var cuadro in cuadros)
    {
        cuadro.BackColor = Color.Green;
    }
}

private void QuicksortRecursivo(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int inicio, int fin)
{
    if (inicio < fin)
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
                // Intercambiar elementos en el arreglo
                int temp = A[i];
                A[i] = A[j];
                A[j] = temp;

                // Actualizar los cuadros visualmente
                cuadros[i].Controls[0].Text = A[i].ToString();
                cuadros[j].Controls[0].Text = A[j].ToString();

                // Animar el cambio de tamaño
                cuadrito.AnimarCambioDeTamaño(cuadros[i], A[i]);
                cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

                cuadros[i].BackColor = Color.Green; // Resaltar el cuadro intercambiado
                cuadros[j].BackColor = Color.Green;

                // Forzar actualización visual
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500); // Pausa para mostrar el intercambio
            }
        }

        // Colocar el pivote en su posición correcta
        int pivoteFinalIndex = i + 1;
        int tempPivote = A[pivoteFinalIndex];
        A[pivoteFinalIndex] = A[fin];
        A[fin] = tempPivote;

        // Actualizar los cuadros visualmente
        cuadros[pivoteFinalIndex].Controls[0].Text = A[pivoteFinalIndex].ToString();
        cuadros[fin].Controls[0].Text = A[fin].ToString();

        // Animar el cambio de tamaño del pivote
        cuadrito.AnimarCambioDeTamaño(cuadros[pivoteFinalIndex], A[pivoteFinalIndex]);
        cuadrito.AnimarCambioDeTamaño(cuadros[fin], A[fin]);

        cuadros[pivoteFinalIndex].BackColor = Color.Green; // Resaltar el pivote en su posición final
        cuadroPivote.BackColor = Color.Blue; // Restaurar el color del pivote

        // Llamadas recursivas
        QuicksortRecursivo(A, cuadros, panel, inicio, pivoteFinalIndex - 1);
        QuicksortRecursivo(A, cuadros, panel, pivoteFinalIndex + 1, fin);
    }
}
        public void QuicksortDescendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Establecer todos los cuadros en azul al inicio
            foreach (var cuadro in cuadros)
            {
                cuadro.BackColor = Color.Blue;
            }

            // Llamamos al método recursivo de Quicksort
            QuicksortDRecursivo(A, cuadros, panel, 0, N - 1);

            // Al final, aseguramos que todos los cuadros procesados estén en verde
            foreach (var cuadro in cuadros)
            {
                cuadro.BackColor = Color.Green;
            }
        }

        private void QuicksortDRecursivo(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int inicio, int fin)
        {
            if (inicio < fin)
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
                    // Comparar elementos con el pivote para orden descendente
                    if (A[j] >= pivote) // Condición descendente
                    {
                        i++;
                        // Intercambiar elementos en el arreglo
                        int temp = A[i];
                        A[i] = A[j];
                        A[j] = temp;

                        // Actualizar los cuadros visualmente
                        cuadros[i].Controls[0].Text = A[i].ToString();
                        cuadros[j].Controls[0].Text = A[j].ToString();

                        // Animar el cambio de tamaño
                        cuadrito.AnimarCambioDeTamaño(cuadros[i], A[i]);
                        cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

                        cuadros[i].BackColor = Color.Green; // Resaltar el cuadro intercambiado
                        cuadros[j].BackColor = Color.Green;

                        // Forzar actualización visual
                        panel.Refresh();
                        Application.DoEvents();
                        Thread.Sleep(500); // Pausa para mostrar el intercambio
                    }
                }

                // Colocar el pivote en su posición correcta
                int pivoteFinalIndex = i + 1;
                int tempPivote = A[pivoteFinalIndex];
                A[pivoteFinalIndex] = A[fin];
                A[fin] = tempPivote;

                // Actualizar los cuadros visualmente
                cuadros[pivoteFinalIndex].Controls[0].Text = A[pivoteFinalIndex].ToString();
                cuadros[fin].Controls[0].Text = A[fin].ToString();

                // Animar el cambio de tamaño del pivote
                cuadrito.AnimarCambioDeTamaño(cuadros[pivoteFinalIndex], A[pivoteFinalIndex]);
                cuadrito.AnimarCambioDeTamaño(cuadros[fin], A[fin]);

                cuadros[pivoteFinalIndex].BackColor = Color.Green; // Resaltar el pivote en su posición final
                cuadroPivote.BackColor = Color.Blue; // Restaurar el color del pivote

                // Llamadas recursivas
                QuicksortDRecursivo(A, cuadros, panel, inicio, pivoteFinalIndex - 1);
                QuicksortDRecursivo(A, cuadros, panel, pivoteFinalIndex + 1, fin);
            }
        }
    }
}
