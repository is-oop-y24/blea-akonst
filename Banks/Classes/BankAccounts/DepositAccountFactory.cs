using Banks.Classes.BankClients;

namespace Banks.Classes.BankAccounts
{
    public class DepositAccountFactory
    {
        public static DepositAccount MakeAccount(Bank bank, BankClient owner, int depositExpiryDate)
        {
            var account = new DepositAccount
            {
                BankName = bank.BankName,
                FirstDepositPercent = bank.FirstDepositPercent,
                SecondDepositPercent = bank.SecondDepositPercent,
                DepositPercentIncreasingBorderSum = bank.DepositPercentIncreasingBorderSum,
                DepositExpiryDate = depositExpiryDate,
                Owner = owner,
            };

            return account;
        }
    }
}