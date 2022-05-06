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

        List<string[]> ListaPasosRealizados = new List<string[]>();

        public bool VerValor = true;

        public bool CadenaAceptada = false;

        public Form2(string ruta)
        {
            
            Ruta = ruta;
            lines = File.ReadAllLines(ruta).ToList();
            MatrizTrans = new string[lines.Count - 3, 5];
            CantEstados = Convert.ToInt32(lines[0]);
            EstadoInicial = Convert.ToInt32(lines[1]);
            EstadosFinales = lines[2].Split(',').ToList();
            for (int i = 3; i < lines.Count(); i++)
            {
                string[] TempMatriz = lines[i].Split(',');
                for (int f = 0; f < TempMatriz.Length; f++)
                {
                   
                    MatrizTrans[i - 3, f] = TempMatriz[f].Trim();
                }
            }
            Estado_Actual = EstadoInicial;
            
            
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            resetearvalores();
            verificar();
            if (CadenaAceptada == true)
            {
                listBox1.Items.Clear();
                Pila = new Stack<string>();
                Cadena = CadenaVerificar.Text;
                int tamañocadena = Cadena.Length;

                Verificartexto.Text = "Cadena es aceptada!";
                string tempstring = "Estado actual | Por Leer | Pila";
                listBox1.Items.Add(tempstring);
                tempstring = EstadoInicial + " | " + ver_cadena() + " | " + "e" ;
                listBox1.Items.Add(tempstring);

                foreach(var pasoactual in ListaPasosRealizados)
                {
                    if (pasoactual[2] != "")
                    {
                        Pila.Pop();
                    }
                    if (pasoactual[3] != "")
                    {
                        if (pasoactual[3].Length > 1)
                        {
                            foreach (var x in pasoactual[3])
                            {
                                Pila.Push(Convert.ToString(x));
                            }
                        }
                        else
                        {
                            Pila.Push(pasoactual[3]);
                        }
                        
                    }
                    Estado_Actual = Convert.ToInt32(pasoactual[4]);
                    if (pasoactual[1] != "")
                    {
                        Cadena = Cadena.Substring(1, Cadena.Length - 1);
                    }
                    
                    tempstring = Estado_Actual + " | " + ver_cadena()  +" | " + ver_valores_en_pila() ;
                    listBox1.Items.Add(tempstring);
                }
            }
            else
            {
                Verificartexto.Text = "Cadena no es aceptada";
                listBox1.Items.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 abrirform2 = new Form1();
            abrirform2.Show();
        }
        

        public void verificar()
        {
            for (int i = 0; i < MatrizTrans.Length / 5; i++)
            {
                if (CadenaAceptada == false)
                {
                    VerValor = true;
                    string[] PasoActual = new string[5];
                    if (Convert.ToInt32(MatrizTrans[i, 0]) == Estado_Actual)
                    {
                        if (MatrizTrans[i, 1] == revisarcadena()|| MatrizTrans[i, 1] == "")
                        {
                            if (MatrizTrans[i, 2] == revisarpilar() || MatrizTrans[i, 2] == "")
                            {
                                if (MatrizTrans[i, 1] != "")
                                {
                                    Cadena = Cadena.Substring(1, Cadena.Length - 1);
                                    PasoActual[1] = MatrizTrans[i, 1];
                                }
                                else
                                {
                                    PasoActual[1] = "";
                                }

                                if (MatrizTrans[i, 2] != "")
                                {
                                    Pila.Pop();
                                    PasoActual[2] = MatrizTrans[i, 2];
                                }
                                else
                                {
                                    PasoActual[2] = "";
                                }
                                if (MatrizTrans[i, 3] != "")
                                {
                                    if (MatrizTrans[i, 3].Length > 1)
                                    {
                                        foreach (var c in MatrizTrans[i, 3])
                                        {
                                            Pila.Push(Convert.ToString(c));
                                        }
                                    }
                                    else
                                    {
                                        Pila.Push(MatrizTrans[i, 3]);
                                    }
                                    
                                    PasoActual[3] = MatrizTrans[i, 3];
                                }
                                else
                                {
                                    PasoActual[3] = "";
                                }
                                PasoActual[0]= Convert.ToString(Estado_Actual);
                                Estado_Actual = Convert.ToInt32(MatrizTrans[i, 4]);
                                PasoActual[4] = Convert.ToString(Estado_Actual);
                            }
                            else
                            {
                                VerValor = false;
                            }
                        }
                        else
                        {
                            VerValor = false;
                        }

                    }
                    else
                    {
                        VerValor = false;
                    }

                    if (VerValor == true)
                    {

                        if (Pila.Count == 0)
                        {
                            if (EstadosFinales.Contains(Convert.ToString(Estado_Actual)))
                            {
                                if (Cadena == "")
                                {
                                    ListaPasosRealizados.Add(PasoActual);
                                    CadenaAceptada = true;
                                    return;
                                }
                            }
                        }
                        ListaPasosRealizados.Add(PasoActual);

                        verificar();
                    }
                }
                

            }
            regresarpaso();
            return;
        }


        public void regresarpaso()
        {
            if (ListaPasosRealizados.Count != 0)
            {
                if (CadenaAceptada == false)
                {
                    string[] PasoRealizado = ListaPasosRealizados[ListaPasosRealizados.Count - 1];
                    ListaPasosRealizados.RemoveAt(ListaPasosRealizados.Count - 1);
                    Estado_Actual = Convert.ToInt32(PasoRealizado[0]);
                    if (PasoRealizado[1] != "")
                    {
                        Cadena = PasoRealizado[1] + Cadena;
                    }
                    if (PasoRealizado[3] != "")
                    {
                        if (PasoRealizado[3].Length > 1)
                        {
                            realizarvariospop(PasoRealizado[3].Length);
                        }
                        else
                        {
                            Pila.Pop();
                        }
                        
                    }
                    if (PasoRealizado[2] != "")
                    {
                        Pila.Push(PasoRealizado[2]);
                    }
                }
            }
            
            return;
        }
        public string revisarpilar()
        {
            if (Pila.Count != 0)
            {
                return Pila.Peek();
            }
            return "";
        }
        
        public string revisarcadena()
        {
            if (Cadena.Length != 0)
            {
                return Cadena.Substring(0, 1);
            }
            return "";
        }
        
        public void resetearvalores()
        {
            ListaPasosRealizados = new List<string[]>();
            CadenaAceptada = false;
            Pila = new Stack<string>();
            Cadena = CadenaVerificar.Text;
            Estado_Actual = EstadoInicial;
            return;
        }

        public string ver_valores_en_pila()
        {
            string tempret = "";

            foreach (var x in Pila)
            {
                tempret = x + tempret;
            }
            if (tempret == "")
            {
                return "e";
            }

            return tempret;
        }

        public string ver_cadena()
        {
            if (Cadena.Length != 0)
            {
                return Cadena;
            }
            return "e";
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void realizarvariospop(int cantidad)
        {
            for (int i = 0; i < cantidad; i++)
            {
                Pila.Pop();
            }
            return;
        }
    }
}
