using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLocadora.Models;


namespace WebLocadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ApiDbContext context;
        public TicketController(ApiDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Ticket> GetTicket()
        {
            return context.Tickets.ToList();
        }


        [HttpGet("{id}", Name = "TicketCriado")]
        public IActionResult GetTicketById(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var tic = context.Tickets.Select(x => x.idCliente == id && x.Ativo == true).ToList();

            return Ok(tic);
        }


        [HttpPost]
        public IActionResult PostTicket([FromBody] Ticket tic)
        {
            if (ModelState.IsValid)
            {
                context.Tickets.Add(tic);
                context.SaveChanges();
                return new CreatedAtRouteResult("TicketCriado", new { id = tic.id }, tic);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("recebe/{id}")]
        public IActionResult RecebeTicket(int id)
        {

            var tic = context.Tickets.FirstOrDefault(x => x.id == id);

            if (tic == null)
            {
                return BadRequest();
            }

            var loc = context.Locacoes.Where(x => x.idTicket == tic.id);
            if (loc == null)
            {
                return BadRequest();
            }

            tic.Ativo = false;


            foreach (var row in loc)
            {

                var fil = context.Filmes.FirstOrDefault(x => x.id == row.idFilme);

                if (DateTime.Today > row.dataDevolucao)
                {
                    row.valorDesconto = 0M;
                    row.valorMulta = calculaMulta(fil.valorLocacao, row.dataDevolucao);
                    row.Ativo = true;
                    row.Aviso = "Filme entregue com atraso de "+ (((TimeSpan)(DateTime.Now - row.dataDevolucao)).Days).ToString() +" dias, multa calculada";
                }
                else if (DateTime.Today < row.dataDevolucao) {
                    row.valorDesconto = calculaDesconto(fil.valorLocacao, row.dataDevolucao);
                    row.valorMulta = 0M;
                    row.Ativo = true;
                }
                fil.Locado = false;
                context.Entry(fil).State = EntityState.Modified;
                context.Entry(row).State = EntityState.Modified;
            }
            context.Entry(tic).State = EntityState.Modified;
            //context.Entry(loc).State = EntityState.Modified;
            context.SaveChanges();
            return Ok(loc);
        }


        private decimal calculaMulta(decimal valorLocaco, DateTime dataDevolucao)
        {

            DateTime hoje = DateTime.Today;

            int diasAtraso = ((TimeSpan)(hoje - dataDevolucao)).Days;

            return (valorLocaco * diasAtraso);
        }

        private decimal calculaDesconto(decimal valorLocaco, DateTime dataDevolucao)
        {

            DateTime hoje = DateTime.Today;

            int diasAtraso = ((TimeSpan)(hoje - dataDevolucao)).Days;

            return (valorLocaco * diasAtraso)/2;
        }
    }
}
