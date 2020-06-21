using System;


namespace WebLocadora.Models
{
    public class Cliente
    {

        public int id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }

    }
}
