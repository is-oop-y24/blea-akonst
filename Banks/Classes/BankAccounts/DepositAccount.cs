using Banks.Classes.BankClients;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Classes.BankAccounts
{
    public class DepositAccount : IBankAccount
    {
        public DepositAccount(int id, int depositExpiryDate, string bankName)
        {
            Id = id;
            DepositExpiryDate = depositExpiryDate;
            BankName = bankName;
        }

        public int Id { get; set; }
        public string BankName { get; }
        public double Balance { get; private set; } = 0;
        public BankClient Owner { get; set; }
        public int CurrentDate { get; set; }
        public int DepositExpiryDate { get; }

        public double Refill(double sum)
        {
            Balance += sum;

            return Balance;
        }

        public double Withdraw(double sum)
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

        public double ImmediatelyWithdraw(double sum)
        {
            Balance -= sum;

            return Balance;
        }
    }
}