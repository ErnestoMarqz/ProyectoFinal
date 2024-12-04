﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    internal class AnimacionAlgoritmo
    {
        private readonly string[] pasosAlgoritmo, pasosAlgoritmoShell, pasosAlgoritmoInDirecta, pasosAlgoritmoQuickSort,
            pasosAlgoritmoCubetaAsc, pasosAlgoritmoCubetasDes, pasosAlgoritmoRadixSortAsc, pasosAlgoritmoRadixSortDes, pasosAlgoritmoInsercionBinaria, pasosAlgortimoHeapSort, AlgoritmoHeapSortDescendente, pasosAlgoritmoBaraja, AlgoritmoBarajaDescendente; // Pasos detallados del algoritmo
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
                        MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 6);
                        (valores[j], valores[j + 1]) = (valores[j + 1], valores[j]);

                        await Cuadritos.IntercambiarCuadrosAnimado(parent, j, j + 1);

                        intercambio = true;
                        MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 7); // Línea donde "intercambio" se actualiza
                        await Task.Delay(500);
                    }
                }

                MostrarAlgoritmoRichTextBox(algoritmo, richTextBox, 10); // Línea del "if (!intercambio)"
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



        public AnimacionAlgoritmo (RichTextBox richTextBox)
        {

            pasosAlgoritmoShell = new string[]
    {
        "Comienza con una secuencia de intervalos (gap) inicial",
        "Realiza un ciclo mientras el gap sea mayor que 0",
        "Para cada valor de j, compara los elementos con el gap",
        "Si el elemento j es mayor que el elemento j - gap, intercámbialos",
        "Reduce el gap (por lo general se divide por 2)",
        "Repite hasta que el gap sea 1 y realiza la última pasada de comparación",
        "El arreglo está ordenado cuando ya no se hacen intercambios"
    };

            pasosAlgoritmoInDirecta = new string[]
    {
        "Comienza con el segundo elemento del arreglo",
        "Compara el elemento actual con los elementos anteriores",
        "Si el elemento actual es menor que el anterior, intercámbialos",
        "Continúa hacia atrás hasta encontrar el lugar correcto",
        "Repite para cada elemento del arreglo",
        "El arreglo estará ordenado cuando no haya más intercambios"
    };

            pasosAlgoritmoQuickSort = new string[]
    {
        "Elige un pivote (puede ser el primero, el último o el medio)",
        "Divide el arreglo en dos partes: menor que el pivote y mayor que el pivote",
        "Recursivamente ordena ambas partes",
        "Cuando la partición es de tamaño 1, el arreglo está ordenado"
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
        "Comienza con el segundo elemento del arreglo",
        "Realiza una búsqueda binaria para encontrar la posición donde debe insertarse",
        "Desplaza los elementos mayores para hacer espacio",
        "Inserta el elemento en la posición encontrada",
        "Repite el proceso para cada elemento del arreglo"
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


            this.richTextBox = richTextBox;
            textoTimer = new Timer();
        }

        public void IniciarShell()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompleto();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoShell.Length)
                {
                    ResaltarPaso(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        public void Iniciar_Inserción_Directa()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompleto();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoInDirecta.Length)
                {
                    ResaltarPaso(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        public void IniciarQuickSort()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompleto();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoQuickSort.Length)
                {
                    ResaltarPaso(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        public void IniciarCubeta()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompleto();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                //if (pasoActual < pasosAlgoritmoCubeta.Length)
                //{
                //    ResaltarPaso(pasoActual);
                //    pasoActual++;
                //}
                //else
                //{
                //    textoTimer.Stop(); // Detener animación del texto al finalizar
                //}
            };
            textoTimer.Start();
        }

        public void IniciarRadixSort()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompleto();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                //if (pasoActual < pasosAlgoritmoRadixSort.Length)
                //{
                //    ResaltarPaso(pasoActual);
                //    pasoActual++;
                //}
                //else
                //{
                //    textoTimer.Stop(); // Detener animación del texto al finalizar
                //}
            };
            textoTimer.Start();
        }

        public void IniciarInsercionBinaria()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompleto();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoInsercionBinaria.Length)
                {
                    ResaltarPaso(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        public void IniciarHeapSort()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompleto();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmoInsercionBinaria.Length)
                {
                    ResaltarPaso(pasoActual);
                    pasoActual++;
                }
                else
                {
                    textoTimer.Stop(); // Detener animación del texto al finalizar
                }
            };
            textoTimer.Start();
        }

        private void MostrarAlgoritmoCompleto()
        {
            richTextBox.Clear();
            foreach (string paso in pasosAlgoritmo)
            {
                richTextBox.AppendText(paso + Environment.NewLine);
            }
        }

        private void ResaltarPaso(int paso)
        {
            // Remueve cualquier resaltado previo
            richTextBox.SelectAll();
            richTextBox.SelectionBackColor = Color.White;

            // Resalta el paso actual
            int inicio = ObtenerInicioDeLinea(paso);
            int longitud = pasosAlgoritmo[paso].Length;

            richTextBox.Select(inicio, longitud);
            richTextBox.SelectionBackColor = Color.LightBlue;
            richTextBox.ScrollToCaret(); // Desplaza el texto para que el paso sea visible
        }

        private int ObtenerInicioDeLinea(int linea)
        {
            int inicio = 0;
            for (int i = 0; i < linea; i++)
            {
                inicio += pasosAlgoritmo[i].Length + Environment.NewLine.Length;
            }
            return inicio;
        }

        public void AvanzarPasoManual(int paso)
        {
            // Método para avanzar manualmente el paso desde otro punto
            if (paso >= 0 && paso < pasosAlgoritmo.Length)
            {
                ResaltarPaso(paso);
            }
        }
    }
}
