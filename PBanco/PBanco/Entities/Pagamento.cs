using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBanco.Entities
{
    public class Pagamento
    {
        public DateTime DataTransacao { get; set; }
        public string MetodoOperacao { get; set; }
        public string Tipo { get; set; }
        public double Valor { get; set; }

        public Pagamento()
        {
        }

        public Pagamento(DateTime dataTransacoes, string tipoDaOperacao, double valor, string tipo)
        {
            DataTransacao = dataTransacoes;
            MetodoOperacao = tipoDaOperacao;
            Valor = valor;
            Tipo = tipo;
        }

        public override string ToString()
        {
            return $"Tipo da operação: {MetodoOperacao}\nValor: R$ {Tipo} {Valor:F2}\nData da transação: {DataTransacao.ToShortDateString()}\n";
        }
    }
}
