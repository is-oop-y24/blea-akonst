using Banks.Classes;
using Banks.Classes.BankAccounts;

namespace Banks.Interfaces
{
    public interface IMediator
    {
        void ChargingBankOperation(Bank bank, BankAccount account, int monthsToCharge);
    }
}