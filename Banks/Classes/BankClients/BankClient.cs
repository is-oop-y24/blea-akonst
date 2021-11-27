namespace Banks.Classes.BankClients
{
    public class BankClient
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string PassportNumber { get; set; }

        public bool IsIncorrectClient()
        {
            return string.IsNullOrEmpty(Address) ||
                   string.IsNullOrEmpty(PassportNumber);
        }
    }
}