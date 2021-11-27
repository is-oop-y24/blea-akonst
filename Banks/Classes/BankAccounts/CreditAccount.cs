using System;
using Banks.Classes.BankClients;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Classes.BankAccounts
{
    public class CreditAccount : IBankAccount
    {
        public CreditAccount(int id, double creditLimit, string bankName)
        {
            Id = id;
            CreditLimit = creditLimit;
            BankName = bankName;
        }

        public int Id { get; set; }
        public string BankName { get; }
        public double Balance { get; private set; } = 0;
        public BankClient Owner { get; set; }
        public double CreditLimit { get; }
        public int CurrentDate { get; set; }

        public double Refill(double sum)
        {
            Balance += sum;

            return Balance;
        }

        public double Withdraw(double sum)
        {
            if (Balance < 0 && Math.Abs(Balance - sum) > CreditLimit)
            {
                throw new BanksException("You can't take more money than there is in the account!");
            }

            if (!Owner.IsIncorrectClient())
            {
                throw new BanksException("Please, fill your address and passport data!");
            }

            Balance -= sum;

            return Balance;
        }

        public double ImmediatelyWithdraw(double sum)
        {
            Balance -= sum;

            return Balance;
        }
    }
}