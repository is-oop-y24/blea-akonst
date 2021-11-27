namespace Banks.Classes.BankClients
{
    public class BankClientBuilder
    {
        private BankClient _client = new BankClient();

        public BankClientBuilder SetName(string name)
        {
            _client.Name = name;
            return this;
        }

        public BankClientBuilder SetSurname(string surname)
        {
            _client.Surname = surname;
            return this;
        }

        public BankClientBuilder SetAddress(string address)
        {
            _client.Address = address;
            return this;
        }

        public BankClientBuilder SetPassportNumber(string number)
        {
            _client.PassportNumber = number;
            return this;
        }

        public BankClient GetClient()
        {
            BankClient res = _client;

            Reset();

            return res;
        }

        private void Reset()
        {
            _client = new BankClient();
        }
    }
}