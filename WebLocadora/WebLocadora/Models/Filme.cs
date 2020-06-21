using System;


namespace WebLocadora.Models
{
    public class Filme
    {
        public int id { get; set; }
        public string NomeFilme { get; set; }
        public string Sinopse { get; set; }
        public decimal valorLocacao { get; set; }
        public bool Locado { get; set; }
        public bool Ativo { get; set; }
    }
}
