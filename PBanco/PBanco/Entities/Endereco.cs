using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBanco.Entities
{
    public class Endereco
    {
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }

        public Endereco()
        {
        }

        public Endereco(string rua, int numero, string cidade)
        {
            Rua = rua;
            Numero = numero;
            Cidade = cidade;
        }

        public Endereco CadastrarEndereco()
        {
            Console.Write("Informe o nome da Rua: ");
            string rua = Console.ReadLine();

            Console.Write("Informe o número do Local: ");
            int n = int.Parse(Console.ReadLine());

            Console.Write("Informe a Cidade: ");
            string cidade = Console.ReadLine();

            return new Endereco(rua, n, cidade);
        }
    }
}
