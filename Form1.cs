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

        private async Task button8_ClickAsync(object sender, EventArgs e)
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

            switch(opcion) 
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
                    await ShellSortSimplificado(arreglo, flowLayoutPanel1);
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
        private async Task ShellSortSimplificado(int[] arreglo, FlowLayoutPanel panel)
        {
            int n = arreglo.Length;

            // Inicializamos el intervalo (gap) en la mitad del tamaño del arreglo
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                // Recorremos los elementos separados por el intervalo
                for (int i = gap; i < n; i++)
                {
                    int temp = arreglo[i];
                    int j;

                    // Ordenamos los elementos dentro de este intervalo
                    for (j = i; j >= gap && arreglo[j - gap] > temp; j -= gap)
                    {
                        arreglo[j] = arreglo[j - gap];
                    }

                    arreglo[j] = temp;

                    // Actualizamos la interfaz gráfica en cada iteración importante
                    ActualizarInterfaz(panel, arreglo);
                    await Task.Delay(100); // Breve pausa para visualizar el cambio
                }
            }

            MessageBox.Show("Ordenamiento por Shell completado.");
        }

        private void ActualizarInterfaz(FlowLayoutPanel panel, int[] arreglo)
        {
            // Limpiar el FlowLayoutPanel
            panel.Controls.Clear();

            // Crear y agregar cuadros al panel
            foreach (int valor in arreglo)
            {
                Panel cuadro = new Panel
                {
                    Size = new Size(valor * 10, 20), // Tamaño basado en el valor
                    BackColor = Color.FromArgb(100, valor * 5 % 255, valor * 3 % 255),
                    Margin = new Padding(5)
                };

                Label etiqueta = new Label
                {
                    Text = valor.ToString(),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White
                };

                cuadro.Controls.Add(etiqueta);
                panel.Controls.Add(cuadro);
            }
        }
    }
}

