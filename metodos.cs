using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ProyectoFinal
{
    internal class Metodos
    {
        private Cuadritos cuadritos;

        public Metodos()
        {
            cuadritos = new Cuadritos(); // Inicialización correcta
        }

        internal Cuadritos Cuadritos { get => cuadritos; set => cuadritos = value; }

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


        public void Burbuja(FlowLayoutPanel panel, bool ascendente)
        {
            var cuadros = panel.Controls.OfType<Panel>().ToList();

            for (int i = 0; i < cuadros.Count - 1; i++)
            {
                for (int j = 0; j < cuadros.Count - i - 1; j++)
                {
                    // Obtener valores de las etiquetas
                    Label etiqueta1 = cuadros[j].Controls[0] as Label;
                    Label etiqueta2 = cuadros[j + 1].Controls[0] as Label;

                    int valor1 = int.Parse(etiqueta1.Text);
                    int valor2 = int.Parse(etiqueta2.Text);

                    // Cambiar color para animación
                    cuadros[j].BackColor = Color.Yellow;
                    cuadros[j + 1].BackColor = Color.Yellow;

                    Thread.Sleep(500);

                    // Condición de intercambio
                    if ((ascendente && valor1 > valor2) || (!ascendente && valor1 < valor2))
                    {
                        // Intercambiar visualmente
                        IntercambiarCuadros(cuadros[j], cuadros[j + 1]);

                        // Intercambiar lógica en la lista
                        var temp = cuadros[j];
                        cuadros[j] = cuadros[j + 1];
                        cuadros[j + 1] = temp;
                    }

                    // Restaurar colores originales
                    cuadros[j].BackColor = Cuadritos.GenerarColorUnico();
                    cuadros[j + 1].BackColor = Cuadritos.GenerarColorUnico();
                }
            }

            // Reorganizar los controles en el FlowLayoutPanel
            panel.Controls.Clear();
            foreach (var cuadro in cuadros)
            {
                panel.Controls.Add(cuadro);
            }
        }

        public void AjustarTamaño(Panel cuadro)
        {
            Label etiqueta = cuadro.Controls[0] as Label;
            int valor = int.Parse(etiqueta.Text);

            // Escalar el tamaño del cuadro proporcional al valor
            int nuevoTamaño = Math.Max(30, valor * 5); // Por ejemplo, escalar con un factor
            cuadro.Size = new Size(nuevoTamaño, nuevoTamaño);
        }
    }
}
