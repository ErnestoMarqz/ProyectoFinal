﻿using System;
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

