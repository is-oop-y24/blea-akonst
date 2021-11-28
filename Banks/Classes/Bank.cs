using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Classes.BankAccounts;
using Banks.Classes.BankClients;
using Banks.Tools;

namespace Banks.Classes
{
    public class Bank
    {
        private int _currentDate;
        private List<BankClient> _clients = new List<BankClient>();
        private List<BankAccount> _accounts = new List<BankAccount>();

        public int CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;

                foreach (BankAccount acc in _accounts)
                {
                    int prevDate = acc.CurrentDate;
                    int monthsToCharge = ((prevDate % 30) + _currentDate - prevDate) / 30;
                    acc.ChargePercentsAndCommissions(monthsToCharge);
                    acc.CurrentDate = _currentDate;
                }
            }
        }

        public string BankName { get; set; }
        public double DebitPercent { get; set; }
        public double FirstDepositPercent { get; set; }
        public double SecondDepositPercent { get; set; }
        public double DepositPercentIncreasingBorderSum { get; set; }
        public double CreditCommission { get; set; }
        public double CreditLimit { get; set; }
        public double UntrustedClientTransactionLimit { get; set; }
        public int StandardDepositTerm { get; set; }

        public BankClient AddClient(BankClient client)
        {
            _clients.Add(client);

            return _clients.First(cl => cl.Equals(client));
        }

        public void AddAccount(BankAccount account)
        {
            int id = _accounts.Count;
            account.Id = id;
            _accounts.Add(account);
        }

        public double Refill(BankAccount account, double sum)
        {
            return account.Refill(sum);
        }

        public double Withdraw(BankAccount account, double sum)
        {
            return account.Withdraw(sum);
        }

        public BankAccount GetAccount(int id)
        {
            BankAccount account = _accounts.FirstOrDefault(acc => acc.Id.Equals(id));

            if (account == default(BankAccount))
            {
                throw new BanksException("This account doesn't exists!");
            }

            return account;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Bank)obj);
        }

        public override int GetHashCode()
        {
            return BankName != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(BankName) : 0;
        }

        private bool Equals(Bank other)
        {
            return string.Equals(BankName, other.BankName, StringComparison.OrdinalIgnoreCase);
        }
    }
}