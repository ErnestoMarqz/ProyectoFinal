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
                    MessageBox.Show("sa");
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
    }
}

