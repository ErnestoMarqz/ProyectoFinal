using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private AnimacionAlgoritmo animacionTexto;
        public Form1()
        {
            InitializeComponent();
            //poner el forms en el centro de la pantalla
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(250, 10); // Cambia a la posición deseada
            animacionTexto = new AnimacionAlgoritmo(richTextBox2);
            // Configurar valores mínimo y máximo para el NumericUpDown
            numericUpDown1.Minimum = 1; // Valor mínimo
            numericUpDown1.Maximum = 20; // Valor máximo
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value < numericUpDown1.Minimum)
            {
                numericUpDown1.Value = numericUpDown1.Minimum;
            }
            else if (numericUpDown1.Value > numericUpDown1.Maximum)
            {
                numericUpDown1.Value = numericUpDown1.Maximum;
            }
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

        private async void btnIniciar_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("Por favor, crea los cuadros antes de iniciar el ordenamiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.SelectedItem.ToString() == "Burbuja")
            {
                bool ascendente = rbAsendente.Checked; // Verificar si es orden ascendente o descendente
                await animacionTexto.OrdenarBurbujaEstándarConAlgoritmoAnimado(flowLayoutPanel1, richTextBox2, ascendente);
            }

            if (comboBox1.SelectedItem.ToString() == "Burbuja Mejorado")
            {
                // Determinar si es ascendente o descendente
                bool ascendente = rbAsendente.Checked;

                // Llamar al método de ordenamiento optimizado
                await animacionTexto.OrdenarBurbujaMejoradoConAlgoritmoAnimado(flowLayoutPanel1, richTextBox2, ascendente);
                
            }

            if (comboBox1.SelectedItem.ToString() == "Merges")
            {
                bool ascendente = rbAsendente.Checked;

                // Llamar al método de MergeSortAnimado
                Metodos metodos = new Metodos();
                await metodos.MergeSortConAnimacion(flowLayoutPanel1, ascendente, richTextBox2);
            }

            if (comboBox1.SelectedItem.ToString() == "Baraja")
            {
                bool ascendente = true;
                if (rbAsendente.Checked == true)
                {
                    ascendente = true;
                    metodos.Baraja(arreglo, flowLayoutPanel1, ascendente);
                    animacionTexto.BarajaAsc();
                    await Task.Delay(300);

                }
                else
                {
                    ascendente = false;
                    metodos.Baraja(arreglo, flowLayoutPanel1, ascendente);
                    animacionTexto.BarajaDes();
                    await Task.Delay(300);
                }
            }
            if (comboBox1.SelectedItem.ToString() == "Shell")
            {
                if (rbAsendente.Checked == true)
                {
                    metodos.ShellAsendente(arreglo, flowLayoutPanel1);
                    animacionTexto.IniciarShell();
                    await Task.Delay(300);
                }
                if (rbDesendente.Checked == true)
                {
                    metodos.ShellDescendente(arreglo, flowLayoutPanel1);
                    animacionTexto.IniciarShell();
                    await Task.Delay(300);
                }
            }
            if (comboBox1.SelectedItem.ToString() == "Inter. Directa")
            {
                if (rbAsendente.Checked == true)
                {
                    metodos.InsertionDirecta(arreglo, flowLayoutPanel1);
                    animacionTexto.Iniciar_Inserción_Directa();
                    await Task.Delay(300);
                }
                if (rbDesendente.Checked == true)
                {
                    metodos.InsertionDescendente(arreglo, flowLayoutPanel1);
                    animacionTexto.Iniciar_Inserción_Directa();
                    await Task.Delay(300);
                }
            }
            if (comboBox1.SelectedItem.ToString() == "Quick Sort")
            {
                if (rbAsendente.Checked == true)
                {
                    metodos.QuicksortAscendente(arreglo, flowLayoutPanel1);
                    animacionTexto.IniciarQuickSort();
                    await Task.Delay(300);
                }
                if (rbDesendente.Checked == true)
                {
                    metodos.QuicksortDescendente(arreglo, flowLayoutPanel1);
                    animacionTexto.IniciarQuickSort();
                    await Task.Delay(300);
                }
            }
            if (comboBox1.SelectedItem.ToString() == "Heap Sort")
            {

                bool ascendente = true;
                if (rbAsendente.Checked == true)
                {
                    ascendente = true;
                    metodos.HeapSort(arreglo, flowLayoutPanel1, ascendente);
                    animacionTexto.IniciarHeapSortAsc();
                    await Task.Delay(300);
                }
                else
                {
                    ascendente = false;
                    metodos.HeapSort(arreglo, flowLayoutPanel1, ascendente);
                    animacionTexto.IniciarHeapSortDes();
                    await Task.Delay(300);
                }
            }

            if (comboBox1.SelectedItem.ToString() == "Inter. Binaria") //ya funciona
            {
                // Determinar si es ascendente o descendente
                bool ascendente = rbAsendente.Checked;

                Metodos metodos = new Metodos(); // Tu clase que contiene los algoritmos
                await metodos.OrdenarInsercionBinariaConAnimacion(flowLayoutPanel1, arreglo, ascendente);
                animacionTexto.IniciarInsercionBinaria();
                await Task.Delay(300);

            }

            if (comboBox1.SelectedItem.ToString() == "Cubeta")
            {
                if (rbAsendente.Checked == true)
                {
                    //metodos.BucketSortAscendente(arreglo,flowLayoutPanel1,richTextBox1);
                    metodos.BucketSortAscendente(arreglo, flowLayoutPanel1, (int)numericUpDown3.Value, (int)numericUpDown4.Value);
                    richTextBox1.Visible = false;
                    animacionTexto.IniciarCubetaAsc();
                    await Task.Delay(300);
                }
                if (rbDesendente.Checked == true)
                {
                    metodos.BucketSortDescendente(arreglo, flowLayoutPanel1, (int)numericUpDown3.Value, (int)numericUpDown4.Value);
                    //metodos.BucketSortDescendente2(arreglo, flowLayoutPanel1, (int)numericUpDown3.Value,(int)numericUpDown4.Value);
                    richTextBox1.Visible = false;
                    animacionTexto.IniciarCubetasDes();
                    await Task.Delay(300);
                }
            }
            if (comboBox1.SelectedItem.ToString() == "Radix Sort")
            {
                if (rbAsendente.Checked == true)
                {
                    metodos.RadixSort(arreglo, flowLayoutPanel1, richTextBox1);
                    //richTextBox1.Visible = false;
                    //richTextBox1.Text = string.Empty;
                    animacionTexto.IniciarRadixSortAsc();
                    await Task.Delay(300);
                }
                if (rbDesendente.Checked == true)
                {
                    metodos.RadixSortDescendente(arreglo, flowLayoutPanel1, richTextBox1);
                    //richTextBox1.Visible = false;
                    //richTextBox1.Text = string.Empty;
                    animacionTexto.IniciarRadixSortDes();
                    await Task.Delay(300);
                }

            }
        }

        private int currentLineIndex = 0;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (comboBox1.SelectedItem.ToString() == "Burbuja")
            //{
            //    animacionTexto.Iniciar();
            //    await Task.Delay(300);
            //}
            //    animacionTexto.Iniciar();
            //await Task.Delay(300);

            if (comboBox1.SelectedItem.ToString() == "Cubeta")
            {
                groupBox4.Visible = true;
                LabelTam.Visible = true;
                numericUpDown3.Visible = true;
                Numcub.Visible = true;
                numericUpDown4.Visible = true;
            }
            if (comboBox1.SelectedItem.ToString() != "Cubeta")
            {
                groupBox4.Visible = false;
                LabelTam.Visible = false;
                numericUpDown3.Visible = false;
                Numcub.Visible = false;
                numericUpDown4.Visible = false;
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

        private async void button4_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("Primero genera una lista de números.", "Advertencia");
                return;
            }

            // Obtener el número a buscar
            if (int.TryParse(numericUpDown2.Text, out int numeroBuscar))
            {
                // Llamar al método de búsqueda
                int indice = await BuscarNumeroConAnimacion(flowLayoutPanel1, numeroBuscar);
                animacionTexto.BusqBin();
                await Task.Delay(300);

                // Si no se encontró el número
                if (indice == -1)
                {
                    MessageBox.Show($"El número {numeroBuscar} no fue encontrado.", "Resultado");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un número válido para buscar.", "Error");
            }
        }
        private async Task<int> BuscarNumeroConAnimacion(FlowLayoutPanel parent, int valorBuscado)
        {
            int totalElementos = parent.Controls.Count;

            if (totalElementos == 0)
            {
                MessageBox.Show("El panel está vacío. No hay elementos para buscar.", "Advertencia");
                return -1;
            }

            // Calcular el índice del centro
            int centro = totalElementos / 2;

            // Variables para moverse hacia los extremos
            int izquierda = centro - 1;
            int derecha = centro + 1;

            // Revisar el elemento central primero
            if (await RevisarIndiceConAnimacion(parent, centro, valorBuscado))
            {
                return centro;
            }

            // Recorrer izquierda y derecha
            while (izquierda >= 0 || derecha < totalElementos)
            {
                // Revisar a la izquierda
                if (izquierda >= 0 && await RevisarIndiceConAnimacion(parent, izquierda, valorBuscado))
                {
                    return izquierda;
                }

                // Revisar a la derecha
                if (derecha < totalElementos && await RevisarIndiceConAnimacion(parent, derecha, valorBuscado))
                {
                    return derecha;
                }

                // Avanzar hacia los extremos
                izquierda--;
                derecha++;
            }

            // Si no se encuentra el número
            MessageBox.Show($"El número {valorBuscado} no fue encontrado.", "Resultado");
            return -1;
        }

        // Método auxiliar para revisar un índice y realizar animación
        private async Task<bool> RevisarIndiceConAnimacion(FlowLayoutPanel parent, int indice, int valorBuscado)
        {
            if (indice < 0 || indice >= parent.Controls.Count) return false;

            // Obtener el cuadro en el índice actual
            Panel cuadro = parent.Controls[indice] as Panel;
            if (cuadro == null) return false;

            // Obtener el valor visual del cuadro
            int valorActual = int.Parse((cuadro.Controls[0] as Label).Text);

            // Resaltar el cuadro actual
            cuadro.BackColor = Color.Yellow;
            cuadro.Refresh();
            await Task.Delay(500);

            if (valorActual == valorBuscado)
            {
                // Resaltar en verde si coincide
                cuadro.BackColor = Color.Green;
                cuadro.Refresh();
                await Task.Delay(500);

                MessageBox.Show($"Número {valorBuscado} encontrado en el índice {indice}.", "Resultado");
                return true;
            }
            else
            {
                // Resaltar en rojo si no coincide
                cuadro.BackColor = Color.Red;
                cuadro.Refresh();
                await Task.Delay(500);

                cuadro.BackColor = Color.Black; // Restaurar el color original
                cuadro.Refresh();
                return false;
            }
        }
    }     
}

