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
            int n = 0;
            bool validacao = false;

            Console.Write("Informe o nome da Rua: ");
            string rua = Console.ReadLine();

            do
            {
                Console.Write("Informe o número do Local: ");
                try
                {
                    n = int.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nDigite apenas números!\n");
                    validacao = true;
                }

                if (n < 0)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nNúmero não pode ser negativo!\n");
                        validacao = true;
                    }                    
                }                

            } while (validacao);


            Console.Write("Informe a Cidade: ");
            string cidade = Console.ReadLine();

            return new Endereco(rua, n, cidade);
        }
    }
}
