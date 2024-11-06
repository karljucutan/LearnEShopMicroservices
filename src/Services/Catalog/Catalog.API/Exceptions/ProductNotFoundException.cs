using System.Diagnostics.CodeAnalysis;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base("Product", Id) { }

        public static void ThrowIfNull([NotNull] Product? product, Guid key)
        {
            if (product is null)
                throw new ProductNotFoundException(key);
        }
    }
}
