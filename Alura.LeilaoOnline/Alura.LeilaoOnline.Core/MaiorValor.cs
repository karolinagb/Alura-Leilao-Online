using Alura.LeilaoOnline.Core.Interfaces;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class MaiorValor : IModalidadeAvaliacao
    {
        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
               .DefaultIfEmpty(new Lance(null, 0)) //Definir um valor default
               .OrderBy(l => l.Valor)
               .LastOrDefault(); //LastOrDefault() = pegar o ultimo da lista, se tiver vazio ele retorna
                                 //um objeto default qualquer
        }
    }
}
