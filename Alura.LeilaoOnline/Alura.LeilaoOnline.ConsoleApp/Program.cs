using Alura.LeilaoOnline.Core;
using System;

namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.ReceberLance(fulano, 800);
            leilao.ReceberLance(maria, 900);
            leilao.ReceberLance(fulano, 1000);
            leilao.ReceberLance(maria, 990);

            leilao.TerminaPregao();

            Console.WriteLine(leilao.Ganhador.Valor);
        }
    }
}
