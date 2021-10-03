using System;

namespace Shops.Tools
{
    public class ShopsException : Exception
    {
        public ShopsException()
            : base("Some error is occurred with the shop!")
        { }

        public ShopsException(string message)
            : base(message)
        { }

        public ShopsException(Exception innerException)
        {
            throw innerException;
        }
    }
}