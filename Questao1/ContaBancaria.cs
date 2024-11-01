using System;
using System.Globalization;
using System.Xml.Linq;

namespace Questao1
{
    internal class ContaBancaria
    {
        protected int Numero;
        private string Titular;
        private double? DepositoInicial;
        private double Saldo;

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
            CriarSaldoInicial(0);
        }

        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            Numero = numero;
            Titular = titular;
            DepositoInicial = depositoInicial;
            CriarSaldoInicial(depositoInicial);
        }


        private void CriarSaldoInicial(double depositoInicial) 
        {
            if (depositoInicial < 0)
            {
                throw new ArgumentException("O valor do depósito inicial não pode ser negativo");
            }

            Saldo = depositoInicial;
        }
        
        internal void Deposito(double quantia)
        {
            if (quantia < 0) 
            {
                throw new ArgumentException("O valor do depósito não pode ser negativo");
            }

            Saldo += quantia;
        }

        internal void Saque(double quantia)
        {
            if (quantia <= 0)
            {
                throw new ArgumentException("O valor do saque não pode ser negativo");
            }

            Saldo -= quantia;

            DescontoTaxaAdministrativa();
        }
        
        private void DescontoTaxaAdministrativa()
        {
            Saldo -= 3.5;
        }

        public override string ToString()
        {
            return $"Conta: {Numero}, Titular: {Titular}, Saldo: {Saldo:C} ";

        }
    }
}
