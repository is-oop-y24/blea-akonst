namespace Banks.Classes.BankAccounts
{
    public class DebitAccountCreator
    {
        public static DebitAccount MakeAccount(int id, string bankName)
        {
            return new DebitAccount(id, bankName);
        }
    }
}