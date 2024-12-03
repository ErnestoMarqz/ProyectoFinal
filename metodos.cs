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
        public void IntercalacionDirectaAscendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Llamamos a la función recursiva de Merge Sort
            MergeSortAnimado(A, cuadros, panel, 0, N - 1);
        }

        // Función recursiva para dividir y ordenar los subarreglos
        private void MergeSortAnimado(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int inicio, int fin)
        {
            if (inicio >= fin)
                return;

            int medio = (inicio + fin) / 2;

            // Dividir el arreglo en dos partes
            MergeSortAnimado(A, cuadros, panel, inicio, medio);
            MergeSortAnimado(A, cuadros, panel, medio + 1, fin);

            // Mezclar las dos mitades ordenadas
            MezclarConAnimacion(A, cuadros, panel, inicio, medio, fin);
        }

        // Función para mezclar dos mitades ordenadas
        private void MezclarConAnimacion(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int inicio, int medio, int fin)
        {
            int[] temp = new int[fin - inicio + 1];
            int i = inicio, j = medio + 1, k = 0;

            // Mezclamos las dos mitades
            while (i <= medio && j <= fin)
            {
                if (A[i] <= A[j]) // Condición ascendente
                {
                    temp[k] = A[i];
                    ActualizarCuadro(cuadros[i], temp[k]);
                    i++;
                }
                else
                {
                    temp[k] = A[j];
                    ActualizarCuadro(cuadros[j], temp[k]);
                    j++;
                }
                k++;
            }

            // Copiamos los elementos restantes de la primera mitad
            while (i <= medio)
            {
                temp[k] = A[i];
                ActualizarCuadro(cuadros[i], temp[k]);
                i++;
                k++;
            }

            // Copiamos los elementos restantes de la segunda mitad
            while (j <= fin)
            {
                temp[k] = A[j];
                ActualizarCuadro(cuadros[j], temp[k]);
                j++;
                k++;
            }

            // Copiamos los elementos ordenados de temp de regreso a A y actualizamos la interfaz
            for (k = 0; k < temp.Length; k++)
            {
                A[inicio + k] = temp[k];
                cuadros[inicio + k].Controls[0].Text = A[inicio + k].ToString();
                cuadros[inicio + k].BackColor = cuadrito.GenerarColorUnico();
                cuadros[inicio + k].Refresh();

                // Simulación de animación
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500);
            }
        }

        // Método auxiliar para actualizar el contenido y el tamaño de un cuadro
        private void ActualizarCuadro(Panel cuadro, int valor)
        {
            cuadro.Controls[0].Text = valor.ToString();
            cuadrito.AnimarCambioDeTamaño(cuadro, valor);
            cuadro.BackColor = cuadrito.GenerarColorUnico();
            cuadro.Refresh();
        }

        public void IntercalacionDirectaDescendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Llamamos a la función recursiva de Merge Sort
            MergeSortAnimado2(A, cuadros, panel, 0, N - 1);
        }

        // Función recursiva para dividir y ordenar los subarreglos
        private void MergeSortAnimado2(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int inicio, int fin)
        {
            if (inicio >= fin)
                return;

            int medio = (inicio + fin) / 2;

            // Dividir el arreglo en dos partes
            MergeSortAnimado2(A, cuadros, panel, inicio, medio);
            MergeSortAnimado2(A, cuadros, panel, medio + 1, fin);

            // Mezclar las dos mitades ordenadas
            MezclarConAnimacion2(A, cuadros, panel, inicio, medio, fin);
        }

        // Función para mezclar dos mitades ordenadas en orden descendente
        private void MezclarConAnimacion2(int[] A, List<Panel> cuadros, FlowLayoutPanel panel, int inicio, int medio, int fin)
        {
            int[] temp = new int[fin - inicio + 1];
            int i = inicio, j = medio + 1, k = 0;

            // Mezclamos las dos mitades
            while (i <= medio && j <= fin)
            {
                if (A[i] >= A[j]) // Condición descendente (mayor a menor)
                {
                    temp[k] = A[i];
                    ActualizarCuadro2(cuadros[i], temp[k]);
                    i++;
                }
                else
                {
                    temp[k] = A[j];
                    ActualizarCuadro2(cuadros[j], temp[k]);
                    j++;
                }
                k++;
            }

            // Copiamos los elementos restantes de la primera mitad
            while (i <= medio)
            {
                temp[k] = A[i];
                ActualizarCuadro2(cuadros[i], temp[k]);
                i++;
                k++;
            }

            // Copiamos los elementos restantes de la segunda mitad
            while (j <= fin)
            {
                temp[k] = A[j];
                ActualizarCuadro2(cuadros[j], temp[k]);
                j++;
                k++;
            }

            // Copiamos los elementos ordenados de temp de regreso a A y actualizamos la interfaz
            for (k = 0; k < temp.Length; k++)
            {
                A[inicio + k] = temp[k];
                cuadros[inicio + k].Controls[0].Text = A[inicio + k].ToString();
                cuadros[inicio + k].BackColor = cuadrito.GenerarColorUnico();
                cuadros[inicio + k].Refresh();

                // Simulación de animación
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500);
            }
        }

        // Método auxiliar para actualizar el contenido y el tamaño de un cuadro
        private void ActualizarCuadro2(Panel cuadro, int valor)
        {
            cuadro.Controls[0].Text = valor.ToString();
            cuadrito.AnimarCambioDeTamaño(cuadro, valor);
            cuadro.BackColor = cuadrito.GenerarColorUnico();
            cuadro.Refresh();
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

        public void BucketSortAscendente(int[] A, FlowLayoutPanel panel, RichTextBox richTextBoxBuckets)
        {
            int N = A.Length;
            richTextBoxBuckets.Visible = true;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Encontrar el valor máximo
            int maxValue = A.Max();
            int bucketCount = maxValue / 5 + 1; // Ajustar según el rango de los datos
            List<List<int>> buckets = new List<List<int>>(new List<int>[bucketCount]);

            // Inicializar los cubos
            for (int i = 0; i < bucketCount; i++)
            {
                buckets[i] = new List<int>();
            }

            // Distribuir los elementos en los cubos
            for (int i = 0; i < N; i++)
            {
                int bucketIndex = A[i] / 5; // Ajustar según el rango de los datos
                buckets[bucketIndex].Add(A[i]);

                // Actualizar visualización
                cuadros[i].BackColor = Color.Orange; // Resaltar el cuadro que se está procesando
                cuadros[i].Controls[0].Text = A[i].ToString();
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500); // Pausa para que se vea el cambio de color

                // Actualizar texto de cubetas en el RichTextBox
                UpdateBucketsDisplay(buckets, richTextBoxBuckets);
            }

            // Ordenar cada cubo y combinar
            int index = 0;
            foreach (var bucket in buckets)
            {
                // Ordenar el cubo (puedes usar cualquier método de ordenamiento, aquí se usa Array.Sort)
                bucket.Sort();

                // Actualizar visualización de cada cubo
                foreach (var num in bucket)
                {
                    A[index] = num;

                    // Actualizar el panel correspondiente
                    cuadros[index].Controls[0].Text = num.ToString();
                    cuadrito.AnimarCambioDeTamaño(cuadros[index], num); // Método para animar el cambio de tamaño
                    cuadros[index].BackColor = Color.Red; // Resaltar el cuadro que ha sido colocado
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500); // Pausa para que se vea el cambio de color

                    index++;
                }
            }

            // Finalizar la visualización
            for (int i = 0; i < N; i++)
            {
                cuadros[i].BackColor = Color.Green; // Cambiar color final
            }
            panel.Refresh();
        }

        //Método para actualizar el RichTextBox con el contenido de las cubetas
        private void UpdateBucketsDisplay(List<List<int>> buckets, RichTextBox richTextBox)
        {
            richTextBox.Clear(); // Limpiar el RichTextBox antes de actualizar
            for (int i = 0; i < buckets.Count; i++)
            {
                richTextBox.AppendText($"Bucket {i + 1}: {string.Join(", ", buckets[i])}\n");
            }
            richTextBox.Refresh(); // Forzar actualización visual
        }
        public void BucketSortDescendente(int[] A, FlowLayoutPanel panel, RichTextBox richTextBoxBuckets)
        {
            int N = A.Length;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Encontrar el valor máximo
            int maxValue = A.Max();
            int bucketCount = maxValue / 10 + 1; // Ajustar según el rango de los datos
            List<List<int>> buckets = new List<List<int>>(new List<int>[bucketCount]);

            // Inicializar los cubos
            for (int i = 0; i < bucketCount; i++)
            {
                buckets[i] = new List<int>();
            }

            // Distribuir los elementos en los cubos
            for (int i = 0; i < N; i++)
            {
                int bucketIndex = A[i] / 10; // Ajustar según el rango de los datos
                buckets[bucketIndex].Add(A[i]);

                // Actualizar visualización
                cuadros[i].BackColor = Color.Orange; // Resaltar el cuadro que se está procesando
                cuadros[i].Controls[0].Text = A[i].ToString();
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500); // Pausa para que se vea el cambio de color

                // Actualizar texto de cubetas en el RichTextBox
                UpdateBucketsDisplay(buckets, richTextBoxBuckets);
            }

            // Ordenar cada cubo y combinar en orden descendente
            int index = 0;
            foreach (var bucket in buckets)
            {
                // Ordenar el cubo en orden descendente
                bucket.Sort((x, y) => y.CompareTo(x)); // Ordenar de mayor a menor

                // Actualizar visualización de cada cubo
                foreach (var num in bucket)
                {
                    A[index] = num;

                    // Actualizar el panel correspondiente
                    cuadros[index].Controls[0].Text = num.ToString();
                    cuadrito.AnimarCambioDeTamaño(cuadros[index], num); // Método para animar el cambio de tamaño
                    cuadros[index].BackColor = Color.Red; // Resaltar el cuadro que ha sido colocado
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500); // Pausa para que se vea el cambio de color

                    index++;
                }
            }

            // Finalizar la visualización
            for (int i = 0; i < N; i++)
            {
                cuadros[i].BackColor = Color.Green; // Cambiar color final
            }
            panel.Refresh();
        }

        public int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public void BucketSortAcendente(int[] arr, FlowLayoutPanel panel, int bucketCount,int tamañoCubeta)
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
            //bucketCount = maxValue / 10 + 1; // Ajustar tamaño del cubo

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
                int bucketIndex = num / tamañoCubeta; // Ajustar tamaño del cubo
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

        public void BucketSortDescendente2(int[] arr, FlowLayoutPanel panel, int bucketCount, int tamañoCubeta)
        {
            // Crear lista de cuadros
            if (panel.Controls.Count != arr.Length)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Encontrar el valor máximo para determinar el número de cubos
            int maxValue = arr.Max();

            int bucketSize = (maxValue / bucketCount) + 1;

            // Distribuir los números en las cubetas
            List<List<int>> buckets = new List<List<int>>(bucketCount);
            List<FlowLayoutPanel> cubosVisuales = new List<FlowLayoutPanel>();

            FlowLayoutPanel cubosPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoSize = true,
                BackColor = Color.LightGray,
                Margin = new Padding(10),
                Dock = DockStyle.Top
            };

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
                int bucketIndex = num / tamañoCubeta;
                buckets[bucketIndex].Add(num);

                // Mover visualmente el cuadro al cubo correspondiente
                Panel cuadro = cuadros.First(c => c.Controls[0].Text == num.ToString());
                cuadros.Remove(cuadro);
                AnimarMovimientoDes2(cuadro, cubosVisuales[bucketIndex]);
            }

            // Ordenar cada cubo y animar
            int index = 0;
            foreach (List<int> bucket in buckets)
            {
                bucket.Sort((a, b) => b.CompareTo(a)); // Ordenar cubo de forma descendente
                FlowLayoutPanel cuboVisual = cubosVisuales[buckets.IndexOf(bucket)];

                foreach (int num in bucket)
                {
                    Panel cuadro = cuboVisual.Controls.Cast<Panel>().First(c => c.Controls[0].Text == num.ToString());
                    cuadros.Add(cuadro);

                    // Combinar visualmente los elementos en el panel original
                    AnimarMovimientoDes2(cuadro, panel);
                    arr[index++] = num;
                }
            }
            panel.Parent.Controls.Remove(cubosPanel);
        }

        private void AnimarMovimientoDes2(Panel cuadro, FlowLayoutPanel destino)
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

        public async void RadixSort(int[] A, FlowLayoutPanel panel, RichTextBox richTextBoxDigits)
        {
            int N = A.Length;
            richTextBoxDigits.Visible = true;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Encontrar el valor máximo
            int max = A.Max();

            // Llamar a CountingSort para cada dígito
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                await CountingSortWithAnimation(A, exp, cuadros, richTextBoxDigits, panel);
            }

            // Finalizar la visualización
            for (int i = 0; i < N; i++)
            {
                cuadros[i].BackColor = Color.Green; // Cambiar color final
                AnimarCambioDeTamaño(cuadros[i], A[i]); // Ajustar al tamaño final
            }
            panel.Refresh();
        }

        private async Task CountingSortWithAnimation(int[] arr, int exp, List<Panel> cuadros, RichTextBox richTextBoxDigits, FlowLayoutPanel panel)
        {
            int n = arr.Length;
            int[] output = new int[n];
            int[] count = new int[10];

            // Inicializar el arreglo de conteo
            for (int i = 0; i < count.Length; i++)
                count[i] = 0;

            // Contar ocurrencias de cada dígito
            for (int i = 0; i < n; i++)
            {
                int digit = GetDigit(arr[i], exp);
                count[digit]++;
            }

            // Cambiar count[i] para que contenga la posición real de este dígito en output[]
            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            // Construir el arreglo de salida
            for (int i = n - 1; i >= 0; i--)
            {
                int digit = GetDigit(arr[i], exp);
                output[count[digit] - 1] = arr[i];
                count[digit]--;

                // Actualizar visualización
                int index = count[digit];
                cuadros[index].BackColor = Color.Orange; // Resaltar el cuadro que se está procesando
                cuadros[index].Controls[0].Text = arr[i].ToString();
                panel.Refresh();
                await Task.Delay(500); // Pausa para que se vea el cambio de color

                // Animar el cambio de tamaño
                AnimarCambioDeTamaño(cuadros[index], arr[i]);

                // Actualizar el RichTextBox con el estado actual
                UpdateDigitsDisplay(arr, exp, richTextBoxDigits);
            }

            // Copiar el arreglo de salida a arr[]
            for (int i = 0; i < n; i++)
            {
                arr[i] = output[i];

                // Actualizar el panel correspondiente
                cuadros[i].Controls[0].Text = arr[i].ToString();
                cuadros[i].BackColor = Color.Red; // Resaltar el cuadro que ha sido colocado
                panel.Refresh();
                await Task.Delay(500); // Pausa para que se vea el cambio de color

                // Animar el cambio de tamaño al valor final
                AnimarCambioDeTamaño(cuadros[i], arr[i]);
            }
        }

        // Método para obtener el dígito en la posición 'exp'
        private int GetDigit(int number, int exp)
        {
            return (number / exp) % 10;
        }

        // Método para actualizar el RichTextBox con el estado actual de los dígitos
        private void UpdateDigitsDisplay(int[] arr, int exp, RichTextBox richTextBox)
        {
            richTextBox.Clear(); // Limpiar el RichTextBox antes de actualizar
            for (int i = 0; i < arr.Length; i++)
            {
                int digit = GetDigit(arr[i], exp);
                richTextBox.AppendText($"Número: {arr[i]}, Dígito en posición {exp}: {digit}\n");
            }
            richTextBox.Refresh(); // Forzar actualización visual
        }

        // Método para animar el cambio de tamaño
        public void AnimarCambioDeTamaño(Panel cuadro, int numero)
        {
            Size tamañoFinal = new Size(numero * 10, numero * 10);
            int incremento = 4;

            while (cuadro.Width != tamañoFinal.Width || cuadro.Height != tamañoFinal.Height)
            {
                // Calcular el próximo tamaño
                int nuevoAncho = cuadro.Width < tamañoFinal.Width
                    ? Math.Min(cuadro.Width + incremento, tamañoFinal.Width)
                    : Math.Max(cuadro.Width - incremento, tamañoFinal.Width);

                int nuevoAlto = cuadro.Height < tamañoFinal.Height
                    ? Math.Min(cuadro.Height + incremento, tamañoFinal.Height)
                    : Math.Max(cuadro.Height - incremento, tamañoFinal.Height);

                // Actualizar el tamaño del cuadro
                cuadro.Size = new Size(nuevoAncho, nuevoAlto);
                cuadro.Refresh();

                // Forzar actualización visual
                Application.DoEvents();
                Thread.Sleep(70); // Pausa para que la animación sea visible
            }
        }
        //public async void RadixSort(int[] A, FlowLayoutPanel panel, RichTextBox richTextBoxDigits)
        //{
        //    int N = A.Length;
        //    richTextBoxDigits.Visible = true;

        //    if (panel.Controls.Count != N)
        //    {
        //        throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
        //    }

        //    List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

        //    // Encontrar el valor máximo
        //    int max = A.Max();

        //    // Llamar a CountingSort para cada dígito
        //    for (int exp = 1; max / exp > 0; exp *= 10)
        //    {
        //        await CountingSortWithAnimation(A, exp, cuadros, richTextBoxDigits,panel);
        //    }

        //    // Finalizar la visualización
        //    for (int i = 0; i < N; i++)
        //    {
        //        cuadros[i].BackColor = Color.Green; // Cambiar color final
        //    }
        //    panel.Refresh();
        //}

        //private async Task CountingSortWithAnimation(int[] arr, int exp, List<Panel> cuadros, RichTextBox richTextBoxDigits, FlowLayoutPanel panel)
        //{
        //    int n = arr.Length;
        //    int[] output = new int[n];
        //    int[] count = new int[10];

        //    // Inicializar el arreglo de conteo
        //    for (int i = 0; i < count.Length; i++)
        //        count[i] = 0;

        //    // Contar ocurrencias de cada dígito
        //    for (int i = 0; i < n; i++)
        //    {
        //        int digit = GetDigit(arr[i], exp);
        //        count[digit]++;
        //    }

        //    // Cambiar count[i] para que contenga la posición real de este dígito en output[]
        //    for (int i = 1; i < 10; i++)
        //        count[i] += count[i - 1];

        //    // Construir el arreglo de salida
        //    for (int i = n - 1; i >= 0; i--)
        //    {
        //        int digit = GetDigit(arr[i], exp);
        //        output[count[digit] - 1] = arr[i];
        //        count[digit]--;

        //        // Actualizar visualización
        //        int index = count[digit];
        //        cuadros[index].BackColor = Color.Orange; // Resaltar el cuadro que se está procesando
        //        cuadros[index].Controls[0].Text = arr[i].ToString();
        //        panel.Refresh();
        //        await Task.Delay(500); // Pausa para que se vea el cambio de color

        //        // Actualizar el RichTextBox con el estado actual
        //        UpdateDigitsDisplay(arr, exp, richTextBoxDigits);
        //    }

        //    // Copiar el arreglo de salida a arr[]
        //    for (int i = 0; i < n; i++)
        //    {
        //        arr[i] = output[i];

        //        // Actualizar el panel correspondiente
        //        cuadros[i].Controls[0].Text = arr[i].ToString();
        //        cuadros[i].BackColor = Color.Red; // Resaltar el cuadro que ha sido colocado
        //        panel.Refresh();
        //        await Task.Delay(500); // Pausa para que se vea el cambio de color
        //    }
        //}

        //// Método para obtener el dígito en la posición 'exp'
        //private int GetDigit(int number, int exp)
        //{
        //    return (number / exp) % 10;
        //}

        //// Método para actualizar el RichTextBox con el estado actual de los dígitos
        //private void UpdateDigitsDisplay(int[] arr, int exp, RichTextBox richTextBox)
        //{
        //    richTextBox.Clear(); // Limpiar el RichTextBox antes de actualizar
        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //        int digit = GetDigit(arr[i], exp);
        //        richTextBox.AppendText($"Número: {arr[i]}, Dígito en posición {exp}: {digit}\n");
        //    }
        //    richTextBox.Refresh(); // Forzar actualización visual
        //}
    }
}
