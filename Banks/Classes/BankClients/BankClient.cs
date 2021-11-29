namespace Banks.Classes.BankClients
{
    public class BankClient
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string PassportNumber { get; set; }
        public string CurrentBankInfo { get; private set; }

        public void UpdateInfo(string info)
        {
            CurrentBankInfo = info;
        }

        public bool IsIncorrectClient()
        {
            return string.IsNullOrEmpty(Address) ||
                   string.IsNullOrEmpty(PassportNumber);
        }
    }
}