using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Universo2D
{
    class Universo2D
    {   // Força -> medida em N
        // Massa -> medida em Kg
        // Distância -> medida em m
        // G = 6,67408 X 10E-11 

        //private Corpo[] lstCorpos;
        private ObservableCollection<Corpo> lstCorpos; // Alterado para privado
        public ObservableCollection<Corpo> getLstCorpos() { return lstCorpos; }
        public void setLstCorpos(ObservableCollection<Corpo> lst) { this.lstCorpos = lst; }
        private double G = 6.67408 * Math.Pow(10, -11.0);

        public Universo2D()
        {
            lstCorpos = new ObservableCollection<Corpo>();
        }

        public Corpo getCorpo(int pos)
        {
            if ((pos >= 0) && (pos < lstCorpos.Count()))
            {
                return lstCorpos.ElementAt(pos);
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<Corpo> getCorpo()
        {
            return lstCorpos;
        }

        public void setCorpo(Corpo cp, int pos)
        {
            //Caso a posição de inserção seja dentro da região dos corpos, substitui o corpo na posição
            if (pos < lstCorpos.Count())
            {
                lstCorpos.ElementAt(pos).CopiaCorpo(cp);
            }
            else // Caso contrário, insere o corpo no final da região dos corpos
            {
                lstCorpos.Add(cp);
            }
        }

        public int qtdCorpos()
        {
            return lstCorpos.Count();
        }

        public double distancia(Corpo c1, Corpo c2)
        {
            double b, c;

            b = c1.PosY - c2.PosY;
            c = c1.PosX - c2.PosX;

            return Math.Sqrt(Math.Pow(b, 2) + Math.Pow(c, 2));
        }

        private void forcaG(Corpo c1, Corpo c2)
        {
            double hipotenusa = distancia(c2, c1);

            double catetoAdjacenteC1 = c2.PosY - c1.PosY;
            double catetoOpostoC1 = c2.PosX - c1.PosX;

            double forca = G * ((c1.Massa * c2.Massa) / (Math.Pow(hipotenusa, 2)));
            double forcaY = catetoAdjacenteC1 * forca / hipotenusa;
            double forcaX = catetoOpostoC1 * forca / hipotenusa;

            c1.ForcaX = (c1.ForcaX + forcaX);
            c1.ForcaY = (c1.ForcaY + forcaY);

            c2.ForcaX = (c2.ForcaX - forcaX);
            c2.ForcaY = (c2.ForcaY - forcaY);
        }

        private bool colisao(Corpo c1, Corpo c2)
        {
            double Px;
            double Py;
            double d;
            bool teveColisao = false;

            // Colisão somente caso a distância entre os corpos for menor ou igual à soma dos raios
            if ((distancia(c1, c2)) <= (c1.Raio + c2.Raio))
            {
                // Calcula a quantidade de movimento resultante -> P = m * v
                teveColisao = true;
                Px = (c1.Massa * c1.VelX) + (c2.Massa * c2.VelX);
                Py = (c1.Massa * c1.VelY) + (c2.Massa * c2.VelY);

                // Calcula a densidade resultante
                d = ((c1.Massa * c1.Densidade + c2.Massa * c2.Densidade) /
                     (c1.Massa + c2.Massa));

                // Caso haja colisão, o corpo de menor massa será engolido pelo de maior massa.
                if (c1.Massa >= c2.Massa)
                {
                    c1.Nome = (c1.Nome + c2.Nome);
                    c1.Massa = (c1.Massa + c2.Massa);
                    c1.Densidade = d;

                    // Calcula velocidade final do novo corpo
                    c1.VelX = (Px / c1.Massa);
                    c1.VelY = (Py / c1.Massa);

                    // Invalida o corpo 2, para retirá-lo da lista
                    c2.EValido = false;
                }
                else
                {
                    c2.Nome = (c2.Nome + c1.Nome);
                    c2.Massa = (c2.Massa + c1.Massa);
                    c2.Densidade = d;

                    // Calcula velocidade final do novo corpo
                    c2.VelX = (Px / c2.Massa);
                    c2.VelY = (Py / c2.Massa);

                    // Invalida o corpo 1, para retirá-lo da lista
                    c1.EValido = false;

                }
            }
            return teveColisao;
        }

        public void carregaCorpos(int numCorpos, int xIni, int xFim, int yIni, int yFim, int masIni, int masFim)
        {
            int i;
            string nome;
            int massa;

            Random rd = new Random();

            for (i = 0; i < numCorpos; i++)
            {
                nome = "cp" + i;
                massa = rd.Next(masIni, masFim);
                lstCorpos.Add(new Corpo(nome, massa, rd.Next(xIni, xFim), rd.Next(yIni, yFim), 0, 0, 0, 0, rd.Next(1, 255)));
            }
        }

        public void interacaoCorpos(int qtdSegundos)
        {
            bool teveColisao = false;
            zeraForcas();
            int i;

            // Trata as colisões
            for (i = 0; i < qtdCorpos() - 1; i++)
            {
                for (int j = i + 1; j < qtdCorpos(); j++)
                {
                    if (colisao(lstCorpos.ElementAt(i), lstCorpos.ElementAt(j)))
                    {
                        teveColisao = true;
                    }
                }
            }

            if (teveColisao)
            {
                OrganizaUniverso();
            }

            // Calcula a força final em cada corpo do Universo
            for (i = 0; i < qtdCorpos() - 1; i++)
            {
                for (int j = i + 1; j < qtdCorpos(); j++)
                {
                    forcaG(lstCorpos.ElementAt(i), lstCorpos.ElementAt(j));
                }

                // Calcula a velocidade e a posição final de cada corpo no Universo
                calculaVelPosCorpos(qtdSegundos, lstCorpos.ElementAt(i));
            }
            // Calcula a velocidade e a posição final do último corpo no Universo
            calculaVelPosCorpos(qtdSegundos, lstCorpos.ElementAt(i));

        }

        public void interacaoCorpos(int qtdInteracoes, int qtdSegundos)
        {

            while (qtdInteracoes > 0)
            {
                bool teveColisao = false;
                zeraForcas();
                int i = 0;

                // Calcula a força final em cada corpo do Universo
                for (i = 0; i < qtdCorpos() - 1; i++)
                {
                    for (int j = i + 1; j < qtdCorpos(); j++)
                    {
                        forcaG(lstCorpos[i], lstCorpos[j]);
                    }

                    // Calcula a velocidade e a posição final de cada corpo no Universo
                    calculaVelPosCorpos(qtdSegundos, lstCorpos[i]);
                }
                // Calcula a velocidade e a posição final do último corpo no Universo
                calculaVelPosCorpos(qtdSegundos, lstCorpos[i]);

                // Trata as colisões
                for (i = 0; i < qtdCorpos() - 1; i++)
                {
                    for (int j = i + 1; j < qtdCorpos(); j++)
                    {
                        if (colisao(lstCorpos[i], lstCorpos[j]))
                        {
                            teveColisao = true;
                        }
                    }
                }

                if (teveColisao)
                {
                    OrganizaUniverso();
                }

                qtdInteracoes--;
            } //while (qtdInteracoes > 0)
        }

        public void copiaUniverso(Universo2D u)
        {
            lstCorpos = new ObservableCollection<Corpo>();
            Corpo cp;
            for (int i = 0; i < u.qtdCorpos(); i++)
            {
                cp = new Corpo(u.getCorpo(i).Nome,
                               u.getCorpo(i).Massa,
                               u.getCorpo(i).PosX,
                               u.getCorpo(i).PosY,
                               u.getCorpo(i).PosZ,
                               u.getCorpo(i).VelX,
                               u.getCorpo(i).VelY,
                               u.getCorpo(i).VelZ,
                               u.getCorpo(i).Densidade);
                this.setCorpo(cp, i);
            }
        }

        private void zeraForcas()
        {
            for (int i = 0; i < qtdCorpos(); i++)
            {
                // Zera as forças da interação
                lstCorpos.ElementAt(i).ForcaX = 0;
                lstCorpos.ElementAt(i).ForcaY = 0;
                lstCorpos.ElementAt(i).ForcaZ = 0;
            }
        }

        private void calculaVelPosCorpos(int qtdSegundos, Corpo c1)
        {
            double acelX;
            double acelY;

            acelX = c1.ForcaX / c1.Massa;
            acelY = c1.ForcaY / c1.Massa;

            c1.PosX = (c1.PosX + (c1.VelX * qtdSegundos) + (acelX * Math.Pow(qtdSegundos, 2) / 2));
            c1.VelX = (c1.VelX + (acelX * qtdSegundos));

            c1.PosY = (c1.PosY + (c1.VelY * qtdSegundos) + (acelY * Math.Pow(qtdSegundos, 2) / 2));
            c1.VelY = (c1.VelY + (acelY * qtdSegundos));

        }


        private void OrganizaUniverso()
        {
            int i;
            for (i = 0; i < qtdCorpos(); i++)
            {
                if (!lstCorpos.ElementAt(i).EValido)
                {
                    lstCorpos.RemoveAt(i);
                }
            }
        }
    }
}