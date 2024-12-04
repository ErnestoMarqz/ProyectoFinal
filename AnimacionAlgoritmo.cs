using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ProyectoFinal
{
    internal class AnimacionAlgoritmo
    {
        private readonly string[] pasosAlgoritmoShell, pasosAlgoritmoInDirecta, pasosAlgoritmoQuickSort,
            pasosAlgoritmoCubetaAsc, pasosAlgoritmoCubetasDes, pasosAlgoritmoRadixSortAsc, pasosAlgoritmoRadixSortDes, pasosAlgoritmoInsercionBinaria, pasosAlgortimoHeapSort, AlgoritmoHeapSortDescendente, pasosAlgoritmoBaraja, AlgoritmoBarajaDescendente, AlgoritmoBusquedaBinaria; // Pasos detallados del algoritmo
        private int pasoActual;
        private Timer textoTimer;
        private RichTextBox richTextBox;

        public async Task OrdenarBurbujaMejoradoConAlgoritmoAnimado(FlowLayoutPanel parent, RichTextBox richTextBox, bool ascendente)
        {
            // Paso 1: Mostrar el algoritmo en el RichTextBox
            List<string> algoritmo = new List<string>
    {
        "for (int i = 0; i < valores.Count - 1; i++)",
        "{",
        "    bool intercambio = false;",
        "    for (int j = 0; j < valores.Count - 1 - i; j++)",
        "    {",
        "        if (ascendente ? valores[j] > valores[j + 1] : valores[j] < valores[j + 1])",
        "        {",
        "            Intercambiar(valores[j], valores[j + 1]);",
        "            intercambio = true;",
        "        }",
        "    }",
        "    if (!intercambio) break;",
        "}"
    };

            MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, -1);

            // Paso 2: Ejecutar el algoritmo mientras se actualiza el RichTextBox
            List<int> valores = parent.Controls.Cast<Panel>()
                .Select(cuadro => int.Parse((cuadro.Controls[0] as Label).Text))
                .ToList();

            for (int i = 0; i < valores.Count - 1; i++)
            {
                // Resaltar línea del primer bucle
                MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 0);
                await Task.Delay(500);

                bool intercambio = false;

                MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 2); // Línea de inicialización de "intercambio"
                await Task.Delay(500);

                for (int j = 0; j < valores.Count - 1 - i; j++)
                {
                    MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 3); // Línea del segundo bucle
                    await Task.Delay(500);

                    bool intercambiar = ascendente ? valores[j] > valores[j + 1] : valores[j] < valores[j + 1];
                    MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 5); // Condición de intercambio
                    await Task.Delay(500);

                    if (intercambiar)
                    {
                        // Resaltar la línea del intercambio
                        MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 7);
                        (valores[j], valores[j + 1]) = (valores[j + 1], valores[j]);

                        await Cuadritos.IntercambiarCuadrosAnimado(parent, j, j + 1);

                        intercambio = true;
                        MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 8); // Línea donde "intercambio" se actualiza
                        await Task.Delay(500);
                    }
                }

                MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 11); // Línea del "if (!intercambio)"
                if (!intercambio) break;
            }

            MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, -1); // Restablecer (ninguna línea resaltada)
        }

        private void MostrarAlgoritmoRichTextBox(List<string> algoritmo, RichTextBox richTextBox, int lineaActual)
        {
            richTextBox.Clear();

            for (int i = 0; i < algoritmo.Count; i++)
            {
                if (i == lineaActual)
                {
                    richTextBox.SelectionColor = Color.Red; // Resaltar la línea actual
                }
                else
                {
                    richTextBox.SelectionColor = Color.Black;
                }

                richTextBox.AppendText(algoritmo[i] + Environment.NewLine);
            }

            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.ScrollToCaret();
        }

        public async Task OrdenarBurbujaEstándarConAlgoritmoAnimado(FlowLayoutPanel parent, RichTextBox richTextBox, bool ascendente)
        {
            // Paso 1: Mostrar el algoritmo en el RichTextBox
            List<string> algoritmo = new List<string>
    {
        "for (int i = 0; i < valores.Count - 1; i++)",
        "{",
        "    for (int j = 0; j < valores.Count - 1 - i; j++)",
        "    {",
        "        if (ascendente ? valores[j] > valores[j + 1] : valores[j] < valores[j + 1])",
        "        {",
        "            Intercambiar(valores[j], valores[j + 1]);",
        "        }",
        "    }",
        "}"
    };

            MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, -1);

            // Paso 2: Ejecutar el algoritmo mientras se actualiza el RichTextBox
            List<int> valores = parent.Controls.Cast<Panel>()
                .Select(cuadro => int.Parse((cuadro.Controls[0] as Label).Text))
                .ToList();

            for (int i = 0; i < valores.Count - 1; i++)
            {
                // Resaltar línea del primer bucle
                MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 0);
                await Task.Delay(500);

                for (int j = 0; j < valores.Count - 1 - i; j++)
                {
                    MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 2); // Línea del segundo bucle
                    await Task.Delay(500);

                    bool intercambiar = ascendente ? valores[j] > valores[j + 1] : valores[j] < valores[j + 1];
                    MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 4); // Condición de intercambio
                    await Task.Delay(500);

                    if (intercambiar)
                    {
                        // Resaltar la línea del intercambio
                        MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 6);
                        (valores[j], valores[j + 1]) = (valores[j + 1], valores[j]);

                        await Cuadritos.IntercambiarCuadrosAnimado(parent, j, j + 1);
                        await Task.Delay(500);
                    }
                }
            }

            MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, -1); // Restablecer (ninguna línea resaltada)
        }



        public AnimacionAlgoritmo(RichTextBox richTextBox)
        {

            pasosAlgoritmoShell = new string[]
    {
            "{",

                "while (intervalo > 0)",
                "{",
                    "for (int i = intervalo; i < n; i++)",
                    "{",
                        "int j = i;",

                        "while (j >= intervalo)",
                        "{",
                           "Panel cuadroA = parent.Controls[j - intervalo] as Panel;",
                            "Panel cuadroB = parent.Controls[j] as Panel;",

                            "if (cuadroA == null || cuadroB == null) break;",

                            "int valorA = int.Parse((cuadroA.Controls[0] as Label).Text);",
                            "int valorB = int.Parse((cuadroB.Controls[0] as Label).Text);",

                            "bool intercambiar = ascendente ? valorA > valorB : valorA < valorB;",
                            "if (intercambiar)",
                            "{",
                                "parent.Controls.SetChildIndex(cuadroB, j - intervalo);",
                                "parent.Controls.SetChildIndex(cuadroA, j);",
                            "}",

                            "j -= intervalo;",
                        "}",
                    "}",

                    "intervalo /= 2;",
                "}",
            "}"
        };

            pasosAlgoritmoInDirecta = new string[]
    {
         "InsertionDirecta(int[] A, FlowLayoutPanel panel)",
            "{",
                "int N = A.Length;",

                "if (panel.Controls.Count != N)",
                "{",
                    "throw new InvalidOperationException(El número de cuadros no coincide con el tamaño del arreglo.);",
                "}",

                "for (int i = 1; i < N; i++)",
                "{",
                    "int AUX = A[i];",
                    "int j = i - 1; ",

                    "while (j >= 0 && A[j] > AUX)",
                    "{",
                        "A[j + 1] = A[j];",

                        "Panel cuadro = panel.Controls[j + 1] as Panel;",
                        "if (cuadro != null)",
                        "{",
                            "Label label = cuadro.Controls[0] as Label;",
                            "if (label != null)",
                            "{",
                                "label.Text = A[j].ToString();",
                            "}",
                        "}",

                        "j--;",
                    "}",

                    "A[j + 1] = AUX;",

                    "Panel cuadroInsertado = panel.Controls[j + 1] as Panel;",
                    "if (cuadroInsertado != null)",
                    "{",
                        "Label label = cuadroInsertado.Controls[0] as Label;",
                        "if (label != null)",
                        "{",
                            "label.Text = AUX.ToString();",
                        "}",
                    "}",
                "}",
            "}",

        };

            pasosAlgoritmoQuickSort = new string[]
    {
            "{",
                "if (panel.Controls.Count != A.Length)",
                    "throw new InvalidOperationException(El número de cuadros no coincide con el tamaño del arreglo.);",

                "if (low < high)",
               "{",
                    "int pivot = A[high];",
                    "int i = low - 1;",

                    "for (int j = low; j < high; j++)",
                    "{",
                        "if (A[j] < pivot) // Cambiar lógica para ascendente",
                        "{",
                            "i++;",
                            "(A[i], A[j]) = (A[j], A[i]);",

                            "var labelI = panel.Controls[i]?.Controls[0] as Label;",
                            "var labelJ = panel.Controls[j]?.Controls[0] as Label;",
                            "if (labelI != null && labelJ != null)",
                                "(labelI.Text, labelJ.Text) = (labelJ.Text, labelI.Text);",
                        "}",
                    "}",

                    "(A[i + 1], A[high]) = (A[high], A[i + 1]);",
                    "var labelPivot = panel.Controls[i + 1]?.Controls[0] as Label;",
                    "var labelHigh = panel.Controls[high]?.Controls[0] as Label;",
                    "if (labelPivot != null && labelHigh != null)",
                        "(labelPivot.Text, labelHigh.Text) = (labelHigh.Text, labelPivot.Text);",

                    "int partitionIndex = i + 1;",

                    "QuicksortAscendente(A, panel, low, partitionIndex - 1);",
                    "QuicksortAscendente(A, panel, partitionIndex + 1, high);",
                "}",
            "};"

        };

            pasosAlgoritmoCubetaAsc = new string[]
    {
        "{",
            "if (arr.Length == 0) return;",

            "List<List<int>> buckets = new List<List<int>>(bucketCount);",
            "for (int i = 0; i < bucketCount; i++)",
            "{",
                "buckets.Add(new List<int>());",
            "}",

            "foreach (int num in arr)",
            "{",
                "int bucketIndex = num * bucketCount / (arr.Max() + 1);",
                "buckets[bucketIndex].Add(num);",
            "}",

            "int index = 0;",
            "foreach (var bucket in buckets)",
            "{",
                "bucket.Sort();",
                "foreach (var num in bucket)",
                "{",
                    "arr[index++] = num;",
                "}",
            "}",
        "}",

    };
            pasosAlgoritmoCubetasDes = new string[]
          {

        "{",
            "if (arr.Length == 0) return;",

           "List<List<int>> buckets = new List<List<int>>(bucketCount);" ,
            "for (int i = 0; i < bucketCount; i++)",
            "{",
                "buckets.Add(new List<int>());",
            "}",

            "foreach (int num in arr)",
            "{",
                "int bucketIndex = num * bucketCount / (arr.Max() + 1);",
                "buckets[bucketIndex].Add(num);",
            "}",

            "int index = 0;",
            "foreach (var bucket in buckets)",
            "{",
                "bucket.Sort();" ,
                "bucket.Reverse();",
                "foreach (var num in bucket)",
                "{",
                    "arr[index++] = num;",
                "}",
            "}",
        };

            pasosAlgoritmoRadixSortAsc = new string[]
    {
        "{    ",
            "int max = arr.Max();",
                "for (int exp = 1;max / exp > 0; exp *= 10)    ",
                "{",
                    "CountingSort(arr, exp);",
                "}",
        "}",
                                        
        "{",
            " int n = arr.Length;",
                    "int[] output = new int[n];",
                    "int[] count = new int[10];",

                    "for (int i = 0; i < count.Length; i++)",
                        "count[i] = 0;",
                    "for (int i = 0; i < n; i++)",
                        "count[(arr[i] / exp) % 10]++;",
                    " for (int i = 1; i < 10; i++)",
                        "count[i] += count[i - 1];2",
                    "for (int i = n - 1; i >= 0; i--)",
                    "{",
                        "output[count[(arr[i] / exp) % 10] - 1] = arr[i];",
                        "count[(arr[i] / exp) % 10]--;",
                    "}",
                    "for (int i = 0; i < n; i++)",
                        "arr[i] = output[i];",
        "}",
    };
            pasosAlgoritmoRadixSortDes = new string[]
    {
            "{",
                "int max = arr.Max();",
                "for (int exp = 1; max / exp > 0; exp *= 10)",
                "{",
                    "CountingSortDescendente(arr, exp);",
                "}",
            "}",

            "{",
                "int n = arr.Length;",
                "int[] output = new int[n];",
                "int[] count = new int[10];",

                "for (int i = 0; i < count.Length; i++)",
                    "count[i] = 0;",

                "for (int i = 0; i < n; i++)",
                    "count[(arr[i] / exp) % 10]++;",

                "for (int i = 8; i >= 0; i--)",
                    "count[i] += count[i + 1];",

                "for (int i = 0; i < n; i++)",
                "{",
                    "output[count[(arr[i] / exp) % 10] - 1] = arr[i];",
                    "count[(arr[i] / exp) % 10]--;",
                "}",

                "for (int i = 0; i < n; i++)",
                    "arr[i] = output[i];",
        "}",
    };

    pasosAlgoritmoInsercionBinaria = new string[]
    {
            "int n = parent.Controls.Count;",
            "for (int i = 1; i < n; i++)",
            "{",
                "Panel cuadroActual = parent.Controls[i] as Panel;",
                "int valorActual = int.Parse((cuadroActual.Controls[0] as Label).Text);",
                "int posicion = await BuscarPosicionBinariaAnimada(parent, arreglo, valorActual, 0, i - 1, ascendente);",
                "if (posicion < i)",
                "{",
                    "int temp = arreglo[i];",
                    "for (int j = i; j > posicion; j--)",
                    "{",
                        "arreglo[j] = arreglo[j - 1];",
                    "}",
                    "arreglo[posicion] = temp;",
                "}",
            "}"
        };

            pasosAlgortimoHeapSort = new string[]
             {

                    "for (int I = 1; I < N; I++)",
                    "{",
                        "int K = I + 1;",
                      "  bool BAND = true;",

                        "while (K > 1 && BAND)",
                       " {",
                           " BAND = false;",
                           " int padre = (K - 1) / 2;",

                           " if (A[K - 1] > A[padre])",
                            "{",
                               " int AUX = A[padre];",
                                "A[padre] = A[K - 1];",
                                "A[K - 1] = AUX;",

                               " K = padre + 1;",
                                "BAND = true;",
                           " }",
                       " }",
                   " }",
                "}",
               "for (int I = N - 1; I >= 1; I--)",
               " {",
                   " int AUX = A[I];",
                    "A[I] = A[0];",
                    "int IZQ = 1, DER = 2, K = 0;",
                    "bool BOOL = true;",

                    "while ((IZQ < I) && (BOOL == true))",
                   " {",
                        "int MAYOR = A[IZQ];",
                        "int AP = IZQ;",

                       " if ((DER < I) && (MAYOR < A[DER]))",
                      "  {",
                           " MAYOR = A[DER];",
                           " AP = DER;",
                        "}",

                       " if (AUX < MAYOR)",
                       " {",
                           " A[K] = A[AP];",
                           " K = AP;",
                      "  }",
                        "else",
                        "{",
                           " BOOL = false;",
                        "}",

                        "IZQ = 2 * K + 1;",
                        "DER = IZQ + 1;",
                    "}",

                   " A[K] = AUX;",
                "}",
            "}",
          
                };

            AlgoritmoHeapSortDescendente = new string[]
        {
        "public void HeapSortDescendente(int N, int[] A)",
            "{",
                "for (int I = 1; I < N; I++)",
                "{",
                    "int K = I + 1;",
                    "bool BAND = true;",

                    "while (K > 1 && BAND)",
                    "{",
                        "BAND = false;",
                        "int padre = (K - 1) / 2;",

                        "if (A[K - 1] < A[padre])",
                        "{",
                            "int AUX = A[padre];",
                            "A[padre] = A[K - 1];",
                           " A[K - 1] = AUX;",

                            "K = padre + 1;",
                            "BAND = true;",
                        "}",
                    "}",
                "}",
                "for (int I = N - 1; I >= 1; I--)",
                "{",
                    "int AUX = A[I];",
                    "A[I] = A[0];",
                    "int IZQ = 1, DER = 2, K = 0;",
                    "bool BOOL = true;",

                    "while ((IZQ < I) && (BOOL == true))",
                    "{",
                        "int MAYOR = A[IZQ];",
                        "int AP = IZQ;",

                        "if ((DER < I) && (MAYOR < A[DER]))",
                        "{",
                            "MAYOR = A[DER];",
                            "AP = DER;",
                        "}",

                        "if (AUX > MAYOR)",
                        "{",
                           " A[K] = A[AP];",
                            "K = AP;",
                        "}",
                        "else",
                        "{",
                            "BOOL = false;",
                        "}",

                        "IZQ = 2 * K + 1;",
                        "DER = IZQ + 1;",
                    "}",

                    "A[K] = AUX;",
                "}",
            "}",

            };

            pasosAlgoritmoBaraja = new string[]
    {
            "int N = A.Length;",
            "for (int I = 1; I < N; I++) ",
            "{",
            "int AUX = A[I];",
                "int K = I - 1;",

                "while (K >= 0 && AUX > A[K])",
                "{",
                    "A[K + 1] = A[K];",
                    "K--;",
                "}",
                "A[K + 1] = AUX;",
            "}"
        };
            AlgoritmoBarajaDescendente = new string[]
    {
        "int N = A.Length;",
            "for (int I = 1; I < N; I++)",
            "{",
                "int AUX = A[I];",
                "int K = I - 1;",

                "while (K >= 0 && AUX > A[K])",
                "{",
                    "A[K + 1] = A[K];",
                    "K--;",
                "}",

               " A[K + 1] = AUX;",
            "}",
        };
            AlgoritmoBusquedaBinaria = new string[]
        {
            "int totalElementos = parent.Controls.Count;",
            "if (totalElementos == 0)",
            "{",
                "MessageBox.Show('El panel está vacío. No hay elementos para buscar.', 'Advertencia');",
                "return -1;",
            "}",
            "int centro = totalElementos / 2;",
            "int izquierda = centro - 1;",
            "int derecha = centro + 1;",
            "if (await RevisarIndiceConAnimacion(parent, centro, valorBuscado))",
            "{",
                "return centro;",
            "}",
            "while (izquierda >= 0 || derecha < totalElementos)",
            "{",
                "if (izquierda >= 0 && await RevisarIndiceConAnimacion(parent, izquierda, valorBuscado))",
                "{",
                    "return izquierda;",
                "}",
                "if (derecha < totalElementos && await RevisarIndiceConAnimacion(parent, derecha, valorBuscado))",
                "{",
                    "return derecha;",
                "}",
                "izquierda--;",
                "derecha++;",
            "}",
            "MessageBox.Show($'El número {valorBuscado} no fue encontrado.', 'Resultado');",
            "return -1;"
        };

            this.richTextBox = richTextBox;
            textoTimer = new Timer();
        }

        public void IniciarShell()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompletoShell();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoShell.Length)
                {
                    ResaltarPasoShell(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }
        private void MostrarAlgoritmoCompletoShell()
        {
            richTextBox.Clear();
            foreach (string paso in pasosAlgoritmoShell)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }
        private void ResaltarPasoShell(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLineaShell(paso);
            int longitud = pasosAlgoritmoShell[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLineaShell(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += pasosAlgoritmoShell[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManualShell(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < pasosAlgoritmoShell.Length)
            {
                ResaltarPasoShell(paso);
            }
        }

        public void Iniciar_Inserción_Directa()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompleto_Inserción_Directa();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoInDirecta.Length)
                {
                    ResaltarPaso_Inserción_Directa(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }
        private void MostrarAlgoritmoCompleto_Inserción_Directa()
        {
            richTextBox.Clear();
            foreach (string paso in pasosAlgoritmoInDirecta)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }
        private void ResaltarPaso_Inserción_Directa(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLinea_Inserción_Directa(paso);
            int longitud = pasosAlgoritmoInDirecta[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLinea_Inserción_Directa(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += pasosAlgoritmoInDirecta[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManual_Inserción_Directa(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < pasosAlgoritmoInDirecta.Length)
            {
                ResaltarPaso_Inserción_Directa(paso);
            }
        }

        public void IniciarQuickSort()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompletoQuickSort();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoQuickSort.Length)
                {
                    ResaltarPasoQuickSort(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        private void MostrarAlgoritmoCompletoQuickSort()
        {
            richTextBox.Clear();
            foreach (string paso in pasosAlgoritmoQuickSort)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }

        private void ResaltarPasoQuickSort(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLineaQuickSort(paso);
            int longitud = pasosAlgoritmoQuickSort[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLineaQuickSort(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += pasosAlgoritmoQuickSort[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManualQuickSort(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < pasosAlgoritmoQuickSort.Length)
            {
                ResaltarPasoQuickSort(paso);
            }
        }

        public void IniciarCubetaAsc()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompletoCubetaAsc();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoCubetaAsc.Length)
                {
                    ResaltarPasoCubetaAsc(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        private void MostrarAlgoritmoCompletoCubetaAsc()
        {
            richTextBox.Clear();
            foreach (string paso in pasosAlgoritmoCubetaAsc)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }
        private void ResaltarPasoCubetaAsc(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLineaCubetaAsc(paso);
            int longitud = pasosAlgoritmoCubetaAsc[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLineaCubetaAsc(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += pasosAlgoritmoCubetaAsc[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManualCubetaAsc(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < pasosAlgoritmoCubetaAsc.Length)
            {
                ResaltarPasoCubetaAsc(paso);
            }
        }

        public void IniciarCubetasDes()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompletoCubetasDes();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoCubetasDes.Length)
                {
                    ResaltarPasoCubetasDes(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        private void MostrarAlgoritmoCompletoCubetasDes()
        {
            richTextBox.Clear();
            foreach (string paso in pasosAlgoritmoCubetasDes)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }

        private void ResaltarPasoCubetasDes(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLineaCubetasDes(paso);
            int longitud = pasosAlgoritmoCubetasDes[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLineaCubetasDes(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += pasosAlgoritmoCubetasDes[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManualCubetasDes(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < pasosAlgoritmoCubetasDes.Length)
            {
                ResaltarPasoCubetasDes(paso);
            }
        }

        public void IniciarRadixSortAsc()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompletoRadixSortAsc();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoRadixSortAsc.Length)
                {
                    ResaltarPasoRadixSortAsc(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }
        private void MostrarAlgoritmoCompletoRadixSortAsc()
        {
            richTextBox.Clear();
            foreach (string paso in pasosAlgoritmoRadixSortAsc)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }
        private void ResaltarPasoRadixSortAsc(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLineaRadixSortAsc(paso);
            int longitud = pasosAlgoritmoRadixSortAsc[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLineaRadixSortAsc(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += pasosAlgoritmoRadixSortAsc[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManualRadixSortAsc(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < pasosAlgoritmoRadixSortAsc.Length)
            {
                ResaltarPasoRadixSortAsc(paso);
            }
        }

        public void IniciarRadixSortDes()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompletoRadixSortDes();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoRadixSortDes.Length)
                {
                    ResaltarPasoRadixSortDes(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        private void MostrarAlgoritmoCompletoRadixSortDes()
        {
            richTextBox.Clear();
            foreach (string paso in pasosAlgoritmoRadixSortDes)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }

        private void ResaltarPasoRadixSortDes(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLineaRadixSortDes(paso);
            int longitud = pasosAlgoritmoRadixSortDes[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLineaRadixSortDes(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += pasosAlgoritmoRadixSortDes[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManualRadixSortDes(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < pasosAlgoritmoRadixSortDes.Length)
            {
                ResaltarPasoRadixSortDes(paso);
            }
        }

        public void IniciarInsercionBinaria()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompletoInsercionBinaria();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoInsercionBinaria.Length)
                {
                    ResaltarPasoInsercionBinaria(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        private void MostrarAlgoritmoCompletoInsercionBinaria()
        {
            richTextBox.Clear();
            foreach (string paso in pasosAlgoritmoInsercionBinaria)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }

        private void ResaltarPasoInsercionBinaria(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLineaInsercionBinaria(paso);
            int longitud = pasosAlgoritmoInsercionBinaria[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLineaInsercionBinaria(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += pasosAlgoritmoInsercionBinaria[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManualInsercionBinaria(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < pasosAlgoritmoInsercionBinaria.Length)
            {
                ResaltarPasoInsercionBinaria(paso);
            }
        }

        public void IniciarHeapSortAsc()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompletoHeapSort();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgortimoHeapSort.Length)
                {
                    ResaltarPasoHeapSort(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        private void MostrarAlgoritmoCompletoHeapSort()
        {
            richTextBox.Clear();
            foreach (string paso in pasosAlgortimoHeapSort)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }

        private void ResaltarPasoHeapSort(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLineaHeapSort(paso);
            int longitud = pasosAlgortimoHeapSort[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLineaHeapSort(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += pasosAlgortimoHeapSort[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManualHeapSort(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < pasosAlgortimoHeapSort.Length)
            {
                ResaltarPasoHeapSort(paso);
            }
        }

        public void IniciarHeapSortDes()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompletoHeapSortDescendente();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < AlgoritmoHeapSortDescendente.Length)
                {
                    ResaltarPasoHeapSortDescendente(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        private void MostrarAlgoritmoCompletoHeapSortDescendente()
        {
            richTextBox.Clear();
            foreach (string paso in AlgoritmoHeapSortDescendente)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }

        private void ResaltarPasoHeapSortDescendente(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLineaHeapSortDescendente(paso);
            int longitud = AlgoritmoHeapSortDescendente[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLineaHeapSortDescendente(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += AlgoritmoHeapSortDescendente[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManualHeapSortDescendente(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < AlgoritmoHeapSortDescendente.Length)
            {
                ResaltarPasoHeapSortDescendente(paso);
            }
        }

        public void BarajaAsc()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompletoBaraja();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoBaraja.Length)
                {
                    ResaltarPasoBaraja(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        private void MostrarAlgoritmoCompletoBaraja()
        {
            richTextBox.Clear();
            foreach (string paso in pasosAlgoritmoBaraja)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }

        private void ResaltarPasoBaraja(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLineaBaraja(paso);
            int longitud = pasosAlgoritmoBaraja[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLineaBaraja(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += pasosAlgoritmoBaraja[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManualBaraja(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < pasosAlgoritmoBaraja.Length)
            {
                ResaltarPasoBaraja(paso);
            }
        }

        public void BarajaDes()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompletoBarajaDescendente();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < AlgoritmoBarajaDescendente.Length)
                {
                    ResaltarPasoBarajaDescendente(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        private void MostrarAlgoritmoCompletoBarajaDescendente()
        {
            richTextBox.Clear();
            foreach (string paso in AlgoritmoBarajaDescendente)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }

        private void ResaltarPasoBarajaDescendente(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLineaBarajaDescendente(paso);
            int longitud = AlgoritmoBarajaDescendente[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLineaBarajaDescendente(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += AlgoritmoBarajaDescendente[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManualBarajaDescendente(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < AlgoritmoBarajaDescendente.Length)
            {
                ResaltarPasoBarajaDescendente(paso);
            }
        }

        public void BusqBin()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompletoBusquedaBinaria();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < AlgoritmoBusquedaBinaria.Length)
                {
                    ResaltarPasoBusquedaBinaria(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        private void MostrarAlgoritmoCompletoBusquedaBinaria()
        {
            richTextBox.Clear();
            foreach (string paso in AlgoritmoBusquedaBinaria)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }

        private void ResaltarPasoBusquedaBinaria(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLineaBusquedaBinaria(paso);
            int longitud = AlgoritmoBusquedaBinaria[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLineaBusquedaBinaria(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += AlgoritmoBusquedaBinaria[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManualBusquedaBinaria(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < AlgoritmoBusquedaBinaria.Length)
            {
                ResaltarPasoBusquedaBinaria(paso);
            }
        }

    }
}
