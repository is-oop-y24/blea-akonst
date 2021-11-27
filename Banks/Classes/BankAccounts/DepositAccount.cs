using Banks.Classes.BankAccounts.Enums;
using Banks.Classes.BankClients;
using Banks.Tools;

namespace Banks.Classes.BankAccounts
{
    public class DepositAccount : BankAccount
    {
        public DepositAccount(int depositExpiryDate, string bankName, BankClient owner)
        {
            DepositExpiryDate = depositExpiryDate;
            BankName = bankName;
            Owner = owner;
        }

        public int DepositExpiryDate { get; }

        public override double Withdraw(double sum)
        {
            if (CurrentDate < DepositExpiryDate)
            {
                throw new BanksException("The term of the deposit has not ended!");
            }

            if (Balance - sum < 0)
            {
                throw new BanksException("Not enough money in the account!");
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