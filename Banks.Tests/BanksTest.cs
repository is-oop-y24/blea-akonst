using NUnit.Framework;
using Banks.Classes;
using Banks.Classes.BankAccounts;
using Banks.Classes.BankClients;
using Banks.Tools;

namespace Banks.Tests
{
    [TestFixture]
    public class BanksTest
    {
        private static CentralBank _centralBank = new CentralBank();

        private Bank _bankOne;
        private Bank _bankTwo;
        
        private DebitAccount _accountBankOneDebit;
        private CreditAccount _accountBankTwo;

        private const double DepositPercentFirst = 5;
        private const double DepositPercentSecond = 7;

        [SetUp]
        public void Setup()
        {
            Reset();

            var bankBuilder = new BankBuilder();

            _bankOne = bankBuilder.SetBankName("Tinkoff Bank")
                .SetCreditCommission(2000)
                .SetDebitPercent(3)
                .SetCreditLimit(200000)
                .SetFirstDepositPercent(DepositPercentFirst)
                .SetSecondDepositPercent(DepositPercentSecond)
                .SetDepositPercentIncreasingBorderSum(50000)
                .SetUntrustedClientTransactionLimit(15000)
                .GetBank();
            _centralBank.AddBank(_bankOne);
                
            _bankTwo = bankBuilder.SetBankName("Alfa-Bank")
                .SetCreditCommission(3000)
                .SetDebitPercent(4)
                .SetCreditLimit(100000)
                .SetFirstDepositPercent(DepositPercentFirst)
                .SetSecondDepositPercent(DepositPercentSecond)
                .SetDepositPercentIncreasingBorderSum(100000)
                .SetUntrustedClientTransactionLimit(10000)
                .GetBank();
            _centralBank.AddBank(_bankTwo);
            
            var clientBuilder = new BankClientBuilder();

            BankClient client;

            client = clientBuilder.SetName("Gi-hun")
                .SetSurname("Seong")
                .SetAddress("Republic of Korea, Seoul city, Seangmungdong district")
                .SetPassportNumber("KR-SQDGM")
                .GetClient();

            client = _bankOne.AddClient(client);

            _accountBankOneDebit = DebitAccountFactory.MakeAccount(_bankOne.BankName, client);
            _bankOne.AddAccount(_accountBankOneDebit);
            
            client = clientBuilder.SetName("Il-nam")
                .SetSurname("Oh")
                .SetAddress("Republic of Korea, Seoul city, Seoul Sky building")
                .SetPassportNumber("KR-FRST")
                .GetClient();

            client = _bankTwo.AddClient(client);

            _accountBankTwo = CreditAccountFactory.MakeAccount(_bankTwo.CreditLimit, _bankTwo.BankName, client);
        }

        [Test]
        public void CreateClientAndRefillHimBalance_BalanceIsCorrect()
        {
            Assert.AreEqual(0, _accountBankOneDebit.Balance);
            double refillSum = _accountBankOneDebit.Refill(456);
            Assert.AreEqual(refillSum, _accountBankOneDebit.Balance);
        }
        
        [Test]
        public void IncreaseTimeForOneMonth_ExpectedBalanceWithPercents()
        {
            double refillSum = _accountBankOneDebit.Refill(456);
            _centralBank.IncreaseTime(31);
            Assert.IsTrue(_accountBankOneDebit.Balance > refillSum);
        }

        [Test]
        public void MoneyTransferFromFirstBankToSecond_SecondBankClientReceivedMoney()
        {
            _accountBankOneDebit.Refill(1000);
            _centralBank.MoneyTransfer(_accountBankOneDebit, _accountBankTwo, 1000);

            Assert.AreEqual(0, _accountBankOneDebit.Balance);
            Assert.AreEqual(1000, _accountBankTwo.Balance);
        }

        [Test]
        public void ClientInfoIsIncompleteAndHeTriesWithdrawMoney_ThrowsException()
        {
            _accountBankOneDebit.Owner.PassportNumber = "";
            _accountBankOneDebit.Refill(1000);
            Assert.Throws<BanksException>(() => _accountBankOneDebit.Withdraw(1000));
        }

        [Test]
        public void ClientHasNotEnoughMoneyForWithdraw_ThrowsException()
        {
            _accountBankOneDebit.Refill(999);
            Assert.Throws<BanksException>(() => _accountBankOneDebit.Withdraw(1000));
        }

        private void Reset()
        {
            _centralBank = new CentralBank();
        }
    }
}