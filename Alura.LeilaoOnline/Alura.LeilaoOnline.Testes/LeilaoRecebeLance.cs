using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoRecebeLance
    {
        //OBS: Teste para fazer: Não pode haver lançamentos se não tiver interessados

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
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
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
        [InlineData(2, new double[] { 1000, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado2(int valorEsperado, double[] ofertas)
        {
            //Arranje - cenário - dados de entrada
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciarPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if (i % 2 == 0)
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
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Monalisa", modalidade);
            var fulano = new Interessada("Fulano", leilao);

            leilao.ReceberLance(fulano, 1000);

            Assert.Equal(0, leilao.Lances.Count());
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
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
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

        [Theory]
        [InlineData (2, new double[] { 100, 0, 400, -100, -300})]
        public void NaoAceitaLanceDadoValorLanceNegativo(int valorEsperado, double[] lances)
        {
            //Arranje
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Monalisa", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciarPregao();

            //Act
            for(int i = 0; i < lances.Length; i++)
            {
                var valor = lances[i];
                if (i % 2 == 0)
                {
                    leilao.ReceberLance(maria, valor);
                }
                else
                {
                    leilao.ReceberLance(fulano, valor);
                }
            }

            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Lances.Count();
            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}

