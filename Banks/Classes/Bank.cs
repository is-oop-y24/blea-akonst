using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Classes.BankAccounts;
using Banks.Classes.BankAccounts.Enums;
using Banks.Classes.BankClients;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Classes
{
    public class Bank
    {
        private int _currentDate;
        private List<BankClient> _clients = new List<BankClient>();

        private List<DebitAccount> _debitAccounts = new List<DebitAccount>();
        private List<CreditAccount> _creditAccounts = new List<CreditAccount>();
        private List<DepositAccount> _depositAccounts = new List<DepositAccount>();

        public int CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;

                foreach (DebitAccount acc in _debitAccounts)
                {
                    int prevDate = acc.CurrentDate;
                    int monthsToCharge = ((prevDate % 30) + _currentDate - prevDate) / 30;
                    ChargePercentToAccounts(AccountType.Debit, monthsToCharge);
                    acc.CurrentDate = _currentDate;
                }

                foreach (CreditAccount acc in _creditAccounts)
                {
                    int prevDate = acc.CurrentDate;
                    int monthsToCharge = ((prevDate % 30) + _currentDate - prevDate) / 30;
                    ChargeCreditCommissions(monthsToCharge);
                    acc.CurrentDate = _currentDate;
                }

                foreach (DepositAccount acc in _depositAccounts)
                {
                    int prevDate = acc.CurrentDate;
                    int monthsToCharge = ((prevDate % 30) + _currentDate - prevDate) / 30;
                    ChargePercentToAccounts(AccountType.Debit, monthsToCharge);
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

        public BankClient AddClient(BankClient client)
        {
            _clients.Add(client);

            return _clients.First(cl => cl.Equals(client));
        }

        public DebitAccount AddDebitAccount(BankClient client)
        {
            int id = _debitAccounts.Count;
            DebitAccount debit = DebitAccountCreator.MakeAccount(id, BankName);
            debit.Owner = client;
            _debitAccounts.Add(debit);

            return _debitAccounts.First(d => d.Equals(debit));
        }

        public CreditAccount AddCreditAccount(BankClient client)
        {
            int id = _creditAccounts.Count;
            CreditAccount credit = CreditAccountCreator.MakeAccount(id, CreditLimit, BankName);
            credit.Owner = client;
            _creditAccounts.Add(credit);

            return _creditAccounts.First(c => c.Equals(credit));
        }

        public DepositAccount AddDepositAccount(BankClient client, int term)
        {
            int id = _depositAccounts.Count;
            DepositAccount deposit = DepositAccountCreator.MakeAccount(id, term, BankName);
            deposit.Owner = client;
            _depositAccounts.Add(deposit);

            return _depositAccounts.First(d => d.Equals(deposit));
        }

        public double Refill(IBankAccount account, double sum)
        {
            return account.Refill(sum);
        }

        public double Withdraw(IBankAccount account, double sum)
        {
            return account.Withdraw(sum);
        }

        public void ChargePercentToAccounts(AccountType type, int monthsCount)
        {
            switch (type)
            {
                case AccountType.Debit:
                    foreach (DebitAccount account in _debitAccounts)
                    {
                        ChargeDebitPercent(account, monthsCount);
                    }

                    break;
                case AccountType.Deposit:
                    foreach (DepositAccount account in _depositAccounts)
                    {
                        if (account.DepositExpiryDate < _currentDate)
                        {
                            continue;
                        }

                        ChargeDepositPercent(account, monthsCount);
                    }

                    break;
                default:
                    throw new BanksException("Incorrect account type!");
            }
        }

        public void ChargeCreditCommissions(int months)
        {
            foreach (CreditAccount account in _creditAccounts)
            {
                account.ImmediatelyWithdraw(CreditCommission * months);
            }
        }

        public IBankAccount GetAccount(int id)
        {
            IBankAccount account = _debitAccounts.FirstOrDefault(acc => acc.Id.Equals(id));

            if (account == default(DebitAccount))
            {
                account = _creditAccounts.FirstOrDefault(acc => acc.Id.Equals(id));
            }

            if (account == default(CreditAccount))
            {
                account = _depositAccounts.FirstOrDefault(acc => acc.Id.Equals(id));
            }

            if (account == default(DepositAccount))
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

        private void ChargeDebitPercent(DebitAccount account, int months)
        {
            double monthlyPart = (DebitPercent / 12) / 100;
            double chargingPercent = months * monthlyPart;

            double chargingSum = account.Balance * chargingPercent;
            account.Refill(chargingSum);
        }

        private void ChargeDepositPercent(DepositAccount account, int months)
        {
            double percent;
            if (account.Balance < DepositPercentIncreasingBorderSum)
            {
                percent = FirstDepositPercent;
            }
            else
            {
                percent = SecondDepositPercent;
            }

            double monthlyPart = (percent / 12) / 100;
            double chargingPercent = months * monthlyPart;

            double chargingSum = account.Balance * chargingPercent;
            account.Refill(chargingSum);
        }
    }
}