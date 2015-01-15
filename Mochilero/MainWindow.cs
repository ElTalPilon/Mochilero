using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mochilero {
	public partial class MainWindow : Form {
		
		private string[] nombresArticulos;
		private int[][] matrizArticulos;

		public MainWindow() {
			InitializeComponent();
			tipoAritmetico.SelectedIndex = 0;
		}

		private void radioButton9_CheckedChanged(object sender, EventArgs e) {
			tipoAritmetico.Enabled = true;
		}

		private void radioButton7_CheckedChanged(object sender, EventArgs e) {
			tipoAritmetico.Enabled = false;
		}

		private void radio31_CheckedChanged(object sender, EventArgs e) {
			tipoAritmetico.Enabled = false;
		}

		private void radio41_CheckedChanged(object sender, EventArgs e) {
			numGen.Enabled = true;
		}

		private void radio42_CheckedChanged(object sender, EventArgs e) {
			numGen.Enabled = false;
		}

		private void botonResolver_Click(object sender, EventArgs e) {
			if (allSystemsGo()) {
				int numArticulos = articulos.Rows.Count - 1;
				//Crea el vector con los nombres y la matriz con el peso y la utilidad de los artículos disponibles
				nombresArticulos = new string[numArticulos];
				for (int i = 0; i < numArticulos; i++) {
					nombresArticulos[i] = (string)articulos[0, i].Value;
				}
				matrizArticulos = new int[numArticulos][];
				for (int i = 0; i < numArticulos; i++) {
					matrizArticulos[i] = new int[2];
					matrizArticulos[i][0] = Convert.ToInt32(articulos[1, i].Value); // Utilidad
					matrizArticulos[i][1] = Convert.ToInt32(articulos[2, i].Value); // Peso
				}

				//Comienza el algoritmo
				int generacion = 1;
				int tamanoPoblacion = Convert.ToInt32(tamPoblacion.Text);
				Poblacion poblacion = new Poblacion(nombresArticulos.Length, tamanoPoblacion, matrizArticulos);
				poblacion.llenar();
					//Console.WriteLine("Poblacion inicial:");
					//Console.WriteLine(poblacion.imprimir());
				int maxCap = Convert.ToInt32(maxCapMochila.Text);
				if (radio11.Checked) { 
					poblacion.evaluar(maxCap, 0);
				}
				else { 
					poblacion.evaluar(maxCap, 1);
				}
					//Console.WriteLine("Poblacion evaluada:");
					//Console.WriteLine(poblacion.imprimir());
				string mejorSolucion = poblacion.mejorOrganismo();
					ResultsWindow results = new ResultsWindow();
					results.Show();
					int[] elecciones = desplegarParametros(results);
					desplegarSolucion(results, generacion, mejorSolucion);
				if (radio51.Checked) {
					// El algoritmo se ejecuta por una cantidad establecida de generaciones
					for (generacion = 2; generacion < Convert.ToInt32(numGen.Text); generacion++) {
							//Console.WriteLine("Generacion " + generacion);
						Poblacion padres = (elecciones[1] != 3) ? poblacion.seleccionar(elecciones[1], Convert.ToInt32(cantPadres.Text)) : poblacion.seleccionar(elecciones[1], 2);
							//Console.WriteLine("Padres:");
							//Console.WriteLine(padres.imprimir());
						Poblacion hijos = poblacion.cruzar(elecciones[2], padres);
							//Console.WriteLine("Hijos:");
							//Console.WriteLine(hijos.imprimir());
						poblacion.mutar(hijos);
							//Console.WriteLine("Hijos mutados:");
							//Console.WriteLine(hijos.imprimir());
						poblacion.reemplazar(elecciones[3], padres, hijos);
							//Console.WriteLine("Nueva poblacion:");
							//Console.WriteLine(poblacion.imprimir());
						poblacion.evaluar(maxCap, elecciones[0]);
							//Console.WriteLine("Poblacion evaluada:");
							//Console.WriteLine(poblacion.imprimir());
						mejorSolucion = poblacion.mejorOrganismo();
							desplegarSolucion(results, generacion, mejorSolucion);
					}
				}
				else {
					Poblacion pobAnterior = poblacion;
					// El algoritmo se ejecuta hasta que no hayan cambios de una gen a otra
					do {
						generacion++;
						pobAnterior = poblacion;
							//Console.WriteLine("Generacion " + generacion);
						Poblacion padres = (elecciones[1] != 3) ? poblacion.seleccionar(elecciones[1], Convert.ToInt32(cantPadres.Text)) : poblacion.seleccionar(elecciones[1], 2);
							//Console.WriteLine("Padres:");
							//Console.WriteLine(padres.imprimir());
						Poblacion hijos = poblacion.cruzar(elecciones[2], padres);
							//Console.WriteLine("Hijos:");
							//Console.WriteLine(hijos.imprimir());
						poblacion.mutar(hijos);
							//Console.WriteLine("Hijos mutados:");
							//Console.WriteLine(hijos.imprimir());
						poblacion.reemplazar(elecciones[3], padres, hijos);
							//Console.WriteLine("Nueva poblacion:");
							//Console.WriteLine(poblacion.imprimir());
						poblacion.evaluar(maxCap, elecciones[0]);
							//Console.WriteLine("Poblacion evaluada:");
							//Console.WriteLine(poblacion.imprimir());
						mejorSolucion = poblacion.mejorOrganismo();
						desplegarSolucion(results, generacion, mejorSolucion);
					} while (poblacion.sonIguales(pobAnterior) == false);
				}
				desplegarSolucion(results, 0, mejorSolucion);
			}			
		}

		private Boolean allSystemsGo() {
			Boolean allSystemsGo = true;
			// Revisa si hay al menos 1 artículo disponible
			if (articulos.Rows.Count == 1) {
				errorArticulos.Text = "Por favor ingrese como mínimo 1 artículo disponible.";
				errorArticulos.Visible = true;
				allSystemsGo = false;
			}
			else {
				errorArticulos.Visible = false;
			}

			// Valida los valores ingresados en los parámetros del algoritmo
			if (maxCapMochila.Text.Length == 0) {
				errorCapGen.Text = "Por favor indique la capacidad máxima de la mochila.";
				errorCapGen.Visible = true;
				allSystemsGo = false;
			}
			else {
				if (tamPoblacion.Text.Length == 0) {
					errorCapGen.Text = "Por favor indique el tamaño deseado de la población.";
					errorCapGen.Visible = true;
					allSystemsGo = false;
				}
				else {
					if (cantPadres.Text.Length == 0) {
						errorCapGen.Text = "Por favor indiquela cantidad de padres a seleccionar.";
						errorCapGen.Visible = true;
						allSystemsGo = false;
					}
					else {
						if (Convert.ToInt32(cantPadres.Text) % 2 != 0) {
							errorCapGen.Text = "La cantidad de padres a seleccionar debe ser par.";
							errorCapGen.Visible = true;
							allSystemsGo = false;
						}
						else {
							if (radio51.Checked) {
								if (numGen.Text.Length == 0) {
									errorCapGen.Text = "Por favor indique la cantidad de generaciones deseadas.";
									errorCapGen.Visible = true;
									allSystemsGo = false;
								}
								else {
									errorCapGen.Visible = false;
								}
							}
							else {
								errorCapGen.Visible = false;
							}
						}
					}
				}
			}
			return allSystemsGo;
		}

		private int[] desplegarParametros(ResultsWindow results) {
			int[] elecciones = new int[5];
			// Máxima Capacidad
			results.setMaxCap(maxCapMochila.Text);
			// Objetivo
			if(radio11.Checked){
				results.setObjetivo("Máxima utilidad.");
				elecciones[0] = 0;
			}
			else{
				results.setObjetivo("Máxima cantidad.");
				elecciones[0] = 1;
			}
			// Seleccion
			if(radio21.Checked){
				results.setSeleccion("Ruleta por rango.");
				elecciones[1] = 0;
			}
			else{
				if(radio22.Checked){
					results.setSeleccion("Ruleta simple.");
					elecciones[1] = 1;
				}
				else{
					if(radio23.Checked){
						results.setSeleccion("Elitismo.");
						elecciones[1] = 2;
					}
					else{
						results.setSeleccion("Estado estacionario.");
						elecciones[1] = 3;
					}
				}
			}
			// Cruce
			if(radio31.Checked){
				results.setCruce("Uniforme.");
				elecciones[2] = 0;
			}
			else{
				if(radio32.Checked){
					results.setCruce("Un punto de cruce.");
					elecciones[2] = 1;
				}
				else{
					switch(tipoAritmetico.SelectedIndex){
						case 0:
							results.setCruce("Aritmético (OR).");
							elecciones[2] = 2;
						break;
						case 1:
							results.setCruce("Aritmético (AND).");
							elecciones[2] = 3;
						break;
						case 2:
							results.setCruce("Aritmético (XOR).");
							elecciones[2] = 4;
						break;
					}
				}
			}
			// Reemplazo
			if (radio41.Checked) {
				results.setReemplazo("Individuos similares.");
				elecciones[3] = 0;
			}
			else {
				if(radio42.Checked){
					results.setReemplazo("Padres.");
					elecciones[3] = 1;
				}
				else{
					if(radio43.Checked){
						results.setReemplazo("Peores individuos");
						elecciones[3] = 2;
					}
					else{
						results.setReemplazo("Aleatorio");
						elecciones[3] = 3;
					}
				}
			}
			// Conclusion
			if(radio51.Checked){
				results.setConclusion(numGen.Text + " generaciones.");
				elecciones[4] = 0;
			}
			else {
				results.setConclusion("Sin cambios entre generacion.");
				elecciones[4] = 1;
			}
			return elecciones;
		}

		private void desplegarSolucion(ResultsWindow results, int generacion, string mejorSolucion) {
			int articulosEnSolucion = 0;
			for (int i = 0; i < nombresArticulos.Length; i++) {
				if (mejorSolucion.ElementAt(i) == '1') {
					articulosEnSolucion++;
				}
			}
			string[] articulosMS = new string[articulosEnSolucion];
			articulosEnSolucion = 0;
			int utilidadMS = 0;
			int pesoMS = 0;
			for (int i = 0; i < nombresArticulos.Length; i++) {
				if (mejorSolucion.ElementAt(i) == '1') {
					articulosMS[articulosEnSolucion] = nombresArticulos[i];
					utilidadMS += matrizArticulos[i][1];
					pesoMS += matrizArticulos[i][0];
					articulosEnSolucion++;
				}
			}
			results.agregarSolucion(generacion, pesoMS, utilidadMS, articulosMS);
		}

		private void radio24_CheckedChanged(object sender, EventArgs e) {
			cantPadres.Enabled = false;
		}

		private void radio23_CheckedChanged(object sender, EventArgs e) {
			cantPadres.Enabled = true;
		}

		private void radio22_CheckedChanged(object sender, EventArgs e) {
			cantPadres.Enabled = true;
		}

		private void radio21_CheckedChanged(object sender, EventArgs e) {
			cantPadres.Enabled = true;
		}

		private void botonLlenar_Click(object sender, EventArgs e) {
			articulos.Rows.Add(11);
			articulos[0, 0].Value = "Botella";
			articulos[1, 0].Value = 2;
			articulos[2, 0].Value = 5;
			articulos[0, 1].Value = "Cuchillo";
			articulos[1, 1].Value = 1;
			articulos[2, 1].Value = 6;
			articulos[0, 2].Value = "Sleeping";
			articulos[1, 2].Value = 8;
			articulos[2, 2].Value = 7;
			articulos[0, 3].Value = "Tienda";
			articulos[1, 3].Value = 10;
			articulos[2, 3].Value = 10;
			articulos[0, 4].Value = "Libreta";
			articulos[1, 4].Value = 3;
			articulos[2, 4].Value = 2;
			articulos[0, 5].Value = "Binoculares";
			articulos[1, 5].Value = 5;
			articulos[2, 5].Value = 3;
			articulos[0, 6].Value = "Linterna";
			articulos[1, 6].Value = 6;
			articulos[2, 6].Value = 5;
			articulos[0, 7].Value = "Termo";
			articulos[1, 7].Value = 8;
			articulos[2, 7].Value = 7;
			articulos[0, 8].Value = "Bloqueador solar";
			articulos[1, 8].Value = 4;
			articulos[2, 8].Value = 8;
			articulos[0, 9].Value = "Lapicero";
			articulos[1, 9].Value = 1;
			articulos[2, 9].Value = 1;
			articulos[0, 10].Value = "Reloj";
			articulos[1, 10].Value = 3;
			articulos[2, 10].Value = 6;
		}
	}
}
