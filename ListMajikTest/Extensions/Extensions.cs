using System.Collections;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace ListMajikTest.Extensions
{
    static class Extensions
    {
        public static void ShouldContain<T>(this ICollection collection, T item)
        {
            Assert.That(collection, Has.Member(item));
        }

        public static void ShouldNotContain<T>(this ICollection collection, T item)
        {
            Assert.That(collection, Has.No.Member(item));
        }

        public static void ShouldBe(this object me, object other)
        {
            Assert.AreEqual(other, me);
        }
    }
}
