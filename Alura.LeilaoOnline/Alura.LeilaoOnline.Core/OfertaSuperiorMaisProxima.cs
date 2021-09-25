using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
      
        public double ValorDestion { get;  }

        public OfertaSuperiorMaisProxima(double valorDestino)
        {
            this.ValorDestion = valorDestino;
        }

        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                                .DefaultIfEmpty(new Lance(null, 0))
                                .Where(l => l.Valor > ValorDestion)
                                .DefaultIfEmpty(new Lance(null, 0))
                                .OrderBy(l => l.Valor)
                                .FirstOrDefault();
        }
    }
}