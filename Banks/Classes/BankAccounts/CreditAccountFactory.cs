using Banks.Classes.BankClients;

namespace Banks.Classes.BankAccounts
{
    public class CreditAccountFactory
    {
        public static CreditAccount MakeAccount(Bank bank, BankClient owner)
        {
            var account = new CreditAccount
            {
                BankName = bank.BankName,
                CreditComission = bank.CreditCommission,
                CreditLimit = bank.CreditLimit,
                Owner = owner,
            };

            return account;
        }
    }
}