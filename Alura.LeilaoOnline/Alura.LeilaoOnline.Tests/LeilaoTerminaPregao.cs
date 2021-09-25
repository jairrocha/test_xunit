using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] {800, 1150, 1400, 1250 })]
        [InlineData(1200, 0, new double[] { 800, 700, 100, 1 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(
                        double valorDestino, double valorEsperado, double[] ofertas)
        {
            //Arranje

            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulana", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }

            leilao.TerminaPregao();

            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }


        [Fact]
        public void LancaExcecaoComMenssagemOperacaoNaoPermitida()
        {
            //Arranje
            var leilao = new Leilao("Van Gogh", new MaiorValor());
            string msgEsperada = "Operação não permitida";

            var excecaoMsgRecebida = Assert.Throws<InvalidOperationException>(
                //Act - método sob teste
                () => leilao.TerminaPregao()
            );

            Assert.Equal(msgEsperada, excecaoMsgRecebida.Message);

        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arranje
            var leilao = new Leilao("Van Gogh", new MaiorValor());

            Assert.Throws<InvalidOperationException>(
                //Act - método sob teste
                () => leilao.TerminaPregao()
            );

           
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLance()
        {
            var leilao = new Leilao("Van Gogh", new MaiorValor());
            double valorEsperado = 0;
            
            leilao.IniciaPregao();
            leilao.TerminaPregao();
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }


        [Theory]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(1000, new double[] { 800, 900, 990, 1000 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulana", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i%2 == 0)
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }

            leilao.TerminaPregao();

            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);

        }


    }
}
