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
        public string TipoDaOperacao { get; set; }
        public double Valor { get; set; }

        public Pagamento()
        {
        }

        public Pagamento(DateTime dataTransacoes, string tipoDaOperacao, double valor)
        {
            DataTransacao = dataTransacoes;
            TipoDaOperacao = tipoDaOperacao;
            Valor = valor;
        }

        public override string ToString()
        {
            return $"Tipo da operação: {TipoDaOperacao} - Valor: R$ {Valor:F2} - Data da transação: {DataTransacao.ToShortDateString()}";
        }
    }
}
