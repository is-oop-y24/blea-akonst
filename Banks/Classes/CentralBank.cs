using System.Collections.Generic;
using System.Linq;
using Banks.Classes.BankAccounts;
using Banks.Classes.BankClients;
using Banks.Classes.BankTransactions;
using Banks.Tools;

namespace Banks.Classes
{
    public class CentralBank
    {
        private List<Bank> _banks = new List<Bank>();
        private List<BankTransaction> _transactions = new List<BankTransaction>();
        private List<BankClient> _subscribers = new List<BankClient>();

        private int _currentTransactionId = 0;

        public int CurrentDate { get; private set; } = 0;

        public Bank AddBank(Bank bank)
        {
            if (_banks.Contains(bank))
            {
                throw new BanksException("This bank is exists!");
            }

            _banks.Add(bank);

            return _banks.First(b => b.Equals(bank));
        }

        public Bank GetBank(string bankName)
        {
            Bank bank = _banks.FirstOrDefault(b => b.BankName.Equals(bankName));

            if (bank == default(Bank))
            {
                throw new BanksException("This bank doesn't exists!");
            }

            return bank;
        }

        public void MoneyTransfer(BankAccount sendersAccount, BankAccount receiversAccount, double sum)
        {
            Bank sendersBank = _banks.FirstOrDefault(bank => bank.BankName.Equals(sendersAccount.BankName));

            if (sendersAccount.Owner.IsIncorrectClient() && sum > sendersBank.UntrustedClientTransactionLimit)
            {
                throw new BanksException("Please, enter your email and passport data for transaction for this sum!");
            }

            sendersAccount.Withdraw(sum);
            receiversAccount.Refill(sum);

            var transactionBuilder = new BankTransactionBuilder();

            BankTransaction transaction = transactionBuilder.SetSum(sum)
                .SetSendersBank(sendersAccount.BankName)
                .SetSenderId(sendersAccount.Id)
                .SetReceiversBank(receiversAccount.BankName)
                .SetReceiverId(receiversAccount.Id)
                .SetTransactionId(_currentTransactionId++)
                .GetTransaction();

            _transactions.Add(transaction);
        }

        // percent and commission charging causes in time increasing
        public void IncreaseTime(int days)
        {
            CurrentDate += days;
            NotifyBanksAboutCurrentDate();
        }

        public void CancelTransaction(int transactionId)
        {
            BankTransaction transaction = _transactions.FirstOrDefault(tr => tr.Id.Equals(transactionId));

            Bank sendersBank = _banks.FirstOrDefault(b => b.BankName.Equals(transaction.SendersBank));
            Bank receiversBank = _banks.FirstOrDefault(b => b.BankName.Equals(transaction.ReceiversBank));

            BankAccount sendersAccount = sendersBank.GetAccount(transaction.SenderId);
            BankAccount receiversAccount = receiversBank.GetAccount(transaction.ReceiverId);

            sendersAccount.Refill(transaction.Sum);
            receiversAccount.ImmediatelyWithdraw(transaction.Sum);

            _transactions.Remove(transaction);
        }

        private void NotifyBanksAboutCurrentDate()
        {
            foreach (Bank bank in _banks)
            {
                bank.CurrentDate = CurrentDate;
            }
        }

        private void BankUpdatesSubscribe(BankClient client)
        {
            _subscribers.Add(client);
        }

        private void NotifyClientsAboutAction(string info)
        {
            foreach (BankClient client in _subscribers)
            {
                Notify(client, info);
            }
        }

        private void Notify(BankClient client, string info)
        {
            client.UpdateInfo(info);
        }
    }
}