using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoTerminaPregao
    {
        //Cenário 1
        //Arranje
        //Dado leilão com pelo menos 1 lance
        //Act
        //Quando o pregão/leilão termina
        //Assert
        //Então o valor esperado é o maior valor dado
        //E o cliente ganhador é o que deu o maior lance

        //xUnit não trabalha com métodos estáticos

        [Theory] //Para enviar vários dados de teste
        [InlineData(1200, new double[] { 1000, 1200 })] //Passo os dados de entrada aqui
        [InlineData(1000, new double[] { 800, 1000 })]
        [InlineData(800, new double[] { 800 })]  //Tenho que receber esses dados como argumento de entrada do método
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas) //Classes de teste precisam ser publicas
        {
            //Arranje - cenário - dados de entrada
            var leilao = new Leilao("Van Gogh");
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

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido); //Verifica se os valores são iguais
        }

        //Cenário 2
        //Arranje
        //Dado leilão sem qualquer lance
        //Act
        //Quando o pregão/leilão termina
        //Assert
        //Então o valor do lance ganhador é zero
        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje - cenário - dados de entrada
            var leilao = new Leilao("Van Gogh");

            leilao.IniciarPregao();

            //Act - método sob teste
            leilao.TerminaPregao(); //O pregão não pode terminar antes de ser iniciado

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arranje - cenário - dados de entrada
            var leilao = new Leilao("Van Gogh");

            //Assert
            //Para testar exceções com xUnit
            var mensagemObtida = Assert.Throws<InvalidOperationException>(
                //Act - método sob teste
                () => leilao.TerminaPregao()
                );

            var mensagemEsperada = "Não é possível finalizar o pregão sem que ele tenha iniciado" +
                "Utilize o método IniciaPregao()";
            Assert.Equal(mensagemEsperada, mensagemObtida.Message);
        }

        //Arrange
        //Dado leilao com valor destino
        //Act
        //Quando termina o pregão
        //Assert
        //O vencedor é o lance com valor superior ao destino mais próximo
        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void RetornaValorSuperiorMaixProximoDadoLeilaoValorEsperado(double valorDestino, double valorEsperado, double[] ofertas)
        {
            //Arranje
            var leilao = new Leilao("Van Gogh", valorDestino);
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

            //Act
            leilao.TerminaPregao();

            //Assert

        }
    }
}
