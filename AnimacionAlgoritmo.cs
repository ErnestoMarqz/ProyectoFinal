using System;
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
        private readonly string[] pasosAlgoritmo; // Pasos detallados del algoritmo
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
            // Algoritmo de ordenamiento burbuja
            pasosAlgoritmo = new string[]
            {
            "Inicio del bucle externo (i = 0 a n-1)",
            "Inicio del bucle interno (j = 0 a n-i-1)",
            "Comparar elementos adyacentes (arreglo[j] y arreglo[j+1])",
            "Intercambiar si arreglo[j] > arreglo[j+1]",
            "Finalizar iteración interna (incrementar j)",
            "Verificar si hubo intercambios (optimización)",
            "Finalizar si el arreglo ya está ordenado"
            };

            this.richTextBox = richTextBox;
            textoTimer = new Timer();
        }

        public void Iniciar()
        {
            // Muestra todo el texto del algoritmo
            MostrarAlgoritmoCompleto();

            pasoActual = 0;
            textoTimer.Interval = 1000; // Tiempo entre pasos (1 segundo)
            textoTimer.Tick += (s, e) =>
            {
                if (pasoActual < pasosAlgoritmo.Length)
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
