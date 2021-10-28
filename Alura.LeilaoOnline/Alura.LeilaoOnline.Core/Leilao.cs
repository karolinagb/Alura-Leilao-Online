using System;
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
        private List<Interessada> _ultimosClientes = new List<Interessada>();
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
            if(LanceAceito(cliente, valor))
            {
                _ultimosClientes.Add(cliente);
                _lances.Add(new Lance(cliente, valor));
            }
        }

        public void IniciarPregao()
        {
            EstadoLeilao = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if(EstadoLeilao != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new InvalidOperationException("Não é possível finalizar o pregão sem que ele tenha iniciado" +
                "Utilize o método IniciaPregao()");
            }

            Ganhador = Lances
                .DefaultIfEmpty(new Lance(null, 0)) //Definir um valor default
                .OrderBy(l => l.Valor)
                .LastOrDefault(); //LastOrDefault() = pegar o ultimo da lista, se tiver vazio ele retorna
            //um objeto default qualquer

            EstadoLeilao = EstadoLeilao.LeilaoFinalizado;
        }

        private bool LanceAceito(Interessada cliente, double valor)
        {
            return (EstadoLeilao == EstadoLeilao.LeilaoEmAndamento) && (!(_ultimosClientes.Any(c => c == cliente))) && (valor >= 0);
        }
    }
}
