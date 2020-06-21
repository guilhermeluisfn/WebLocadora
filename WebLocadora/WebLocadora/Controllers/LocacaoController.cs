using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebLocadora.Models;

namespace WebLocadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly ApiDbContext context;
        public LocacaoController(ApiDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IEnumerable<Locacao> GetLocacao()
        {
            return context.Locacoes.ToList().OrderBy(x => x.dataLocacao);
        }
        [HttpGet("{ativo}")]
        public IActionResult GetLocacaoAtiva(bool ativo)
        {
            using (var contexto = context)
            {

                var joinLocacao = from l in contexto.Locacoes
                                  join t in contexto.Tickets on l.idTicket equals t.id
                                  join f in contexto.Filmes on l.idFilme equals f.id
                                  join c in contexto.Clientes on t.idCliente equals c.id
                                  where l.Ativo == ativo
                                  select new
                                  {
                                      comanda = t.id,
                                      cliente = c.Nome,
                                      endereco = c.Endereco,
                                      numero = c.Numero,
                                      bairro = c.Bairro,
                                      cidade = c.Cidade,
                                      filme = f.NomeFilme,
                                      diaria = f.valorLocacao,
                                      datalocacao = l.dataLocacao,
                                      datadevolucao = l.dataDevolucao,
                                      valorlocacao = (f.valorLocacao * l.diasLocacao),
                                      dias = l.diasLocacao
                                  };
                if (joinLocacao == null)
                {
                    return NotFound();
                }

                return Ok(joinLocacao.ToList());
            }
        }

        [HttpGet("{cpf}")]
        public IActionResult GetLocacaoByCliente(string CPF)
        {
            using (var contexto = context)
            {

                var joinLocacao = from l in contexto.Locacoes
                                  join t in contexto.Tickets on l.idTicket equals t.id
                                  join f in contexto.Filmes on l.idFilme equals f.id
                                  join c in contexto.Clientes on t.idCliente equals c.id
                                  select new
                                  {
                                      comanda = t.id,
                                      cliente = c.Nome,
                                      endereco = c.Endereco,
                                      numero = c.Numero,
                                      bairro = c.Bairro,
                                      cidade = c.Cidade,
                                      filme = f.NomeFilme,
                                      diaria = f.valorLocacao,
                                      datalocacao = l.dataLocacao,
                                      datadevolucao = l.dataDevolucao,
                                      valorlocacao = (f.valorLocacao * l.diasLocacao),
                                      dias = l.diasLocacao

                                  };
                if (joinLocacao == null)
                {
                    return NotFound();
                }

                return Ok(joinLocacao.ToList());
            }

        }

        [HttpGet("{cpf}/{id}")]
        public IActionResult GetLocacaoByTicket(string CPF, int id)
        {
            using (var contexto = context)
            {

                var joinLocacao = from l in contexto.Locacoes
                                  join t in contexto.Tickets on l.idTicket equals t.id
                                  join f in contexto.Filmes on l.idFilme equals f.id
                                  join c in contexto.Clientes on t.idCliente equals c.id
                                  where t.id == id && c.CPF == CPF
                                  select new
                                  {
                                      comanda = t.id,
                                      cliente = c.Nome,
                                      endereco = c.Endereco,
                                      numero = c.Numero,
                                      bairro = c.Bairro,
                                      cidade = c.Cidade,
                                      filme = f.NomeFilme,
                                      diaria = f.valorLocacao,
                                      datalocacao = l.dataLocacao,
                                      datadevolucao = l.dataDevolucao,
                                      valorlocacao = (f.valorLocacao * l.diasLocacao),
                                      dias = l.diasLocacao
                                  };
                if (joinLocacao == null)
                {
                    return NotFound();
                }

                return Ok(joinLocacao.ToList());
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Locacao loc)
        {

            if (ModelState.IsValid)
            {
                context.Locacoes.Add(loc);
                context.SaveChanges();
                return Ok();

            }

            return BadRequest();
        }
    }

}
