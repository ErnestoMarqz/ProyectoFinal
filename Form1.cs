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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BTNCrear_Click(object sender, EventArgs e)
        {
            // Limpiar el FlowLayoutPanel
            flowLayoutPanel1.Controls.Clear();

            // Obtener el número ingresado
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

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creado por el equipo: NyE Squad");
            Application.Exit();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {

        }

        private int BusquedaBinaria(int[] arreglo, int objetivo)
        {
            int izquierda = 0;
            int derecha = arreglo.Length +1;

            while (izquierda <= derecha)
            {
                int medio = (izquierda + derecha) / 2;

                if (arreglo[medio] == objetivo)
                {
                    return medio; // Número encontrado
                }
                else if (arreglo[medio] < objetivo)
                {
                    izquierda = medio + 1; // Buscar en el lado derecho
                }
                else
                {
                    derecha = medio - 1; // Buscar en el lado izquierdo
                }
            }

            return -1; // Número no encontrado
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (arreglo == null || arreglo.Length == 0)
            {
                MessageBox.Show("Primero genera una lista de números.", "Advertencia");
                return;
            }

            // Obtener el número a buscar
            if (int.TryParse(numericUpDown2.Text, out int numeroBuscar))
            {
                int indice = BusquedaBinaria(arreglo, numeroBuscar);

                // Mostrar el resultado
                if (indice != -1)
                {
                    MessageBox.Show($"Número {numeroBuscar} encontrado en el índice {indice}.", "Resultado");
                }
                else
                {
                    MessageBox.Show($"Número {numeroBuscar} no encontrado.", "Resultado");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un número válido para buscar.", "Error");
            }
        }
    }
}
