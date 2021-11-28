using Banks.Tools;

namespace Banks.Classes.BankAccounts
{
    public class DepositAccount : BankAccount
    {
        public int DepositExpiryDate { get; set; }
        public double FirstDepositPercent { get; set; }
        public double SecondDepositPercent { get; set; }
        public double DepositPercentIncreasingBorderSum { get; set; }

        public override void ChargePercentsAndCommissions(int monthsToCharge)
        {
            double percent;

            if (DepositExpiryDate < CurrentDate)
            {
                return;
            }

            percent = Balance < DepositPercentIncreasingBorderSum ? FirstDepositPercent : SecondDepositPercent;

            double monthlyPart = (percent / 12) / 100;
            double chargingPercent = monthsToCharge * monthlyPart;

            double chargingSum = Balance * chargingPercent;
            Refill(chargingSum);
        }

        public override double Withdraw(double sum)
        {
            if (CurrentDate < DepositExpiryDate)
            {
                throw new BanksException("The term of the deposit has not ended!");
            }

            if (Balance - sum < 0)
            {
                throw new BanksException("Not enough money in the account!");
            }

            if (!Owner.IsIncorrectClient())
            {
                throw new BanksException("Please, fill your address and passport data!");
            }

            Balance -= sum;

            return Balance;
        }
    }
}