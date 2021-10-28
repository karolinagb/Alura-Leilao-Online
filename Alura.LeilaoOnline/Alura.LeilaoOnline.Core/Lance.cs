using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.LeilaoOnline.Core
{
    public class Lance
    {
        public Interessada Cliente { get; set; }
        public double Valor { get; set; }

        public Lance(Interessada cliente, double valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor do lance não pode ser negativo");
            }

            Cliente = cliente;
            Valor = valor;
        }
    }
}
