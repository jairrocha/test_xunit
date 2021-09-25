using Alura.LeilaoOnline.Core;
using System;

namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {
        private static void Verifica(double esperado, double obtido)
        {
            if (esperado == obtido)
            {
                Console.WriteLine("Teste OK");
            }
            else
            {
                Console.WriteLine("Teste NOK");
            }
        }
        private static void LeilaoComVariosLances()
        {
            
            var leilao = new Leilao("Van Gogh", new MaiorValor());
            var fulano = new Interessada("Fulana", leilao);
            var maria = new Interessada("Lidiane", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 990);

            leilao.TerminaPregao();

            Verifica(1000, leilao.Ganhador.Valor);
      
        }
        private static void LeilaoComApenasUmLAnce()
        {
            var leilao = new Leilao("Van Gogh", new MaiorValor());
            var fulano = new Interessada("Fulana", leilao);


            leilao.RecebeLance(fulano, 800);

            leilao.TerminaPregao();

            Verifica(800, leilao.Ganhador.Valor);
        }
        static void Main(string[] args)
        {
            LeilaoComVariosLances();
            LeilaoComApenasUmLAnce();
        }


    }
}
