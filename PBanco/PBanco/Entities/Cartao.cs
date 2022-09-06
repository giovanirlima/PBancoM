using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBanco.Entities
{
    public class Cartao : ContaCorrente
    {
        public double Limite { get; set; }
        public int Vencimento { get; set; }
        public List<Pagamento> RegistroCartao { get; set; }
        public bool VerificarDesbloqueio { get; set; }

        public Cartao()
        {
        }

        public Cartao(double limite, int vencimento)
        {
            Limite = limite;
            Vencimento = vencimento;
            RegistroCartao = new List<Pagamento>();
            VerificarDesbloqueio = false;
        }

        public Cartao(int id, double limite, int vencimento, bool verificarDesbloqueio) : base(id)
        {
            this.Id = id;
            Limite = limite;
            Vencimento = vencimento;
            RegistroCartao = new List<Pagamento>();
            VerificarDesbloqueio = verificarDesbloqueio;
        }

        public void ImprimirExtratoCartao(Cliente cliente)
        {
            Console.Clear();

            Console.WriteLine("Extrato Cartão de Crédito\n");

            foreach (var registro in RegistroCartao)
            {
                Console.WriteLine(registro.ToString());
            }

            Console.WriteLine($"\nSaldo Restante {cliente.ContaCorrente.CartaoDeCredito.Limite.ToString("F2")}");
        }

        public void GerarFatura(Cliente cliente)
        {
            double soma = 0;

            Console.Clear();

            Console.WriteLine("FATURA GERADA\n");

            foreach (var registro in RegistroCartao)
            {
                Console.WriteLine(registro.ToString());
                soma += registro.Valor;
            }

            Console.WriteLine($"\nValor total a pagar: R$ {soma.ToString("F2")}");

            if (soma > 0)
            {

                Console.WriteLine("\nQual será forma de pagamento");
                Console.WriteLine("\n1 - Dinheiro\n2 - Débito em conta");
                Console.Write("\nOpção: ");
                int opcao = int.Parse(Console.ReadLine());

                if (opcao == 1)
                {

                    Console.Write("\nInforme o valor que irá pagar R$: ");
                    double valor = double.Parse(Console.ReadLine());

                    if (valor >= soma)
                    {
                        Console.WriteLine("\nPagamento da fatura efetuado com sucesso!");
                        cliente.ContaCorrente.CartaoDeCredito.Limite += soma;
                        RegistroCartao = new List<Pagamento>();

                        if (valor > soma)
                        {
                            Console.WriteLine($"Seu troco será de: R$ {(valor - soma).ToString("F2")}");
                        }                      
                        
                    }

                    else
                    {
                        Console.WriteLine("\nVocê só pode pagar o valor integral da fatura!");
                        Console.WriteLine("Pagamento cancelado!");
                    }


                }

                else
                {
                    Console.WriteLine($"\nValor que será debitado da conta R$ {soma.ToString("F2")}");
                    Console.WriteLine("Deseja confirmar pagamento: (s/n)");
                    Console.Write("\nResposta: ");
                    char confirmarPagamento = char.Parse(Console.ReadLine().ToLower());

                    if (confirmarPagamento == 's')
                    {
                        if (cliente.ContaCorrente.Saldo >= soma)
                        {
                            Console.WriteLine("\nPagamento da fatura efetuado com sucesso!");
                            Console.WriteLine($"Saldo restante em conta é: {cliente.ContaCorrente.Saldo.ToString("F2")}");
                            cliente.ContaCorrente.Saldo -= soma;
                            cliente.ContaCorrente.CartaoDeCredito.Limite += soma;
                            cliente.ContaCorrente.Registro.Add(new Pagamento(DateTime.Now, "Pagamento fatura cartão", soma));
                            RegistroCartao = new List<Pagamento>();
                            return;
                        }

                        else
                        {
                            Console.WriteLine("\nVocê não possue saldo o suficiente para pagar a fatura!");
                            Console.WriteLine("Pagamento cancelado!");
                            return;
                        }
                    }

                    else
                    {
                        Console.WriteLine("\nVocê só pode pagar o valor integral da fatura!");
                        Console.WriteLine("Pagamento cancelado!");
                    }
                }
            }

            else
            {
                Console.WriteLine("\nFatura do cartão zerada!");
            }
        }

        public void AcessarCartao(Cliente cliente)
        {
            int opcao = 0;
            bool validacao = true;

            do
            {
                Console.Clear();

                Console.WriteLine($"Olá Sr.(a) {cliente.Nome}\n");
                Console.WriteLine("Informe qual Opção deseja\n");

                Console.WriteLine("1 - Imprimir extrato do cartão");
                Console.WriteLine("2 - Gerar fatura para pagamento");

                Console.WriteLine("\n9 - Voltar ao menu anterior");
                Console.Write("\nOpção: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
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
                if (opcao <= 0 || opcao > 2 && opcao != 9)
                {
                    Console.WriteLine("Opção inválida!");
                    Console.WriteLine("Escolha uma das opções informadas!");
                    Console.WriteLine("Pressione uma tecla para continuar");
                    Console.ReadKey();
                    Console.Clear();
                }

                switch (opcao)
                {
                    case 1:
                        cliente.ContaCorrente.CartaoDeCredito.ImprimirExtratoCartao(cliente);
                        Console.ReadKey();
                        break;

                    case 2:
                        cliente.ContaCorrente.CartaoDeCredito.GerarFatura(cliente);
                        Console.ReadKey();
                        break;

                    case 9:
                        validacao = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida");
                        Console.ReadKey();
                        break;
                }

            } while (validacao);
        }

    }
}
