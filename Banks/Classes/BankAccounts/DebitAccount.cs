using Banks.Classes.BankClients;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Classes.BankAccounts
{
    public class DebitAccount : IBankAccount
    {
        public DebitAccount(int id, string bankName)
        {
            Id = id;
            BankName = bankName;
        }

        public int Id { get; set; }
        public string BankName { get; private set; }
        public double Balance { get; private set; } = 0;
        public BankClient Owner { get; set; }
        public int CurrentDate { get; set; }

        public double Refill(double sum)
        {
            Balance += sum;

            return Balance;
        }

        public double Withdraw(double sum)
        {
            if (Balance - sum < 0)
            {
                throw new BanksException("You can't take more money than there is in the account!");
            }

            if (Owner.IsIncorrectClient())
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