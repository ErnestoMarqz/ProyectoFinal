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

    }
}




    

