using System;
using PBanco.Entities;
using System.Collections.Generic;
using System.Drawing;

namespace PBanco
{
    internal class Program
    {
        static List<Gerente> IniciarGerentes(List<Agencia> agencias)
        {
            List<Gerente> gerentes = new List<Gerente>();
            gerentes.Add(new Gerente("Pestana", 123, 123, agencias[0]));
            gerentes.Add(new Gerente("Papini", 345, 345, agencias[1]));
            gerentes.Add(new Gerente("Fabricio", 567, 567, agencias[2]));

            return gerentes;
        }
        static List<Funcionario> IniciarFuncionarios(List<Agencia> agencias)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();

            funcionarios.Add(new Funcionario("Giovani", 234, 234, agencias[0]));
            funcionarios.Add(new Funcionario("Rocha", 567, 567, agencias[1]));
            funcionarios.Add(new Funcionario("Lima", 891, 891, agencias[2]));

            return funcionarios;

        }
        static List<Agencia> IniciarAgencias()
        {
            List<Agencia> agencias = new List<Agencia>();
            agencias.Add(new Agencia(1, new Endereco("R: Pedro Manoel", 407, "Araraquara")));
            agencias.Add(new Agencia(2, new Endereco("R: Manu Mathias", 999, "Araraquara")));
            agencias.Add(new Agencia(3, new Endereco("R: Professor pedro manoel", 795, "Araraquara")));

            return agencias;
        }
        static List<Cliente> IniciarClientes(List<Agencia> agencias)
        {
            List<Cliente> clientes = new List<Cliente>();

            clientes.Add(new Cliente("João", "123.157.259-55", new DateTime(1994, 12, 03), "(15) 99665-1799", new Endereco("R: Fabio papini", 915, "Araraquara"), 1000, new ContaCorrente(123, 123, agencias[1], 0, 1000, new Cartao(0, 0, 30, false)), new ContaPoupanca(123, agencias[1], 0)));
            clientes.Add(new Cliente("Pedro", "145.177.229-33", new DateTime(1970, 01, 05), "(15) 99662-1855", new Endereco("R: Mario Pestana", 917, "Araraquara"), 2000, new ContaCorrente(456, 456, agencias[2], 0, 2500, new Cartao(0, 0, 30, false)), new ContaPoupanca(456, agencias[2], 0)));
            clientes.Add(new Cliente("Mário", "773.152.524-00", new DateTime(1986, 07, 13), "(15) 99725-6614", new Endereco("R: Alamedas", 325, "Araraquara"), 5000, new ContaCorrente(789, 789, agencias[0], 0, 5000, new Cartao(0, 0, 30, false)), new ContaPoupanca(789, agencias[0], 0)));

            return clientes;
        }
        static void IniciarBanco()
        {
            int opcao = 10, opcaoGerente = 10, opcaoCliente = 10, matricula = 0, senha = 0;
            bool validarMatricula = true, validarSenha = true, condicaoDeParada;
            char resposta = 'a';

            List<Gerente> gerentes = new List<Gerente>();
            List<Funcionario> funcionarios = new List<Funcionario>();
            List<Agencia> agencias = new List<Agencia>();
            List<Cliente> clientes = new List<Cliente>();
            Gerente g = new Gerente();
            Funcionario funcionario = new Funcionario();
            Cliente cliente = new Cliente();

            agencias = IniciarAgencias();
            gerentes = IniciarGerentes(agencias);
            funcionarios = IniciarFuncionarios(agencias);
            clientes = IniciarClientes(agencias);

            do
            {
                do
                {
                    Console.Clear();

                    Console.WriteLine("\t\t\tBEM VINDO AO BANCO MORANGÃO!");
                    Console.WriteLine("\n\nVocê é um: ");
                    Console.WriteLine("\n1 - Funcionário\n2 - Cliente\n\n0 - Sair");
                    Console.Write("\nOpção: ");
                    try
                    {
                        opcao = int.Parse(Console.ReadLine());
                        condicaoDeParada = false;
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("\nFormato inválido!");
                        Console.WriteLine("Pressione enter para escolher novamente!");
                        Console.ReadKey();
                        condicaoDeParada = true;

                    }

                    if (opcao < 0 || opcao > 2)
                    {
                        if (!condicaoDeParada)
                        {
                            Console.WriteLine("\nError! Escolha uma das opções listadas!");
                            Console.WriteLine("Pressione enter para escolher novamente!");
                            Console.ReadKey();
                            condicaoDeParada = true;
                        }
                    }
                } while (condicaoDeParada);


                switch (opcao)
                {
                    case 1:
                        Console.Clear();

                        try
                        {
                            Console.Write("Olá Gerente, \n\nInfome sua matricula: ");
                            matricula = int.Parse(Console.ReadLine());
                            condicaoDeParada = false;
                        }

                        catch (Exception)
                        {
                            Console.WriteLine("\nFormato inválido!");
                            Console.WriteLine("Pressione enter para retornar ao menu anterior!");
                            condicaoDeParada = true;
                            Console.ReadKey();
                        }

                        foreach (var gerente in gerentes)
                        {
                            if (gerente.Matricula == matricula)
                            {
                                validarMatricula = false;
                                g = gerente;
                            }
                        }

                        if (!validarMatricula)
                        {
                            try
                            {
                                Console.Write("Senha: ");
                                senha = int.Parse(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\nFormato inválido!");
                                Console.WriteLine("Pressione enter para retornar ao menu anterior!");
                                condicaoDeParada = true;
                                Console.ReadKey();
                            }


                            foreach (var gerente in gerentes)
                            {
                                if (gerente.Senha == senha)
                                {
                                    validarSenha = false;
                                }
                            }

                            if (validarSenha)
                            {
                                if (!condicaoDeParada)
                                {
                                    Console.WriteLine("\nSenha inválida!");
                                    Console.WriteLine("Pressione enter para retornar ao menu anterior!");
                                    Console.ReadKey();
                                }
                            }
                        }

                        else
                        {
                            if (!condicaoDeParada)
                            {
                                Console.WriteLine("\nMatricula inválida!");
                                Console.WriteLine("Pressione enter para retornar ao menu anterior!");
                                Console.ReadKey();
                                condicaoDeParada = true;
                            }
                            
                        }

                        if (!validarSenha)
                        {
                            do
                            {
                                Console.Clear();

                                Console.WriteLine($"Olá {g.Nome}, Infome a opção desejada: ");
                                Console.WriteLine("");
                                Console.WriteLine("1 - Cadastrar Funcionario");
                                Console.WriteLine("2 - Cadastrar Agência");
                                Console.WriteLine("3 - Ver Funcionarios cadastrados");
                                Console.WriteLine("4 - Ver Clientes cadastrados");
                                Console.WriteLine("5 - Ver Agências cadastradas");
                                Console.WriteLine();
                                Console.WriteLine("9 - Voltar ao menu anterior");
                                Console.Write("\nOpção: ");
                                try
                                {
                                    opcaoGerente = int.Parse(Console.ReadLine());
                                    condicaoDeParada = false;
                                }

                                catch (System.FormatException)
                                {

                                    Console.WriteLine("\nOpção inválida!");
                                    Console.WriteLine("Escolha uma das opções informadas!");
                                    Console.WriteLine("Pressione uma tecla para continuar");
                                    condicaoDeParada = true;
                                    Console.ReadKey();                                    
                                }

                                if (opcaoGerente < 1 || opcaoGerente > 5 && opcaoGerente != 9)
                                {
                                    if (!condicaoDeParada)
                                    {                                        
                                        Console.WriteLine("\nOpção inválida!");
                                        Console.WriteLine("Escolha uma das opções informadas!");
                                        Console.WriteLine("Pressione uma tecla para continuar");
                                        condicaoDeParada = true;
                                        Console.ReadKey();                                        
                                    }                                    
                                }

                            

                                                        
                                switch (opcaoGerente)
                                {
                                    case 1:

                                        Console.Clear();

                                        do
                                        {
                                            Console.Write("Novo funcionário é um gerente? (s/n) ");

                                            try
                                            {
                                                resposta = char.Parse(Console.ReadLine().ToLower());
                                                condicaoDeParada = false;
                                            }

                                            catch (Exception)
                                            {
                                                Console.WriteLine("\nParametro de entrada inválido, escolha entre (s / n)\n");
                                                condicaoDeParada = true;
                                            }

                                            if (resposta != 's' && resposta != 'n')
                                            {
                                                if (!condicaoDeParada)
                                                {
                                                    Console.WriteLine("\nParametro de entrada inválido, escolha entre (s / n)\n");
                                                    condicaoDeParada = true;
                                                }
                                            }


                                        } while (condicaoDeParada);


                                        if (resposta == 'n')
                                        {
                                            g.CadastraFuncionario(gerentes, funcionarios, agencias);
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            g.CadastrarGerente(gerentes, funcionarios, agencias);
                                            Console.ReadKey();
                                        }
                                        break;

                                    case 2:
                                        g.CadastrarAgencia(agencias);
                                        Console.ReadKey();
                                        break;

                                    case 3:
                                        g.VerFuncionariosCadastrados(gerentes, funcionarios);
                                        Console.ReadKey();
                                        break;

                                    case 4:
                                        g.VerClientesCadastrados(clientes);
                                        Console.ReadKey();
                                        break;

                                    case 5:
                                        g.VerAgenciasCadastradas(agencias);
                                        Console.ReadKey();
                                        break;

                                }

                            } while (opcaoGerente != 9);
                        }
                        break;

                    case 2:
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Olá usúario, Infome a opção desejada: \n");
                            Console.WriteLine("1 - Solicitar abertura de conta");
                            Console.WriteLine("2 - Desbloquear cartão");
                            Console.WriteLine("3 - Solicitar empréstimo");
                            Console.WriteLine("4 - Acessar sua conta");
                            Console.WriteLine();
                            Console.WriteLine("9 - Voltar ao menu anterior");
                            Console.Write("\nOpção: ");
                            try
                            {
                                opcaoCliente = int.Parse(Console.ReadLine());
                            }

                            catch (System.FormatException)
                            {
                                Console.WriteLine("\nOpção inválida!");
                                Console.WriteLine("Pressione enter para voltar ao menu");
                                Console.ReadKey();
                                return;
                            }

                            if (opcaoCliente < 1 || opcaoCliente > 4 && opcaoCliente != 9)
                            {

                                Console.WriteLine("\nDigite um número válido!");
                                Console.WriteLine("Pressione enter para voltar ao menu");
                                Console.ReadKey();
                            }

                            switch (opcaoCliente)
                            {
                                case 1:
                                    cliente.SolicitarAbertura(clientes, agencias);
                                    Console.ReadKey();
                                    break;

                                case 2:
                                    cliente.DesbloquearCartao(clientes);
                                    Console.ReadKey();
                                    break;

                                case 3:
                                    cliente.SolicitarEmprestimo(clientes);
                                    Console.ReadKey();
                                    break;

                                case 4:
                                    cliente.AcessarConta(clientes);
                                    break;

                            }

                        } while (opcaoCliente != 9);
                        break;
                }

            } while (opcao != 0);
        }

        static void Main(string[] args)
        {

            IniciarBanco();

        }
    }
}