using Banks.Classes.BankAccounts;
using Banks.Interfaces;

namespace Banks.Classes
{
    public class BankOperationMediator : IMediator
    {
        public void ChargingBankOperation(Bank bank, BankAccount account, int monthsToCharge)
        {
            if (account.GetType() == typeof(DepositAccount))
            {
                bank.ChargeDepositPercent((DepositAccount)account, monthsToCharge);
            }
            else if (account.GetType() == typeof(CreditAccount))
            {
                bank.ChargeCreditCommissions((CreditAccount)account, monthsToCharge);
            }
            else if (account.GetType() == typeof(DebitAccount))
            {
                bank.ChargeDebitPercent((DebitAccount)account, monthsToCharge);
            }
        }
    }
}