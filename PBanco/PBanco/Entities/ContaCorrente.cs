using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PBanco.Entities
{
    public class ContaCorrente
    {
        public int Id { get; set; }
        public int Senha { get; set; }
        public Agencia Agencia { get; set; }
        public double Saldo { get; set; }
        public double ChequeEspecial { get; set; }
        public bool LimiteUtilizado { get; set; }
        public bool EmprestimoAprovado { get; set; }
        public List<double> Parcelas { get; set; }
        public Cartao CartaoDeCredito { get; set; }
        public List<Pagamento> Registro { get; set; }

        public ContaCorrente()
        {
        }
        public ContaCorrente(int id)
        {
            Id = Id;
        }
        public ContaCorrente(int id, Agencia agencia)
        {
            Id = id;
            Agencia = agencia;
        }
        public ContaCorrente(int id, int senha, Agencia agencia, double saldo, double chequeEspecial, Cartao cartaoDeCredito)
        {
            Id = id;
            Senha = senha;
            Agencia = agencia;
            Saldo = saldo;
            ChequeEspecial = chequeEspecial;
            Parcelas = new List<double>();
            Registro = new List<Pagamento>();
            LimiteUtilizado = false;
            EmprestimoAprovado = false;
            CartaoDeCredito = cartaoDeCredito;
        }


        public void SaldoBancario(Cliente cliente)
        {
            Console.Clear();

            Console.WriteLine($"Olá Sr.(a) {cliente.Nome}");
            Console.WriteLine("\nDeseja ver o saldo da sua: \n\n1 - Conta Corrente\n2 - Conta Poupança ");
            Console.Write("\nOpção: ");
            int opcao = int.Parse(Console.ReadLine());

            CobrarSaldoDevedor(cliente);

            if (opcao == 1)
            {
                Console.Clear();
                Console.WriteLine($"Saldo da Conta Corrente é R$ {cliente.ContaCorrente.Saldo.ToString("F2")}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Saldo da Conta Poupança é R$ {cliente.ContaPoupanca.SaldoCP.ToString("F2")}");
            }

        }
        public void Sacar(Cliente cliente)
        {
            bool verificacao;
            int resposta = 0;

            Console.Clear();

            Console.Write("Informe o valor do saque: R$ ");
            double saque = double.Parse(Console.ReadLine());

            do
            {
                Console.WriteLine("\nDeseja retirar o valor de qual conta");
                Console.Write("\n1 - Conta Corrente\n2 - Conta Poupança\n\nOpção: ");
                resposta = int.Parse(Console.ReadLine());

                if (resposta != 1 && resposta != 2)
                {
                    Console.WriteLine("\nOpção inválida!\n");
                }

            } while (resposta != 1 && resposta != 2);

            if (resposta == 1)
            {
                cliente.ContaCorrente.CobrarSaldoDevedor(cliente);

                verificacao = VerificarSaldoDevedor(cliente, saque);

                if (verificacao)
                {
                    Saldo -= saque;
                    Console.WriteLine("\nOperação efetuada com sucesso!");

                    cliente.ContaCorrente.Registro.Add(new Pagamento(DateTime.Now, "Saque conta corrente", saque, "-"));
                }

                else
                {
                    Console.WriteLine("\nSaque negado!");
                }
            }

            else
            {
                if (cliente.ContaPoupanca.SaldoCP >= saque)
                {
                    cliente.ContaPoupanca.SaldoCP -= saque;
                    Console.WriteLine("\nOperação efetuada com sucesso!");

                    cliente.ContaCorrente.Registro.Add(new Pagamento(DateTime.Now, "Saque conta poupança", saque, "-"));
                }

                else
                {
                    Console.WriteLine("\nSaque negado!");
                }
            }

        }
        public void Depositar(Cliente cliente)
        {
            Console.Clear();

            Console.WriteLine("Qual das contas deseja fazer o depósito?");
            Console.WriteLine("\n1 - Conta corrente\n2 - Conta poupança");
            Console.Write("\nOpção: ");
            int resposta = int.Parse(Console.ReadLine());

            Console.Write("\nInforme o valor que deseja depositar: R$ ");
            double deposito = double.Parse(Console.ReadLine());

            if (resposta == 1)
            {
                Saldo += deposito;
                Registro.Add(new Pagamento(DateTime.Now, "Deposito conta corrente", deposito, "+"));
                Console.WriteLine("\nDeposito efetuado com sucesso!");
            }

            else
            {
                cliente.ContaPoupanca.SaldoCP += deposito;
                Registro.Add(new Pagamento(DateTime.Now, "Deposito conta poupanca", deposito, "+"));
                Console.WriteLine("\nDeposito efetuado com sucesso!");
            }

        }
        public void TransferirDinheiro(Cliente cliente, List<Cliente> clientes)
        {
            bool validacaoId = false;
            double valor;
            Cliente recebedor = new Cliente();

            Console.Clear();

            Console.WriteLine($"Ola Sr.{cliente.Nome}");

            Console.Write("\nInforme o Id da conta que deseja realizar a transfêrencia: ");
            int id = int.Parse(Console.ReadLine());

            foreach (var verificarRecebedor in clientes)
            {
                if (verificarRecebedor.ContaCorrente.Id == id)
                {
                    recebedor = verificarRecebedor;
                    validacaoId = true;
                }
            }

            if (validacaoId)
            {
                if (cliente.ContaCorrente.Id != recebedor.ContaCorrente.Id)
                {
                    do
                    {
                        Console.Write("Informe o valor que deseja transferir: R$ ");
                        valor = double.Parse(Console.ReadLine());

                        if (valor <= 0)
                        {
                            Console.WriteLine("\nValor não pode ser menor ou igual a R$ 0,00\n");
                        }

                    } while (valor <= 0);

                    cliente.ContaCorrente.CobrarSaldoDevedor(cliente);

                    bool saldoNegativo = VerificarSaldoDevedor(cliente, valor);

                    if (!saldoNegativo)
                    {
                        Console.WriteLine("Você não possue saldo suficiente para está transação!");
                        Console.WriteLine("Processo cancelado!");
                    }

                    else
                    {
                        cliente.ContaCorrente.Saldo -= valor;
                        recebedor.ContaCorrente.Saldo += valor;
                        Console.WriteLine($"\nTransfêrencia efetuada com sucesso para {recebedor.Nome}!");

                        Registro.Add(new Pagamento(DateTime.Now, "Transfêrencia Bancaria", valor, "-"));
                    }
                }

                else
                {
                    Console.WriteLine("\nVocê não pode fazer uma transfêrencia para você mesmo!");
                    Console.WriteLine("Pressione enter para voltar ao menu anterior!");
                }
            }

            else
            {
                Console.WriteLine("\nId inválido!");
                Console.WriteLine("Pressione enter para voltar ao menu anterior!");
                return;
            }
        }
        public void ImprimirExtrato()
        {
            Console.Clear();

            Console.WriteLine("Extrato conta corrente\n");

            foreach (var registro in Registro)
            {
                Console.WriteLine(registro.ToString());
            }
        }
        public void PagarContas(Cliente cliente)
        {
            double valor = 0;
            int formaDePagamento = 0;
            bool verificarResposta = false;

            Console.Clear();

            do
            {
                Console.Write("Informe o valor da conta que deseja pagar: R$ ");
                valor = double.Parse(Console.ReadLine());

                if (valor <= 0)
                {
                    Console.WriteLine("\nVocê não pode pagar uma conta, com o valor sendo menor ou igual a R$ 0,00");
                    Console.WriteLine("Pressione enter para retornar ao menu anterior!");
                    return;
                }

            } while (valor <= 0);

            do
            {
                Console.WriteLine("\nForma de pagamento será\n\n1 - Débito \n2 - Crédito ");
                Console.Write("\nOpção: ");
                formaDePagamento = int.Parse(Console.ReadLine());
                verificarResposta = true;

                if (formaDePagamento != 1 && formaDePagamento != 2)
                {
                    Console.WriteLine("\nOpção inválida!");
                    Console.WriteLine("Digite uma opção enter 1 ou 2");
                    verificarResposta = false;
                }

            } while (!verificarResposta);


            if (formaDePagamento == 1)
            {

                cliente.ContaCorrente.CobrarSaldoDevedor(cliente);

                if (VerificarSaldoDevedor(cliente, valor))
                {
                    Console.WriteLine($"\nValor de R$ {valor.ToString("F2")} será debitado de sua conta corrente");

                    Saldo -= valor;

                    Registro.Add(new Pagamento(DateTime.Now, "Pagamento de Conta", valor, "-"));

                    Console.WriteLine("\nPagamento efetuado com sucesso!");
                }

                else
                {
                    Console.WriteLine("\nVocê não possue saldo suficiente para esta operação!");
                }
            }

            else
            {
                if (cliente.ContaCorrente.CartaoDeCredito.VerificarDesbloqueio == true)
                {
                    if (cliente.ContaCorrente.CartaoDeCredito.Limite >= valor)
                    {
                        Console.WriteLine("\nPagamento aprovado!");
                        cliente.ContaCorrente.CartaoDeCredito.Limite -= valor;
                        cliente.ContaCorrente.CartaoDeCredito.RegistroCartao.Add(new Pagamento(DateTime.Now, "Pagamento de contas", valor, "-"));
                    }

                    else
                    {
                        Console.WriteLine("\nSaldo insuficiente!");
                    }
                }
                else
                {
                    Console.WriteLine("\nCartão bloqueado! Desbloqueie seu cartão.");
                }
            }
        }
        public bool VerificarSaldoDevedor(Cliente cliente, double valor)
        {
            if (cliente.ContaCorrente.LimiteUtilizado == false)
            {
                if (cliente.ContaCorrente.Saldo - valor < 0)
                {
                    Console.WriteLine("\nNão é possivel efetuar a operação, pois você não tem dinheiro suficiente em conta!");
                    Console.WriteLine("Deseja utilizar o valor do cheque especial, porém será cobrado o valor de R$ 20 pelo uso (S/N)");
                    Console.Write("\nResposta: ");
                    string resposta = Console.ReadLine().ToLower();

                    if (resposta == "s" || resposta == "sim")
                    {
                        cliente.ContaCorrente.LimiteUtilizado = true;
                        cliente.ContaCorrente.Saldo += cliente.ContaCorrente.ChequeEspecial;

                        if (cliente.ContaCorrente.Saldo - valor < 0)
                        {
                            Console.WriteLine("\nCheque especial não cobre o valor da conta, operação negada!");
                            cliente.ContaCorrente.Saldo -= ChequeEspecial;
                            cliente.ContaCorrente.LimiteUtilizado = false;

                            return false;
                        }

                        else
                        {
                            cliente.ContaCorrente.ChequeEspecial += 20;
                            return true;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }


            }
            else
            {
                if (cliente.ContaCorrente.Saldo >= valor)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void CobrarSaldoDevedor(Cliente cliente)
        {
            if (cliente.ContaCorrente.LimiteUtilizado == true)
            {
                if (cliente.ContaCorrente.Saldo >= cliente.ContaCorrente.ChequeEspecial)
                {
                    cliente.ContaCorrente.Saldo -= cliente.ContaCorrente.ChequeEspecial;
                    cliente.ContaCorrente.Registro.Add(new Pagamento(DateTime.Now, "Valor cobrado cheque especial", cliente.ContaCorrente.ChequeEspecial, "-"));
                    cliente.ContaCorrente.ChequeEspecial -= 20;
                    cliente.ContaCorrente.LimiteUtilizado = false;
                }
            }
        }
        public void VerParcelasDoEmprestimo(Cliente cliente)
        {
            int quantidadeParcelas = 0, pularLinha = 0;

            Console.Clear();

            if (cliente.ContaCorrente.EmprestimoAprovado == true)
            {

                Console.WriteLine("PARCELAS A PAGAR\n");

                foreach (var parcelas in Parcelas)
                {

                    Console.Write($"Parcela {quantidadeParcelas + 1}: R$ {parcelas.ToString("F2")} \t");

                    if (pularLinha == 3)
                    {
                        Console.WriteLine();
                        pularLinha = -1;
                    }
                    quantidadeParcelas++;
                    pularLinha++;
                }
                Console.WriteLine("\nPressione enter para sair!");
            }

            else
            {
                Console.WriteLine("Você não possue empréstimo no momento!");
            }

        }
        public void PagarEmprestimo(Cliente cliente)
        {
            int quantidadeParcelas = 0, pularLinha = 0;

            Console.Clear();

            if (cliente.ContaCorrente.EmprestimoAprovado == true)
            {
                Console.WriteLine("PARCELAS A PAGAR\n");

                foreach (var parcelas in Parcelas)
                {

                    Console.Write($"Parcela {quantidadeParcelas + 1}: R$ {parcelas.ToString("F2")} \t");

                    if (pularLinha == 3)
                    {
                        Console.WriteLine();
                        pularLinha = -1;
                    }
                    quantidadeParcelas++;
                    pularLinha++;
                }

                Console.WriteLine("\nDeseja pagar parcela");
                Console.Write("\n1 - Sim\n2 - Não\n\nOpção: ");
                int resposta = int.Parse(Console.ReadLine());

                if (resposta == 1)
                {
                    if (cliente.ContaCorrente.Saldo >= cliente.ContaCorrente.Parcelas[0])
                    {
                        Console.WriteLine("\nValor será debitado de sua conta corrente!");
                        Console.WriteLine("\nPagamento efetuado com sucesso!");
                        cliente.ContaCorrente.Saldo -= cliente.ContaCorrente.Parcelas[0];
                        cliente.ContaCorrente.Registro.Add(new Pagamento(DateTime.Now, "Pagamento parcela empréstimo", cliente.ContaCorrente.Parcelas[0], "-"));
                        cliente.ContaCorrente.Parcelas.RemoveAt(0);
                    }

                    else
                    {
                        Console.WriteLine("\nVocê não possui saldo suficiente na conta!");
                        Console.WriteLine("\nPagamento cancelado!");
                    }
                }

                else
                {
                    Console.WriteLine("Até mais");
                }
            }

            else
            {
                Console.WriteLine("Você não possue empréstimo no momento!");
            }
        }

    }
}
