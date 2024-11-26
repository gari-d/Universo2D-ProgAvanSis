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
using System.IO;

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


            xMax = 1538;
            yMax = 821;
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



            for (int i = 0; i <= numInterac; i++)
            {
                U.InteracaoCorpos(numTempoInterac);



                // Plota os corpos a cada 100 interações
                if ((i % 100 == 0) && (Form1.ActiveForm != null))
                {
                    Form1.ActiveForm.Refresh();
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
            int controle = 0;
            Corpo cp;

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*",
                Title = "Abrir arquivo"
            };

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName))
                {
                    // Ler a primeira linha para definir a quantidade de corpos
                    if (!sr.EndOfStream)
                    {
                        texto = sr.ReadLine();
                        if (int.TryParse(texto, out int numCorposLidos))
                        {
                            numCorpos = numCorposLidos; // Atualizar a variável global
                            qtdCorpos.Text = numCorpos.ToString();
                            U = new Universo2D();
                        }
                        else
                        {
                            MessageBox.Show("Erro: a primeira linha do arquivo deve conter a quantidade de corpos como um número inteiro.",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Ler as demais linhas para criar os corpos
                    while (!sr.EndOfStream)
                    {
                        texto = sr.ReadLine();
                        string[] valores = texto.Split(';');

                        if (valores.Length == 9) // Verificar o número esperado de valores
                        {
                            try
                            {
                                // Criar o objeto Corpo com os valores lidos
                                cp = new Corpo(
                                    valores[0],
                                    Convert.ToDouble(valores[1].Replace(',', '.')),
                                    Convert.ToDouble(valores[2].Replace(',', '.')),
                                    Convert.ToDouble(valores[3].Replace(',', '.')),
                                    Convert.ToDouble(valores[4].Replace(',', '.')),
                                    Convert.ToDouble(valores[5].Replace(',', '.')),
                                    Convert.ToDouble(valores[6].Replace(',', '.')),
                                    Convert.ToDouble(valores[7].Replace(',', '.')),
                                    Convert.ToDouble(valores[8].Replace(',', '.'))
                                );
                                U.SetCorpo(cp, controle);
                                controle++;
                            }
                            catch (FormatException ex)
                            {
                                MessageBox.Show($"Erro ao processar a linha: {texto}\n{ex.Message}",
                                    "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Linha ignorada: número de valores incorreto.\nLinha: {texto}",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                // Copiar o universo carregado para o universo inicial
                Uinicial = new Universo2D();
                Uinicial.CopiaUniverso(U);

                // Atualizar a interface
                Form1.ActiveForm.Refresh();
            }
        }


        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Alterando a cor de fundo do Form
            this.BackColor = Color.LightSkyBlue;

            // Personalizando a label
            label2.ForeColor = Color.Black;
            label2.Font = new Font("Arial", 10);

            // Personalizando o botão
            btn_aleatorio.ForeColor = Color.DarkSlateGray;
            btn_aleatorio.Font = new Font("Arial", 10);


            groupBox1.ForeColor = Color.Black;
        }

        private void btn_carregaSimulacao_Click(object sender, EventArgs e)
        {
            string texto;
            string[] valores;
            int controle;
            Corpo cp;

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*",
                Title = "Abrir arquivo"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    // Lê a primeira linha do arquivo, onde está o número de corpos e interações
                    texto = sr.ReadLine();
                    valores = texto.Split(';');

                    // Carrega as informações da primeira linha
                    qtdCorpos.Text = valores[0];  // Número de corpos

                    numCorpos = Convert.ToInt32(qtdCorpos.Text);
                    controle = 0;

                    // Inicializa o objeto U e a lista de corpos
                    U = new Universo2D();
                    while (U.lstCorpos.Count < numCorpos)
                    {
                        U.lstCorpos.Add(new Corpo());
                    }

                    // Lê as interações (corpos) a partir da segunda linha
                    while (!sr.EndOfStream)
                    {
                        texto = sr.ReadLine();

                        // Verifica se a linha começa com '*', indicando uma nova interação
                        if (texto[0] == '*')
                        {
                            controle = 0;
                            if (ActiveForm != null)
                            {
                                ActiveForm.Refresh();
                            }

                            U = new Universo2D();

                            // Reinicializa a lista de corpos para a nova interação
                            while (U.lstCorpos.Count < numCorpos)
                            {
                                U.lstCorpos.Add(new Corpo());
                            }
                        }
                        else // Carrega os corpos a partir da segunda linha
                        {
                            valores = texto.Split(';');

                            if (controle >= numCorpos)
                            {
                                MessageBox.Show("Número de corpos no arquivo excede o esperado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }

                            // Criação e adição de um corpo na lista
                            cp = new Corpo(valores[0],
                                           Convert.ToDouble(valores[1]),
                                           Convert.ToDouble(valores[2]),
                                           Convert.ToDouble(valores[3]),
                                           Convert.ToDouble(valores[4]),
                                           Convert.ToDouble(valores[5]),
                                           Convert.ToDouble(valores[6]),
                                           Convert.ToDouble(valores[7]),
                                           Convert.ToDouble(valores[8]));
                            U.SetCorpo(cp, controle); // Atualiza o corpo na posição controle
                            controle++;
                        }
                    }
                }

                if (ActiveForm != null)
                {
                    ActiveForm.Refresh();
                }
            }
        }


    }
}