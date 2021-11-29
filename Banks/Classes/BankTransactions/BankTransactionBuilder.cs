namespace Banks.Classes.BankTransactions
{
    internal class BankTransactionBuilder
    {
        private BankTransaction _transaction = new BankTransaction();

        public BankTransactionBuilder SetSum(double sum)
        {
            _transaction.Sum = sum;
            return this;
        }

        public BankTransactionBuilder SetTransactionId(int id)
        {
            _transaction.Id = id;
            return this;
        }

        public BankTransactionBuilder SetSenderId(int id)
        {
            _transaction.SenderId = id;
            return this;
        }

        public BankTransactionBuilder SetReceiverId(int id)
        {
            _transaction.ReceiverId = id;
            return this;
        }

        public BankTransactionBuilder SetSendersBank(string bank)
        {
            _transaction.SendersBank = bank;
            return this;
        }

        public BankTransactionBuilder SetReceiversBank(string bank)
        {
            _transaction.ReceiversBank = bank;
            return this;
        }

        public BankTransaction GetTransaction()
        {
            BankTransaction res = _transaction;

            Reset();

            return res;
        }

        private void Reset()
        {
            _transaction = new BankTransaction();
        }
    }
}