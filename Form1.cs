﻿using System;
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
        Cuadritos creador = new Cuadritos();
        Metodos metodos = new Metodos();
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
                metodos.OrdenarBurbujaConAnimacion(flowLayoutPanel1, ascendente);
            }

            if (comboBox1.SelectedItem.ToString() == "Burbuja Mejorado")
            {
                // Determinar si es ascendente o descendente
                bool ascendente = rbAsendente.Checked;

                // Llamar al método de ordenamiento optimizado
                metodos.OrdenarBurbujaConAnimacionMejorado(flowLayoutPanel1, ascendente);
            }

        }

        private int currentLineIndex = 0;

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Burbuja")
            {
                // Reseteamos el índice de la línea y preparamos el texto
                currentLineIndex = 0;
                richTextBox2.Clear();  // Cambiar a RichTextBox

                // Código del algoritmo de Burbuja
                string codigo = @"

            public void OrdenarBurbujaConAnimacion(FlowLayoutPanel parent, bool ascendente)
            {
                for (int i = 0; i < parent.Controls.Count - 1; i++)
                {
                    for (int j = 0; j < parent.Controls.Count - 1 - i; j++)
                    {
                        // Obtener los cuadros actuales y adyacentes
                        Panel cuadroA = parent.Controls[j] as Panel;
                        Panel cuadroB = parent.Controls[j + 1] as Panel;

                        // Validar que los cuadros existen
                        if (cuadroA == null || cuadroB == null) continue;

                        // Obtener los números de los cuadros
                        int valorA = int.Parse((cuadroA.Controls[0] as Label).Text);
                        int valorB = int.Parse((cuadroB.Controls[0] as Label).Text);

                        // Determinar si intercambiar basado en el orden ascendente/descendente
                        bool intercambiar = ascendente ? valorA > valorB : valorA < valorB;
                        if (intercambiar)
                        {
                            Cuadritos.IntercambiarCuadrosAnimado(parent, j, j + 1);
                        }
                    }
                }
            }
            ";
            richTextBox2.Text = codigo;

                richTextBox2.Text = codigo;  // Usamos RichTextBox en lugar de TextBox

                // Llamamos al método para animar el código
                await AnimateCodeAsync();  // Ejecutamos la animación de forma asíncrona
            }

            if (comboBox1.SelectedItem.ToString() == "Burbuja Mejorado")
            {
                // Reseteamos el índice de la línea y preparamos el texto
                currentLineIndex = 0;
                richTextBox2.Clear();  // Cambiar a RichTextBox

                // Código del algoritmo de Burbuja
                string codigo = @"

                    public void OrdenarBurbujaConAnimacionMejorado(FlowLayoutPanel parent, bool ascendente)
                {
                    for (int i = 0; i < parent.Controls.Count - 1; i++)
                    {
                        bool intercambio = false;
                        for (int j = 0; j < parent.Controls.Count - 1 - i; j++)
                        {
                            // Obtener los cuadros actuales y adyacentes
                            Panel cuadroA = parent.Controls[j] as Panel;
                            Panel cuadroB = parent.Controls[j + 1] as Panel;

                            // Validar que los cuadros existen
                            if (cuadroA == null || cuadroB == null) continue;

                            // Obtener los números de los cuadros
                            int valorA = int.Parse((cuadroA.Controls[0] as Label).Text);
                            int valorB = int.Parse((cuadroB.Controls[0] as Label).Text);

                            // Determinar si intercambiar basado en el orden ascendente/descendente
                            bool intercambiar = ascendente ? valorA > valorB : valorA < valorB;
                            if (intercambiar)
                            {
                                Cuadritos.IntercambiarCuadrosAnimado(parent, j, j + 1);
                                intercambio = true;
                            }
                        }

                        // Si no hubo intercambios, el arreglo ya está ordenado
                        if (!intercambio) break;
                    }
                }
                ";
                richTextBox2.Text = codigo;

                richTextBox2.Text = codigo;  // Usamos RichTextBox en lugar de TextBox

                // Llamamos al método para animar el código
                await AnimateCodeAsync();  // Ejecutamos la animación de forma asíncrona
            }
        }

        // Método asíncrono para animar las líneas de código
        private async Task AnimateCodeAsync()
        {
            for (currentLineIndex = 0; currentLineIndex < richTextBox2.Lines.Length; currentLineIndex++)
            {
                // Resaltamos la línea actual
                HighlightLine(currentLineIndex);

                // Esperamos 500 ms antes de resaltar la siguiente línea
                await Task.Delay(500);  // Retraso de 500ms
            }
        }

        // Método para resaltar la línea actual en el RichTextBox
        private void HighlightLine(int lineIndex)
        {
            int startIndex = richTextBox2.GetFirstCharIndexFromLine(lineIndex);
            int length = richTextBox2.Lines[lineIndex].Length;

            // Aseguramos que la actualización del RichTextBox ocurra en el hilo principal
            richTextBox2.Invoke((MethodInvoker)(() =>
            {
                // Resaltamos la línea actual (esto cambiará el color o el fondo)
                richTextBox2.SelectionStart = startIndex;
                richTextBox2.SelectionLength = length;
                richTextBox2.SelectionBackColor = Color.LightBlue;  // Cambiar el color de fondo para simular "sombreado azul"
                richTextBox2.SelectionColor = Color.Black;  // Cambiar el color del texto si es necesario
            }));

            // Después de un pequeño retraso, revertimos el resaltado de la línea
            var revertTimer = Task.Delay(500);  // Duración del resaltado
            revertTimer.ContinueWith(_ =>
            {
                // Revertimos el color de fondo en el hilo principal
                richTextBox2.Invoke((MethodInvoker)(() =>
                {
                    richTextBox2.SelectionBackColor = richTextBox2.BackColor; // Revertimos el color de fondo
                }));
            });
        }
    }     
}

