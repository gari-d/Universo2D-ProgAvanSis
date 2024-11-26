using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Universo2D
{

    public partial class Form1 : Form
    {
        private Graphics g;
        private Universo2D U, Uinicial;
        int numCorpos;
        int numInterac;
        int numTempoInterac;


        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int xMax, yMax, mMin, mMax;

            numCorpos = Convert.ToInt32(qtdCorpos.Text);
            U = new Universo2D();

            progressBar1.Value = 0;
            xMax = Convert.ToInt32(valXMax.Text);
            yMax = Convert.ToInt32(valYMax.Text);
            mMin = Convert.ToInt32(masMin.Text);
            mMax = Convert.ToInt32(masMax.Text);

            U.CarregaCorpos(numCorpos, 0, xMax, 0, yMax, mMin, mMax);

            Uinicial = new Universo2D();
            Uinicial.CopiaUniverso(U);

            Form1.ActiveForm.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numInterac = Convert.ToInt32(qtdInterac.Text);
            numTempoInterac = Convert.ToInt32(qtdTempoInterac.Text);

            progressBar1.Maximum = numInterac;
            progressBar1.Minimum = 0;

            if (radioButton1.Checked) //Atualiza Tela
            {
                for (int i = 0; i <= numInterac; i++)
                {
                    U.InteracaoCorpos(numTempoInterac);
                    progressBar1.Value = i;


                    // Plota os corpos a cada 100 interações
                    if ((i % 100 == 0) && (Form1.ActiveForm != null))
                    {
                        Form1.ActiveForm.Refresh();
                    }
                }
            }
            else if (radioButton2.Checked) //Background
            {
                for (int i = 0; i <= numInterac; i++)
                {
                    U.InteracaoCorpos(numTempoInterac);
                    progressBar1.Value = i;
                }

                // Plota os corpos ao final das interações
                if (Form1.ActiveForm != null)
                {
                    Form1.ActiveForm.Refresh();
                }
            }
            else if (radioButton3.Checked) //Para Arquivo
            {
                string texto;
                Corpo cp;

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
                saveFileDialog1.Title = "Salvar arquivo";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);

                    // Grava a quantidade total de corpos no Universo e a quantidade de interações
                    sw.WriteLine(U.QtdCorpos() + ";" + numInterac);

                    // Faz as interações
                    for (int i = 0; i <= numInterac; i++)
                    {
                        U.InteracaoCorpos(numTempoInterac);
                        progressBar1.Value = i;

                        // A cada 10 interações, grava a situação dos corpos no arquivo
                        if (i % 10 == 0)
                        {
                            texto = "** Interacao " + i + " ************";
                            sw.WriteLine(texto);

                            for (int j = 0; j < U.QtdCorpos(); j++)
                            {
                                cp = U.GetCorpo(j);
                                if (cp != null)
                                {
                                    texto = cp.Nome + ";"
                                            + cp.Massa + ";"
                                            + cp.PosX + ";"
                                            + cp.PosY + ";"
                                            + cp.PosZ + ";"
                                            + cp.VelX + ";"
                                            + cp.VelY + ";"
                                            + cp.VelZ + ";"
                                            + cp.Densidade;

                                    sw.WriteLine(texto);
                                }
                            }
                        }
                    }

                    sw.Close();
                    fs.Close();
                }
            }
        }

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs pe)
        {
            Corpo cp;
            float prop = 1, propX = 1, propY = 1;
            float deslocX = 0;
            float deslocY = 0;
            float maxX = 0, W = 0, H = 0, maxY = 0;
            double posX = 0, posY = 0;
            int qtdCp;

            if (Form1.ActiveForm != null)
            {

                if (valXMax.Text == "")
                {
                    valXMax.Text = Form1.ActiveForm.Size.Width.ToString();
                    valYMax.Text = Form1.ActiveForm.Size.Height.ToString();
                }

                W = Form1.ActiveForm.Size.Width - 50;
                H = Form1.ActiveForm.Size.Height - 50;
            }

            if (U != null)
            {
                g = pe.Graphics;
                qtdCp = U.QtdCorpos();

                //Calcula a proporção da tela e deslocamento

                for (int i = 0; i < qtdCp; i++)
                {
                    cp = U.GetCorpo(i);
                    if (cp != null)
                    {
                        posX = cp.PosX;
                        posY = cp.PosY;

                        // Busca os menores valores de X e Y para Deslocamento
                        if (posX < deslocX)
                        {
                            deslocX = (float)posX;
                        }
                        if (posY < deslocY)
                        {
                            deslocY = (float)posY;
                        }

                        // Busca os maiores valores de X e Y para Proporção
                        if (posY > maxY)
                        {
                            maxY = (float)posY;
                        }
                        if (posX > maxX)
                        {
                            maxX = (float)posX;
                        }
                    }
                }

                if ((maxX - deslocX) > W)
                {
                    propX = (maxX - deslocX) / W;
                }
                if ((maxY - deslocY) > H)
                {
                    propY = (maxY - deslocY) / H;
                }

                if (propX > propY)
                {
                    prop = propX;
                }
                else
                {
                    prop = propY;
                }

                txtProporcao.Text = (1 / prop).ToString();
                qtdCorposAtual.Text = qtdCp.ToString();

                // Desenha o corpo
                for (int i = 0; i < qtdCp; i++)
                {
                    cp = U.GetCorpo(i);
                    if (cp != null)
                    {
                        posX = cp.PosX - deslocX;
                        posY = cp.PosY - deslocY;

                        // Posição do corpo
                        g.DrawEllipse(new Pen(Color.FromArgb((int)cp.Densidade, 0, 0)),
                            (float)(posX - cp.Raio) / prop,
                            (float)(posY - cp.Raio) / prop,
                            (float)(cp.Raio * 2) / prop,
                            (float)(cp.Raio * 2) / prop);

                        // Barras das forças em X e Y
                        g.DrawLine(new Pen(Color.FromArgb(0, 0, 255)),
                            (float)(posX) / prop,
                            (float)(posY) / prop,
                            (float)(posX + (cp.ForcaX * 50)) / prop,
                            (float)(posY) / prop);
                        g.DrawLine(new Pen(Color.FromArgb(0, 0, 255)),
                            (float)posX / prop,
                            (float)posY / prop,
                            (float)(posX) / prop,
                            (float)(posY + (cp.ForcaY * 50)) / prop);
                    }
                }
            }
        }

        private void btn_grava_Click(object sender, EventArgs e)
        {
            Corpo cp;
            int i;
            string texto;

            if (U != null)
            {

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
                saveFileDialog1.Title = "Salvar arquivo";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);

                    sw.WriteLine(U.QtdCorpos());
                    for (i = 0; i < U.QtdCorpos(); i++)
                    {
                        cp = U.GetCorpo(i);
                        if (cp != null)
                        {
                            texto = cp.Nome + ";"
                                  + cp.Massa + ";"
                                  + cp.PosX + ";"
                                  + cp.PosY + ";"
                                  + cp.PosZ + ";"
                                  + cp.VelX + ";"
                                  + cp.VelY + ";"
                                  + cp.VelZ + ";"
                                  + cp.Densidade;

                            sw.WriteLine(texto);
                        }
                    }

                    sw.Close();
                    fs.Close();
                }
            }
            else // Não ha corpos no Universo
            {
                MessageBox.Show("Não há corpos no Universo a serem salvos", "Atenção");
            }
        }

        private void btn_grava_ini_Click(object sender, EventArgs e)
        {
            Corpo cp;
            int i;
            string texto;

            if (Uinicial != null)
            {
                // Displays a SaveFileDialog so the user can save the Image
                // assigned to Button2.
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
                saveFileDialog1.Title = "Salvar arquivo";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);

                    sw.WriteLine(Uinicial.QtdCorpos());
                    for (i = 0; i < Uinicial.QtdCorpos(); i++)
                    {
                        cp = Uinicial.GetCorpo(i);
                        if (cp != null)
                        {
                            texto = cp.Nome + ";"
                                  + cp.Massa + ";"
                                  + cp.PosX + ";"
                                  + cp.PosY + ";"
                                  + cp.PosZ + ";"
                                  + cp.VelX + ";"
                                  + cp.VelY + ";"
                                  + cp.VelZ + ";"
                                  + cp.Densidade;

                            sw.WriteLine(texto);
                        }
                    }

                    sw.Close();
                    fs.Close();
                }
            }
            else // Não ha corpos no Universo
            {
                MessageBox.Show("Não há corpos no Universo a serem salvos", "Atenção");
            }

        }

        private void btn_carrega_Click(object sender, EventArgs e)
        {
            string texto;
            int controle;
            Corpo cp;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
            openFileDialog1.Title = "Abrir arquivo";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                controle = 0;

                while (!sr.EndOfStream)
                {
                    texto = sr.ReadLine();
                    if (controle != 0)
                    {
                        string[] valores = texto.Split(';');

                        cp = new Corpo(valores[0],
                                       Convert.ToDouble(valores[1]),
                                       Convert.ToDouble(valores[2]),
                                       Convert.ToDouble(valores[3]),
                                       Convert.ToDouble(valores[4]),
                                       Convert.ToDouble(valores[5]),
                                       Convert.ToDouble(valores[6]),
                                       Convert.ToDouble(valores[7]),
                                       Convert.ToDouble(valores[8]));
                        U.SetCorpo(cp, controle - 1);
                    }
                    else
                    {
                        qtdCorpos.Text = texto;

                        numCorpos = Convert.ToInt32(qtdCorpos.Text);
                        U = new Universo2D();
                    }
                    controle++;
                }

                sr.Close();

                progressBar1.Value = 0;

                Uinicial = new Universo2D();
                Uinicial.CopiaUniverso(U);

                Form1.ActiveForm.Refresh();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_carregaSimulacao_Click(object sender, EventArgs e)
        {
            string texto;
            string[] valores;
            int controle;
            Corpo cp;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
            openFileDialog1.Title = "Abrir arquivo";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);

                // Le a primeira linah do arquivo, onde está o número inicial de corpos                
                texto = sr.ReadLine();

                valores = texto.Split(';');

                qtdCorpos.Text = valores[0]; // Posição 0 armazena o número de corpos
                qtdInterac.Text = valores[1]; // Posição 1 armazena a quantidade de interações
                progressBar1.Maximum = (Convert.ToInt32(qtdInterac.Text) / 10) + 1;

                numCorpos = Convert.ToInt32(qtdCorpos.Text);

                controle = 0;
                progressBar1.Value = 0;

                while (!sr.EndOfStream)
                {
                    texto = sr.ReadLine();

                    //Identifica o início da interação
                    if (texto[0] == '*')
                    {
                        controle = 0;
                        progressBar1.Value++;

                        if (Form1.ActiveForm != null)
                        {
                            Form1.ActiveForm.Refresh();
                        }

                        U = new Universo2D();
                    }
                    else // Carrega uma interação
                    {
                        valores = texto.Split(';');

                        cp = new Corpo(valores[0],
                                       Convert.ToDouble(valores[1]),
                                       Convert.ToDouble(valores[2]),
                                       Convert.ToDouble(valores[3]),
                                       Convert.ToDouble(valores[4]),
                                       Convert.ToDouble(valores[5]),
                                       Convert.ToDouble(valores[6]),
                                       Convert.ToDouble(valores[7]),
                                       Convert.ToDouble(valores[8]));
                        U.SetCorpo(cp, controle);
                        controle++;
                    }
                }

                sr.Close();

                if (Form1.ActiveForm != null)
                {
                    Form1.ActiveForm.Refresh();
                }
            }
        }
    }
}