using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Finalizado
    {
        public int Id { get; set; }
        public Solicitacao solicitacao { get; set; }
    }
}
