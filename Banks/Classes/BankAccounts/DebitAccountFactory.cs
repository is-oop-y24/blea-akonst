using Banks.Classes.BankClients;

namespace Banks.Classes.BankAccounts
{
    public class DebitAccountFactory
    {
        public static DebitAccount MakeAccount(Bank bank, BankClient owner)
        {
            var account = new DebitAccount
            {
                BankName = bank.BankName,
                Owner = owner,
                DebitPercent = bank.DebitPercent,
            };

            return account;
        }
    }
}