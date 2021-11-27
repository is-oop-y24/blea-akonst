namespace Banks.Classes.BankAccounts
{
    public class DepositAccountCreator
    {
        public static DepositAccount MakeAccount(int id, int depositExpiryDate, string bankName)
        {
            return new DepositAccount(id, depositExpiryDate, bankName);
        }
    }
}