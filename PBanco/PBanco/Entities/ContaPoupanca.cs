using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBanco.Entities
{
    public class ContaPoupanca : ContaCorrente
    {
        public double SaldoCP { get; set; }

        public ContaPoupanca(int id, Agencia agencia, double saldoCP) : base(id, agencia)
        {
            SaldoCP = saldoCP;
        }
                

    }
}
