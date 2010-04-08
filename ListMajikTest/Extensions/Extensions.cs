using System.Collections;
using System.Collections.Generic;
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

        public static void ShouldBeLike<T>(this IEnumerator<T> enumerator, IEnumerable<T> expected)
        {
            foreach (var item in expected)
            {
                enumerator.MoveNext().ShouldBe(true);
                enumerator.Current.ShouldBe(item);
            }

            enumerator.MoveNext().ShouldBe(false);
        }
    }
}
