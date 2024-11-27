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
            Panel cuadro = new Panel
            {
                Size = new Size(5, 5), // Tamaño inicial
                BackColor = GenerarColorUnico(),
                Margin = new Padding(5) // Asegurar un margen agradable dentro del FlowLayoutPanel
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

            // Animar el crecimiento del cuadro
            int incremento = 4;
            while (cuadro.Width < tamañoFinal.Width || cuadro.Height < tamañoFinal.Height)
            {
                // Incrementar tamaño gradualmente
                if (cuadro.Width < tamañoFinal.Width)
                    cuadro.Width = Math.Min(cuadro.Width + incremento, tamañoFinal.Width);

                if (cuadro.Height < tamañoFinal.Height)
                    cuadro.Height = Math.Min(cuadro.Height + incremento, tamañoFinal.Height);

                // Forzar la actualización del control
                cuadro.Refresh(); // Redibuja solo el cuadro
                parent.Refresh(); // Redibuja el contenedor

                // Pausa para crear el efecto de animación
                Thread.Sleep(70); // Ajustar para la velocidad deseada
            }

            // Rehabilitar la disposición automática
            parent.ResumeLayout();

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

