namespace Banks.Classes.BankAccounts
{
    public class CreditAccountCreator
    {
        public static CreditAccount MakeAccount(int id, double creditLimit, string bankName)
        {
            return new CreditAccount(id, creditLimit, bankName);
        }
    }
}