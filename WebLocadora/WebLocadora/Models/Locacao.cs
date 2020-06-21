using System;


namespace WebLocadora.Models
{
    public class Locacao
    {
        public int id { get; set; }
        public int idTicket { get; set; }
        public int idFilme { get; set; }
        public DateTime dataLocacao { get; set; }
        public DateTime dataDevolucao { get; set; }
        public int diasLocacao { get; set; }
        public DateTime dataEfetivaDevolucao { get; set; }
        public decimal valorLocacao { get; set; }
        public decimal valorMulta { get; set; }
        public decimal valorDesconto { get; set; }
        public string Aviso { get; set; }
        public bool Ativo { get; set; }
    }
}
