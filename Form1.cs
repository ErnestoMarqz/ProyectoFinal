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
        metodos metodos = new metodos();
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

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creado por el equipo: NyE Squad");
            Application.Exit();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("Por favor, crea los cuadros antes de iniciar el ordenamiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.SelectedItem.ToString() == "Burbuja")
            {
                // Determinar si es ascendente o descendente
                bool ascendente = rbAsendente.Checked;

                // Llamar al método de ordenamiento
               // metodos.OrdenarBurbujaConAnimacion(flowLayoutPanel1, ascendente);
            }
            if(comboBox1.SelectedItem.ToString() == "Baraja")
            {
                if(rbAsendente.Checked == true)
                {
                    metodos.BarajaAcendnete(arreglo, flowLayoutPanel1);
                }
                else
                {
                    metodos.BarajaDescendente(arreglo, flowLayoutPanel1);
                }
            }
            if (comboBox1.SelectedItem.ToString() == "Shell")
            {
                if (rbAsendente.Checked == true)
                {
                    metodos.ShellAsendente(arreglo, flowLayoutPanel1);
                }
                if (rbDesendente.Checked == true)
                {
                    metodos.ShellDescendente(arreglo, flowLayoutPanel1);
                }
            }
            if (comboBox1.SelectedItem.ToString() == "Inter. Directa") 
            {
                if (rbAsendente.Checked == true)
                {
                    metodos.InsertionDirecta(arreglo, flowLayoutPanel1);
                }
                if (rbDesendente.Checked == true)
                {
                    metodos.InsertionDescendente(arreglo, flowLayoutPanel1);
                }
            }
            if (comboBox1.SelectedItem.ToString() == "Quick Sort") 
            {
                if (rbAsendente.Checked == true) 
                {
                    metodos.QuicksortAscendente(arreglo, flowLayoutPanel1);
                }
                if (rbDesendente.Checked == true)
                {
                    metodos.QuicksortDescendente(arreglo, flowLayoutPanel1);
                }
            }
            if (comboBox1.SelectedItem.ToString() == "Heap Sort")
            {
                if (rbAsendente.Checked == true)
                {
                    metodos.HeapSortAcendente(arreglo, flowLayoutPanel1);
                }
                if (rbDesendente.Checked == true)
                {
                }
            }
        }
    }
}
