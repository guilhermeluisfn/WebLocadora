using System;


namespace WebLocadora.Models
{
    public class Ticket
    {
        public int id { get; set; }
        public int idCliente { get; set; }
        public bool Ativo { get; set; }
    }
}
