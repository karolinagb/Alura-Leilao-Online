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
            Cliente = cliente;
            Valor = valor;
        }
    }
}
