using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        LeilaoEmAndamento,
        LeilaoFinalizado,
    }

    public class Leilao
    {
        private IList<Lance> _lances;
        private Interessada _ultimoCliente;
        public string Peca { get; }
        //O mesmo que public IEnumerable<Lance> Lances { return _lances }
        public IEnumerable<Lance> Lances => _lances;
        public Lance Ganhador { get; private set; }
        public EstadoLeilao EstadoLeilao { get; private set; }

        public Leilao(string peca)
        {
            Peca = peca;
            _lances = new List<Lance>();
            EstadoLeilao = EstadoLeilao.LeilaoAntesDoPregao;
        }

        public void ReceberLance(Interessada cliente, double valor)
        {
            if(EstadoLeilao == EstadoLeilao.LeilaoEmAndamento)
            {
                if(cliente != _ultimoCliente)
                {
                    _lances.Add(new Lance(cliente, valor));
                    _ultimoCliente = cliente;
                }   
            }
        }

        public void IniciarPregao()
        {
            EstadoLeilao = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {

            Ganhador = Lances
                .DefaultIfEmpty(new Lance(null, 0)) //Definir um valor default
                .OrderBy(l => l.Valor)
                .LastOrDefault(); //LastOrDefault() = pegar o ultimo da lista, se tiver vazio ele retorna
            //um objeto default qualquer

            EstadoLeilao = EstadoLeilao.LeilaoFinalizado;
        }
    }
}
