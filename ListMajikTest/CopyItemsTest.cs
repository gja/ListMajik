using System.Collections.Generic;
using ListMajik;
using NUnit.Framework;
using ListMajikTest.Extensions;

namespace ListMajikTest
{
    [TestFixture]
    public class CopyItemsTest
    {
        [Test]
        public void ShouldCopyItems()
        {
            var list1 = new List<int> {2, 3, 4};
            var list2 = new List<int> {1, 3, 5};

            list1.CopyItemsFrom(list2);

            list1.ShouldBe(new List<int> {3, 1, 5});
        }
    }        
}
