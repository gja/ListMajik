using System.Collections.Generic;
using System.Collections.ObjectModel;
using ListMajik;
using ListMajikTest.Extensions;
using NUnit.Framework;

namespace ListMajikTest
{
    [TestFixture]
    public class PushChangesToTest
    {
        [Test]
        public void ShouldPushChangesTo()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<int>();

            source.PushChangesTo(dest);

            source.Add(1);
            dest.ShouldContain(1);

            source.Remove(1);
            dest.ShouldNotContain(1);
        }

        [Test]
        public void ShouldMaintainInitialDifferences()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<int>();

            source.Add(1);

            source.PushChangesTo(dest);

            dest.ShouldNotContain(1);

            source.Remove(1);
            dest.ShouldNotContain(1);
        }
    }
}
