using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBanco.Entities
{
    public class Gerente : Funcionario
    {
        public Gerente()
        {
        }
        public Gerente(string nome, int matricula, int senha, Agencia agencia) : base(nome, matricula, senha, agencia)
        {
        }

        public void CadastrarGerente(List<Gerente> gerentes, List<Funcionario> funcionarios, List<Agencia> agencias)
        {
            Console.Clear();
            Console.WriteLine("Olá Gerente\n");
            Console.Write("Informe o nome do novo funcionário: ");
            string nome = Console.ReadLine();

            bool validacao;
            int matricula, senha;

            do
            {
                Console.Write("Informe a matricula do novo funcionário: ");
                matricula = int.Parse(Console.ReadLine());

                validacao = false;

                foreach (var gerente in gerentes)
                {
                    if (gerente.Matricula == matricula)
                    {
                        Console.WriteLine($"\nO Gerente {gerente.Nome} já possue está mátricula!");
                        Console.WriteLine("Necessário escolher uma nova!\n");
                        validacao = true;
                    }
                }

                foreach (var funcionario in funcionarios)
                {
                    if (funcionario.Matricula == matricula)
                    {
                        Console.WriteLine($"\nO Funcionário {funcionario.Nome} já possue está mátricula!");
                        Console.WriteLine("Necessário escolher uma nova!\n");
                        validacao = true;
                    }
                }

            } while (validacao);

            Console.Write("Informe a senha: ");
            senha = int.Parse(Console.ReadLine());

            Console.Clear();

            Console.WriteLine("Informe o id da agência que o funcionario será registrado");

            foreach (var agencia in agencias)
            {

                Console.WriteLine(agencia.ToString());

            }

            Console.Write("\nID: ");

            int idAgencia = int.Parse(Console.ReadLine());

            Console.WriteLine("\nFuncionário cadastrado com sucesso!");

            Console.WriteLine("\nPressione Enter para continuar!");

            gerentes.Add(new Gerente(nome, matricula, senha, agencias[idAgencia - 1]));

        }
        public void CadastraFuncionario(List<Gerente> gerentes, List<Funcionario> funcionarios, List<Agencia> agencias)
        {
            Console.Clear();
            Console.WriteLine("Olá Gerente\n");
            Console.Write("Informe o nome do novo funcionário: ");
            string nome = Console.ReadLine();

            bool validacao;
            int matricula, senha;

            do
            {
                Console.Write("Informe a matricula do novo funcionário: ");
                matricula = int.Parse(Console.ReadLine());

                validacao = false;

                foreach (var gerente in gerentes)
                {
                    if (gerente.Matricula == matricula)
                    {
                        Console.WriteLine($"\nO Gerente {gerente.Nome} já possue está mátricula!");
                        Console.WriteLine("Necessário escolher uma nova!\n");
                        validacao = true;
                    }
                }

                foreach (var funcionario in funcionarios)
                {
                    if (funcionario.Matricula == matricula)
                    {
                        Console.WriteLine($"\nO Funcionário {funcionario.Nome} já possue está mátricula!");
                        Console.WriteLine("Necessário escolher uma nova!\n");
                        validacao = true;
                    }
                }

            } while (validacao);

            Console.Write("Informe a senha: ");
            senha = int.Parse(Console.ReadLine());

            Console.Clear();

            Console.WriteLine("Informe o id da agência que o funcionario será registrado");

            foreach (var agencia in agencias)
            {

                Console.WriteLine(agencia.ToString());

            }

            Console.Write("\nID: ");

            int idAgencia = int.Parse(Console.ReadLine());

            Console.WriteLine("\nFuncionário cadastrado com sucesso!");

            Console.WriteLine("\nPressione Enter para continuar!");

            funcionarios.Add(new Gerente(nome, matricula, senha, agencias[idAgencia - 1]));

        }
        public void CadastrarAgencia(List<Agencia> agencias)
        {
            bool verificacao;
            int id;

            Console.Clear();

            Console.WriteLine("Olá, Sr. Gerente");
            do
            {
                Console.Write("\nInforme id da nova agência: ");
                id = int.Parse(Console.ReadLine());
                verificacao = false;

                foreach (var agencia in agencias)
                {
                    if (agencia.Id == id)
                    {
                        Console.WriteLine("\nJá existe este ID cadastrado");
                        verificacao = true;

                    }
                }

            } while (verificacao);

            Console.Clear();

            Endereco endereco = new Endereco();

            agencias.Add(new Agencia(id, endereco.CadastrarEndereco()));

            Console.WriteLine("\nAgência cadastrada com sucesso!");
        }
        public bool AprovarConta(string nome, string cpf, DateTime nascimento, string telefone, double renda)
        {
            Console.Clear();
            Console.WriteLine("Olá sr. Gerente\n");
            Console.WriteLine("Dados do cliente para aprovação: ");
            Console.WriteLine($"Nome: {nome}\nCPF: {cpf}\nData de Nascimento: {nascimento.ToShortDateString()}" +
                              $"\nTelefone: {telefone}\nRenda mensal: R$ {renda.ToString("F2")}");

            Console.WriteLine("Deseja aprovar criação da conta: SIM/NAO");

            Console.Write("\nResposta: ");
            string resposta = Console.ReadLine().ToLower();

            if (resposta == "sim" || resposta == "s")
            {
                Console.WriteLine("\nConta aprovada!");
                Console.WriteLine("Prossiga com o cadastro!");

                return true;
            }

            else
            {
                Console.WriteLine("\nConta não aprovada!");
                Console.WriteLine("Cadastro negado!");
                Console.ReadKey();
                return false;
            }
        }
        public bool AprovarEmprestimo(Cliente cliente, double valorSolicitado)
        {
            bool validacao;
            int resposta = 0;                      

            do
            {
                Console.Clear();

                Console.WriteLine("olá sr.(a) Gerente!");
                Console.WriteLine("\nPedido de empréstimo para aprovação");
                Console.WriteLine($"\nNome: {cliente.Nome}");
                Console.WriteLine($"Renda: R$ {cliente.Salario.ToString("F2")}");
                Console.WriteLine($"Valor solicitado: R$ {valorSolicitado.ToString("F2")}");
                Console.WriteLine();
                Console.WriteLine("1 - Aprovar\n2 - Recusar");
                Console.Write("\nOpção: ");

                try
                {
                    resposta = int.Parse(Console.ReadLine());
                    validacao = false;
                }

                catch (Exception)
                {
                    Console.WriteLine("\nOpção inválida!\n");
                    Console.WriteLine("Digite enter para continuar");
                    Console.ReadKey();
                    validacao = true;
                }

                if (resposta != 1 && resposta != 2)
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nOpção escolhida é inexistente!");
                        Console.WriteLine("Escolha 1 ou 2\n");
                        Console.WriteLine("Digite enter para continuar");
                        Console.ReadKey();
                        validacao = true;
                    }                    
                }

                Console.Clear();

            } while (validacao);

            if (resposta == 1)
            {
                return true;
            }

            else
            {
                return false;
            }
        }        
        public void VerFuncionariosCadastrados(List<Gerente> gerentes, List<Funcionario> funcionarios)
        {
            Console.Clear();

            Console.WriteLine("Gerentes cadastrados: ");

            foreach (var gerente in gerentes)
            {
                Console.WriteLine(gerente.ToString());
            }

            Console.ReadKey();

            Console.Clear();

            Console.WriteLine("Funcionarios cadastrados: ");

            foreach (var funcionario in funcionarios)
            {
                Console.WriteLine(funcionario.ToString());
            }

        }
        public void VerAgenciasCadastradas(List<Agencia> agencias)
        {
            Console.Clear();

            Console.WriteLine("Agencias Cadastradas: ");

            foreach (var agencia in agencias)
            {
                Console.WriteLine(agencia.ToString());
            }

        }
        public void VerClientesCadastrados(List<Cliente> clientes)
        {
            Console.Clear();

            Console.WriteLine("Clientes Cadastrados: \n");

            foreach (var cliente in clientes)
            {
                Console.WriteLine(cliente.ToString());
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
