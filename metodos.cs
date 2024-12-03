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
                cuadroActual.BackColor = Color.Red;
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

                    cuadroMayor.BackColor = Color.DarkSeaGreen;
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
                cuadroActual.BackColor = Color.Red;  // Resaltar el cuadro actual
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

        public void HeapSortAcendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            for (int I = 1; I < N; I++) // Adaptado a índices base 0
            {
                int K = I + 1; // Ajuste para representar la posición correcta
                bool BAND = true;

                while (K > 1 && BAND)
                {
                    BAND = false;
                    int padre = (K - 1) / 2; // Índice del nodo padre

                    if (padre < 0 || padre >= A.Length || K - 1 >= A.Length)
                        break;

                    if (A[K - 1] > A[padre])
                    {
                        // Cambiar color de los cuadros a rojo antes de la comparación
                        cuadros[K - 1].BackColor = Color.Red;
                        cuadros[padre].BackColor = Color.Red;
                        Thread.Sleep(200);

                        // Generar un color aleatorio para después
                        Random rnd = new Random();
                        Color colorAleatorio = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                        // Intercambiar valores y actualizar texto
                        int AUX = A[padre];
                        A[padre] = A[K - 1];
                        A[K - 1] = AUX;

                        cuadros[K - 1].Controls[0].Text = A[K - 1].ToString();
                        cuadros[padre].Controls[0].Text = A[padre].ToString();

                        // Animar el cambio de tamaño de los cuadros
                        cuadrito.AnimarCambioDeTamaño(cuadros[K - 1], A[K - 1]);
                        cuadrito.AnimarCambioDeTamaño(cuadros[padre], A[padre]);

                        // Restaurar colores a aleatorio después de la comparación
                        cuadros[K - 1].BackColor = cuadrito.GenerarColorUnico();
                        cuadros[padre].BackColor = cuadrito.GenerarColorUnico();

                        K = padre + 1;
                        BAND = true;

                        Thread.Sleep(500);
                    }
                }
            }
            EliminarMonticuloAnimado(A, panel);
        }

        public void EliminarMonticuloAnimado3(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            for (int I = N - 1; I >= 1; I--)
            {
                int AUX = A[I];
                A[I] = A[0];

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

                    cuadros[K].BackColor = Color.Red;
                    cuadros[AP].BackColor = Color.Red;
                    Thread.Sleep(200);

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

                    cuadros[K].BackColor = cuadrito.GenerarColorUnico();
                    cuadros[AP].BackColor = cuadrito.GenerarColorUnico();

                    IZQ = 2 * K + 1;
                    DER = IZQ + 1;
                }

                A[K] = AUX;
                cuadros[K].Controls[0].Text = AUX.ToString();

                cuadrito.AnimarCambioDeTamaño(cuadros[K], A[K]);
                cuadrito.AnimarCambioDeTamaño(cuadros[I], A[I]);
            }

            cuadrito.AnimarCambioDeTamaño(cuadros[0], A[0]);
        }

        public void EliminarMonticuloAnimado(int[] A, FlowLayoutPanel panel)
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

                Thread.Sleep(200); // Pausa para visualizar el cambio

                // Actualizar visualmente los cuadros
                cuadros[I].Controls[0].Text = A[I].ToString();
                cuadros[0].Controls[0].Text = AUX.ToString();

                // Cambiar a un color aleatorio después de la actualización
                cuadros[I].BackColor = cuadrito.GenerarColorUnico();
                cuadros[0].BackColor = cuadrito.GenerarColorUnico();
                Thread.Sleep(500);
                cuadros[I].Refresh();
                cuadros[0].Refresh();

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

                    // Resaltar en rojo los cuadros que van a cambiar
                    cuadros[K].BackColor = Color.Red;
                    cuadros[AP].BackColor = Color.Red;

                    // Forzar actualización visual
                    cuadros[K].Refresh();
                    cuadros[AP].Refresh();

                    Thread.Sleep(500); // Pausa para visualizar el cambio

                    if (AUX < MAYOR)
                    {
                        A[K] = A[AP];

                        // Actualizar visualmente los cuadros
                        cuadros[K].Controls[0].Text = A[K].ToString();
                        cuadros[AP].Controls[0].Text = MAYOR.ToString();

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

        public void HeapSortDescendente(int[] A, FlowLayoutPanel panel)
        {
            int N = A.Length;
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Construcción del heap (mín-heap)
            for (int I = 1; I < N; I++)
            {
                int K = I + 1; // Ajuste a índices 1-based
                bool BAND = true;

                while (K > 1 && BAND)
                {
                    BAND = false;
                    int padre = (K - 1) / 2;

                    if (padre < 0 || padre >= A.Length || K - 1 >= A.Length)
                        break;

                    if (A[K - 1] < A[padre]) // Cambia el operador para hacer un mín-heap
                    {
                        cuadros[K - 1].BackColor = Color.Red;
                        cuadros[padre].BackColor = Color.Red;
                        Thread.Sleep(200);

                        int AUX = A[padre];
                        A[padre] = A[K - 1];
                        A[K - 1] = AUX;

                        cuadros[K - 1].Controls[0].Text = A[K - 1].ToString();
                        cuadros[padre].Controls[0].Text = A[padre].ToString();

                        cuadrito.AnimarCambioDeTamaño(cuadros[K - 1], A[K - 1]);
                        cuadrito.AnimarCambioDeTamaño(cuadros[padre], A[padre]);

                        cuadros[K - 1].BackColor = cuadrito.GenerarColorUnico();
                        cuadros[padre].BackColor = cuadrito.GenerarColorUnico();

                        K = padre + 1;
                        BAND = true;

                        Thread.Sleep(500);
                    }
                }
            }

            // Ordenar extrayendo el menor elemento
            EliminarMonticuloDescendente(A, panel);
        }
        public void EliminarMonticuloDescendente(int[] A, FlowLayoutPanel panel)
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
                    int MENOR = A[IZQ];
                    int AP = IZQ;

                    if ((DER < I) && (MENOR > A[DER]))
                    {
                        MENOR = A[DER];
                        AP = DER;
                    }

                    // Resaltar en rojo los cuadros que van a cambiar
                    cuadros[K].BackColor = Color.Red;
                    cuadros[AP].BackColor = Color.Red;

                    // Forzar actualización visual
                    cuadros[K].Refresh();
                    cuadros[AP].Refresh();

                    Thread.Sleep(500); // Pausa para visualizar el cambio

                    if (AUX > MENOR)
                    {
                        A[K] = A[AP];

                        // Actualizar visualmente los cuadros
                        cuadros[K].Controls[0].Text = A[K].ToString();
                        cuadros[AP].Controls[0].Text = MENOR.ToString();

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

    }
}
