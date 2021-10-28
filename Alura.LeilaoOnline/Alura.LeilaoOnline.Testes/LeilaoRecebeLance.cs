using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoRecebeLance
    {
        //Cenário 1
        //Arranje
        //Dado leilão finalizado com X lances
        //Act
        //Quando o leilão recebe nova oferta de lance
        //Assert
        //Então novo lance é ignorado e quantidade de lances continua sendo X

        [Fact]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado()
        {
            //Arranje - cenário - dados de entrada
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciarPregao();

            leilao.ReceberLance(fulano, 800);
            leilao.ReceberLance(fulano, 900);

            leilao.TerminaPregao(); //O termina pregao faz parte do cenário

            //Act - método sob teste
            leilao.ReceberLance(fulano, 1000);

            //Assert
            var valorEsperado = 1;
            var valorObtido = leilao.Lances.Count();

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(4, new double[] { 1000, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado2(int valorEsperado, double[] ofertas)
        {
            //Arranje - cenário - dados de entrada
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciarPregao();

            for(int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if(i % 2 == 0)
                {
                    leilao.ReceberLance(fulano, valor);
                }
                else
                {
                    leilao.ReceberLance(maria, valor);
                }
            }

            leilao.TerminaPregao(); //O termina pregao faz parte do cenário

            //Act - método sob teste
            leilao.ReceberLance(fulano, 1000);

            //Assert
            var valorObtido = leilao.Lances.Count();

            Assert.Equal(valorEsperado, valorObtido);
        }

        //Cenário 2
        //Dado um leilão ainda sem ter iniciado um pregão
        //Quando leilão recebe qualquer quantidade de lances
        //Então tais lances serão ignorados pelo leilão
        [Fact]
        public void IgnorarLancesDadoLeilaoSemIniciarPregao()
        {
            var leilao = new Leilao("Monalisa");
            var fulano = new Interessada("Fulano", leilao);

            leilao.ReceberLance(fulano, 1000);

            Assert.Equal(0, leilao.Lances.Count());
        }
        
        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje - cenário - dados de entrada
            var leilao = new Leilao("Van Gogh");

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        //Cenário 3
        //Arranje
        //Dado leilão iniciado e interessado X realizou o ultimo lance
        //Act
        //Quando mesmo interessado X realiza o p´roximo lance
        //Assert
        //Então leilão não aceita o segundo lance
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoInteressadoRealizouUltimoLance()
        {
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciarPregao();
            leilao.ReceberLance(fulano, 800); //Lance inicial

            //Act - método sob teste
            leilao.ReceberLance(fulano, 1000); //Lance consecutivo

            //Assert - Resultado
            var valorEsperado = 1;
            var valorObtido = leilao.Lances.Count();
            Assert.Equal(valorEsperado, valorObtido);

        }
    }
}

