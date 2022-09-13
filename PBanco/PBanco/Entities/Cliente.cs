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

        public Cliente()
        {
        }
        public Cliente(string nome, string cpf, DateTime nascimento, string telefone, Endereco endereco, double salario, ContaCorrente contaCorrente, ContaPoupanca contaPoupanca)
        {
            Nome = nome;
            CPF = cpf;
            Nascimento = nascimento;
            Telefone = telefone;
            Endereco = endereco;
            Salario = salario;
            ContaCorrente = contaCorrente;
            ContaPoupanca = contaPoupanca;
        }

        public void SolicitarAbertura(List<Cliente> clientes, List<Agencia> agencias)
        {
            Funcionario funcionario = new Funcionario();
            char resposta = 'a';
            bool validacao;

            do
            {
                Console.Clear();

                Console.WriteLine("Olá Sr.(a) cliente!");
                Console.Write("\nDeseja solicitar abertura de uma conta? (s/n): ");

                try
                {
                    resposta = char.Parse(Console.ReadLine().ToLower());
                    validacao = false;
                }

                catch (Exception)
                {
                    Console.WriteLine("\nParametro digitado inválido, digite apenas (s / n):");
                    Console.WriteLine("Digite enter para iniciar novamente!");
                    validacao = true;
                }

                if (resposta != 's' && resposta != 'n')
                {
                    if (!validacao)
                    {
                        Console.WriteLine("\nParametro digitado inválido, digite apenas (s / n):");
                        Console.WriteLine("Digite enter para iniciar novamente!");
                        validacao = true;
                    }
                }

            } while (validacao);


            if (resposta == 's')
            {
                funcionario.CadastrarCliente(clientes, agencias);
            }
            else
            {
                Console.WriteLine("Até logo!");
                return;
            }
        }
        public void DesbloquearCartao(List<Cliente> clientes)
        {
            int id;
            bool validarId = true, validarSenha = true, verificarId, condicaoDeParada;
            double senha;
            char resposta = 'a';
            Cliente cliente = new Cliente();

            do
            {
                Console.Clear();

                Console.Write("Olá Sr.(a) cliente, \n\nInfome o id da sua conta: ");
                id = int.Parse(Console.ReadLine());

                foreach (var c in clientes)
                {
                    if (c.ContaCorrente.Id == id)
                    {
                        validarId = false;
                    }
                }

                if (!validarId)
                {
                    Console.Write("Senha: ");
                    senha = int.Parse(Console.ReadLine());

                    foreach (var c in clientes)
                    {
                        if (c.ContaCorrente.Senha == senha)
                        {
                            validarSenha = false;
                            cliente = c;
                        }
                    }

                    if (validarSenha)
                    {
                        Console.WriteLine("\nSenha inválida!");
                        Console.WriteLine("Pressione enter para voltar ao menu anterior");
                        return;
                    }

                }

                else
                {
                    Console.WriteLine("\nId inválido!");
                    Console.WriteLine("Pressione enter para voltar ao menu anterior");
                    return;
                }

            } while (validarSenha);

            do
            {
                Console.Clear();

                Console.WriteLine($"Olá, Sr.(a) {cliente.Nome}\n");
                Console.WriteLine("Deseja confirmar o desbloqueio do seu cartão? (s/n)");
                Console.Write("Resposta: ");

                try
                {
                    resposta = char.Parse(Console.ReadLine().ToLower());
                    condicaoDeParada = false;
                }

                catch (Exception)
                {
                    Console.WriteLine("\nOpção digitada é inválida!");
                    Console.WriteLine("Pressione enter para escolher novamente!");
                    Console.ReadKey();
                    condicaoDeParada = true;
                }

                if (resposta != 's' && resposta != 'n')
                {
                    if (!condicaoDeParada)
                    {
                        Console.WriteLine("\nOpção digitada é inválida!");
                        Console.WriteLine("Pressione enter para escolher novamente!");
                        Console.ReadKey();
                        condicaoDeParada = true;
                    }

                }

                else
                {
                    condicaoDeParada = false;
                }

            } while (condicaoDeParada);

            if (resposta == 's')
            {
                do
                {
                    if (cliente.ContaCorrente.CartaoDeCredito.VerificarDesbloqueio == false)
                    {
                        verificarId = false;
                        cliente.ContaCorrente.CartaoDeCredito.VerificarDesbloqueio = true;

                        Console.WriteLine("\nCartão desbloquado com sucesso!");
                        cliente.ContaCorrente.CartaoDeCredito.Limite += cliente.ContaCorrente.ChequeEspecial;

                        Console.WriteLine($"\nSr.(a) {cliente.Nome} seu cartão foi desbloqueado com limite de R$ {cliente.ContaCorrente.CartaoDeCredito.Limite.ToString("F2")}");
                    }

                    else
                    {
                        Console.WriteLine("\ncliente já desbloqueou seu cartão!");
                        Console.WriteLine($"Saldo do cartão: R$ {cliente.ContaCorrente.CartaoDeCredito.Limite.ToString("F2")}");
                        verificarId = false;

                    }


                } while (verificarId);
            }

            else
            {
                Console.WriteLine("\nProcesso cancelado pelo usuário!");
            }
        }
        public void SolicitarEmprestimo(List<Cliente> clientes)
        {
            int id, senha, resposta = 0;
            double valorFinal, parcelas, valorSolicitado = 0;
            bool verificarId = false, verificarAprovacao, condicaoDeParada;
            bool validarId = true, validarSenha = true;
            Gerente gerente = new Gerente();
            Cliente cliente = new Cliente();


            do
            {
                Console.Clear();

                Console.Write("Olá cliente, \n\nInfome o id da sua conta: ");
                id = int.Parse(Console.ReadLine());

                foreach (var c in clientes)
                {
                    if (c.ContaCorrente.Id == id)
                    {
                        validarId = false;
                    }
                }

                if (!validarId)
                {
                    Console.Write("Senha: ");
                    senha = int.Parse(Console.ReadLine());

                    foreach (var c in clientes)
                    {
                        if (c.ContaCorrente.Senha == senha)
                        {
                            validarSenha = false;
                            cliente = c;
                        }
                    }

                    if (validarSenha)
                    {
                        Console.WriteLine("\nSenha inválida!");
                        Console.WriteLine("Pressione enter para voltar ao menu anterior");
                        return;
                    }

                }

                else
                {
                    Console.WriteLine("\nId inválido!");
                    Console.WriteLine("Pressione enter para voltar ao menu anterior");
                    return;
                }

            } while (validarSenha);

            if (cliente.ContaCorrente.EmprestimoAprovado == true)
            {
                if (cliente.ContaCorrente.Parcelas.Count == 0)
                {
                    cliente.ContaCorrente.EmprestimoAprovado = false;
                }

            }

            if (cliente.ContaCorrente.EmprestimoAprovado == false)
            {

                do
                {
                    do
                    {
                        Console.Clear();

                        Console.WriteLine($"Olá sr.(a) {cliente.Nome}");
                        Console.Write("\nInforme o valor que deseja solicitar: R$ ");

                        try
                        {
                            valorSolicitado = double.Parse(Console.ReadLine());
                            condicaoDeParada = false;
                        }

                        catch (Exception)
                        {
                            Console.WriteLine("\nDigite apenas números!");
                            Console.WriteLine("Pressione enter para continuar!");
                            Console.ReadKey();
                            condicaoDeParada = true;
                        }

                        if (valorSolicitado <= 0)
                        {
                            if (!condicaoDeParada)
                            {
                                Console.WriteLine("\nValor não pode ser menor ou igual a R$ 00,00");
                                Console.WriteLine("Pressione enter para continuar!");
                                Console.ReadKey();
                                condicaoDeParada = true;
                            }
                        }

                    } while (condicaoDeParada);


                    Console.WriteLine("\nSolicitação sendo encaminhada para aprovação!");
                    Console.WriteLine("Pressione enter para continuar!");
                    Console.ReadKey();

                    verificarAprovacao = gerente.AprovarEmprestimo(cliente, valorSolicitado);

                    if (!verificarAprovacao)
                    {
                        Console.WriteLine("Retornando resposta para o cliente!");
                        Console.WriteLine("Pressione enter para continuar!");

                        Console.ReadKey();

                        Console.Clear();

                        Console.WriteLine("Pedido de empréstimo reprovado!");
                        Console.WriteLine("Tente novamente mais tarde!");

                        verificarId = false;
                    }

                    else
                    {
                        do
                        {
                            Console.Clear();

                            Console.WriteLine("Pedido de empréstimo aprovado!");
                            Console.Write("\nDeseja pagar o empréstimo em\n\n1 - 36 vezes \n2 - 72 vezes \n\nOpção: ");

                            try
                            {
                                resposta = int.Parse(Console.ReadLine());
                                condicaoDeParada = false;
                            }

                            catch (Exception)
                            {
                                Console.WriteLine("\nOpção inválida!\n");
                                Console.WriteLine("Pressione enter para continuar!");
                                Console.ReadKey();
                                condicaoDeParada = true;
                            }

                            if (resposta != 1 && resposta != 2)
                            {
                                if (!condicaoDeParada)
                                {
                                    Console.WriteLine("\nOpção escolhida é inexistente!");
                                    Console.WriteLine("Escolha 1 ou 2\n");
                                    Console.WriteLine("Pressione enter para continuar!");
                                    Console.ReadKey();
                                    condicaoDeParada = true;
                                }
                            }

                        } while (condicaoDeParada);

                        Console.Clear();
                        do
                        {

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

                            Console.WriteLine("\nDeseja aceitar o empréstimo: ");
                            Console.Write("\n1 - Sim\n2 - Não\n\nOpção: ");

                            try
                            {
                                resposta = int.Parse(Console.ReadLine());
                                condicaoDeParada = false;
                            }

                            catch (Exception)
                            {
                                Console.WriteLine("\nOpção inválida!\n");
                                Console.WriteLine("Pressione enter para continuar!");
                                Console.ReadKey();
                                condicaoDeParada = true;
                            }

                            if (resposta != 1 && resposta != 2)
                            {
                                if (!condicaoDeParada)
                                {
                                    Console.WriteLine("\nOpção escolhida é inexistente!");
                                    Console.WriteLine("Escolha 1 ou 2\n");
                                    Console.WriteLine("Pressione enter para continuar!");
                                    Console.ReadKey();
                                    condicaoDeParada = true;
                                }
                            }

                        } while (condicaoDeParada);

                        if (resposta == 1)
                        {
                            Console.WriteLine($"\nValor de R$ {valorSolicitado.ToString("F2")} será depositado em sua conta!");
                            Console.WriteLine("Até mais!");
                            cliente.ContaCorrente.EmprestimoAprovado = true;
                            cliente.ContaCorrente.Saldo += valorSolicitado;
                            cliente.ContaCorrente.Registro.Add(new Pagamento(DateTime.Now, "Empréstimo", valorSolicitado, "+"));
                            verificarId = false;
                        }

                        else
                        {
                            cliente.ContaCorrente.EmprestimoAprovado = false;
                            Console.WriteLine("\nSolicitação cancelada pelo cliente!");
                        }
                    }

                } while (verificarId);
            }

            else
            {
                Console.WriteLine("\nCliente já possue um empréstimo!");
            }
        }
        public void AcessarConta(List<Cliente> clientes)
        {
            int id = 0, opcao = 0;
            bool validacao = true, validacaoSenha = false, validacaoId = false;
            int senha;
            Cliente cliente = new Cliente();


            Console.Clear();

            Console.WriteLine("Olá Sr.(a) Cliente!");
            Console.Write("\nInforme o id da sua conta: ");

            try
            {
                id = int.Parse(Console.ReadLine());
            }

            catch (System.FormatException)
            {
                Console.WriteLine("\nFormato inválido!");
                Console.ReadKey();
                return;
            }

            foreach (var c in clientes)
            {
                if (c.ContaCorrente.Id == id)
                {
                    validacaoId = true;
                }
            }

            if (validacaoId)
            {
                Console.Write("Senha: ");
                senha = int.Parse(Console.ReadLine());

                foreach (var c in clientes)
                {
                    if (c.ContaCorrente.Senha == senha)
                    {
                        validacaoSenha = true;
                        cliente = c;
                    }
                }

                if (!validacaoSenha)
                {
                    Console.WriteLine("\nSenha inválida!");
                    Console.WriteLine("Pressione enter para voltar ao menu anterior");
                    Console.ReadKey();
                    return;
                }
            }

            else
            {
                Console.WriteLine("\nId inválido!");
                Console.WriteLine("Pressione enter para voltar ao menu anterior");
                Console.ReadKey();
                return;
            }

            if (validacaoSenha)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine($"Olá Sr.(a) {cliente.Nome}\n");
                    Console.WriteLine("Informe qual opção deseja:\n");

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
                        Console.ReadKey();
                        validacao = true;
                    }

                    if (opcao <= 0 || opcao > 10)
                    {
                        if (!validacao)
                        {
                            Console.WriteLine("\nOpção inválida!");
                            Console.WriteLine("Pressione uma tecla para continuar");
                            Console.ReadKey();
                            validacao = true;
                        }

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
        }

        public override string ToString()
        {
            return $"Nome: {Nome}\nCPF: {CPF}\nData de Nascimento: {Nascimento.ToShortDateString()}\nTelefone: {Telefone}" +
                   $"\nRenda mensal: R$ {Salario.ToString("F2")}\nConta id: {ContaCorrente.Id}\n";
        }
    }

}
