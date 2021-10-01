namespace Shops
{
    public class Person
    {
        private uint _balance;

        public Person(uint balance)
        {
            Balance = balance;
        }

        public uint Balance
        {
            get => _balance;
            set => _balance = value;
        }
    }
}