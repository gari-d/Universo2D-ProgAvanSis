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
                lstCorpos.ElementAt(pos).copiaCorpo(cp);
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

            b = c1.getPosY() - c2.getPosY();
            c = c1.getPosX() - c2.getPosX();

            return Math.Sqrt(Math.Pow(b, 2) + Math.Pow(c, 2));
        }

        private void forcaG(Corpo c1, Corpo c2)
        {
            double hipotenusa = distancia(c2, c1);

            double catetoAdjacenteC1 = c2.getPosY() - c1.getPosY();
            double catetoOpostoC1 = c2.getPosX() - c1.getPosX();

            double forca = G * ((c1.getMassa() * c2.getMassa()) / (Math.Pow(hipotenusa, 2)));
            double forcaY = catetoAdjacenteC1 * forca / hipotenusa;
            double forcaX = catetoOpostoC1 * forca / hipotenusa;

            c1.setForcaX(c1.getForcaX() + forcaX);
            c1.setForcaY(c1.getForcaY() + forcaY);

            c2.setForcaX(c2.getForcaX() - forcaX);
            c2.setForcaY(c2.getForcaY() - forcaY);
        }

        private bool colisao(Corpo c1, Corpo c2)
        {
            double Px;
            double Py;
            double d;
            bool teveColisao = false;

            // Colisão somente caso a distância entre os corpos for menor ou igual à soma dos raios
            if ((distancia(c1, c2)) <= (c1.getRaio() + c2.getRaio()))
            {
                // Calcula a quantidade de movimento resultante -> P = m * v
                teveColisao = true;
                Px = (c1.getMassa() * c1.getVelX()) + (c2.getMassa() * c2.getVelX());
                Py = (c1.getMassa() * c1.getVelY()) + (c2.getMassa() * c2.getVelY());

                // Calcula a densidade resultante
                d = ((c1.getMassa() * c1.getDensidade() + c2.getMassa() * c2.getDensidade()) /
                     (c1.getMassa() + c2.getMassa()));

                // Caso haja colisão, o corpo de menor massa será engolido pelo de maior massa.
                if (c1.getMassa() >= c2.getMassa())
                {
                    c1.setNome(c1.getNome() + c2.getNome());
                    c1.setMassa(c1.getMassa() + c2.getMassa());
                    c1.setDensidade(d);

                    // Calcula velocidade final do novo corpo
                    c1.setVelX(Px / c1.getMassa());
                    c1.setVelY(Py / c1.getMassa());

                    // Invalida o corpo 2, para retirá-lo da lista
                    c2.setValido(false);
                }
                else
                {
                    c2.setNome(c2.getNome() + c1.getNome());
                    c2.setMassa(c2.getMassa() + c1.getMassa());
                    c2.setDensidade(d);

                    // Calcula velocidade final do novo corpo
                    c2.setVelX(Px / c2.getMassa());
                    c2.setVelY(Py / c2.getMassa());

                    // Invalida o corpo 1, para retirá-lo da lista
                    c1.setValido(false);

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
                lstCorpos.Add(new Corpo(nome, massa,
                                              rd.Next(xIni, xFim), rd.Next(yIni, yFim), 0,
                                              0, 0, 0, rd.Next(1, 255)));
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
                cp = new Corpo(u.getCorpo(i).getNome(),
                               u.getCorpo(i).getMassa(),
                               u.getCorpo(i).getPosX(),
                               u.getCorpo(i).getPosY(),
                               u.getCorpo(i).getPosZ(),
                               u.getCorpo(i).getVelX(),
                               u.getCorpo(i).getVelY(),
                               u.getCorpo(i).getVelZ(),
                               u.getCorpo(i).getDensidade());
                this.setCorpo(cp, i);
            }
        }

        private void zeraForcas()
        {
            for (int i = 0; i < qtdCorpos(); i++)
            {
                // Zera as forças da interação
                lstCorpos.ElementAt(i).setForcaX(0);
                lstCorpos.ElementAt(i).setForcaY(0);
                lstCorpos.ElementAt(i).setForcaZ(0);
            }
        }

        private void calculaVelPosCorpos(int qtdSegundos, Corpo c1)
        {
            double acelX;
            double acelY;

            acelX = c1.getForcaX() / c1.getMassa();
            acelY = c1.getForcaY() / c1.getMassa();

            c1.setPosX(c1.getPosX() + (c1.getVelX() * qtdSegundos) + (acelX * Math.Pow(qtdSegundos, 2) / 2));
            c1.setVelX(c1.getVelX() + (acelX * qtdSegundos));

            c1.setPosY(c1.getPosY() + (c1.getVelY() * qtdSegundos) + (acelY * Math.Pow(qtdSegundos, 2) / 2));
            c1.setVelY(c1.getVelY() + (acelY * qtdSegundos));

        }


        private void OrganizaUniverso()
        {
            int i;
            for (i = 0; i < qtdCorpos(); i++)
            {
                if (!lstCorpos.ElementAt(i).getValido())
                {
                    lstCorpos.RemoveAt(i);
                }
            }
        }
    }
}