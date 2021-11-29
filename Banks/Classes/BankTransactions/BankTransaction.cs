namespace Banks.Classes.BankTransactions
{
    internal class BankTransaction
    {
        public int Id { get; set; }
        public double Sum { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string SendersBank { get; set; }
        public string ReceiversBank { get; set; }
    }
}