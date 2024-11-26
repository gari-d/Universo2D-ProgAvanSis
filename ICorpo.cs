using System;

namespace Universo2D
{
    public interface ICorpo
    {
        bool EValido { get; set; }
        string Nome { get; set; }
        double Massa { get; set; }
        double Densidade { get; set; }
        double PosX { get; set; }
        double PosY { get; set; }
        double PosZ { get; set; }
        double VelX { get; set; }
        double VelY { get; set; }
        double VelZ { get; set; }
        double ForcaX { get; set; }
        double ForcaY { get; set; }
        double ForcaZ { get; set; }
        double Raio { get; }

        void CopiaCorpo(ICorpo cp);
    }
}