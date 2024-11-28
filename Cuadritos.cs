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
            Panel cuadro = new Panel
            {
                Size = new Size(5, 5),
                BackColor = GenerarColorUnico(),
                Margin = new Padding(5)
            };

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
            parent.Controls.Add(cuadro);

            Size tamañoFinal = new Size(numero * 10, numero * 10);

            parent.SuspendLayout();
            int incremento = 4;

            while (cuadro.Width < tamañoFinal.Width || cuadro.Height < tamañoFinal.Height)
            {
                if (cuadro.Width < tamañoFinal.Width)
                    cuadro.Width = Math.Min(cuadro.Width + incremento, tamañoFinal.Width);

                if (cuadro.Height < tamañoFinal.Height)
                    cuadro.Height = Math.Min(cuadro.Height + incremento, tamañoFinal.Height);

                cuadro.Refresh();
                parent.Refresh();
                Thread.Sleep(70);
            }

            parent.ResumeLayout();
            return cuadro;
        }

        public void CambiarColor(Panel cuadro, Color color)
        {
            cuadro.BackColor = color;
            cuadro.Refresh();
        }

        public void IntercambiarCuadros(Panel cuadro1, Panel cuadro2)
        {
            cuadro1.SuspendLayout();
            cuadro2.SuspendLayout();

            // Intercambiar colores para la animación
            Color tempColor = cuadro1.BackColor;
            cuadro1.BackColor = cuadro2.BackColor;
            cuadro2.BackColor = tempColor;

            // Intercambiar textos de las etiquetas
            Label label1 = cuadro1.Controls[0] as Label;
            Label label2 = cuadro2.Controls[0] as Label;

            string tempText = label1.Text;
            label1.Text = label2.Text;
            label2.Text = tempText;

            // Intercambiar tamaños (opcional, si aplicas el tamaño proporcional al número)
            Size tempSize = cuadro1.Size;
            cuadro1.Size = cuadro2.Size;
            cuadro2.Size = tempSize;

            cuadro1.ResumeLayout();
            cuadro2.ResumeLayout();
        }

        public Color GenerarColorUnico()
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

