using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProyectoFinal
{
    public partial class Form1 : Form
    {
        int[] arreglo;
        public Form1()
        {
            InitializeComponent();
            //poner el forms en el centro de la pantalla
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(250, 10); // Cambia a la posición deseada

        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creado por el equipo: NyE Squad");
            Application.Exit();
        }

        private void btnCrear_Click_1(object sender, EventArgs e)
        {
            // Limpiar el FlowLayoutPanel antes de agregar nuevos cuadros
            flowLayoutPanel1.Controls.Clear();

            // Obtener el número ingresado por el usuario
            int cantidad = (int)numericUpDown1.Value;
            arreglo = new int[cantidad];
            // Crear una lista de números y desordenarlos
            Random random = new Random();
            var numeros = Enumerable.Range(1, cantidad).OrderBy(x => random.Next()).ToList();
            for (int i = 0; i < cantidad; i++)
            {
                arreglo[i] = numeros[i];
            }
            // Crear y agregar los cuadros al FlowLayoutPanel
            Cuadritos creador = new Cuadritos();
            foreach (int numero in numeros)
            {
                flowLayoutPanel1.Invalidate();
                var cuadro = creador.CrearCuadroAnimadoEnLayout(flowLayoutPanel1, numero);
                flowLayoutPanel1.Controls.Add(cuadro);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (arreglo == null)
            {
                MessageBox.Show("No existe lista para organizar");
                return;
            }

            if (cbMetodo.SelectedItem == null)
            {
                MessageBox.Show("Seleciona un metodo");
                return;
            }

            string opcion = cbMetodo.SelectedItem.ToString();

            switch (opcion)
            {
                case "Burbuja":
                    MessageBox.Show("sa");
                    break;

                case "Burbuja Mejorada":
                    MessageBox.Show("sa");
                    break;

                case "Merges":
                    MessageBox.Show("sa");
                    break;

                case "Shell":

                    break;

                case "Inter. Directa":
                    MessageBox.Show("sa");
                    break;

                case "Quick Sort":
                    MessageBox.Show("sa");
                    break;

                case "Cubeta":
                    MessageBox.Show("sa");
                    break;

                case "Inter. Binaria":
                    MessageBox.Show("sa");
                    break;

                case "Radix Sort":
                    MessageBox.Show("sa");
                    break;

                case "Heap Sort":
                    MessageBox.Show("sa");
                    break;

                case "Baraja":
                    MessageBox.Show("sa");
                    break;
            }
        }
        static void ShellSort(int[] arreglo)
        {
            int n = arreglo.Length;

            // Empezamos con un gap grande, luego lo reducimos
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                // Recorremos los elementos dentro de gap
                for (int i = gap; i < n; i++)
                {
                    // Guardamos el valor actual y creamos un índice para comparar
                    int temp = arreglo[i];
                    int j;

                    // Ordenamos usando el gap
                    for (j = i; j >= gap && arreglo[j - gap] > temp; j -= gap)
                    {
                        arreglo[j] = arreglo[j - gap];
                    }

                    // Colocamos el valor temporal en su posición correcta
                    arreglo[j] = temp;
                }
            }
        }
        private void HighlightLine(int lineIndex)
        {
            int startIndex = textBox2.GetFirstCharIndexFromLine(lineIndex);
            int length = textBox2.Lines[lineIndex].Length;

            // Resaltamos la línea actual (esto cambiará el color o el fondo)
            textBox2.SelectionStart = startIndex;
            textBox2.SelectionLength = length;

            // Después de un pequeño retraso, revertimos el resaltado de la línea
            var revertTimer = Task.Delay(300);  // Duración del resaltado
            revertTimer.ContinueWith(_ =>
            {
                // Revertimos el color de fondo
                textBox2.Invoke((MethodInvoker)(() =>
                {
                    textBox2 = textBox2; // Revertimos el color de fondo
                }));
            });
        }
        //private void btnIniciar_Click(object sender, EventArgs e)
        //{
        //    if (flowLayoutPanel1.Controls.Count == 0)
        //    {
        //        MessageBox.Show("Por favor, crea los cuadros antes de iniciar el ordenamiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // Preparar el algoritmo de Burbuja
        //    if (comboBox1.SelectedItem.ToString() == "Burbuja")
        //    {
        //        // Determinar si es ascendente o descendente
        //        bool ascendente = rbAsendente.Checked;

        //        // Llamar al método para iniciar las animaciones en paralelo
        //        AnimateCodeAndSort(ascendente);
        //    }
        //}

        //private void AnimateCodeAndSort(bool ascendente)
        //{
        //    int currentLineIndex = 0;  // Índice de la línea actual del código
        //    int totalLines = richTextBox2.Lines.Length;

        //    // Empezamos el proceso de ordenamiento
        //    for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
        //    {
        //        // Realizar un paso del algoritmo de burbuja para ordenar los cuadros
        //        // Este método debe intercambiar los cuadros de acuerdo al algoritmo
        //        PerformBubbleSortStep(i, ascendente);

        //        // Resaltar la línea actual en el RichTextBox
        //        if (currentLineIndex < totalLines)
        //        {
        //            HighlightLine(currentLineIndex);
        //            currentLineIndex++;
        //        }

        //        // Permitir que la interfaz de usuario se actualice
        //        Application.DoEvents();

        //        // Retrasar un poco para que se vea el cambio
        //        System.Threading.Thread.Sleep(300);  // 300ms de retraso para visualizar las animaciones
        //    }
        //}

        //private void PerformBubbleSortStep(int index, bool ascendente)
        //{
        //    // Aquí debes implementar la lógica de un paso del algoritmo de burbuja para mover los cuadros en el FlowLayoutPanel
        //    // Ejemplo: intercambiar dos cuadros (esto es un pseudocódigo, debes ajustarlo según tu implementación)
        //    string codigo = @"
        //            void Burbuja(int[] arreglo)
        //            {
        //                for (int i = 0; i < arreglo.Length - 1; i++)
        //                {
        //                    for (int j = 0; j < arreglo.Length - i - 1; j++)
        //                    {
        //                        if (arreglo[j] > arreglo[j + 1])
        //                        {
        //                            int temp = arreglo[j];
        //                            arreglo[j] = arreglo[j + 1];
        //                            arreglo[j + 1] = temp;
        //                       }
        //                    }
        //                }
        //            }";
        //    richTextBox2.Text = codigo;


        //    // Asegúrate de que no estamos fuera de rango al acceder a cuadro2
        //    if (index + 1 >= flowLayoutPanel1.Controls.Count)
        //    {
        //        return; // No hacer nada si estamos en el último cuadro
        //    }

        //    // Obtén los cuadros que se van a comparar
        //    Control cuadro1 = flowLayoutPanel1.Controls[index];
        //    Control cuadro2 = flowLayoutPanel1.Controls[index + 1];

        //    // Obtener el texto de los cuadros y parsearlos de forma segura
        //    int valor1, valor2;

        //    if (!int.TryParse(cuadro1.Controls[0].Text, out valor1) || !int.TryParse(cuadro2.Controls[0].Text, out valor2))
        //    {
        //        // Si la conversión falla, salir del método
        //        MessageBox.Show("Error: Uno de los cuadros no contiene un número válido.");
        //        return;
        //    }

        //    // Si el orden no es correcto, intercambiamos los cuadros
        //    if ((ascendente && valor1 > valor2) || (!ascendente && valor1 < valor2))
        //    {
        //        // Intercambiar las posiciones de los cuadros visualmente
        //        flowLayoutPanel1.Controls.SetChildIndex(cuadro1, index + 1);
        //        flowLayoutPanel1.Controls.SetChildIndex(cuadro2, index);
        //    }
        //}

        //private void HighlightLine(int lineIndex)
        //{
        //    int startIndex = richTextBox2.GetFirstCharIndexFromLine(lineIndex);
        //    int length = richTextBox2.Lines[lineIndex].Length;

        //    // Resaltamos la línea actual (esto cambiará el color o el fondo)
        //    richTextBox2.SelectionStart = startIndex;
        //    richTextBox2.SelectionLength = length;
        //    richTextBox2.SelectionBackColor = Color.LightBlue;  // Cambiar el color de fondo para simular "sombreado azul"
        //    richTextBox2.SelectionColor = Color.Black;  // Cambiar el color del texto si es necesario

        //    // Después de un pequeño retraso, revertimos el resaltado de la línea
        //    var revertTimer = Task.Delay(300);  // Duración del resaltado
        //    revertTimer.ContinueWith(_ =>
        //    {
        //        // Revertimos el color de fondo
        //        richTextBox2.Invoke((MethodInvoker)(() =>
        //        {
        //            richTextBox2.SelectionBackColor = richTextBox2.BackColor; // Revertimos el color de fondo
        //        }));
        //    });
        //}
    }
}

