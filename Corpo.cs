using System;

namespace Universo2D
{
    public class Corpo : ICorpo
    {
        public bool EValido { get; set; } = true;
        public string Nome { get; set; } = "";
        public double Massa { get; set; } = 0;
        public double Densidade { get; set; } = 1;
        public double PosX { get; set; } = 0;
        public double PosY { get; set; } = 0;
        public double PosZ { get; set; } = 0;
        public double VelX { get; set; } = 0;
        public double VelY { get; set; } = 0;
        public double VelZ { get; set; } = 0;
        public double ForcaX { get; set; } = 0;
        public double ForcaY { get; set; } = 0;
        public double ForcaZ { get; set; } = 0;

        public Corpo() { }

        public Corpo(string n, double m, double pX, double pY, double pZ, double vX, double vY, double vZ, double d)
        {
            EValido = true;
            Nome = n;
            Massa = m;
            PosX = pX;
            PosY = pY;
            PosZ = pZ;
            VelX = vX;
            VelY = vY;
            VelZ = vZ;
            ForcaX = 0;
            ForcaY = 0;
            ForcaZ = 0;
            Densidade = d;
        }

        // Sobrecarga do operador "+"
        public static Corpo operator +(Corpo c1, Corpo c2)
        {
            // Criando um novo corpo que representa a combinação de ambos
            Corpo resultado = new Corpo();

            // Soma de propriedades como massa, posição, densidade, e velocidade
            resultado.Massa = c1.Massa + c2.Massa; // Soma da massa
            resultado.Densidade = (c1.Massa * c1.Densidade + c2.Massa * c2.Densidade) / resultado.Massa; // Densidade média ponderada
            resultado.PosX = (c1.PosX * c1.Massa + c2.PosX * c2.Massa) / resultado.Massa; // Posição ponderada pela massa
            resultado.PosY = (c1.PosY * c1.Massa + c2.PosY * c2.Massa) / resultado.Massa;
            resultado.PosZ = (c1.PosZ * c1.Massa + c2.PosZ * c2.Massa) / resultado.Massa;

            // Velocidade média ponderada
            resultado.VelX = (c1.VelX * c1.Massa + c2.VelX * c2.Massa) / resultado.Massa;
            resultado.VelY = (c1.VelY * c1.Massa + c2.VelY * c2.Massa) / resultado.Massa;
            resultado.VelZ = (c1.VelZ * c1.Massa + c2.VelZ * c2.Massa) / resultado.Massa;

            // Nome é a junção dos nomes dos corpos, pode ser ajustado conforme necessário
            resultado.Nome = c1.Nome + " & " + c2.Nome;

            return resultado;
        }

        public double Raio => Math.Pow((3 * Math.PI * Massa) / (4 * Densidade), 1.0 / 3) / 5;

        public void CopiaCorpo(ICorpo cp)
        {
            Nome = cp.Nome;
            Massa = cp.Massa;
            PosX = cp.PosX;
            PosY = cp.PosY;
            PosZ = cp.PosZ;
            VelX = cp.VelX;
            VelY = cp.VelY;
            VelZ = cp.VelZ;
        }
    }
}