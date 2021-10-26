using System;

namespace Alura.LeilaoOnline.Core
{
    public class Interessada
    {
        public string  Nome { get; set; }
        public Leilao Leilao { get; set; }

        public Interessada(string nome, Leilao leilao)
        {
            Nome = nome;
            Leilao = leilao;
        }
    }
}
