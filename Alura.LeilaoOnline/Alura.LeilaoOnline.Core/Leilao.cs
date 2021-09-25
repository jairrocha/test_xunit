using System.Collections.Generic;
using System.Linq;
using System;

namespace Alura.LeilaoOnline.Core
{

    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        LeilaoEmAdamento,
        LeilaoFinalizado
    }

    public class Leilao
    {
        private Interessada _ultimoCliente;
        private IList<Lance> _lances;
        private IModalidadeAvaliacao _avaliador;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }


        public EstadoLeilao Estado { get; private set; }

        private bool NovoLanceAceito(Interessada cliente, double valor)
        {
            return (cliente != _ultimoCliente) 
                && (Estado == EstadoLeilao.LeilaoEmAdamento);
        }

        public Leilao(string peca, IModalidadeAvaliacao avaliador )
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            _avaliador = avaliador; 
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }

        }



        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAdamento;
        }

        public void TerminaPregao()
        {

            if (Estado != EstadoLeilao.LeilaoEmAdamento)
            {
                throw new InvalidOperationException("Operação não permitida");
            }

            Ganhador = _avaliador.Avalia(this);


            Estado = EstadoLeilao.LeilaoFinalizado;
            
        }

        public object DefaultIfEmpty(Lance lance)
        {
            throw new NotImplementedException();
        }
    }
}
