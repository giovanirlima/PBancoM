using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBanco.Entities
{
    public class Funcionario
    {
        public string Nome { get; set; }
        public int Matricula { get; set; }
        public int Senha { get; set; }
        public Agencia Agencia { get; set; }

        public Funcionario()
        {
        }
        public Funcionario(string nome, int matricula, int senha, Agencia agencia)
        {
            Nome = nome;
            Matricula = matricula;
            Senha = senha;
            Agencia = agencia;
        }

        public double VerificarTipoDeConta(double renda)
        {
            double chequeEspecial;

            Console.Clear();

            if (renda <= 1000)
            {
                Console.WriteLine("Sua conta será encaminhada para aprovação!\nCaso o Gerente do banco aprove sua conta, ela será modelo universitário(a)");
                chequeEspecial = 1000;
                Console.WriteLine($"Com cheque especial liberado no valor de R$ {chequeEspecial.ToString("F2")}");
                return chequeEspecial;
            }

            else
            {
                if (renda > 1000 && renda <= 2500)
                {
                    Console.WriteLine("Sua conta será encaminhada para aprovação!\nCaso o Gerente do banco aprove sua conta, ela será modelo normal class");
                    chequeEspecial = 2500;
                    Console.WriteLine($"Com cheque especial liberado no valor de R$ {chequeEspecial.ToString("F2")}");
                    return chequeEspecial;
                }

                else
                {
                    Console.WriteLine("Sua conta será encaminhada para aprovação!\nCaso o Gerente do banco aprove sua conta, ela será modelo VIP");
                    chequeEspecial = 5000;
                    Console.WriteLine($"Com cheque especial liberado no valor de R$ {chequeEspecial.ToString("F2")}");
                    return chequeEspecial;
                }
            }
        }
        public Cliente CadastrarCliente(List<Cliente> clientes, List<Agencia> agencias)
        {
            DateTime nascimento = DateTime.Now;
            Cliente cliente1;
            Gerente gerente = new Gerente();
            int id;
            double chequeEspecial, salario = 0;
            bool validacao;

            Console.Clear();

            Console.WriteLine("Vamos iniciar seu cadastro:\n");

            Console.Write("Informe seu nome: ");
            string nome = Console.ReadLine();

            Console.Write("Informe seu CPF: ");
            string cpf = Console.ReadLine();

            do
            {
                Console.Write("Informe sua data de nascimento: ");
                try
                {
                    nascimento = DateTime.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nFormato invalido!");
                    Console.WriteLine("(dd/mm/yyyy) ou (dd.mm.yyyy)\n");
                    validacao = true;
                }
                

            } while (validacao);
            

            Console.Write("Informe seu Telefone: ");
            string telefone = Console.ReadLine();

            Endereco endereco = new Endereco();

            endereco.CadastrarEndereco();

            do
            {
                Console.Write("Informe seu salário mensal: R$ ");
                try
                {
                    salario = double.Parse(Console.ReadLine());
                    validacao = false;
                }

                catch (Exception)
                {
                    Console.WriteLine("\nParamento inválido, digite apenas números!\n");
                    validacao = true;
                }

                if (salario < 0)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nNão é possivél declarar um salário negativo!\n");
                        validacao = true;
                    }
                }                

            } while (validacao);
            

            chequeEspecial = VerificarTipoDeConta(salario);

            Console.WriteLine("\nPressione enter para continuar!");
            Console.ReadKey();

            bool resposta = gerente.AprovarConta(nome, cpf, nascimento, telefone, salario);

            if (resposta)
            {
                Console.WriteLine("\nRetornando aprovação para o funcionário . . .");

                Console.WriteLine("Pressione enter para continuar!");
                Console.ReadKey();

                Console.Clear();

                do
                {
                    Console.Write("Informe o id da nova conta do cliente: ");
                    id = int.Parse(Console.ReadLine());
                    validacao = false;

                    foreach (var cliente in clientes)
                    {
                        if (cliente.ContaCorrente.Id == id)
                        {
                            Console.WriteLine("\nId informado já está sendo utilizado por outro cliente!");
                            Console.WriteLine("Escolha outro Id!\n");

                            validacao = true;

                        }
                    }

                } while (validacao);

                Console.Write("Digite a senha da conta: ");
                int senha = int.Parse(Console.ReadLine());

                Console.Clear();

                Console.WriteLine("Qual agência id cliente prefêre como principal: ");

                foreach (var agencia in agencias)
                {
                    Console.WriteLine(agencia.ToString());
                }
                Console.Write("\nOpção: ");
                int idAgencia = int.Parse(Console.ReadLine());

                Console.WriteLine("\nConta criada com sucesso!");

                Console.WriteLine("Pressione enter para continuar!");

                cliente1 = new Cliente(nome, cpf, nascimento, telefone, endereco, salario, new ContaCorrente(id, senha, agencias[idAgencia - 1], 0, chequeEspecial, new Cartao(id, 0, 30, false)), new ContaPoupanca(id, agencias[idAgencia - 1], 0));

                return cliente1;
            }

            else
            {
                return null;
            }
        }


        public override string ToString()
        {
            return $"\nNome: {Nome}\nMatricula: {Matricula}\nAgência: {Agencia.ToString()}";
        }
    }
}
