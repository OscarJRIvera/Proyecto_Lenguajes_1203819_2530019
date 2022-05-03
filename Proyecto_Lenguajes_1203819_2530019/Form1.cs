using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Lenguajes_1203819_2530019
{
    public partial class Form1 : Form
    {
        public string Ruta = ""; 
           
        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            if (Ruta == "")
            {
                string temp = "Escoge una ruta primero";
                string title = "Error";
                MessageBox.Show(temp, title);
            }
            else
            {

                this.Hide();
                Form2 abrirform2 = new Form2(Ruta);
                abrirform2.Show();
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            ofd.Filter = "txt|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Ruta = ofd.FileName;
                Lruta.Text = Ruta;
            }
        }
    }
}
