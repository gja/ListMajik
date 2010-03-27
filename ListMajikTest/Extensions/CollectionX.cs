using System.Collections;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace ListMajikTest.Extensions
{
    static class CollectionX
    {
        public static void ShouldContain<T>(this ICollection collection, T item)
        {
            Assert.That(collection, Has.Member(item));
        }

        public static void ShouldNotContain<T>(this ICollection collection, T item)
        {
            Assert.That(collection, Has.No.Member(item));
        }
    }
}
