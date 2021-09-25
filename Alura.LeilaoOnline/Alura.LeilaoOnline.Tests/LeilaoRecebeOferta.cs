using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {

        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 800, 900, 1400, 8000 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int valorEsperado, double[] ofertas)
        {
            var leilao = new Leilao("Van Gogh" , new MaiorValor());
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i%2==0)
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }

            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtndeObtida = leilao.Lances.Count();

            Assert.Equal(valorEsperado, qtndeObtida);


        }


        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Assert
            var leilao = new Leilao("Van Gogh", new MaiorValor());
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();
            
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(fulano, 2000);
            leilao.RecebeLance(maria, 500);

            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtndeObtida = leilao.Lances.Count();
            var qtndeEsperada = 2;

            Assert.Equal(qtndeEsperada, qtndeObtida);

        }
    }

}