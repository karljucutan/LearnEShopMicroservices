using System.Diagnostics.CodeAnalysis;

namespace Basket.API.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string userName) : base("Basket", userName) { }

        public static void ThrowIfNull([NotNull] ShoppingCart? basket, string userName)
        {
            if (basket is null)
                throw new BasketNotFoundException(userName);
        }
    }
}
