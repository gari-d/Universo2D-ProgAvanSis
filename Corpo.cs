using System;

namespace Universo2D
{
    public class Corpo
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

        public double Raio => Math.Pow((3 * Math.PI * Massa) / (4 * Densidade), 1.0 / 3) / 5;

        public void CopiaCorpo(Corpo cp)
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