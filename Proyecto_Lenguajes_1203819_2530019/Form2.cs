using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Proyecto_Lenguajes_1203819_2530019
{
    public partial class Form2 : Form
    {
        public string Ruta = "";
        public  List<string> lines = new List<string>();
        public int CantEstados = 0;
        public int EstadoInicial = 0;
        public int Estado_Actual = 0;
        public List<string> EstadosFinales = new List<string>();
        public string[,] MatrizTrans = new string[1000, 5];
        public Stack<string> Pila = new Stack<string>();
        public string Cadena = "";
        Hashtable ht = new Hashtable();

        public Form2(string ruta)
        {
            Ruta = ruta;
            lines = File.ReadAllLines(ruta).ToList();
            CantEstados = Convert.ToInt32(lines[0]);
            EstadoInicial = Convert.ToInt32(lines[1]);
            EstadosFinales = lines[2].Split(',').ToList();
            for (int i = 3; i < lines.Count(); i++)
            {
                string[] TempMatriz = lines[i].Split(',');
                for (int f = 0; f < TempMatriz.Length; f++)
                {
                    MatrizTrans[i - 3, f] = TempMatriz[f];
                }
            }
            Estado_Actual = EstadoInicial;
            
            
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Cadena = CadenaVerificar.Text;
            verificar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 abrirform2 = new Form1();
            abrirform2.Show();
        }
        

        public void verificar()
        {
            for (int i = 0; i < MatrizTrans.Length; i++)
            {

            }
        }
    }
}
