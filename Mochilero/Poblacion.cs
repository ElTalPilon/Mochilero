using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochilero {
	class Poblacion {
		private int[][] matrizArticulos;
		public string[] cromosomas;
		public int[] fitness;
		private int numOrganismos;
		private int numGenes;

		public Poblacion(int numGenes, int numOrganismos, int[][] matrizArticulos) {
			this.numGenes = numGenes;
			this.matrizArticulos = matrizArticulos;
			this.numOrganismos = numOrganismos;
			this.cromosomas = new string[numOrganismos];
			this.fitness = new int[numOrganismos];
			for(int i = 0; i < numOrganismos; i++){
				cromosomas[i] = "";
				fitness[i] = 0;
			}
		}
		
		public void agregar(string cromosoma) {
			int i = 0;
			while(cromosomas[i] != ""){
				i++;
			}
			cromosomas[i] = cromosoma;
			fitness[i] = 0;
		}

		private void agregarOrdenado(string cromosoma, int fit) {
			int i = 0;
			while(cromosomas[i] != "" && fitness[i] > fit){
				i++;
			}
			if(cromosomas[i] == ""){
				cromosomas[i] = cromosoma;
				fitness[i] = fit;
			}
			else{
				int j = numOrganismos - 1;
				while(j > i){
					cromosomas[j] = cromosomas[j-1];
					fitness[j] = fitness[j-1];
					j--;
				}
				cromosomas[i] = cromosoma;
				fitness[i] = fit;
			}
		}

		private void eliminar(int i) {
			while(cromosomas[i-1] != ""){
				cromosomas[i] = cromosomas[i+1];
				fitness[i] = fitness[i+1];
				i++;
			}
		}

		public void llenar() {
			Random rand = new Random();
			for (int i = 0; i < numOrganismos; i++) {
				int cromosoma = 0;
				for(int j = 0; j < numGenes; j++){
					if (rand.NextDouble() > 0.5) {
						cromosoma += (int)Math.Pow(2,j);
					}
				}
				string resultado = Convert.ToString(cromosoma,2);
				while (resultado.Length < numGenes) {
					resultado = "0" + resultado;
				}
				agregar(resultado);
			}
		}

		public void evaluar(int maxCap, int objetivo) {
			int i = 0;
			while (i < numOrganismos) {
				fitness[i] = calcFitness(cromosomas[i], maxCap, objetivo);
				i++;
			}
			//Los ordena por fitness
			Poblacion temp = new Poblacion(numGenes, numOrganismos, matrizArticulos);
			for (i = 0; i < numOrganismos; i++) {
				temp.agregarOrdenado(cromosomas[i], fitness[i]);
			}
			cromosomas = temp.cromosomas;
			fitness = temp.fitness;
		}

		private int calcFitness(string cromosoma, int maxCap, int objetivo) {
			int fitness = 0;
			int utilidad = 0;
			int peso = 0;
			int cantArticulos = 0;
			for (int i = 0; i < numGenes; i++) {
				if (cromosoma.ElementAt(i) == '1') {
					utilidad += matrizArticulos[i][1];
					peso += matrizArticulos[i][0];
					cantArticulos++;
				}
			}
			if(peso <= maxCap){
				if (objetivo == 0) {
					fitness = utilidad;
				}
				else {
					fitness = cantArticulos;
				}
			}
			return fitness;
		}

		public Poblacion seleccionar(int seleccion, int cantPadres) {
			Poblacion padres = null;
			switch (seleccion) {
				case 0:
					padres = ruletaRango(cantPadres);
				break;
				case 1:
					padres = ruletaSimple(cantPadres);
				break;
				case 2:
					padres = elitismo(cantPadres);
				break;
				case 3:
					padres = estadoEstacionario(cantPadres);
				break;
			}
			return padres;
		}

		private Poblacion ruletaSimple(int cantPadres) {
			Poblacion padres = new Poblacion(numGenes, cantPadres, matrizArticulos);
			Random rand = new Random();
			int fitnessTotal = 0;
			int[] numerosRuleta = new int[100]; 
			int i = 0;
			while (i < cantPadres) {
				fitnessTotal += fitness[i];
				i++;
			}
			int desplazamiento = 0;
			for(i = 0; i < numOrganismos; i++){
				int numeros = (fitness[i] * 100) / fitnessTotal;
				for(int j = 0; j < numeros; j++){
					if (desplazamiento + j < 100) {
						numerosRuleta[desplazamiento + j] = i;
					}
				}
				desplazamiento += numeros;
			}
			for(i = 0; i < cantPadres; i++){
				padres.agregar(cromosomas[numerosRuleta[rand.Next(0,99)]]);
			}
			return padres;
		}

		private Poblacion ruletaRango(int cantPadres) {
			Poblacion padres = new Poblacion(numGenes, cantPadres, matrizArticulos);
			Random rand = new Random();
			int fitnessTotal = 0;
			int i = 0;
			while (i < numOrganismos) {
				fitnessTotal += fitness[i];
				i++;
			}
			int[][] numerosRuleta = new int[numOrganismos][];
			for (i = 0; i < numOrganismos; i++) {
				numerosRuleta[i] = new int[2];
				if (fitness[i] != 0) {
					numerosRuleta[i][0] = (fitness[i] * 100) / fitnessTotal;
					numerosRuleta[i][1] = i;
				}
				else {
					numerosRuleta[i][0] = 0;
					numerosRuleta[i][1] = i;
				}
			}
			//Ordenar la matriz por cantidad de "números" de cada 
			for (i = 0; i < numOrganismos; i++) {
				int j = 0;
				while(numerosRuleta[j][0] > numerosRuleta[i][0]){
					j++;
				}
				int tempNums = numerosRuleta[j][0];
				int tempPos = numerosRuleta[j][1];
				numerosRuleta[j][0] = numerosRuleta[i][0];
				numerosRuleta[j][1] = numerosRuleta[i][1];
				numerosRuleta[i][0] = tempNums;
				numerosRuleta[i][1] = tempPos;
			}

			for (i = 0; i < cantPadres; i++) {
				padres.agregar(cromosomas[numerosRuleta[rand.Next(0, numOrganismos)][1]]);
			}
			return padres;
		}

		private Poblacion elitismo(int cantPadres) {
			Poblacion padres = new Poblacion(numGenes, cantPadres, matrizArticulos);
			for(int i = 0; i < cantPadres; i++){
				padres.agregar(cromosomas[i]);
			}
			return padres;
		}

		private Poblacion estadoEstacionario(int cantPadres) {
			Poblacion padres = new Poblacion(numGenes, cantPadres, matrizArticulos);
			// Sólo los 2 mejores de la generación
			padres.agregar(cromosomas[0]);
			padres.agregar(cromosomas[1]);
			return padres;
		}

		public Poblacion cruzar(int seleccion, Poblacion padres) {
			Poblacion hijos = new Poblacion(numGenes, padres.numOrganismos, matrizArticulos);
			Random rand = new Random();
			int padre = 0;
			int madre = 1;
			while (padre < padres.numOrganismos) {
				if(rand.NextDouble() < 0.8){
					switch (seleccion) {
						case 0:
							hijos.agregar(cruceUniforme(padres.cromosomas[padre], padres.cromosomas[madre]));
							hijos.agregar(cruceUniforme(padres.cromosomas[padre], padres.cromosomas[madre]));
						break;
						case 1:
							hijos.agregar(cruceUnPunto(padres.cromosomas[padre], padres.cromosomas[madre]));
							hijos.agregar(cruceUnPunto(padres.cromosomas[padre], padres.cromosomas[madre]));
						break;
						case 2:
							hijos.agregar(cruceAritmetico(0, padres.cromosomas[padre], padres.cromosomas[madre]));
							hijos.agregar(cruceAritmetico(0, padres.cromosomas[padre], padres.cromosomas[madre]));
						break;
						case 3:
							hijos.agregar(cruceAritmetico(1, padres.cromosomas[padre], padres.cromosomas[madre]));
							hijos.agregar(cruceAritmetico(1, padres.cromosomas[padre], padres.cromosomas[madre]));
						break;
						case 4:
							hijos.agregar(cruceAritmetico(2, padres.cromosomas[padre], padres.cromosomas[madre]));
							hijos.agregar(cruceAritmetico(2, padres.cromosomas[padre], padres.cromosomas[madre]));
						break;
					}
				}
				else{
					hijos.agregar(padres.cromosomas[padre]);
					hijos.agregar(padres.cromosomas[madre]);
				}
				padre += 2;
				madre += 2;
			}
			return hijos;
		}

		public string cruceUnPunto(string padre, string madre) {
			Random r = new Random();
			int rand = (int)r.Next(1, numGenes);
			string hijo = (padre.Substring(0,rand)) + (madre.Substring(rand));
			return hijo;
		}

		public string cruceUniforme(string padre, string madre) {
			Random rand = new Random();
			string hijo = "";
			for (int i = 0; i < numGenes; i++) {
				if (rand.NextDouble() > 0.5) {
					hijo += padre.ElementAt(i);
				}
				else {
					hijo += madre.ElementAt(i);
				}
			}
			return hijo;
		}

		public string cruceAritmetico(int tipo, string padre, string madre) {
			int pa = 0;
			int ma = 0;
			int hi = 0;
			switch (tipo) {
				case 0:
					pa = Convert.ToInt32(padre, 2);
					ma = Convert.ToInt32(madre, 2);
					hi = pa | ma;
				break;
				case 1:
					pa = Convert.ToInt32(padre, 2);
					ma = Convert.ToInt32(madre, 2);
					hi = pa & ma;
				break;
				case 2:
					pa = Convert.ToInt32(padre, 2);
					ma = Convert.ToInt32(madre, 2);
					hi = pa ^ ma;
				break;
			}
			string hijo = Convert.ToString(hi, 2);
			while (hijo.Length < numGenes) {
				hijo = "0" + hijo;
			}
			return hijo;
		}

		public void mutar(Poblacion hijos) {
			Random rand = new Random();
			for (int actual = 0; actual < hijos.numOrganismos; actual++) {
				if (rand.NextDouble() < 0.1) {
					int index = rand.Next(0, numGenes);
					if (hijos.cromosomas[actual].ElementAt(index) == '1') {
						hijos.cromosomas[actual] = (hijos.cromosomas[actual].Remove(index, 1)).Insert(index, "0");
					}
					else {
						hijos.cromosomas[actual] = (hijos.cromosomas[actual].Remove(index, 1)).Insert(index, "1");
					}
				}
			}
		}

		public void reemplazar(int eleccion, Poblacion padres, Poblacion hijos) {
			switch (eleccion) {
				case 0:
					reemplazoSimilares(hijos);
				break;
				case 1:
					reemplazoPadres(padres, hijos);
				break;
				case 2:
					reemplazoPeores(hijos);
				break;
				case 3:
					reemplazoAleatorio(hijos);
				break;
			}
		}

		private void reemplazoSimilares(Poblacion hijos) {
			for(int i = 0; i < hijos.numOrganismos; i++){
				int j = 0;
				while(j < numOrganismos && fitness[j] != hijos.fitness[i]){
					j++;
				}
				if (j != numOrganismos) {
					cromosomas[j] = hijos.cromosomas[i];
					fitness[j] = hijos.fitness[i];
				}
			}
		}

		private void reemplazoPadres(Poblacion padres, Poblacion hijos) {
			for (int i = 0; i < hijos.numOrganismos; i++) {
				int j = 0;
				while (cromosomas[j].Equals(padres.cromosomas[i]) == false ) {
					j++;
				}
				cromosomas[j] = hijos.cromosomas[i];
				fitness[j] = hijos.fitness[i];
			}
		}

		private void reemplazoPeores(Poblacion hijos) {
			int j = numOrganismos - 1;
			for(int i = 0; i < hijos.numOrganismos; i++){
				cromosomas[j] = hijos.cromosomas[i];
				fitness[j] = hijos.fitness[i];
				j--;
			}
		}

		private void reemplazoAleatorio(Poblacion hijos) {
			Random rand = new Random();
			for (int i = 0; i < hijos.numOrganismos; i++) {
				int j = (int)rand.Next(0, numOrganismos - 1);
				cromosomas[j] = hijos.cromosomas[i];
				fitness[j] = hijos.fitness[i];
			}
		}

		public string mejorOrganismo() {
			int mejor = 0;
			for (int actual = 0; actual < numOrganismos; actual++) {
				mejor = (fitness[actual] > fitness[mejor]) ? actual : mejor;
			}
			return cromosomas[mejor];
		}

		public bool sonIguales(Poblacion pobAnterior) {
			bool sonIguales = true;
			for(int i = 0; i < numOrganismos; i++){
				bool existe = false;
				for(int j = 0; j < numOrganismos; j++){
					if(cromosomas[i] == pobAnterior.cromosomas[j]){
						existe = true;
						break;
					}
				}
				if (existe == false) {
					sonIguales = false;
					break;
				}
			}
			return sonIguales;
		}

		public string imprimir() {
			string pobResultante = "";
			for (int actual = 0; actual < numOrganismos; actual++) {
				pobResultante += cromosomas[actual] + " " + fitness[actual] + "\n";
			}
			return pobResultante;
		}
	}
}
