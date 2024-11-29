using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            textBox2.SelectionBackColor = Color.LightBlue;  // Cambiar el color de fondo para simular "sombreado azul"
            textBox2.SelectionColor = Color.Black;  // Cambiar el color del texto si es necesario

            // Después de un pequeño retraso, revertimos el resaltado de la línea
            var revertTimer = Task.Delay(300);  // Duración del resaltado
            revertTimer.ContinueWith(_ =>
            {
                // Revertimos el color de fondo
                textBox2.Invoke((MethodInvoker)(() =>
                {
                    textBox2.SelectionBackColor = textBox2.BackColor; // Revertimos el color de fondo
                }));
            });
        }
    }
}

