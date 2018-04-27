using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Transacao
    {
        public long Id { get; set; }
        public DateTime DataTransacaoEfetuada { get; set; }
        public DateTime DataRepasse { get; set; }
        public bool Confirmacao { get; set; }
        public bool Antecipada { get; set; }
        public double ValorTransacao { get; set; }
        public double ValorRepasse { get; set; }
        public int Parcelas { get; set; }

    }
}
