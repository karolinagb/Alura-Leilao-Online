using Alura.LeilaoOnline.Core;
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

        //Cenário 2
        //Arranje
        //Dado leilão sem qualquer lance
        //Act
        //Quando o pregão/leilão termina
        //Assert
        //Então o valor do lance ganhador é zero

        //xUnit não trabalha com métodos estáticos

        [Theory] //Para enviar vários dados de teste
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })] //Passo os dados de entrada aqui
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]  //Tenho que receber esses dados como argumento de entrada do método
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas) //Classes de teste precisam ser publicas
        {
            //Arranje - cenário - dados de entrada
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciarPregao();

            foreach(var valor in ofertas)
            {
                leilao.ReceberLance(fulano, valor);
            }
            
            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido); //Verifica se os valores são iguais
        }

        
    }
}
