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
                    cuadros[K + 1].BackColor = cuadrito.GenerarColorUnico(); ;
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

    }
}
