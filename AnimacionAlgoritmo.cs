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

        public AnimacionAlgoritmo(RichTextBox richTextBox)
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
