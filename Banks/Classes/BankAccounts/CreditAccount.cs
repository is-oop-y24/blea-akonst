using System;
using Banks.Tools;

namespace Banks.Classes.BankAccounts
{
    public class CreditAccount : BankAccount
    {
        public double CreditLimit { get; set; }
        public double CreditComission { get; set; }

        public override void ChargePercentsAndCommissions(int monthsToCharge)
        {
            ImmediatelyWithdraw(CreditComission * monthsToCharge);
        }

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