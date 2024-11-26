using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Universo2D
{
    public interface IUniverso2D
    {
        ObservableCollection<Corpo> GetLstCorpos();  // Retorna a lista de corpos no universo
        void SetLstCorpos(ObservableCollection<Corpo> lst);  // Define a lista de corpos

        Corpo GetCorpo(int pos);  // Retorna o corpo na posição especificada
        void SetCorpo(Corpo cp, int pos);  // Insere ou substitui o corpo na posição
        int QtdCorpos();  // Retorna a quantidade de corpos no universo

        void CarregaCorpos(int numCorpos, int xIni, int xFim, int yIni, int yFim, int masIni, int masFim);  // Carrega corpos no universo
        void InteracaoCorpos(int qtdSegundos);  // Executa uma interação dos corpos durante um intervalo de tempo
        void InteracaoCorpos(int qtdInteracoes, int qtdSegundos);  // Executa várias interações

        double Distancia(Corpo c1, Corpo c2);  // Calcula a distância entre dois corpos
        void CopiaUniverso(IUniverso2D u);  // Copia todos os corpos de outro universo para este

        // Outros métodos de manipulação e interações que você desejar
    }
}