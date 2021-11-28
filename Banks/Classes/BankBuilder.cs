namespace Banks.Classes
{
    public class BankBuilder
    {
        private Bank _bank = new Bank();

        public BankBuilder SetBankName(string name)
        {
            _bank.BankName = name;
            return this;
        }

        public BankBuilder SetDebitPercent(double percent)
        {
            _bank.DebitPercent = percent;
            return this;
        }

        public BankBuilder SetFirstDepositPercent(double percent)
        {
            _bank.FirstDepositPercent = percent;
            return this;
        }

        public BankBuilder SetSecondDepositPercent(double percent)
        {
            _bank.SecondDepositPercent = percent;
            return this;
        }

        public BankBuilder SetDepositPercentIncreasingBorderSum(double sum)
        {
            _bank.DepositPercentIncreasingBorderSum = sum;
            return this;
        }

        public BankBuilder SetCreditCommission(double sum)
        {
            _bank.CreditCommission = sum;
            return this;
        }

        public BankBuilder SetCreditLimit(double sum)
        {
            _bank.CreditLimit = sum;
            return this;
        }

        public BankBuilder SetUntrustedClientTransactionLimit(double sum)
        {
            _bank.UntrustedClientTransactionLimit = sum;
            return this;
        }

        public BankBuilder SetStandardDepositTerm(int term)
        {
            _bank.StandardDepositTerm = term;
            return this;
        }

        public Bank GetBank()
        {
            Bank res = _bank;

            Reset();

            return res;
        }

        private void Reset()
        {
            _bank = new Bank();
        }
    }
}