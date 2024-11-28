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

        public static void IntercambiarCuadrosAnimado(FlowLayoutPanel parent, int indiceA, int indiceB)
        {
            // Lógica de animación de intercambio (sin cambios)
            Panel cuadroA = parent.Controls[indiceA] as Panel;
            Panel cuadroB = parent.Controls[indiceB] as Panel;

            if (cuadroA == null || cuadroB == null) return;

            int numeroA = int.Parse((cuadroA.Controls[0] as Label).Text);
            int numeroB = int.Parse((cuadroB.Controls[0] as Label).Text);

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

                (cuadroA.Controls[0] as Label).Text = numeroB.ToString();
                (cuadroB.Controls[0] as Label).Text = numeroA.ToString();

                cuadroA.Refresh();
                cuadroB.Refresh();
                Thread.Sleep(50);
            }
        }

        private static int Interpolar(int inicio, int fin, int pasoActual, int totalPasos)
        {
            return inicio + (fin - inicio) * pasoActual / totalPasos;
        }
    }
}

