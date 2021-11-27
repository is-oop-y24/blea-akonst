using Banks.Classes.BankClients;

namespace Banks.Classes.BankAccounts
{
    public class DepositAccountFactory
    {
        public static DepositAccount MakeAccount(int depositExpiryDate, string bankName, BankClient owner)
        {
            return new DepositAccount(depositExpiryDate, bankName, owner);
        }
    }
}