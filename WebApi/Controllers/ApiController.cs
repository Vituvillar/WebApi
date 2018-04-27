using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/transacoes")]
    public class ApiController : Controller
    {
        private readonly TransacoesContext _context;

        public ApiController(TransacoesContext context)
        {
            _context = context;

            if (_context.Transacoes.Count() == 0)
            {
                _context.Transacoes.Add(new Transacao { Antecipada = false, Parcelas = 3, ValorTransacao = 300 });
                _context.Transacoes.Add(new Transacao { Antecipada = true, Parcelas = 0, ValorTransacao = 650 });
                _context.Transacoes.Add(new Transacao { Antecipada = false, Parcelas = 5, ValorTransacao = 200 });
                _context.SaveChanges();
            }

            foreach (Transacao t in _context.Transacoes)
            {
                t.ValorRepasse = (t.ValorTransacao - 0.9) - (((t.ValorTransacao * 3.8) / 100) * t.Parcelas);
                _context.SaveChanges();
            }

            _context.Andamento = null;
            _context.SaveChanges();
        }

        [HttpGet("todas")]
        public IEnumerable<Transacao> GetAll()
        {
            return _context.Transacoes.ToList();
        }
        
        // GET: api/<controller>
        [HttpGet("disponiveis")]
        public IEnumerable<Transacao> GetTransacoesDisponiveis()
        {
            var transacoes =  _context.Transacoes.Where(t => t.Antecipada == false).ToList();

            return transacoes;
            
        }

        [HttpPut("solicitar")]
        public IActionResult SolicitarTransacoes([FromBody] ListaTransacoes transacoes)
        {
            var _transacoes = _context.Transacoes.ToList();
            var solicitadas = new List<Transacao>();
            var totalTransacao = new double();
            var totalRepasse = new double();

            if (_context.Andamento == null)
            {
                foreach (int i in transacoes.lista)
                {
                    var transacao = _context.Transacoes.Where(t => t.Id == i && t.Antecipada == false).FirstOrDefault();
                    if (transacao != null)
                    {
                        totalTransacao = totalTransacao + transacao.ValorTransacao;
                        totalRepasse = totalRepasse + transacao.ValorRepasse;
                        solicitadas.Add(transacao);
                        transacao.Antecipada = true;
                    }
                }

                var novaSolicitacao = new Solicitacao { TotalDasTransacoes = totalTransacao, TotalRepasse = totalRepasse, status = 1 };
                _context.Solicitacoes.Add(novaSolicitacao);
                _context.Andamento = novaSolicitacao;
                _context.SaveChanges();

                return new ObjectResult(novaSolicitacao);
            }
            else
            {
                return new ObjectResult("Já existe uma solicitação em andamento");
            }
        }

        // GET api/<controller>/5
        [HttpGet("detalhes")]
        public IActionResult GetEmAndamento()
        {
            if(_context.Andamento != null)
                return new ObjectResult(_context.Solicitacoes.Where(t => t.Id == _context.Andamento.Id));
            return NotFound();
        }

        [HttpGet("solicitacoes/?{date1}&&{date2}")]
        public IEnumerable<Solicitacao> GetSolicitations(DateTime date1, DateTime date2)
        {
            return _context.Solicitacoes.Where(s => s.DataSolicitacao >= date1 && s.DataSolicitacao <= date2).ToList();
        }
    }
}
