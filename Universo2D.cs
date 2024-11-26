using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Universo2D
{
    public class Universo2D : IUniverso2D
    {
        private ObservableCollection<Corpo> lstCorpos;
        public ObservableCollection<Corpo> GetLstCorpos() { return lstCorpos; }
        public void SetLstCorpos(ObservableCollection<Corpo> lst) { this.lstCorpos = lst; }

        private double G = 6.67408 * Math.Pow(10, -11.0);

        public Universo2D()
        {
            lstCorpos = new ObservableCollection<Corpo>();
        }

        public Corpo GetCorpo(int pos)
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

        public void SetCorpo(Corpo cp, int pos)
        {
            if (pos < lstCorpos.Count())
            {
                lstCorpos.ElementAt(pos).CopiaCorpo(cp);
            }
            else
            {
                lstCorpos.Add(cp);
            }
        }

        public int QtdCorpos()
        {
            return lstCorpos.Count();
        }

        public double Distancia(Corpo c1, Corpo c2)
        {
            double b = c1.PosY - c2.PosY;
            double c = c1.PosX - c2.PosX;
            return Math.Sqrt(Math.Pow(b, 2) + Math.Pow(c, 2));
        }

        public void CarregaCorpos(int numCorpos, int xIni, int xFim, int yIni, int yFim, int masIni, int masFim)
        {
            Random rd = new Random();
            for (int i = 0; i < numCorpos; i++)
            {
                string nome = "cp" + i;
                int massa = rd.Next(masIni, masFim);
                lstCorpos.Add(new Corpo(nome, massa, rd.Next(xIni, xFim), rd.Next(yIni, yFim), 0, 0, 0, 0, rd.Next(1, 255)));
            }
        }

        public void InteracaoCorpos(int qtdSegundos)
        {
            bool teveColisao = false;
            ZeraForcas();
            for (int i = 0; i < QtdCorpos() - 1; i++)
            {
                for (int j = i + 1; j < QtdCorpos(); j++)
                {
                    if (Colisao(lstCorpos.ElementAt(i), lstCorpos.ElementAt(j)))
                    {
                        teveColisao = true;
                    }
                }
            }

            if (teveColisao)
            {
                OrganizaUniverso();
            }

            for (int i = 0; i < QtdCorpos() - 1; i++)
            {
                for (int j = i + 1; j < QtdCorpos(); j++)
                {
                    ForcaG(lstCorpos.ElementAt(i), lstCorpos.ElementAt(j));
                }
                CalculaVelPosCorpos(qtdSegundos, lstCorpos.ElementAt(i));
            }
            CalculaVelPosCorpos(qtdSegundos, lstCorpos.ElementAt(QtdCorpos() - 1));
        }

        public void InteracaoCorpos(int qtdInteracoes, int qtdSegundos)
        {
            while (qtdInteracoes > 0)
            {
                bool teveColisao = false;
                ZeraForcas();
                for (int i = 0; i < QtdCorpos() - 1; i++)
                {
                    for (int j = i + 1; j < QtdCorpos(); j++)
                    {
                        ForcaG(lstCorpos[i], lstCorpos[j]);
                    }
                    CalculaVelPosCorpos(qtdSegundos, lstCorpos[i]);
                }
                CalculaVelPosCorpos(qtdSegundos, lstCorpos[QtdCorpos() - 1]);

                for (int i = 0; i < QtdCorpos() - 1; i++)
                {
                    for (int j = i + 1; j < QtdCorpos(); j++)
                    {
                        if (Colisao(lstCorpos[i], lstCorpos[j]))
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
            }
        }

        public void CopiaUniverso(IUniverso2D u)
        {
            lstCorpos = new ObservableCollection<Corpo>();
            Corpo cp;
            for (int i = 0; i < u.QtdCorpos(); i++)
            {
                cp = new Corpo(u.GetCorpo(i).Nome,
                               u.GetCorpo(i).Massa,
                               u.GetCorpo(i).PosX,
                               u.GetCorpo(i).PosY,
                               u.GetCorpo(i).PosZ,
                               u.GetCorpo(i).VelX,
                               u.GetCorpo(i).VelY,
                               u.GetCorpo(i).VelZ,
                               u.GetCorpo(i).Densidade);
                this.SetCorpo(cp, i);
            }
        }

        private void ZeraForcas()
        {
            for (int i = 0; i < QtdCorpos(); i++)
            {
                lstCorpos.ElementAt(i).ForcaX = 0;
                lstCorpos.ElementAt(i).ForcaY = 0;
                lstCorpos.ElementAt(i).ForcaZ = 0;
            }
        }

        private void OrganizaUniverso()
        {
            for (int i = 0; i < QtdCorpos(); i++)
            {
                if (!lstCorpos.ElementAt(i).EValido)
                {
                    lstCorpos.RemoveAt(i);
                }
            }
        }

        private void ForcaG(Corpo c1, Corpo c2)
        {
            double hipotenusa = Distancia(c2, c1);
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

        private bool Colisao(Corpo c1, Corpo c2)
        {
            double Px;
            double Py;
            bool teveColisao = false;

            // Colisão somente caso a distância entre os corpos for menor ou igual à soma dos raios
            if ((Distancia(c1, c2)) <= (c1.Raio + c2.Raio))
            {
                // Calcula a quantidade de movimento resultante -> P = m * v
                teveColisao = true;
                Px = (c1.Massa * c1.VelX) + (c2.Massa * c2.VelX);
                Py = (c1.Massa * c1.VelY) + (c2.Massa * c2.VelY);

                // Caso haja colisão, o corpo de menor massa será engolido pelo de maior massa.
                if (c1.Massa >= c2.Massa)
                {
                    // Cria o corpo resultante com a sobrecarga do operador
                    Corpo resultado = c1 + c2;

                    // Atualiza c1 com as propriedades do resultado
                    c1.Nome = resultado.Nome;
                    c1.Massa = resultado.Massa;
                    c1.Densidade = resultado.Densidade;
                    c1.PosX = resultado.PosX;
                    c1.PosY = resultado.PosY;
                    c1.PosZ = resultado.PosZ;
                    c1.VelX = Px / c1.Massa; // Calcula velocidade final
                    c1.VelY = Py / c1.Massa;

                    // Invalida o corpo 2, para retirá-lo da lista
                    c2.EValido = false;
                }
                else
                {
                    // Cria o corpo resultante com a sobrecarga do operador
                    Corpo resultado = c2 + c1;

                    // Atualiza c2 com as propriedades do resultado
                    c2.Nome = resultado.Nome;
                    c2.Massa = resultado.Massa;
                    c2.Densidade = resultado.Densidade;
                    c2.PosX = resultado.PosX;
                    c2.PosY = resultado.PosY;
                    c2.PosZ = resultado.PosZ;
                    c2.VelX = Px / c2.Massa; // Calcula velocidade final
                    c2.VelY = Py / c2.Massa;

                    // Invalida o corpo 1, para retirá-lo da lista
                    c1.EValido = false;
                }
            }
            return teveColisao;
        }

        private void CalculaVelPosCorpos(int qtdSegundos, Corpo c1)
        {
            double acelX = c1.ForcaX / c1.Massa;
            double acelY = c1.ForcaY / c1.Massa;

            c1.PosX = (c1.PosX + (c1.VelX * qtdSegundos) + (acelX * Math.Pow(qtdSegundos, 2) / 2));
            c1.VelX = (c1.VelX + (acelX * qtdSegundos));

            c1.PosY = (c1.PosY + (c1.VelY * qtdSegundos) + (acelY * Math.Pow(qtdSegundos, 2) / 2));
            c1.VelY = (c1.VelY + (acelY * qtdSegundos));
        }
    }
}