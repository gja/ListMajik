using System;
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

        [Test]
        public void ShouldPushChangesToAListGivenAMapping()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<float>();

            source.PushChangesTo(dest).WithMapping(i => i);

            source.Add(1);
            dest.ShouldContain(1.00f);

            source.Remove(1);
            dest.ShouldNotContain(1.00f);
        }

        [Test]
        public void ShouldOnlyAddEvenNumbers()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<int>();

            source.PushChangesTo(dest).AddOnlyIf(NumberIsEven);

            source.Add(1);
            dest.ShouldNotContain(1);

            source.Add(2);
            dest.ShouldContain(2);
        }

        [Test]
        public void ShouldOnlyRemoveEvenNumbers()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<int>();

            source.PushChangesTo(dest).RemoveOnlyIf(NumberIsEven);

            source.Add(1);
            source.Add(2);
            
            source.Remove(1);
            dest.ShouldContain(1);

            source.Remove(2);
            dest.ShouldNotContain(2);
        }

        [Test]
        public void ShouldActAsAnEvenFilter()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<int>();

            source.PushChangesTo(dest).If(NumberIsEven);

            source.Add(1);
            dest.ShouldNotContain(1);

            source.Add(2);
            dest.ShouldContain(2);

            source.Remove(2);
            dest.ShouldNotContain(2);

            dest.Add(1);
            source.Remove(1);
            dest.ShouldContain(1);
        }

        private bool NumberIsEven(int match)
        {
            return match%2 == 0;
        }
    }
}
