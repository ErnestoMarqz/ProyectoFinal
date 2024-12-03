using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    internal class Cuadritos
    {
        private static readonly Random random = new Random();
        private static List<Color> coloresUsados = new List<Color>();


        public Panel CrearCuadroAnimadoEnLayout(FlowLayoutPanel parent, int numero)
        {
            // Crear el panel inicial
            Color colorGenerado = GenerarColorUnico();
            Panel cuadro = new Panel
            {
                Size = new Size(5, 5), // Tamaño inicial
                BackColor = GenerarColorUnico(),
                Margin = new Padding(5), // Asegurar un margen agradable dentro del FlowLayoutPanel
                Tag = colorGenerado // Guardar el color original en la propiedad Tag
            };

            // Crear la etiqueta
            Label etiqueta = new Label
            {
                Text = numero.ToString(),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Font = new Font("Arial", 14, FontStyle.Bold)
            };

            cuadro.Controls.Add(etiqueta);

            // Agregar el cuadro al contenedor principal
            parent.Controls.Add(cuadro);

            // Tamaño final del cuadro
            Size tamañoFinal = new Size(numero * 10, numero * 10);

            // Deshabilitar la disposición automática
            parent.SuspendLayout();

            AnimarCambioDeTamaño(cuadro, numero);

            // Rehabilitar la disposición automática
            parent.ResumeLayout();

            return cuadro;
        }
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
                Thread.Sleep(70); // Pausa para que la animación sea visible
            }
        }

        public Color GenerarColorUnico()
        {
            const int MaxIntentos = 100;
            Color nuevoColor;
            int intentos = 0;

            do
            {
                nuevoColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                intentos++;
            } while (coloresUsados.Contains(nuevoColor) && intentos < MaxIntentos);

            if (intentos < MaxIntentos)
            {
                coloresUsados.Add(nuevoColor);
            }
            else
            {
                // Reiniciar la lista de colores si se alcanzan demasiados intentos
                coloresUsados.Clear();
                coloresUsados.Add(nuevoColor);
            }

            return nuevoColor;
        }

        public static async Task IntercambiarCuadrosAnimado(FlowLayoutPanel parent, int indiceA, int indiceB)
        {
            // Validar índices
            if (indiceA < 0 || indiceB < 0 || indiceA >= parent.Controls.Count || indiceB >= parent.Controls.Count)
                return;

            // Obtener los cuadros
            Panel cuadroA = parent.Controls[indiceA] as Panel;
            Panel cuadroB = parent.Controls[indiceB] as Panel;

            if (cuadroA == null || cuadroB == null) return;

            int numeroA = int.Parse((cuadroA.Controls[0] as Label).Text);
            int numeroB = int.Parse((cuadroB.Controls[0] as Label).Text);

            // Resaltar en amarillo para indicar comparación
            cuadroA.BackColor = Color.Yellow;
            cuadroB.BackColor = Color.Yellow;
            cuadroA.Refresh();
            cuadroB.Refresh();
            await Task.Delay(500);

            // Determinar cuál debe parpadear (verde si mayor, rojo si menor)
            if (numeroA > numeroB)
            {
                await Parpadear(cuadroA, Color.Green, 3);
            }
            else
            {
                await Parpadear(cuadroA, Color.Red, 3);
            }

            // Intercambiar visualmente las propiedades (animación de cambio de tamaño)
            Size tamañoInicialA = cuadroA.Size;
            Size tamañoFinalA = new Size(numeroB * 10, numeroB * 10);
            Size tamañoInicialB = cuadroB.Size;
            Size tamañoFinalB = new Size(numeroA * 10, numeroA * 10);

            int pasos = 20;
            for (int i = 0; i <= pasos; i++)
            {
                cuadroA.Size = new Size(
                    Interpolar(tamañoInicialA.Width, tamañoFinalA.Width, i, pasos),
                    Interpolar(tamañoInicialA.Height, tamañoFinalA.Height, i, pasos)
                );

                cuadroB.Size = new Size(
                    Interpolar(tamañoInicialB.Width, tamañoFinalB.Width, i, pasos),
                    Interpolar(tamañoInicialB.Height, tamañoFinalB.Height, i, pasos)
                );

                cuadroA.Refresh();
                cuadroB.Refresh();
                await Task.Delay(25);
            }

    // Actualizar los textos
    (cuadroA.Controls[0] as Label).Text = numeroB.ToString();
            (cuadroB.Controls[0] as Label).Text = numeroA.ToString();

            // Restaurar colores
            cuadroA.BackColor = Color.Black;
            cuadroB.BackColor = Color.Black;
            cuadroA.Refresh();
            cuadroB.Refresh();
        }

        private static async Task Parpadear(Panel cuadro, Color color, int repeticiones)
        {
            for (int i = 0; i < repeticiones; i++)
            {
                cuadro.BackColor = color;
                cuadro.Refresh();
                await Task.Delay(250);

                cuadro.BackColor = Color.Yellow; // Restaurar al amarillo entre parpadeos
                cuadro.Refresh();
                await Task.Delay(250);
            }
        }

        private static int Interpolar(int inicio, int fin, int pasoActual, int totalPasos)
        {
            return inicio + (fin - inicio) * pasoActual / totalPasos;
        }
    }
}

