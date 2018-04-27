using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class TransacoesContext : DbContext
    {
        public TransacoesContext(DbContextOptions<TransacoesContext> options) : base(options)
        {
        }

        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<Aguardando> Aguardando { get; set; }
        public DbSet<Finalizado> Finalizados { get; set; }
        public Solicitacao Andamento { get; set; }
    }
}
