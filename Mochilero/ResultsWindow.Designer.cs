namespace Mochilero {
	partial class ResultsWindow {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.mejoresSoluciones = new System.Windows.Forms.TreeView();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.solucionFinal = new System.Windows.Forms.TreeView();
			this.label3 = new System.Windows.Forms.Label();
			this.objetivo = new System.Windows.Forms.Label();
			this.seleccion = new System.Windows.Forms.Label();
			this.cruce = new System.Windows.Forms.Label();
			this.reemplazo = new System.Windows.Forms.Label();
			this.MaxCap = new System.Windows.Forms.Label();
			this.conclusion = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mejoresSoluciones
			// 
			this.mejoresSoluciones.BackColor = System.Drawing.SystemColors.Control;
			this.mejoresSoluciones.Location = new System.Drawing.Point(13, 31);
			this.mejoresSoluciones.Name = "mejoresSoluciones";
			this.mejoresSoluciones.Size = new System.Drawing.Size(274, 201);
			this.mejoresSoluciones.TabIndex = 0;
			this.mejoresSoluciones.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(254, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "MEJORES SOLUCIONES DE CADA GENERACION";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(83, 235);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(135, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "MEJOR SOLUCIÓN FINAL";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.solucionFinal);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.mejoresSoluciones);
			this.panel1.Location = new System.Drawing.Point(219, -1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(298, 347);
			this.panel1.TabIndex = 3;
			// 
			// solucionFinal
			// 
			this.solucionFinal.BackColor = System.Drawing.SystemColors.Control;
			this.solucionFinal.Location = new System.Drawing.Point(13, 255);
			this.solucionFinal.Name = "solucionFinal";
			this.solucionFinal.Size = new System.Drawing.Size(273, 77);
			this.solucionFinal.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(173, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "PARÁMETROS DEL ALGORITMO";
			// 
			// objetivo
			// 
			this.objetivo.AutoSize = true;
			this.objetivo.Location = new System.Drawing.Point(11, 70);
			this.objetivo.Name = "objetivo";
			this.objetivo.Size = new System.Drawing.Size(49, 13);
			this.objetivo.TabIndex = 5;
			this.objetivo.Text = "Objetivo:";
			this.objetivo.Click += new System.EventHandler(this.objetivo_Click);
			// 
			// seleccion
			// 
			this.seleccion.AutoSize = true;
			this.seleccion.Location = new System.Drawing.Point(11, 101);
			this.seleccion.Name = "seleccion";
			this.seleccion.Size = new System.Drawing.Size(57, 13);
			this.seleccion.TabIndex = 6;
			this.seleccion.Text = "Selección:";
			// 
			// cruce
			// 
			this.cruce.AutoSize = true;
			this.cruce.Location = new System.Drawing.Point(11, 132);
			this.cruce.Name = "cruce";
			this.cruce.Size = new System.Drawing.Size(38, 13);
			this.cruce.TabIndex = 7;
			this.cruce.Text = "Cruce:";
			// 
			// reemplazo
			// 
			this.reemplazo.AutoSize = true;
			this.reemplazo.Location = new System.Drawing.Point(11, 163);
			this.reemplazo.Name = "reemplazo";
			this.reemplazo.Size = new System.Drawing.Size(63, 13);
			this.reemplazo.TabIndex = 8;
			this.reemplazo.Text = "Reemplazo:";
			// 
			// MaxCap
			// 
			this.MaxCap.AutoSize = true;
			this.MaxCap.Location = new System.Drawing.Point(11, 39);
			this.MaxCap.Name = "MaxCap";
			this.MaxCap.Size = new System.Drawing.Size(151, 13);
			this.MaxCap.TabIndex = 9;
			this.MaxCap.Text = "Max. capacidad de la mochila:";
			// 
			// conclusion
			// 
			this.conclusion.AutoSize = true;
			this.conclusion.Location = new System.Drawing.Point(11, 193);
			this.conclusion.Name = "conclusion";
			this.conclusion.Size = new System.Drawing.Size(62, 13);
			this.conclusion.TabIndex = 4;
			this.conclusion.Text = "Conclusión:";
			// 
			// ResultsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ClientSize = new System.Drawing.Size(516, 343);
			this.Controls.Add(this.conclusion);
			this.Controls.Add(this.MaxCap);
			this.Controls.Add(this.reemplazo);
			this.Controls.Add(this.cruce);
			this.Controls.Add(this.seleccion);
			this.Controls.Add(this.objetivo);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.panel1);
			this.Name = "ResultsWindow";
			this.Text = "Resultados";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView mejoresSoluciones;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TreeView solucionFinal;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label objetivo;
		private System.Windows.Forms.Label seleccion;
		private System.Windows.Forms.Label cruce;
		private System.Windows.Forms.Label reemplazo;
		private System.Windows.Forms.Label MaxCap;
		private System.Windows.Forms.Label conclusion;
	}
}