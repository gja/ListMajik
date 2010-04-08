using System.Collections.Generic;
using NUnit.Framework;
using ListMajik;
using ListMajikTest.Extensions;

namespace ListMajikTest
{
    [TestFixture]
    public class WrappingEnumeratorTest
    {
        [Test]
        public void ShouldMapNumbersToNegativeNumbers()
        {
            var list = new List<int> {1, 2, 3};

            list.GetEnumerator(i => -i).ShouldBeLike(new List<int>{-1, -2, -3});
        }
    }
}
