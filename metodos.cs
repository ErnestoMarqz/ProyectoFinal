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

        public async Task OrdenarInsercionBinariaConAnimacion(FlowLayoutPanel parent, int[] arreglo, bool ascendente)
        {
            int n = parent.Controls.Count;

            for (int i = 1; i < n; i++)
            {
                // Obtener el cuadro actual y su valor
                Panel cuadroActual = parent.Controls[i] as Panel;
                int valorActual = int.Parse((cuadroActual.Controls[0] as Label).Text);

                // Buscar la posición correcta en el arreglo usando búsqueda binaria
                int posicion = await BuscarPosicionBinariaAnimada(parent, arreglo, valorActual, 0, i - 1, ascendente);

                // Mover el cuadro visualmente y actualizar el arreglo lógico
                if (posicion < i)
                {
                    // Actualizar el arreglo lógico moviendo los elementos
                    int temp = arreglo[i];
                    for (int j = i; j > posicion; j--)
                    {
                        arreglo[j] = arreglo[j - 1];
                    }
                    arreglo[posicion] = temp;

                    // Mover los cuadros visualmente
                    await MoverCuadroConIntercambio(parent, i, posicion);
                }
            }
        }

        private async Task<int> BuscarPosicionBinariaAnimada(FlowLayoutPanel parent, int[] arreglo, int valor, int inicio, int fin, bool ascendente)
        {
            while (inicio <= fin)
            {
                int medio = (inicio + fin) / 2;

                // Comparar con el cuadro en la posición `medio`
                Panel cuadroMedio = parent.Controls[medio] as Panel;
                int valorMedio = arreglo[medio];

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


        public static async Task OrdenarBurbujaConAnimacion(FlowLayoutPanel parent, bool ascendente)
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


        private async Task ResaltarCodigoAsync(RichTextBox richTextBox, int linea)
        {
            // Configura el color para resaltar la línea seleccionada
            richTextBox.SelectionStart = richTextBox.GetFirstCharIndexFromLine(linea);
            richTextBox.SelectionLength = richTextBox.Lines[linea].Length;
            richTextBox.SelectionBackColor = Color.LightBlue;

            // Deja un tiempo para que la animación sea visible
            await Task.Delay(1000); // Tiempo de resalte para cada línea (ajustar si es necesario)

            // Restaura el color original
            richTextBox.SelectionBackColor = Color.White;
        }

        private Cuadritos creador = new Cuadritos();

        public async Task MergeSortConAnimacion(FlowLayoutPanel parent, bool ascendente, RichTextBox richTextBox)
        {
            // Código fuente del algoritmo (líneas divididas para el RichTextBox)
            string codigoFuente = @"
private async Task MergeSortAnimado(...)
{
    if (inicio >= fin) return; // Caso base

    int medio = (inicio + fin) / 2;

    ResaltarRango(parent, inicio, fin, Color.Yellow); // Resaltar partición
    await MergeSortAnimado(...); // Llamada recursiva izquierda
    await MergeSortAnimado(...); // Llamada recursiva derecha

    await MergeAnimado(...); // Combinar las mitades
}

private async Task MergeAnimado(...)
{
    ResaltarRango(...); // Resaltar particiones

    while (i <= medio && j <= fin) { ... } // Combinar elementos

    for (int k = 0; k < temp.Count; k++) { ... } // Actualizar visualmente
}";

            // Configurar el RichTextBox
            richTextBox.Clear();
            richTextBox.Text = codigoFuente.Trim();
            richTextBox.SelectAll();
            richTextBox.SelectionColor = Color.Black;
            richTextBox.DeselectAll();

            // Convertir los bloques a una lista de valores enteros
            List<int> valores = new List<int>();
            foreach (Panel panel in parent.Controls)
            {
                int valor = int.Parse((panel.Controls[0] as Label).Text);
                valores.Add(valor);
            }

            // Llamar a la función recursiva con animación
            await MergeSortAnimado(parent, valores, 0, valores.Count - 1, ascendente, richTextBox);
        }


        public async Task MergeSortAnimado(FlowLayoutPanel parent, List<int> valores, int inicio, int fin, bool ascendente, RichTextBox richTextBox)
        {
            if (inicio >= fin) return;

            int medio = (inicio + fin) / 2;

            // Resaltar la partición actual en el código
            await ResaltarCodigoAsync(richTextBox, 5); // Línea de "ResaltarRango"

            // Resaltar visualmente el rango en el panel
            ResaltarRango(parent, inicio, fin, Color.Yellow);
            await Task.Delay(1000); // Asegura que el resaltado se vea

            // Llamadas recursivas
            await ResaltarCodigoAsync(richTextBox, 6); // Línea de "await MergeSortAnimado (izquierda)"
            await MergeSortAnimado(parent, valores, inicio, medio, ascendente, richTextBox);

            await ResaltarCodigoAsync(richTextBox, 7); // Línea de "await MergeSortAnimado (derecha)"
            await MergeSortAnimado(parent, valores, medio + 1, fin, ascendente, richTextBox);

            // Combinar las dos mitades
            await ResaltarCodigoAsync(richTextBox, 10); // Línea de "await MergeAnimado"
            await MergeAnimado(parent, valores, inicio, medio, fin, ascendente, richTextBox);
        }

        private async Task MergeAnimado(FlowLayoutPanel parent, List<int> valores, int inicio, int medio, int fin, bool ascendente, RichTextBox richTextBox)
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

                // Animar el cambio de tamaño y actualización del valor
                int pasos = 20;
                for (int paso = 0; paso <= pasos; paso++)
                {
                    cuadro.Invoke((MethodInvoker)(() =>
                    {
                        // Interpolamos el tamaño de forma suave
                        cuadro.Size = new Size(
                            Interpolar(tamañoInicial.Width, tamañoFinal.Width, paso, pasos),
                            Interpolar(tamañoInicial.Height, tamañoFinal.Height, paso, pasos)
                        );

                        // Actualizar el valor en el Label del Panel
                        (cuadro.Controls[0] as Label).Text = nuevoValor.ToString();
                        cuadro.BackColor = Color.Green;
                        cuadro.Refresh();
                    }));

                    // Mostrar animación en el RichTextBox
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

        public async Task OrdenarBarajaConAlgoritmoAnimado(FlowLayoutPanel parent, RichTextBox richTextBox, bool ascendente)
        {
            // Código del algoritmo de la Baraja como referencia
            string codigoFuente = @"
    for (int i = 1; i < valores.Length; i++)
    {
        int aux = valores[i];
        int j = i - 1;
        while (j >= 0 && (ascendente ? aux < valores[j] : aux > valores[j]))
        {
            valores[j + 1] = valores[j];
            j--;
        }
        valores[j + 1] = aux;
    }";

            // Inicializar el RichTextBox con el código del algoritmo
            richTextBox.Clear();
            richTextBox.Text = codigoFuente.Trim();
            richTextBox.SelectAll();
            richTextBox.SelectionColor = Color.Black;
            richTextBox.DeselectAll();

            // Obtener los valores de los cuadros en el FlowLayoutPanel
            List<int> valores = new List<int>();
            foreach (Panel panel in parent.Controls)
            {
                int valor = int.Parse((panel.Controls[0] as Label).Text);
                valores.Add(valor);
            }

            // Algoritmo de Baraja con animaciones
            for (int i = 1; i < valores.Count; i++)
            {
                // Resaltar el inicio del bucle externo
                await ResaltarCodigoAsync(richTextBox, 0);

                int aux = valores[i];
                int j = i - 1;

                // Resaltar la declaración del valor auxiliar
                await ResaltarCodigoAsync(richTextBox, 2);

                // Resaltar el inicio del bucle interno
                await ResaltarCodigoAsync(richTextBox, 4);

                // Animar la comparación y el desplazamiento de elementos
                while (j >= 0 && (ascendente ? aux < valores[j] : aux > valores[j]))
                {
                    // Resaltar la condición del bucle interno
                    await ResaltarCodigoAsync(richTextBox, 5);

                    // Actualizar los valores y animar el desplazamiento
                    valores[j + 1] = valores[j];

                    // Animar el movimiento de cuadros
                    Cuadritos.IntercambiarCuadrosAnimado(parent, j, j + 1);

                    await Task.Delay(500); // Pausa para observar la animación

                    j--;

                    // Resaltar la actualización del índice j
                    await ResaltarCodigoAsync(richTextBox, 7);
                }

                // Insertar el elemento en su posición correcta
                valores[j + 1] = aux;

                // Resaltar la asignación final
                await ResaltarCodigoAsync(richTextBox, 9);

                // Animar el cuadro que contiene el valor auxiliar
                Panel cuadroDestino = parent.Controls[j + 1] as Panel;
                cuadroDestino.Controls[0].Text = aux.ToString();
                cuadroDestino.BackColor = Color.LightBlue; // Restaurar color del cuadro

                await Task.Delay(500); // Pausa para observar el cambio
            }
        }

        //public void Baraja(int[] A, FlowLayoutPanel panel, bool esAscendente)
        //{
        //    int N = A.Length;

        //    if (panel.Controls.Count != N)
        //    {
        //        throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
        //    }

        //    List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

        //    for (int I = 1; I < N; I++)
        //    {
        //        int AUX = A[I];
        //        int K = I - 1;

        //        Panel cuadroActual = cuadros[I];
        //        cuadroActual.BackColor = Color.Red;  // Resaltar el cuadro actual
        //        panel.Refresh();
        //        Application.DoEvents();
        //        Thread.Sleep(500);

        //        // Ajustar la comparación según el orden ascendente o descendente
        //        while (K >= 0 && (esAscendente ? AUX < A[K] : AUX > A[K]))  // Dependiendo del booleano
        //        {
        //            Panel cuadroMayor = cuadros[K];
        //            cuadroMayor.BackColor = Color.Red;  // Resaltar el cuadro mayor
        //            panel.Refresh();
        //            Application.DoEvents();
        //            Thread.Sleep(500);

        //            // Actualizar el valor en el arreglo
        //            A[K + 1] = A[K];

        //            // Desplazar visualmente el cuadro hacia la derecha o izquierda según el orden
        //            cuadros[K + 1].Controls[0].Text = cuadros[K].Controls[0].Text;

        //            // Animar el tamaño del cuadro desplazado
        //            cuadrito.AnimarCambioDeTamaño(cuadros[K + 1], A[K + 1]);
        //            cuadros[K + 1].BackColor = cuadrito.GenerarColorUnico();
        //            panel.Refresh();
        //            Application.DoEvents();
        //            Thread.Sleep(500);

        //            // Restaurar color original del cuadro mayor
        //            cuadroMayor.BackColor = cuadrito.GenerarColorUnico();
        //            panel.Refresh();
        //            Application.DoEvents();
        //            Thread.Sleep(500);

        //            K--;
        //        }

        //        // Insertar el elemento en la posición correcta
        //        A[K + 1] = AUX;
        //        cuadros[K + 1].Controls[0].Text = AUX.ToString();

        //        // Animar el tamaño del cuadro insertado
        //        cuadrito.AnimarCambioDeTamaño(cuadros[K + 1], AUX);
        //        cuadros[K + 1].BackColor = Color.LightBlue;  // Restaurar color
        //        panel.Refresh();
        //        Application.DoEvents();
        //        Thread.Sleep(500);
        //    }
        //}

        public async Task HeapSortAnimadoAsync(int[] A, FlowLayoutPanel panel, RichTextBox richTextBox, bool esAscendente)
        {
            // Código del algoritmo de Heap Sort como referencia
            string codigoFuente = @"
    for (int I = (N / 2) - 1; I >= 0; I--)
    {
        HacerMonticulo(A, I, N, esAscendente);
    }

    for (int I = N - 1; I > 0; I--)
    {
        Intercambiar(A[0], A[I]);
        HacerMonticulo(A, 0, I, esAscendente);
    }";

            // Mostrar el código en el RichTextBox
            richTextBox.Clear();
            richTextBox.Text = codigoFuente.Trim();
            richTextBox.SelectAll();
            richTextBox.SelectionColor = Color.Black;
            richTextBox.DeselectAll();

            int N = A.Length;
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Crear el montículo inicial
            for (int I = (N / 2) - 1; I >= 0; I--)
            {
                // Resaltar la línea del código que construye el montículo
                await ResaltarCodigoAsync(richTextBox, 2);
                await HacerMonticuloAnimadoAsync(A, cuadros, I, N, panel, richTextBox, esAscendente);
            }

            // Realizar el ordenamiento
            for (int I = N - 1; I > 0; I--)
            {
                // Resaltar la línea de intercambio
                await ResaltarCodigoAsync(richTextBox, 7);

                // Intercambiar el primer y último elemento del montículo
                int AUX = A[0];
                A[0] = A[I];
                A[I] = AUX;

                // Animar el intercambio de cuadros
                cuadros[0].BackColor = Color.Red;
                cuadros[I].BackColor = Color.Red;
                await Task.Delay(500); // Pausa para animación visual

                // Actualizar visualmente los cuadros
                cuadros[0].Controls[0].Text = A[0].ToString();
                cuadros[I].Controls[0].Text = A[I].ToString();

                cuadros[0].BackColor = cuadrito.GenerarColorUnico();
                cuadros[I].BackColor = cuadrito.GenerarColorUnico();

                cuadrito.AnimarCambioDeTamaño(cuadros[0], A[0]);
                cuadrito.AnimarCambioDeTamaño(cuadros[I], A[I]);
                await Task.Delay(500); // Pausa para visualizar el cambio

                // Reconstruir el montículo con el elemento restante
                await ResaltarCodigoAsync(richTextBox, 8);
                await HacerMonticuloAnimadoAsync(A, cuadros, 0, I, panel, richTextBox, esAscendente);
            }

            // Animar el último cuadro restante
            cuadrito.AnimarCambioDeTamaño(cuadros[0], A[0]);
        }

        private async Task HacerMonticuloAnimadoAsync(int[] A, List<Panel> cuadros, int K, int N, FlowLayoutPanel panel, RichTextBox richTextBox, bool esAscendente)
        {
            bool BAND = true;

            while (BAND && K < N)
            {
                // Resaltar la línea que calcula los índices de los hijos
                await ResaltarCodigoAsync(richTextBox, 4);

                int IZQ = 2 * K + 1;
                int DER = IZQ + 1;
                int AP = K;

                // Comparar con el hijo izquierdo
                if (IZQ < N && (esAscendente ? A[IZQ] > A[AP] : A[IZQ] < A[AP]))
                {
                    AP = IZQ;
                }

                // Comparar con el hijo derecho
                if (DER < N && (esAscendente ? A[DER] > A[AP] : A[DER] < A[AP]))
                {
                    AP = DER;
                }

                if (AP != K)
                {
                    // Resaltar la línea que intercambia los elementos
                    await ResaltarCodigoAsync(richTextBox, 8);

                    // Animar el intercambio de cuadros
                    cuadros[K].BackColor = Color.Red;
                    cuadros[AP].BackColor = Color.Red;
                    await Task.Delay(500); // Pausa para animación visual

                    int AUX = A[K];
                    A[K] = A[AP];
                    A[AP] = AUX;

                    cuadros[K].Controls[0].Text = A[K].ToString();
                    cuadros[AP].Controls[0].Text = A[AP].ToString();

                    cuadros[K].BackColor = cuadrito.GenerarColorUnico();
                    cuadros[AP].BackColor = cuadrito.GenerarColorUnico();

                    cuadrito.AnimarCambioDeTamaño(cuadros[K], A[K]);
                    cuadrito.AnimarCambioDeTamaño(cuadros[AP], A[AP]);
                    await Task.Delay(500); // Pausa para visualizar el cambio

                    K = AP;
                }
                else
                {
                    BAND = false;
                }
            }
        }

        //public void HeapSort(int[] A, FlowLayoutPanel panel, bool esAscendente)
        //{
        //    int N = A.Length;
        //    List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

        //    for (int I = (N / 2) - 1; I >= 0; I--)
        //    {
        //        int K = I;
        //        bool BAND = true;

        //        while (K < N)
        //        {
        //            int IZQ = 2 * K + 1;
        //            int DER = IZQ + 1;
        //            int AP = K;

        //            if (IZQ < N && (esAscendente ? A[IZQ] > A[AP] : A[IZQ] < A[AP]))
        //            {
        //                AP = IZQ;
        //            }

        //            if (DER < N && (esAscendente ? A[DER] > A[AP] : A[DER] < A[AP]))
        //            {
        //                AP = DER;
        //            }

        //            // Si el nodo es mayor/menor que el hijo, intercambiar
        //            if (AP != K)
        //            {
        //                // Resaltar los cuadros para animación
        //                cuadros[K].BackColor = Color.Red;
        //                cuadros[AP].BackColor = Color.Red;
        //                Thread.Sleep(200);

        //                int AUX = A[K];
        //                A[K] = A[AP];
        //                A[AP] = AUX;

        //                // Actualizar el texto de los cuadros
        //                cuadros[K].Controls[0].Text = A[K].ToString();
        //                cuadros[AP].Controls[0].Text = A[AP].ToString();
        //                cuadrito.AnimarCambioDeTamaño(cuadros[K], A[K]);
        //                cuadrito.AnimarCambioDeTamaño(cuadros[AP], A[AP]);

        //                // Restaurar colores a aleatorio
        //                cuadros[K].BackColor = cuadrito.GenerarColorUnico();
        //                cuadros[AP].BackColor = cuadrito.GenerarColorUnico();

        //                K = AP; // Avanzar el índice
        //            }
        //            else
        //            {
        //                break;
        //            }

        //            // Forzar actualización visual
        //            cuadros[K].Refresh();
        //            Thread.Sleep(500); // Pausa para visualizar el cambio
        //        }
        //    }

        //    // Llamar a la función de eliminación del montículo
        //    EliminarMonticulo(A, panel, esAscendente);
        //}

        //public void EliminarMonticulo(int[] A, FlowLayoutPanel panel, bool esAscendente)
        //{
        //    int N = A.Length;
        //    List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

        //    for (int I = N - 1; I >= 1; I--)
        //    {
        //        int AUX = A[I];
        //        A[I] = A[0];

        //        // Resaltar en rojo los cuadros que van a cambiar
        //        cuadros[I].BackColor = Color.Red;
        //        cuadros[0].BackColor = Color.Red;

        //        // Forzar actualización visual
        //        cuadros[I].Refresh();
        //        cuadros[0].Refresh();

        //        Thread.Sleep(500); // Pausa para visualizar el cambio

        //        // Actualizar visualmente los cuadros
        //        cuadros[I].Controls[0].Text = A[I].ToString();
        //        cuadros[0].Controls[0].Text = AUX.ToString();

        //        // Cambiar a un color aleatorio después de la actualización
        //        cuadros[I].BackColor = cuadrito.GenerarColorUnico();
        //        cuadros[0].BackColor = cuadrito.GenerarColorUnico();

        //        cuadros[I].Refresh();
        //        cuadros[0].Refresh();

        //        int IZQ = 1, DER = 2, K = 0;
        //        bool BOOL = true;

        //        while ((IZQ < I) && BOOL)
        //        {
        //            int valorComparado = esAscendente ? A[IZQ] : A[IZQ];
        //            int AP = IZQ;

        //            if ((DER < I) && (esAscendente ? valorComparado < A[DER] : valorComparado > A[DER]))
        //            {
        //                valorComparado = A[DER];
        //                AP = DER;
        //            }

        //            // Resaltar en rojo los cuadros que van a cambiar
        //            cuadros[K].BackColor = Color.Red;
        //            cuadros[AP].BackColor = Color.Red;

        //            // Forzar actualización visual
        //            cuadros[K].Refresh();
        //            cuadros[AP].Refresh();

        //            Thread.Sleep(500); // Pausa para visualizar el cambio

        //            if ((esAscendente && AUX < valorComparado) || (!esAscendente && AUX > valorComparado))
        //            {
        //                A[K] = A[AP];

        //                // Actualizar visualmente los cuadros
        //                cuadros[K].Controls[0].Text = A[K].ToString();
        //                cuadros[AP].Controls[0].Text = valorComparado.ToString();

        //                // Cambiar a un color aleatorio después de la actualización
        //                cuadros[K].BackColor = cuadrito.GenerarColorUnico();
        //                cuadros[AP].BackColor = cuadrito.GenerarColorUnico();

        //                cuadros[K].Refresh();
        //                cuadros[AP].Refresh();

        //                K = AP;
        //            }
        //            else
        //            {
        //                BOOL = false;
        //            }

        //            IZQ = 2 * K + 1;
        //            DER = IZQ + 1;
        //        }

        //        A[K] = AUX;

        //        // Resaltar en rojo el cuadro final que va a cambiar
        //        cuadros[K].BackColor = Color.Red;
        //        cuadros[K].Refresh();

        //        Thread.Sleep(500); // Pausa para visualizar el cambio

        //        cuadros[K].Controls[0].Text = AUX.ToString();

        //        // Cambiar a un color aleatorio después de la actualización
        //        cuadros[K].BackColor = cuadrito.GenerarColorUnico();
        //        cuadros[K].Refresh();

        //        // Animar el cambio de tamaño
        //        cuadrito.AnimarCambioDeTamaño(cuadros[K], A[K]);
        //        cuadrito.AnimarCambioDeTamaño(cuadros[I], A[I]);
        //    }

        //    // Animar el último cuadro restante
        //    cuadrito.AnimarCambioDeTamaño(cuadros[0], A[0]);
        //}

        //MEtodos ernesto

        public async Task ShellSortAnimadoAscendenteAsync(int[] A, FlowLayoutPanel panel, RichTextBox richTextBox)
        {
            // Código del algoritmo Shell Sort para mostrar en el RichTextBox
            string codigoFuente = @"
    int h = N / 2;
    while (h > 0)
    {
        for (int i = h; i < N; i++)
        {
            int AUX = A[i];
            int j = i;

            while (j >= h && A[j - h] > AUX)
            {
                A[j] = A[j - h];
                j -= h;
            }

            A[j] = AUX;
        }

        h /= 2;
    }";

            // Mostrar el código en el RichTextBox
            richTextBox.Clear();
            richTextBox.Text = codigoFuente.Trim();
            richTextBox.SelectAll();
            richTextBox.SelectionColor = Color.Black;
            richTextBox.DeselectAll();

            int N = A.Length;
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            int h = N / 2; // Secuencia inicial de gaps

            while (h > 0)
            {
                // Resaltar la línea que inicializa el gap
                await ResaltarCodigoAsync(richTextBox, 0);

                for (int i = h; i < N; i++)
                {
                    int AUX = A[i];
                    int j = i;

                    // Resaltar la línea que comienza el bucle interno
                    await ResaltarCodigoAsync(richTextBox, 8);

                    Panel cuadroActual = cuadros[i];
                    cuadroActual.BackColor = Color.LightBlue;
                    cuadroActual.Refresh();
                    await Task.Delay(500);

                    while (j >= h && A[j - h] > AUX)
                    {
                        // Resaltar la línea que realiza el desplazamiento
                        await ResaltarCodigoAsync(richTextBox, 10);

                        // Desplazar el valor en el arreglo
                        A[j] = A[j - h];

                        // Animar el desplazamiento de cuadros
                        cuadros[j].Controls[0].Text = A[j].ToString();
                        cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

                        cuadros[j].BackColor = Color.Red; // Resaltar cuadro afectado
                        cuadros[j - h].BackColor = Color.Yellow; // Resaltar cuadro que se compara

                        panel.Refresh();
                        await Task.Delay(500);

                        cuadros[j].BackColor = cuadrito.GenerarColorUnico(); // Restaurar color del cuadro
                        cuadros[j - h].BackColor = cuadrito.GenerarColorUnico(); // Restaurar color del cuadro
                        j -= h;
                    }

                    // Resaltar la línea que inserta el valor en la posición correcta
                    await ResaltarCodigoAsync(richTextBox, 14);

                    A[j] = AUX;

                    // Animar el cuadro insertado
                    cuadros[j].Controls[0].Text = AUX.ToString();
                    cuadrito.AnimarCambioDeTamaño(cuadros[j], AUX);

                    cuadros[j].BackColor = Color.Green; // Resaltar cuadro final
                    panel.Refresh();
                    await Task.Delay(500);
                }

                // Reducir el gap y resaltar la línea correspondiente
                await ResaltarCodigoAsync(richTextBox, 17);
                h /= 2;
            }
        }

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

        public async Task ShellSortAnimadoDescendenteAsync(int[] A, FlowLayoutPanel panel, RichTextBox richTextBox)
        {
            // Código del algoritmo de Shell Sort
            string codigoFuente = @"
    int h = N / 2;                                    // Línea 1
    while (h > 0)                                     // Línea 2
    {
        for (int i = h; i < N; i++)                   // Línea 3
        {
            int AUX = A[i];                           // Línea 4
            int j = i;                                // Línea 5

            while (j >= h && A[j - h] < AUX)          // Línea 6
            {
                A[j] = A[j - h];                      // Línea 7
                j -= h;                               // Línea 8
            }

            A[j] = AUX;                               // Línea 9
        }

        h /= 2;                                       // Línea 10
    }";

            // Configurar el RichTextBox con el código fuente
            richTextBox.Clear();
            richTextBox.Text = codigoFuente.Trim();
            richTextBox.SelectAll();
            richTextBox.SelectionColor = Color.Black; // Todo el texto empieza en negro
            richTextBox.DeselectAll();

            int N = A.Length;
            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            int h = N / 2;

            while (h > 0)
            {
                await ResaltarCodigoAsync(richTextBox, 0); // Resaltando la línea: "int h = N / 2;"

                for (int i = h; i < N; i++)
                {
                    int AUX = A[i];
                    int j = i;

                    await ResaltarCodigoAsync(richTextBox, 5); // Resaltando la línea: "int AUX = A[i];"

                    Panel cuadroActual = cuadros[i];
                    cuadroActual.BackColor = Color.LightBlue; // Resaltar cuadro actual
                    cuadroActual.Refresh();
                    await Task.Delay(500);

                    while (j >= h && A[j - h] < AUX) // Comparación para descendente
                    {
                        await ResaltarCodigoAsync(richTextBox, 8); // Resaltando la línea: "while (j >= h && A[j - h] < AUX)"

                        A[j] = A[j - h];

                        cuadros[j].Controls[0].Text = A[j].ToString(); // Actualizar visualmente el cuadro
                        cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

                        cuadros[j].BackColor = Color.Red; // Resaltar cuadro desplazado
                        cuadros[j - h].BackColor = Color.Yellow; // Resaltar cuadro comparado

                        panel.Refresh();
                        await Task.Delay(500);

                        cuadros[j].BackColor = cuadrito.GenerarColorUnico(); // Restaurar colores originales
                        cuadros[j - h].BackColor = cuadrito.GenerarColorUnico();
                        j -= h;

                        await ResaltarCodigoAsync(richTextBox, 11); // Resaltando la línea: "j -= h;"
                    }

                    await ResaltarCodigoAsync(richTextBox, 14); // Resaltando la línea: "A[j] = AUX;"

                    A[j] = AUX;

                    cuadros[j].Controls[0].Text = AUX.ToString(); // Insertar elemento en la posición correcta
                    cuadrito.AnimarCambioDeTamaño(cuadros[j], AUX);

                    cuadros[j].BackColor = Color.Green; // Resaltar cuadro insertado
                    panel.Refresh();
                    await Task.Delay(500);
                }

                await ResaltarCodigoAsync(richTextBox, 17); // Resaltando la línea: "h /= 2;"
                h /= 2;
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

        //            // Desplazar los elementos hacia la izquierda (para orden descendente)
        //            while (j >= h && A[j - h] < AUX)  // Cambiado de > a <
        //            {
        //                A[j] = A[j - h];

        //                // Actualizamos el cuadro visualmente
        //                cuadros[j].Controls[0].Text = A[j].ToString();
        //                cuadrito.AnimarCambioDeTamaño(cuadros[j], A[j]);

        //                cuadros[j].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado
        //                cuadros[j - h].BackColor = cuadrito.GenerarColorUnico(); // Resaltamos el cuadro que está siendo desplazado

        //                // Forzar actualización visual
        //                panel.Refresh();
        //                Application.DoEvents();
        //                Thread.Sleep(500); // Pausa para que se vea el cambio de color

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


        public async Task InsertionDirectaAnimadoAsync(int[] A, FlowLayoutPanel panel, RichTextBox richTextBox)
        {
            // Código del algoritmo de Inserción Directa
            string codigoFuente = @"
    for (int i = 1; i < N; i++)
    {
        int AUX = A[i];
        int j = i - 1;

        while (j >= 0 && A[j] > AUX)
        {
            A[j + 1] = A[j];
            j--;
        }

        A[j + 1] = AUX;
    }";

            // Mostrar el código en el RichTextBox
            richTextBox.Clear();
            richTextBox.Text = codigoFuente.Trim();
            richTextBox.SelectAll();
            richTextBox.SelectionColor = Color.Black;
            richTextBox.DeselectAll();

            int N = A.Length;
            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            for (int i = 1; i < N; i++)
            {
                // Resaltar la línea inicial del ciclo externo
                await ResaltarCodigoAsync(richTextBox, 0);

                int AUX = A[i];
                int j = i - 1;

                Panel cuadroActual = cuadros[i];
                cuadroActual.BackColor = Color.LightBlue;
                cuadroActual.Refresh();
                await Task.Delay(500); // Pausa para resaltar el cuadro actual

                // Resaltar la línea del bucle interno
                await ResaltarCodigoAsync(richTextBox, 5);

                while (j >= 0 && A[j] > AUX)
                {
                    // Resaltar la línea que realiza el desplazamiento
                    await ResaltarCodigoAsync(richTextBox, 7);

                    // Desplazar los elementos en el arreglo
                    A[j + 1] = A[j];

                    // Actualizar el cuadro visualmente
                    cuadros[j + 1].Controls[0].Text = A[j].ToString();
                    cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], A[j]);

                    cuadros[j + 1].BackColor = Color.Red; // Resaltar el cuadro desplazado
                    cuadros[j].BackColor = Color.Yellow; // Resaltar el cuadro comparado

                    panel.Refresh();
                    await Task.Delay(500);

                    cuadros[j + 1].BackColor = cuadrito.GenerarColorUnico(); // Restaurar color
                    cuadros[j].BackColor = cuadrito.GenerarColorUnico();
                    j--;
                }

                // Resaltar la línea que inserta el elemento en la posición correcta
                await ResaltarCodigoAsync(richTextBox, 11);

                A[j + 1] = AUX;

                cuadros[j + 1].Controls[0].Text = AUX.ToString();
                cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], AUX);

                cuadros[j + 1].BackColor = Color.Green; // Resaltar el cuadro insertado
                panel.Refresh();
                await Task.Delay(500);
            }
        }
        //public void InsertionDirecta(int[] A, FlowLayoutPanel panel)
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

        //        // Mover los elementos de A[0..i-1], que son mayores que AUX,
        //        // a una posición adelante de su posición actual
        //        while (j >= 0 && A[j] > AUX)
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

        public async Task InsertionDescendenteAnimadoAsync(int[] A, FlowLayoutPanel panel, RichTextBox richTextBox)
        {
            // Código del algoritmo de Inserción Descendente
            string codigoFuente = @"
    for (int i = 1; i < N; i++)                      // Línea 1
    {
        int AUX = A[i];                              // Línea 2
        int j = i - 1;                               // Línea 3

        while (j >= 0 && A[j] < AUX)                 // Línea 4
        {
            A[j + 1] = A[j];                         // Línea 5
            j--;                                     // Línea 6
        }

        A[j + 1] = AUX;                              // Línea 7
    }";

            // Mostrar el código en el RichTextBox
            richTextBox.Clear();
            richTextBox.Text = codigoFuente.Trim();
            richTextBox.SelectAll();
            richTextBox.SelectionColor = Color.Black; // Inicializamos con todo el texto en negro
            richTextBox.DeselectAll();

            int N = A.Length;
            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            for (int i = 1; i < N; i++)
            {
                await ResaltarCodigoAsync(richTextBox, 0); // Resaltando la línea: "for (int i = 1; i < N; i++)"

                int AUX = A[i];
                int j = i - 1;

                Panel cuadroActual = cuadros[i];
                cuadroActual.BackColor = Color.LightBlue; // Resaltar el cuadro actual en el panel
                cuadroActual.Refresh();
                await Task.Delay(500);

                await ResaltarCodigoAsync(richTextBox, 5); // Resaltando la línea: "while (j >= 0 && A[j] < AUX)"

                while (j >= 0 && A[j] < AUX)
                {
                    await ResaltarCodigoAsync(richTextBox, 7); // Resaltando la línea: "A[j + 1] = A[j];"

                    A[j + 1] = A[j];

                    cuadros[j + 1].Controls[0].Text = A[j].ToString(); // Actualizar el valor del cuadro
                    cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], A[j]);

                    cuadros[j + 1].BackColor = Color.Red; // Resaltar el cuadro desplazado
                    cuadros[j].BackColor = Color.Yellow; // Resaltar el cuadro comparado

                    panel.Refresh();
                    await Task.Delay(500);

                    cuadros[j + 1].BackColor = cuadrito.GenerarColorUnico(); // Restaurar el color del cuadro
                    cuadros[j].BackColor = cuadrito.GenerarColorUnico();
                    j--;

                    await ResaltarCodigoAsync(richTextBox, 8); // Resaltando la línea: "j--;"
                }

                await ResaltarCodigoAsync(richTextBox, 11); // Resaltando la línea: "A[j + 1] = AUX;"

                A[j + 1] = AUX;

                cuadros[j + 1].Controls[0].Text = AUX.ToString(); // Insertar el elemento en la posición correcta
                cuadrito.AnimarCambioDeTamaño(cuadros[j + 1], AUX);

                cuadros[j + 1].BackColor = Color.Green; // Resaltar el cuadro insertado
                panel.Refresh();
                await Task.Delay(500);
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

        //
        //
        //
        //
        //Métodos fabo

        public void BucketSortDescendente(int[] arr, FlowLayoutPanel panel, int numBuckets, int bucketSize)
        {
            if (bucketSize <= 0 || numBuckets <= 0)
            {
                MessageBox.Show("El número de cubetas y el tamaño debe ser diferente a 0");
                return;
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Crear los cubos visuales
            List<List<int>> buckets = new List<List<int>>(numBuckets);
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

            // Asegurarse de que el panel de cubos se añada al contenedor correcto
            panel.Parent.Controls.Add(cubosPanel);

            for (int i = 0; i < numBuckets; i++)
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

            // Distribuir los elementos en los cubos
            foreach (int num in arr)
            {
                int bucketIndex = num / bucketSize; // Ajustar tamaño del cubo

                // Validar que bucketIndex esté dentro del rango
                if (bucketIndex < 0 || bucketIndex >= numBuckets)
                {
                    MessageBox.Show("Asegúrese de que el tamaño y número de cubetas corresponda con la cantidad de elementos", "Error de índice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }

                buckets[bucketIndex].Add(num);

                // Mover visualmente el cuadro al cubo correspondiente
                Panel cuadro = cuadros.First(c => c.Controls[0].Text == num.ToString());
                cuadros.Remove(cuadro);
                AnimarMovimiento(cuadro, cubosVisuales[bucketIndex]);
            }

            // Ordenar las cubetas por el valor máximo de sus elementos en orden descendente
            var bucketsWithVisuals = buckets
                .Select((bucket, i) => new { Bucket = bucket, Visual = cubosVisuales[i] })
                .Where(b => b.Bucket.Any()) // Filtrar cubetas vacías
                .OrderByDescending(b => b.Bucket.Max())
                .ToList();

            // Ordenar cada cubo y combinar
            int index = 0;
            foreach (var item in bucketsWithVisuals)
            {
                List<int> bucket = item.Bucket;
                FlowLayoutPanel cuboVisual = item.Visual;

                // Ordenar el cubo en orden descendente
                bucket.Sort((a, b) => b.CompareTo(a));

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


        public int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public void BucketSortAscendente(int[] arr, FlowLayoutPanel panel, int numBuckets, int bucketSize)
        {
            if (bucketSize <= 0 || numBuckets <= 0)
            {
                MessageBox.Show("El número de cubetas y el tamaño debe ser diferente a 0");
            }
            else
            {
                List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

                // Crear los cubos visuales
                List<List<int>> buckets = new List<List<int>>(numBuckets);
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

                // Asegurarse de que el panel de cubos se añada al contenedor correcto
                panel.Parent.Controls.Add(cubosPanel);

                for (int i = 0; i < numBuckets; i++)
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

                // Distribuir los elementos en los cubos
                foreach (int num in arr)
                {
                    int bucketIndex = num / bucketSize; // Ajustar tamaño del cubo

                    // Validar que bucketIndex esté dentro del rango
                    if (bucketIndex < 0 || bucketIndex >= numBuckets)
                    {
                        MessageBox.Show($"Asegurese de que el tamaño y numero de cubetas corresponda con la cantidad de elementos", "Error de índice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }

                    buckets[bucketIndex].Add(num);

                    // Mover visualmente el cuadro al cubo correspondiente
                    Panel cuadro = cuadros.First(c => c.Controls[0].Text == num.ToString());
                    cuadros.Remove(cuadro);
                    AnimarMovimiento(cuadro, cubosVisuales[bucketIndex]);
                }

                // Ordenar cada cubo y combinar
                int index = 0;
                foreach (List<int> bucket in buckets)
                {
                    // Ordenar el cubo en orden ascendente
                    bucket.Sort();
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
                richTextBox.Width = 260;
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
                Thread.Sleep(90); // Pausa para que la animación sea visible
            }
        }

        public async void RadixSortDescendente(int[] A, FlowLayoutPanel panel, RichTextBox richTextBoxDigits)
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
                await CountingSortWithAnimationDescendente(A, exp, cuadros, richTextBoxDigits, panel);
            }

            // Finalizar la visualización
            for (int i = 0; i < N; i++)
            {
                cuadros[i].BackColor = Color.Green; // Cambiar color final
                AnimarCambioDeTamaño(cuadros[i], A[i]); // Ajustar al tamaño final
            }
            panel.Refresh();
        }

        private async Task CountingSortWithAnimationDescendente(int[] arr, int exp, List<Panel> cuadros, RichTextBox richTextBoxDigits, FlowLayoutPanel panel)
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

            // Construir el arreglo de salida en orden descendente
            for (int i = n - 1; i >= 0; i--)
            {
                int digit = GetDigit(arr[i], exp);
                output[n - count[digit]] = arr[i]; // Colocar en la posición inversa
                count[digit]--;

                // Actualizar visualización
                int index = n - count[digit] - 1; // Obtener el índice correcto
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

        //
        //
        //Fin Métodos fabo


    }
}
