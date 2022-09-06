using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBanco.Entities
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
        public double Salario { get; set; }
        public ContaCorrente ContaCorrente { get; set; }
        public ContaPoupanca ContaPoupanca { get; set; }
        public Cartao CartaoDeCredito { get; set; }

        public Cliente()
        {
        }
        public Cliente(string nome, string cpf, DateTime nascimento, string telefone, Endereco endereco, double salario, ContaCorrente contaCorrente, ContaPoupanca contaPoupanca, Cartao cartaoDeCredito)
        {
            Nome = nome;
            CPF = cpf;
            Nascimento = nascimento;
            Telefone = telefone;
            Endereco = endereco;
            Salario = salario;
            ContaCorrente = contaCorrente;
            ContaPoupanca = contaPoupanca;
            CartaoDeCredito = cartaoDeCredito;
        }

        public Cliente SolicitarAbertura(List<Cliente> clientes, List<Agencia> agencias)
        {
            Cliente cliente;
            Funcionario funcionario = new Funcionario();

            Console.Clear();

            Console.WriteLine("Olá Sr.(a) cliente!");
            Console.Write("\nDeseja solicitar abertura de uma conta? (s/n): ");
            char resposta = char.Parse(Console.ReadLine().ToLower());

            if (resposta == 's')
            {
                return funcionario.CadastrarCliente(clientes, agencias);
            }
            else
            {
                Console.WriteLine("Até logo!");
                return null;
            }
        }
        public void DesbloquearCartao(List<Cliente> clientes)
        {
            int id;
            bool validarId = true, validarSenha = true, verificarId = true;
            double senha;

            do
            {
                Console.Clear();

                Console.Write("Olá cliente, \n\nInfome o id da sua conta: ");
                id = int.Parse(Console.ReadLine());

                foreach (var cliente in clientes)
                {
                    if (cliente.ContaCorrente.Id == id)
                    {
                        validarId = false;
                    }
                }

                if (!validarId)
                {
                    Console.Write("Senha: ");
                    senha = int.Parse(Console.ReadLine());

                    foreach (var cliente in clientes)
                    {
                        if (cliente.ContaCorrente.Senha == senha)
                        {
                            validarSenha = false;
                        }
                    }

                    if (validarSenha)
                    {
                        Console.WriteLine("Senha inválida!");
                        Console.WriteLine("Pressione enter para continuar");
                        validarSenha = true;
                        Console.ReadKey();
                    }

                }

                else
                {
                    Console.WriteLine("Id inválido!");
                    Console.WriteLine("Pressione enter para continuar");
                    validarId = true;
                    Console.ReadKey();
                }

            } while (validarSenha);

            Console.Clear();

            Console.WriteLine("Olá, Sr.(a) cliente\n");
            Console.WriteLine("Deseja desbloquear seu cartão? (s/n)");
            char resposta = char.Parse(Console.ReadLine().ToLower());

            if (resposta == 's')
            {
                do
                {
                    Console.Write("\nInforme o id da Conta Corrente do cliente: ");
                    id = int.Parse(Console.ReadLine());

                    foreach (var cliente in clientes)
                    {
                        if (cliente.CartaoDeCredito.Id == id)
                        {
                            if (cliente.ContaCorrente.CartaoDeCredito.VerificarDesbloqueio == false)
                            {
                                verificarId = false;
                                cliente.ContaCorrente.CartaoDeCredito.VerificarDesbloqueio = true;

                                Console.WriteLine("Cartão desbloquado com sucesso!");
                                cliente.ContaCorrente.CartaoDeCredito.Limite += cliente.ContaCorrente.ChequeEspecial;

                                Console.WriteLine($"\nSr.(a) {cliente.Nome} seu cartão foi desbloqueado com limite de R$ {cliente.ContaCorrente.CartaoDeCredito.Limite.ToString("F2")}");
                            }

                            else
                            {
                                Console.WriteLine("\ncliente já desbloqueou seu cartão!");
                                Console.WriteLine($"Saldo do cartão: {cliente.CartaoDeCredito.Limite.ToString("F2")}");
                                verificarId = false;

                            }
                        }
                    }                    

                } while (verificarId);
            }

            else
            {
                Console.WriteLine("Processo cancelado pelo usuário!");
            }
        }
        public void SolicitarEmprestimo(List<Cliente> clientes)
        {
            int id, senha;
            double valorFinal = 0, parcelas, valorSolicitado;
            bool verificarId = false, verificarAprovacao;
            bool validarId = true, validarSenha = true;
            Gerente gerente = new Gerente();

            do
            {
                Console.Clear();

                Console.Write("Olá cliente, \n\nInfome o id da sua conta: ");
                id = int.Parse(Console.ReadLine());

                foreach (var cliente in clientes)
                {
                    if (cliente.ContaCorrente.Id == id)
                    {
                        validarId = false;
                    }
                }

                if (!validarId)
                {
                    Console.Write("Senha: ");
                    senha = int.Parse(Console.ReadLine());

                    foreach (var cliente in clientes)
                    {
                        if (cliente.ContaCorrente.Senha == senha)
                        {
                            validarSenha = false;
                        }
                    }

                    if (validarSenha)
                    {
                        Console.WriteLine("Senha inválida!");
                        Console.WriteLine("Pressione enter para continuar");
                        validarSenha = true;
                        Console.ReadKey();
                    }

                }

                else
                {
                    Console.WriteLine("Id inválido!");
                    Console.WriteLine("Pressione enter para continuar");
                    validarId = true;
                    Console.ReadKey();
                }

            } while (validarSenha);

            do
            {
                foreach (var cliente in clientes)
                {
                    if (cliente.ContaCorrente.Id == id)
                    {
                        Console.Clear();
                        Console.WriteLine($"Olá sr.(a) {cliente.Nome}");
                        do
                        {
                            Console.Write("\nInforme o valor que deseja solicitar: R$ ");
                            valorSolicitado = double.Parse(Console.ReadLine());

                            if (valorSolicitado <= 0)
                            {
                                Console.WriteLine("Empréstimo não pode ser menor ou igual a R$ 00,00");
                            }

                        } while (valorSolicitado <= 0);
                        

                        Console.WriteLine("\nSolicitação sendo encaminhada para aprovação!");
                        Console.WriteLine("Pressione enter para continuar!");
                        Console.ReadKey();

                        cliente.ContaCorrente.EmprestimoAprovado = verificarAprovacao = gerente.AprovarEmprestimo(cliente, valorSolicitado);

                        if (!verificarAprovacao)
                        {
                            Console.WriteLine("\nRetornando resposta para o cliente!");
                            Console.WriteLine("Pressione enter para continuar!");

                            Console.ReadKey();

                            Console.Clear();

                            Console.WriteLine("\nPedido de empréstimo reprovado!");
                            Console.WriteLine("Tente novamente mais tarde!");

                            verificarId = false;
                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Pedido de empréstimo aprovado\n");
                            Console.Write("Deseja pagar o empréstimo em\n\n1 - 36 vezes \n2 - 72 vezes \n\nOpção: ");
                            int resposta = int.Parse(Console.ReadLine());
                            Console.WriteLine("Valor da taxa de juros será de 1% do valor solicitado ao mês!");
                            Console.WriteLine("\nValor das parcelas: \n");
                            if (resposta == 1)
                            {
                                parcelas = 36;
                            }
                            else
                            {
                                parcelas = 72;
                            }

                            Console.WriteLine("Parcelas: \n");

                            for (int i = 0, j = 0; i < parcelas; i++, j++)
                            {

                                valorFinal = (valorSolicitado / parcelas) + (valorSolicitado * 0.01);
                                cliente.ContaCorrente.Parcelas.Add(valorFinal);
                                Console.Write($"Parcela {i + 1}: R$ {valorFinal.ToString("F2")}\t");
                                if (j == 3)
                                {
                                    j = -1;
                                    Console.WriteLine();
                                }
                            }

                            Console.Write("\nConfirmar solicitação: \n\n1 - Sim\n2 - Não\n\nOpção: ");
                            resposta = int.Parse(Console.ReadLine());

                            if (resposta == 1)
                            {
                                Console.WriteLine($"\nValor de R$ {valorSolicitado.ToString("F2")} será depositado em sua conta!");
                                Console.WriteLine("Até mais!");
                                cliente.ContaCorrente.Saldo += valorSolicitado;
                                cliente.ContaCorrente.Registro.Add(new Pagamento(DateTime.Now, "Empréstimo", valorSolicitado));
                                verificarId = false;
                            }

                            else
                            {
                                Console.WriteLine("\nSolicitação cancelada pelo cliente!");
                            }
                        }
                    }
                }               

            } while (verificarId);

        }
        public void AcessarConta(List<Cliente> clientes)
        {
            int id, opcao = 0;
            bool validacao = true;
            int senha;


            Console.Clear();
            Console.WriteLine("Olá Sr.(a) Cliente!");

            do
            {
                Console.Write("\nInforme o id da sua conta: ");
                id = int.Parse(Console.ReadLine());

                foreach (var cliente in clientes)
                {
                    if (cliente.ContaCorrente.Id == id)
                    {
                        Console.Write("Senha: ");
                        senha = int.Parse(Console.ReadLine());

                        if (cliente.ContaCorrente.Senha == senha)
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine($"Olá Sr.(a) {cliente.Nome}\n");
                                Console.WriteLine("Informe qual Opção deseja\n");

                                Console.WriteLine("1 - Ver saldo da conta");
                                Console.WriteLine("2 - Sacar dinheiro");
                                Console.WriteLine("3 - Depositar dinheiro");
                                Console.WriteLine("4 - Transferir dinheiro");
                                Console.WriteLine("5 - Extrato de movimentação");
                                Console.WriteLine("6 - Pagar conta");
                                Console.WriteLine("7 - Ver parcelas do empréstimo");
                                Console.WriteLine("8 - Pagar parcelas do empréstimo");
                                Console.WriteLine("9 - Acessar seu cartao");
                                Console.WriteLine("\n10 - Voltar ao menu anterior");
                                Console.Write("\nOpção: ");
                                try
                                {
                                    opcao = int.Parse(Console.ReadLine());
                                    validacao = false;
                                }
                                catch (System.FormatException)
                                {

                                    Console.WriteLine("Opção inválida!");
                                    Console.WriteLine("Escolha uma das opções informadas!");
                                    Console.WriteLine("Pressione uma tecla para continuar");
                                    validacao = true;
                                    Console.ReadKey();
                                    Console.Clear();

                                }
                                if (opcao < 0 || opcao > 10)
                                {
                                    Console.WriteLine("Opção inválida!");
                                    Console.WriteLine("Escolha uma das opções informadas!");
                                    Console.WriteLine("Pressione uma tecla para continuar");
                                    validacao = true;
                                    Console.ReadKey();
                                    Console.Clear();
                                }

                                switch (opcao)
                                {
                                    case 1:
                                        cliente.ContaCorrente.SaldoBancario(cliente);
                                        Console.ReadKey();
                                        break;

                                    case 2:
                                        cliente.ContaCorrente.Sacar(cliente);
                                        Console.ReadKey();
                                        break;

                                    case 3:
                                        cliente.ContaCorrente.Depositar(cliente);
                                        Console.ReadKey();
                                        break;

                                    case 4:
                                        cliente.ContaCorrente.TransferirDinheiro(cliente, clientes);
                                        Console.ReadKey();
                                        break;

                                    case 5:
                                        cliente.ContaCorrente.ImprimirExtrato();
                                        Console.ReadKey();
                                        break;

                                    case 6:
                                        cliente.ContaCorrente.PagarContas(cliente);
                                        Console.ReadKey();
                                        break;

                                    case 7:
                                        cliente.ContaCorrente.VerParcelasDoEmprestimo(cliente);
                                        Console.ReadKey();
                                        break;

                                    case 8:
                                        cliente.ContaCorrente.PagarEmprestimo(cliente);
                                        Console.ReadKey();
                                        break;

                                    case 9:
                                        cliente.ContaCorrente.CartaoDeCredito.AcessarCartao(cliente);
                                        break;

                                    case 10:
                                        validacao = false;
                                        break;

                                    default:
                                        break;
                                }

                            } while (opcao != 10);

                        }
                        else
                        {
                            Console.WriteLine("Senha invalida!");
                            Console.ReadKey();
                            return;
                        }
                    }

                    else if (validacao)
                    {
                        Console.WriteLine("\nId inválido!");
                        Console.WriteLine("Pressione enter para continuar!");
                        Console.ReadKey();
                        return;
                    }
                }

            } while (validacao);
        }

        public override string ToString()
        {
            return $"Nome: {Nome}\nCPF: {CPF}\nData de Nascimento: {Nascimento.ToShortDateString()}\nTelefone: {Telefone}" +
                   $"\nRenda mensal: R$ {Salario.ToString("F2")}\nConta id: {ContaCorrente.Id}\n";
        }
    }
}
