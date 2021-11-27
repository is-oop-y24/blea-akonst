using Banks.Classes.BankClients;

namespace Banks.Interfaces
{
    public interface IBankAccount
    {
        int Id { get; set; }
        int CurrentDate { get; set; }
        string BankName { get; }
        double Balance { get; }
        BankClient Owner { get; set; }
        double Refill(double sum);
        double Withdraw(double sum);
        double ImmediatelyWithdraw(double sum);
    }
}