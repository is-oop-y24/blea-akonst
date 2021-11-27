using Banks.Classes.BankClients;

namespace Banks.Classes.BankAccounts
{
    public class CreditAccountFactory
    {
        public static CreditAccount MakeAccount(double creditLimit, string bankName, BankClient owner)
        {
            return new CreditAccount(creditLimit, bankName, owner);
        }
    }
}