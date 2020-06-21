using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebLocadora.Models;

namespace WebLocadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ApiDbContext context;
        public ClienteController(ApiDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IEnumerable<Cliente> GetCliente()
        {
            return context.Clientes.ToList().OrderBy(x => x.Nome);
        }

        [HttpGet("{cpf}", Name = "ClienteCriado")]
        public IActionResult GetClienteByCPF(string CPF)
        {
            if (CPF == null)
            {
                return NotFound();
            }
            var cli = context.Clientes.FirstOrDefault(x => x.CPF == CPF);

            return Ok(cli);

        }

        [HttpGet("nome/{nome}")]
        public IActionResult GetClienteByNome(string nome)
        {
            if (nome == null)
            {
                return NotFound();
            }
            var cli = context.Clientes.FirstOrDefault(x => x.Nome.ToLower().Contains(nome.ToLower()));

            return Ok(cli);

        }

        [HttpPost]
        public IActionResult PostCliente([FromBody] Cliente cli)
        {

            if (ModelState.IsValid)
            {
                var clienteCad = context.Clientes.Where(x => x.CPF == cli.CPF);

                if (clienteCad == null)
                {
                    context.Clientes.Add(cli);
                    context.SaveChanges();
                    return new CreatedAtRouteResult("ClienteCriado", new { cpf = cli.CPF }, cli);

                }
                               
            }
            return BadRequest(ModelState);
        }
    }
}
