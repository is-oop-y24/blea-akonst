using Banks.Classes.BankClients;

namespace Banks.Classes.BankAccounts
{
    public class DebitAccountFactory
    {
        public static DebitAccount MakeAccount(string bankName, BankClient owner)
        {
            return new DebitAccount(bankName, owner);
        }
    }
}