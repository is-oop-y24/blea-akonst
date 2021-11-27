using System;
using Banks.Classes.BankAccounts.Enums;
using Banks.Classes.BankClients;
using Banks.Tools;

namespace Banks.Classes.BankAccounts
{
    public class CreditAccount : BankAccount
    {
        public CreditAccount(double creditLimit, string bankName, BankClient owner)
        {
            CreditLimit = creditLimit;
            BankName = bankName;
            Owner = owner;
        }

        public double CreditLimit { get; }

        public override double Withdraw(double sum)
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
    }
}