using Banks.Tools;

namespace Banks.Classes.BankAccounts
{
    public class DebitAccount : BankAccount
    {
        public double DebitPercent { get; set; }
        public override void ChargePercentsAndCommissions(int monthsToCharge)
        {
            double monthlyPart = (DebitPercent / 12) / 100;
            double chargingPercent = monthsToCharge * monthlyPart;

            double chargingSum = Balance * chargingPercent;
            Refill(chargingSum);
        }

        public override double Withdraw(double sum)
        {
            if (Balance - sum < 0)
            {
                throw new BanksException("You can't take more money than there is in the account!");
            }

            if (Owner.IsIncorrectClient())
            {
                throw new BanksException("Please, fill your address and passport data!");
            }

            Balance -= sum;

            return Balance;
        }
    }
}