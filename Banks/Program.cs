using System;
using Banks.Classes;
using Banks.Classes.BankClients;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var centralBank = new CentralBank();
            var bankBuilder = new BankBuilder();
            var bankClientBuilder = new BankClientBuilder();
            var bank = new Bank();

            string choose;

            Console.Title = "Banks App";
            Console.WriteLine("Hello! Please, choose the option:");
            while (true)
            {
                Console.Clear();
                PrintMenu();
                choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Please, enter the bank name: ");
                        bankBuilder.SetBankName(Console.ReadLine());

                        Console.WriteLine("Please, enter the debit percent: ");
                        bankBuilder.SetDebitPercent(Convert.ToDouble(Console.ReadLine()));

                        Console.WriteLine("Please, enter the credit commission: ");
                        bankBuilder.SetCreditCommission(Convert.ToDouble(Console.ReadLine()));

                        Console.WriteLine("Please, enter the credit limit (sum in roubles): ");
                        bankBuilder.SetCreditLimit(Convert.ToDouble(Console.ReadLine()));

                        Console.WriteLine("Please, enter the lower border deposit percent: ");
                        bankBuilder.SetFirstDepositPercent(Convert.ToDouble(Console.ReadLine()));

                        Console.WriteLine("Please, enter the upper border deposit percent: ");
                        bankBuilder.SetSecondDepositPercent(Convert.ToDouble(Console.ReadLine()));

                        Console.WriteLine(
                            "Please, enter the sum after which the percent on the deposit will increase: ");
                        bankBuilder.SetDepositPercentIncreasingBorderSum(Convert.ToDouble(Console.ReadLine()));

                        Console.WriteLine(
                            "Please, enter the transaction limit for client without pass or address data: ");
                        bankBuilder.SetUntrustedClientTransactionLimit(Convert.ToDouble(Console.ReadLine()));

                        bank = bankBuilder.GetBank();

                        Console.WriteLine("A bank has been created with the following data: ");
                        PrintBankData(bank);

                        centralBank.AddBank(bank);

                        Console.ReadLine();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Please, enter the bank name which you like to add a client: ");
                        bank = centralBank.GetBank(Console.ReadLine());

                        Console.WriteLine("Please, enter client's name:");
                        bankClientBuilder.SetName(Console.ReadLine());

                        Console.WriteLine("Please, enter client's surname:");
                        bankClientBuilder.SetSurname(Console.ReadLine());

                        Console.WriteLine("Please, enter client's address:");
                        bankClientBuilder.SetAddress(Console.ReadLine());

                        Console.WriteLine("Please, enter client's passport number:");
                        bankClientBuilder.SetPassportNumber(Console.ReadLine());

                        bank.AddClient(bankClientBuilder.GetClient());
                        Console.WriteLine("Client was successfully added!");
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Exit!");
                        Environment.Exit(0);
                        break;
                    default:
                        throw new Exception("Incorrect argument!");
                }
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Add bank");
            Console.WriteLine("2. Add client to bank");
            Console.WriteLine("3. Exit");
            Console.Write("Your choice: ");
        }

        private static void PrintBankData(Bank bank)
        {
            Console.WriteLine($"Bank name: {bank.BankName}");
            Console.WriteLine($"Debit percent: {bank.DebitPercent}");
            Console.WriteLine($"Credit commission: {bank.CreditCommission}");
            Console.WriteLine($"Credit limit: {bank.CreditLimit}");
            Console.WriteLine(
                $"Deposit percent when balance is < {bank.DepositPercentIncreasingBorderSum}: {bank.FirstDepositPercent}");
            Console.WriteLine(
                $"Deposit percent when balance is > {bank.DepositPercentIncreasingBorderSum}: {bank.SecondDepositPercent}");
            Console.WriteLine($"Money transfer limit for untrusted account: {bank.UntrustedClientTransactionLimit}");
        }
    }
}
