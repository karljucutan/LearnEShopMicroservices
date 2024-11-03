using System.Diagnostics.CodeAnalysis;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message = "Product not found!") : base(message) { }

        public static void ThrowIfNull([NotNull] object? product)
        {
            if (product is null)
                throw new ProductNotFoundException();
        }
    }
}
