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
	public partial class ResultsWindow : Form {
		public ResultsWindow() {
			InitializeComponent();
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) {

		}

		public void agregarSolucion(int generacion, int pesoTotal, int utilidadTotal, string[] articulos) {
			TreeNode peso = new TreeNode("Peso: " + pesoTotal);
			TreeNode utilidad = new TreeNode("Utilidad: " + utilidadTotal);
			TreeNode[] articulosB = new TreeNode[articulos.Length];
			for(int i = 0; i < articulos.Length; i++){
				TreeNode nuevo = new TreeNode(articulos[i]);
				articulosB[i] = nuevo;
			}
			TreeNode articulosH = new TreeNode("Artículos:", articulosB);
			TreeNode[] todoB = new TreeNode[] {peso, utilidad, articulosH};
			if(generacion == 0){
				TreeNode todoH = new TreeNode("Solucion final", todoB);
				solucionFinal.Nodes.Add(todoH);
			}
			else{
				TreeNode todoH = new TreeNode("Generacion " + generacion, todoB);
				mejoresSoluciones.Nodes.Add(todoH);
			}
		}

		public void setMaxCap(string mCap) {
			MaxCap.Text += " " + mCap;
		}

		public void setObjetivo(string obj) {
			objetivo.Text += " " + obj;
		}

		public void setSeleccion(string selec) {
			seleccion.Text += " " + selec;
		}

		public void setCruce(string cruc) {
			cruce.Text += " " + cruc;
		}

		public void setReemplazo(string reemp) {
			reemplazo.Text += " " + reemp;
		}

		public void setConclusion(string concl) {
			conclusion.Text += " " + concl;
		}

		private void objetivo_Click(object sender, EventArgs e) {

		}
	}
}
