using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoTestes
    {
        //xUnit não trabalha com métodos estáticos
        [Fact] //Informa que esse método é um teste
        public void LeilaoComVariosLances() //Classes de teste precisam ser publicas
        {
            //Arranje - cenário - dados de entrada
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.ReceberLance(fulano, 800);
            leilao.ReceberLance(maria, 900);
            leilao.ReceberLance(fulano, 1000);
            leilao.ReceberLance(maria, 990);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido); //Verifica se os valores são iguais
        }

        [Fact]
        public void LeilaoComApenasUmLance()
        {
            //Arranje - cenário - dados de entrada
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.ReceberLance(fulano, 800);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 800;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
