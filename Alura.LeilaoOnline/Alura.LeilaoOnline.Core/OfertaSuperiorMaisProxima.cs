using Alura.LeilaoOnline.Core.Interfaces;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
        public double ValorDestino { get; } //Se o valor destino for passado significa que a modalidade é "superior mais próximo, senão ele recebe 0"

        public OfertaSuperiorMaisProxima(double valorDestino)
        {
            ValorDestino = valorDestino;
        }

        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                    .DefaultIfEmpty(new Lance(null, 0)) //Definir um valor default se tivermos uma lista vazia
                    .Where(x => x.Valor > ValorDestino)
                    .OrderBy(x => x.Valor)
                    .FirstOrDefault();
        }
    }
}
