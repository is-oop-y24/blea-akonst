using Banks.Classes.BankClients;

namespace Banks.Classes.BankAccounts
{
    public abstract class BankAccount
    {
        public int Id { get; set; }
        public int CurrentDate { get; set; }
        public BankClient Owner { get; set; }
        public string BankName { get; set; }
        public double Balance { get; protected set; }
        public double Refill(double sum)
        {
            Balance += sum;

            return Balance;
        }

        public double ImmediatelyWithdraw(double sum)
        {
            Balance -= sum;

            return Balance;
        }

        public abstract void ChargePercentsAndCommissions(int monthsToCharge);
        public abstract double Withdraw(double sum);
    }
}