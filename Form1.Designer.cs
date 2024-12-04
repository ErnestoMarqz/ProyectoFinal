namespace ProyectoFinal
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.BTNCrear = new System.Windows.Forms.Button();
            this.rbAsendente = new System.Windows.Forms.RadioButton();
            this.rbDesendente = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.LabelTam = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.Numcub = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seleccione un Metodo";
            // 
            // BTNCrear
            // 
            this.BTNCrear.Location = new System.Drawing.Point(64, 108);
            this.BTNCrear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BTNCrear.Name = "BTNCrear";
            this.BTNCrear.Size = new System.Drawing.Size(112, 35);
            this.BTNCrear.TabIndex = 1;
            this.BTNCrear.Text = "Crear";
            this.BTNCrear.UseVisualStyleBackColor = true;
            this.BTNCrear.Click += new System.EventHandler(this.BTNCrear_Click);
            // 
            // rbAsendente
            // 
            this.rbAsendente.AutoSize = true;
            this.rbAsendente.Checked = true;
            this.rbAsendente.Location = new System.Drawing.Point(44, 134);
            this.rbAsendente.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbAsendente.Name = "rbAsendente";
            this.rbAsendente.Size = new System.Drawing.Size(112, 24);
            this.rbAsendente.TabIndex = 3;
            this.rbAsendente.TabStop = true;
            this.rbAsendente.Text = "Asendente";
            this.rbAsendente.UseVisualStyleBackColor = true;
            // 
            // rbDesendente
            // 
            this.rbDesendente.AutoSize = true;
            this.rbDesendente.Location = new System.Drawing.Point(44, 169);
            this.rbDesendente.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbDesendente.Name = "rbDesendente";
            this.rbDesendente.Size = new System.Drawing.Size(122, 24);
            this.rbDesendente.TabIndex = 4;
            this.rbDesendente.Text = "Desendente";
            this.rbDesendente.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Origen";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "No. Elementos ";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LavenderBlush;
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.BTNCrear);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(712, 555);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(224, 162);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crear ";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(45, 68);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(156, 26);
            this.numericUpDown1.TabIndex = 13;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 35);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Dato a buscar:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(28, 111);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(165, 35);
            this.button4.TabIndex = 15;
            this.button4.Text = "Buscar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Burbuja",
            "Burbuja Mejorado",
            "Merges",
            "Shell",
            "Inter. Directa",
            "Quick Sort",
            "Cubeta",
            "Inter. Binaria",
            "Radix Sort",
            "Heap Sort",
            "Baraja"});
            this.comboBox1.Location = new System.Drawing.Point(21, 60);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(180, 28);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(58, 205);
            this.btnIniciar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(112, 35);
            this.btnIniciar.TabIndex = 9;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1406, 860);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 35);
            this.button1.TabIndex = 20;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Ivory;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1700, 529);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(28, 65);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(156, 26);
            this.numericUpDown2.TabIndex = 20;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Honeydew;
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.rbDesendente);
            this.groupBox2.Controls.Add(this.rbAsendente);
            this.groupBox2.Controls.Add(this.btnIniciar);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(944, 555);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox2.Size = new System.Drawing.Size(224, 255);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ordenar";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.SeaShell;
            this.groupBox3.Controls.Add(this.numericUpDown2);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(1173, 555);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox3.Size = new System.Drawing.Size(225, 162);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Busqueda";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(18, 555);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(672, 372);
            this.richTextBox2.TabIndex = 23;
            this.richTextBox2.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox4.Controls.Add(this.numericUpDown3);
            this.groupBox4.Controls.Add(this.LabelTam);
            this.groupBox4.Controls.Add(this.numericUpDown4);
            this.groupBox4.Controls.Add(this.Numcub);
            this.groupBox4.Location = new System.Drawing.Point(712, 726);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(224, 162);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Cubeta";
            this.groupBox4.Visible = false;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(21, 48);
            this.numericUpDown3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(180, 26);
            this.numericUpDown3.TabIndex = 23;
            // 
            // LabelTam
            // 
            this.LabelTam.AutoSize = true;
            this.LabelTam.Location = new System.Drawing.Point(22, 85);
            this.LabelTam.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelTam.Name = "LabelTam";
            this.LabelTam.Size = new System.Drawing.Size(178, 20);
            this.LabelTam.TabIndex = 24;
            this.LabelTam.Text = "Tamaño de las cubetas:";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(21, 108);
            this.numericUpDown4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(180, 26);
            this.numericUpDown4.TabIndex = 23;
            // 
            // Numcub
            // 
            this.Numcub.AutoSize = true;
            this.Numcub.Location = new System.Drawing.Point(22, 26);
            this.Numcub.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Numcub.Name = "Numcub";
            this.Numcub.Size = new System.Drawing.Size(152, 20);
            this.Numcub.TabIndex = 24;
            this.Numcub.Text = "Numero de cubetas:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1406, 555);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(170, 295);
            this.richTextBox1.TabIndex = 25;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Salmon;
            this.ClientSize = new System.Drawing.Size(1700, 938);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Metodos de ordenamiento y busqueda";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTNCrear;
        private System.Windows.Forms.RadioButton rbAsendente;
        private System.Windows.Forms.RadioButton rbDesendente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label LabelTam;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label Numcub;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

