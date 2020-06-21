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
    public class FilmeController : ControllerBase
    {
        private readonly ApiDbContext context;
        public FilmeController(ApiDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IEnumerable<Filme> GetFilme()
        {
            return context.Filmes.ToList();
        }

        [HttpGet("{nome}", Name = "FilmeCriado")]
        public IActionResult GetClienteByCPF(string nome)
        {
            if (nome == null)
            {
                return NotFound();
            }
            var fil = context.Filmes.FirstOrDefault(x => x.NomeFilme.ToLower().Contains(nome.ToLower()));

            return Ok(fil);

        }

        [HttpPost]
        public IActionResult PostFilme([FromBody] Filme fil)
        {

            if (ModelState.IsValid)
            {
                context.Filmes.Add(fil);
                context.SaveChanges();
                return new CreatedAtRouteResult("FilmeCriado", new { nome = fil.NomeFilme }, fil);

            }
            return BadRequest();
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdaFilme([FromBody] Filme fil, int id)
        {

            if (fil.id != id)
            {
                return BadRequest(ModelState);
            }

            context.Entry(fil).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();

        }

        [HttpPut("delete/{id}")]
        public IActionResult DeleteFilme([FromBody] Filme fil, int id) {

            if (fil.id != id)
            {
                return BadRequest(ModelState);
            }

            fil.Ativo = false;
            context.Entry(fil).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        
        }
    }
}
