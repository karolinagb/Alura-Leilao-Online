using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class Leilao
    {
        private IList<Lance> _lances;
        
        public string Peca { get; }
        //O mesmo que public IEnumerable<Lance> Lances { return _lances }
        public IEnumerable<Lance> Lances => _lances;
        public Lance Ganhador { get; private set; }

        public Leilao(string peca)
        {
            Peca = peca;
            _lances = new List<Lance>();
        }

        public void ReceberLance(Interessada cliente, double valor)
        {
            _lances.Add(new Lance(cliente, valor));
        }

        public void IniciarPregao()
        {

        }

        public void TerminaPregao()
        {

            Ganhador = Lances.Last(); //Last() = pegar o ultimo da lista
        }
    }
}
