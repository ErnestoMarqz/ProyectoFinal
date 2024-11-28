using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

        private void IntercambiarCuadrosAnimado(FlowLayoutPanel parent, int indiceA, int indiceB)
        {
            // Obtener los cuadros
            Panel cuadroA = parent.Controls[indiceA] as Panel;
            Panel cuadroB = parent.Controls[indiceB] as Panel;

            if (cuadroA == null || cuadroB == null) return;

            // Extraer números de los cuadros
            int numeroA = int.Parse((cuadroA.Controls[0] as Label).Text);
            int numeroB = int.Parse((cuadroB.Controls[0] as Label).Text);

            // Tamaños iniciales y finales
            Size tamañoInicialA = cuadroA.Size;
            Size tamañoFinalA = new Size(numeroB * 10, numeroB * 10); // Tamaño final de A
            Size tamañoInicialB = cuadroB.Size;
            Size tamañoFinalB = new Size(numeroA * 10, numeroA * 10); // Tamaño final de B

            // Animación gradual
            int pasos = 20; // Número de pasos para la animación
            for (int i = 0; i <= pasos; i++)
            {
                // Interpolar tamaños para cada cuadro
                cuadroA.Size = new Size(
                    Interpolar(tamañoInicialA.Width, tamañoFinalA.Width, i, pasos),
                    Interpolar(tamañoInicialA.Height, tamañoFinalA.Height, i, pasos)
                );

                cuadroB.Size = new Size(
                    Interpolar(tamañoInicialB.Width, tamañoFinalB.Width, i, pasos),
                    Interpolar(tamañoInicialB.Height, tamañoFinalB.Height, i, pasos)
                );

                // Intercambiar los números cada que se intercambia un cuadro
                (cuadroA.Controls[0] as Label).Text = numeroB.ToString();
                (cuadroB.Controls[0] as Label).Text = numeroA.ToString();

                // Redibujar visualmente
                cuadroA.Refresh();
                cuadroB.Refresh();
                Thread.Sleep(50); // Ajustar para velocidad deseada
            }

            
        }


        private int Interpolar(int inicio, int fin, int pasoActual, int totalPasos)
        {
            return inicio + (fin - inicio) * pasoActual / totalPasos;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {

            // Implementar el algoritmo de burbuja con animación
            for (int i = 0; i < flowLayoutPanel1.Controls.Count - 1; i++)
            {
                for (int j = 0; j < flowLayoutPanel1.Controls.Count - 1 - i; j++)
                {
                    // Obtener los cuadros actuales y adyacentes
                    Panel cuadroA = flowLayoutPanel1.Controls[j] as Panel;
                    Panel cuadroB = flowLayoutPanel1.Controls[j + 1] as Panel;

                    // Validar que los cuadros existen
                    if (cuadroA == null || cuadroB == null) continue;

                    // Obtener los números de los cuadros
                    int valorA = int.Parse((cuadroA.Controls[0] as Label).Text);
                    int valorB = int.Parse((cuadroB.Controls[0] as Label).Text);

                    // Comparar los valores y animar si es necesario
                    if (valorA > valorB)
                    {
                        IntercambiarCuadrosAnimado(flowLayoutPanel1, j, j + 1);
                    }
                }
            }

            //for (int i = 0; i < flowLayoutPanel1.Controls.Count - 1; i++)
            //{
            //    IntercambiarCuadrosAnimado(flowLayoutPanel1, i, i + 1);
            //}

            //// Implementar un algoritmo de ordenamiento (burbuja, por ejemplo)
            //for (int i = 0; i < flowLayoutPanel1.Controls.Count - 1; i++)
            //{
            //    for (int j = 0; j < flowLayoutPanel1.Controls.Count - 1 - i; j++)
            //    {
            //        Panel cuadroA = flowLayoutPanel1.Controls[j] as Panel;
            //        Panel cuadroB = flowLayoutPanel1.Controls[j + 1] as Panel;

            //        int valorA = int.Parse((cuadroA.Controls[0] as Label).Text);
            //        int valorB = int.Parse((cuadroB.Controls[0] as Label).Text);

            //        if (valorA > valorB)
            //        {
            //            IntercambiarCuadrosAnimado(flowLayoutPanel1, j, j + 1);
            //        }
            //    }
            //}

            //// Verificar si hay cuadros en el FlowLayoutPanel
            //if (flowLayoutPanel1.Controls.Count == 0)
            //{
            //    MessageBox.Show("Por favor, genera los cuadros antes de iniciar el ordenamiento.",
            //                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //// Determinar si el orden será ascendente o descendente
            //bool ascendente = rbAsendente.Checked;

            //// Crear una instancia de la clase Metodos
            //Metodos metodos = new Metodos();

            //// Llamar al método Burbuja para realizar el ordenamiento con animación
            //metodos.Burbuja(flowLayoutPanel1, ascendente);

            //// Mensaje de finalización
            //MessageBox.Show("Ordenamiento completado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Burbuja")
            {
                textBox2.Text = @"
                void Burbuja(int[] arreglo)
                {
                    for (int i = 0; i < arreglo.Length - 1; i++)
                    {
                        for (int j = 0; j < arreglo.Length - i - 1; j++)
                        {
                            if (arreglo[j] > arreglo[j + 1])
                            {
                                int temp = arreglo[j];
                                arreglo[j] = arreglo[j + 1];
                                arreglo[j + 1] = temp;
                            }
                        }
                    }
                }";
            }
        }
    }     
}

