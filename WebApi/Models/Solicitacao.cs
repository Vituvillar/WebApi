using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Solicitacao
    {
        public long Id { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime DataAnalise { get; set; }
        public double TotalDasTransacoes { get; set; }
        public double TotalRepasse { get; set; }
        public int status { get; set; } //1 - aguardando, 2 - em análise, 3 - finalizada
    }
}
