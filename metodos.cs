using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    internal class metodos
    {

        Cuadritos cuadrito = new Cuadritos();

        public void BucketSortDescendente(int[] arr, FlowLayoutPanel panel, int numBuckets, int bucketSize)
        {
            if (bucketSize <= 0 || numBuckets <= 0)
            {
                MessageBox.Show("El número de cubetas y el tamaño debe ser diferente a 0");
                return;
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Crear los cubos visuales
            List<List<int>> buckets = new List<List<int>>(numBuckets);
            List<FlowLayoutPanel> cubosVisuales = new List<FlowLayoutPanel>();
            FlowLayoutPanel cubosPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoSize = true,
                BackColor = Color.LightGray,
                Margin = new Padding(10),
                Dock = DockStyle.Top
            };

            // Asegurarse de que el panel de cubos se añada al contenedor correcto
            panel.Parent.Controls.Add(cubosPanel);

            for (int i = 0; i < numBuckets; i++)
            {
                buckets.Add(new List<int>());
                FlowLayoutPanel cuboVisual = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.TopDown,
                    AutoSize = true,
                    BackColor = Color.AliceBlue,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(5)
                };
                cubosPanel.Controls.Add(cuboVisual);
                cubosVisuales.Add(cuboVisual);
            }

            // Distribuir los elementos en los cubos
            foreach (int num in arr)
            {
                int bucketIndex = num / bucketSize; // Ajustar tamaño del cubo

                // Validar que bucketIndex esté dentro del rango
                if (bucketIndex < 0 || bucketIndex >= numBuckets)
                {
                    MessageBox.Show("Asegúrese de que el tamaño y número de cubetas corresponda con la cantidad de elementos", "Error de índice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }

                buckets[bucketIndex].Add(num);

                // Mover visualmente el cuadro al cubo correspondiente
                Panel cuadro = cuadros.First(c => c.Controls[0].Text == num.ToString());
                cuadros.Remove(cuadro);
                AnimarMovimiento(cuadro, cubosVisuales[bucketIndex]);
            }

            // Ordenar las cubetas por el valor máximo de sus elementos en orden descendente
            var bucketsWithVisuals = buckets
                .Select((bucket, i) => new { Bucket = bucket, Visual = cubosVisuales[i] })
                .Where(b => b.Bucket.Any()) // Filtrar cubetas vacías
                .OrderByDescending(b => b.Bucket.Max())
                .ToList();

            // Ordenar cada cubo y combinar
            int index = 0;
            foreach (var item in bucketsWithVisuals)
            {
                List<int> bucket = item.Bucket;
                FlowLayoutPanel cuboVisual = item.Visual;

                // Ordenar el cubo en orden descendente
                bucket.Sort((a, b) => b.CompareTo(a));

                foreach (int num in bucket)
                {
                    Panel cuadro = cuboVisual.Controls.Cast<Panel>().First(c => c.Controls[0].Text == num.ToString());
                    cuadros.Add(cuadro);

                    // Combinar visualmente los elementos en el panel original
                    AnimarMovimiento(cuadro, panel);
                    arr[index++] = num;
                }
            }

            panel.Parent.Controls.Remove(cubosPanel);
        }


        public int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public void BucketSortAscendente(int[] arr, FlowLayoutPanel panel,int numBuckets, int bucketSize)
        {            
            if (bucketSize <= 0 || numBuckets <= 0)
            {
                MessageBox.Show("El número de cubetas y el tamaño debe ser diferente a 0");
            }
            else
            {
                List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

                // Crear los cubos visuales
                List<List<int>> buckets = new List<List<int>>(numBuckets);
                List<FlowLayoutPanel> cubosVisuales = new List<FlowLayoutPanel>();
                FlowLayoutPanel cubosPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = true,
                    AutoSize = true,
                    BackColor = Color.LightGray,
                    Margin = new Padding(10),
                    Dock = DockStyle.Top
                };

                // Asegurarse de que el panel de cubos se añada al contenedor correcto
                panel.Parent.Controls.Add(cubosPanel);

                for (int i = 0; i < numBuckets; i++)
                {
                    buckets.Add(new List<int>());
                    FlowLayoutPanel cuboVisual = new FlowLayoutPanel
                    {
                        FlowDirection = FlowDirection.TopDown,
                        AutoSize = true,
                        BackColor = Color.AliceBlue,
                        Margin = new Padding(5),
                        BorderStyle = BorderStyle.FixedSingle,
                        Padding = new Padding(5)
                    };
                    cubosPanel.Controls.Add(cuboVisual);
                    cubosVisuales.Add(cuboVisual);
                }

                // Distribuir los elementos en los cubos
                foreach (int num in arr)
                {
                    int bucketIndex = num / bucketSize; // Ajustar tamaño del cubo

                    // Validar que bucketIndex esté dentro del rango
                    if (bucketIndex < 0 || bucketIndex >= numBuckets)
                    {
                        MessageBox.Show($"Asegurese de que el tamaño y numero de cubetas corresponda con la cantidad de elementos", "Error de índice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }

                    buckets[bucketIndex].Add(num);

                    // Mover visualmente el cuadro al cubo correspondiente
                    Panel cuadro = cuadros.First(c => c.Controls[0].Text == num.ToString());
                    cuadros.Remove(cuadro);
                    AnimarMovimiento(cuadro, cubosVisuales[bucketIndex]);
                }  

                // Ordenar cada cubo y combinar
                int index = 0;
                foreach (List<int> bucket in buckets)
                {
                    // Ordenar el cubo en orden ascendente
                    bucket.Sort();
                    FlowLayoutPanel cuboVisual = cubosVisuales[buckets.IndexOf(bucket)];

                    foreach (int num in bucket)
                    {
                        Panel cuadro = cuboVisual.Controls.Cast<Panel>().First(c => c.Controls[0].Text == num.ToString());
                        cuadros.Add(cuadro);

                        // Combinar visualmente los elementos en el panel original
                        AnimarMovimiento(cuadro, panel);
                        arr[index++] = num;
                    }
                }
                panel.Parent.Controls.Remove(cubosPanel);
            }

        }



        private void AnimarMovimiento(Panel cuadro, FlowLayoutPanel destino)
        {
            // Coordenadas iniciales y finales dentro del destino
            Point startLocation = cuadro.Location;
            Point endLocation = destino.PointToClient(cuadro.Parent.PointToScreen(cuadro.Location));

            endLocation = new Point(
                Clamp(endLocation.X, 0, destino.ClientSize.Width - cuadro.Width),
                Clamp(endLocation.Y, 0, destino.ClientSize.Height - cuadro.Height)
            );

            int deltaX = (endLocation.X - startLocation.X) / 10;
            int deltaY = (endLocation.Y - startLocation.Y) / 10;

            // Animar movimiento
            for (int i = 0; i < 10; i++)
            {
                cuadro.Location = new Point(
                    Clamp(cuadro.Location.X + deltaX, 0, destino.ClientSize.Width - cuadro.Width),
                    Clamp(cuadro.Location.Y + deltaY, 0, destino.ClientSize.Height - cuadro.Height)
                );

                cuadro.Refresh();
                Application.DoEvents();
                Thread.Sleep(50);
            }

            // Asegurarse de agregar el cuadro al destino al final
            destino.Controls.Add(cuadro);
            destino.Refresh();

        }
        public async void RadixSort(int[] A, FlowLayoutPanel panel, RichTextBox richTextBoxDigits)
        {
            int N = A.Length;
            richTextBoxDigits.Visible = true;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Encontrar el valor máximo
            int max = A.Max();

            // Llamar a CountingSort para cada dígito
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                await CountingSortWithAnimation(A, exp, cuadros, richTextBoxDigits, panel);
            }

            // Finalizar la visualización
            for (int i = 0; i < N; i++)
            {
                cuadros[i].BackColor = Color.Green; // Cambiar color final
                AnimarCambioDeTamaño(cuadros[i], A[i]); // Ajustar al tamaño final
            }
            panel.Refresh();
        }

        private async Task CountingSortWithAnimation(int[] arr, int exp, List<Panel> cuadros, RichTextBox richTextBoxDigits, FlowLayoutPanel panel)
        {
            int n = arr.Length;
            int[] output = new int[n];
            int[] count = new int[10];

            // Inicializar el arreglo de conteo
            for (int i = 0; i < count.Length; i++)
                count[i] = 0;

            // Contar ocurrencias de cada dígito
            for (int i = 0; i < n; i++)
            {
                int digit = GetDigit(arr[i], exp);
                count[digit]++;
            }

            // Cambiar count[i] para que contenga la posición real de este dígito en output[]
            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            // Construir el arreglo de salida
            for (int i = n - 1; i >= 0; i--)
            {
                int digit = GetDigit(arr[i], exp);
                output[count[digit] - 1] = arr[i];
                count[digit]--;

                // Actualizar visualización
                int index = count[digit];
                cuadros[index].BackColor = Color.Orange; // Resaltar el cuadro que se está procesando
                cuadros[index].Controls[0].Text = arr[i].ToString();
                panel.Refresh();
                await Task.Delay(500); // Pausa para que se vea el cambio de color

                // Animar el cambio de tamaño
                AnimarCambioDeTamaño(cuadros[index], arr[i]);

                // Actualizar el RichTextBox con el estado actual
                UpdateDigitsDisplay(arr, exp, richTextBoxDigits);
            }

            // Copiar el arreglo de salida a arr[]
            for (int i = 0; i < n; i++)
            {
                arr[i] = output[i];

                // Actualizar el panel correspondiente
                cuadros[i].Controls[0].Text = arr[i].ToString();
                cuadros[i].BackColor = Color.Red; // Resaltar el cuadro que ha sido colocado
                panel.Refresh();
                await Task.Delay(500); // Pausa para que se vea el cambio de color

                // Animar el cambio de tamaño al valor final
                AnimarCambioDeTamaño(cuadros[i], arr[i]);
            }
        }

        // Método para obtener el dígito en la posición 'exp'
        private int GetDigit(int number, int exp)
        {
            return (number / exp) % 10;
        }

        // Método para actualizar el RichTextBox con el estado actual de los dígitos
        private void UpdateDigitsDisplay(int[] arr, int exp, RichTextBox richTextBox)
        {
            richTextBox.Clear(); // Limpiar el RichTextBox antes de actualizar
            for (int i = 0; i < arr.Length; i++)
            {
                int digit = GetDigit(arr[i], exp);
                richTextBox.Width  = 260;
                richTextBox.AppendText($"Número: {arr[i]}, Dígito en posición {exp}: {digit}\n");
            }
            richTextBox.Refresh(); // Forzar actualización visual
        }

        // Método para animar el cambio de tamaño
        public void AnimarCambioDeTamaño(Panel cuadro, int numero)
        {
            Size tamañoFinal = new Size(numero * 10, numero * 10);
            int incremento = 4;

            while (cuadro.Width != tamañoFinal.Width || cuadro.Height != tamañoFinal.Height)
            {
                // Calcular el próximo tamaño
                int nuevoAncho = cuadro.Width < tamañoFinal.Width
                    ? Math.Min(cuadro.Width + incremento, tamañoFinal.Width)
                    : Math.Max(cuadro.Width - incremento, tamañoFinal.Width);

                int nuevoAlto = cuadro.Height < tamañoFinal.Height
                    ? Math.Min(cuadro.Height + incremento, tamañoFinal.Height)
                    : Math.Max(cuadro.Height - incremento, tamañoFinal.Height);

                // Actualizar el tamaño del cuadro
                cuadro.Size = new Size(nuevoAncho, nuevoAlto);
                cuadro.Refresh();

                // Forzar actualización visual
                Application.DoEvents();
                Thread.Sleep(90); // Pausa para que la animación sea visible
            }
        }

        public async void RadixSortDescendente(int[] A, FlowLayoutPanel panel, RichTextBox richTextBoxDigits)
        {
            int N = A.Length;
            richTextBoxDigits.Visible = true;

            if (panel.Controls.Count != N)
            {
                throw new InvalidOperationException("El número de cuadros no coincide con el tamaño del arreglo.");
            }

            List<Panel> cuadros = panel.Controls.Cast<Panel>().ToList();

            // Encontrar el valor máximo
            int max = A.Max();

            // Llamar a CountingSort para cada dígito
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                await CountingSortWithAnimationDescendente(A, exp, cuadros, richTextBoxDigits, panel);
            }

            // Finalizar la visualización
            for (int i = 0; i < N; i++)
            {
                cuadros[i].BackColor = Color.Green; // Cambiar color final
                AnimarCambioDeTamaño(cuadros[i], A[i]); // Ajustar al tamaño final
            }
            panel.Refresh();
        }

        private async Task CountingSortWithAnimationDescendente(int[] arr, int exp, List<Panel> cuadros, RichTextBox richTextBoxDigits, FlowLayoutPanel panel)
        {
            int n = arr.Length;
            int[] output = new int[n];
            int[] count = new int[10];

            // Inicializar el arreglo de conteo
            for (int i = 0; i < count.Length; i++)
                count[i] = 0;

            // Contar ocurrencias de cada dígito
            for (int i = 0; i < n; i++)
            {
                int digit = GetDigit(arr[i], exp);
                count[digit]++;
            }

            // Cambiar count[i] para que contenga la posición real de este dígito en output[]
            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            // Construir el arreglo de salida en orden descendente
            for (int i = n - 1; i >= 0; i--)
            {
                int digit = GetDigit(arr[i], exp);
                output[n - count[digit]] = arr[i]; // Colocar en la posición inversa
                count[digit]--;

                // Actualizar visualización
                int index = n - count[digit] - 1; // Obtener el índice correcto
                cuadros[index].BackColor = Color.Orange; // Resaltar el cuadro que se está procesando
                cuadros[index].Controls[0].Text = arr[i].ToString();
                panel.Refresh();
                await Task.Delay(500); // Pausa para que se vea el cambio de color

                // Animar el cambio de tamaño
                AnimarCambioDeTamaño(cuadros[index], arr[i]);

                // Actualizar el RichTextBox con el estado actual
                UpdateDigitsDisplay(arr, exp, richTextBoxDigits);
            }

            // Copiar el arreglo de salida a arr[]
            for (int i = 0; i < n; i++)
            {
                arr[i] = output[i];

                // Actualizar el panel correspondiente
                cuadros[i].Controls[0].Text = arr[i].ToString();
                cuadros[i].BackColor = Color.Red; // Resaltar el cuadro que ha sido colocado
                panel.Refresh();
                await Task.Delay(500); // Pausa para que se vea el cambio de color

                // Animar el cambio de tamaño al valor final
                AnimarCambioDeTamaño(cuadros[i], arr[i]);
            }
        }
    }
}
