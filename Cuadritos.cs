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

        public Panel CrearCuadroAnimado(Control parent, int numero)
        {
            // Crear el panel
            Panel cuadro = new Panel
            {
                Size = new Size(5, 5), // Iniciar en tamaño 1x1
                BackColor = GenerarColorUnico(),
                Location = new Point(100, 100) // Ajusta según sea necesario
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

            // Tamaño final del cuadro
            Size tamañoFinal = new Size(numero * 10, numero * 10);

            // Animación: Incrementar tamaño gradualmente
            int incremento = 5; // Tamaño de paso para cada iteración
            while (cuadro.Width < tamañoFinal.Width || cuadro.Height < tamañoFinal.Height)
            {
                Application.DoEvents();

                if (cuadro.Width < tamañoFinal.Width && cuadro.Height < tamañoFinal.Height)
                {
                    cuadro.Width = Math.Min(cuadro.Width + incremento, tamañoFinal.Width);
                    cuadro.Height = Math.Min(cuadro.Height + incremento, tamañoFinal.Height);
                    Application.DoEvents();
                   // Thread.Sleep(50); // Ajustar para velocidad de animación

                }
                // Permitir que la interfaz gráfica se actualice
                Application.DoEvents();
                parent.Invalidate();

                // Paua para crear el efecto de animación
                Thread.Sleep(50); // Ajustar para velocidad de animación

            }
            Application.DoEvents();
            // Devolver el cuadro creado
            return cuadro;

        }


        // Método para generar un color único
        private Color GenerarColorUnico()
        {
            Color nuevoColor;
            do
            {
                nuevoColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            } while (coloresUsados.Contains(nuevoColor));

            coloresUsados.Add(nuevoColor);
            return nuevoColor;
        }
    }
}

