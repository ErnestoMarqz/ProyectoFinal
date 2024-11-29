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
        private void Baraja(int[] A)
        {
            int N = A.Length;
            for (int I = 1; I < N; I++) // En C#, los índices comienzan en 0
            {
                int AUX = A[I];
                int K = I - 1;

                // Desplazar elementos mayores que AUX
                while (K >= 0 && AUX < A[K])
                {
                    A[K + 1] = A[K];
                    K--;
                }

                // Insertar el elemento en la posición correcta
                A[K + 1] = AUX;
            }
        }
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
                Application.DoEvents(); // Permitir que la interfaz gráfica se actualice
                Thread.Sleep(500);

                while (K >= 0 && AUX < A[K])
                {
                    Panel cuadroMayor = cuadros[K];
                    cuadroMayor.BackColor = Color.Red;

                    // Actualizar el valor en el arreglo
                    A[K + 1] = A[K];

                    // Visualmente desplazar el cuadro hacia la derecha
                    cuadros[K + 1].Controls[0].Text = cuadroMayor.Controls[0].Text;
                    cuadrito.AnimarCambioDeTamaño(cuadros[K + 1], A[K + 1]);
                    cuadros[K + 1].BackColor = cuadrito.GenerarColorUnico(); 
                    panel.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(500);

                    cuadroMayor.BackColor = cuadrito.GenerarColorUnico();
                    ; // Restaurar color original
                    K--;
                }

                // Insertar el elemento en la posición correcta
                A[K + 1] = AUX;
                cuadros[K + 1].Controls[0].Text = AUX.ToString();
                cuadrito.AnimarCambioDeTamaño(cuadros[K + 1], AUX);
                cuadrito.GenerarColorUnico();
                //cuadroActual.BackColor = Color.LightBlue; // Restaurar color original
                panel.Refresh();
                Application.DoEvents();
                Thread.Sleep(500);
            }
        }


        public void InsertaMonticuloAnimado(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;
            // Validar que el número de cuadros en el panel coincide con el tamaño del arreglo
            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            // Obtener los cuadros como una lista
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Recorremos los elementos desde el segundo hasta el último
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
    }
}
