using System.Collections.Generic;
using System.Collections.ObjectModel;
using ListMajik;
using ListMajikTest.Extensions;
using NUnit.Framework;

namespace ListMajikTest
{
    [TestFixture]
    public class PushChangesExecutionTest
    {
        private int notifiedCount;

        [SetUp]
        public void SetUp()
        {
            notifiedCount = 0;
        }

        private void IncrementCount(int val)
        {
            notifiedCount++;
        }

        private bool NumberIsEven(int match)
        {
            return match % 2 == 0;
        }

        [Test]
        public void ShouldExecuteCallBacksInCorrectOrder()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<int> {1, 2, 3};

            source.PushChangesTo(dest)
                .Execute(item => dest.Count.ShouldBe(3)).BeforeAdding()
                .Execute(item => dest.Count.ShouldBe(4)).AfterAdding()
                .Execute(item => dest.Count.ShouldBe(4)).BeforeRemoving()
                .Execute(item => dest.Count.ShouldBe(3)).AfterRemoving();

            source.Add(4);
            source.Remove(4);
        }

        [Test]
        public void ShouldExecuteAfterAdding()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<int>();

            source.PushChangesTo(dest).If(NumberIsEven)
                .Execute(IncrementCount).AfterAdding();

            source.Add(1);
            notifiedCount.ShouldBe(0);

            source.Add(2);
            notifiedCount.ShouldBe(1);
        }

        [Test]
        public void ShouldExecuteAfterRemoving()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<int>();

            source.PushChangesTo(dest).If(NumberIsEven)
                .Execute(IncrementCount).AfterRemoving();

            source.Add(1);
            source.Add(2);
            notifiedCount.ShouldBe(0);

            source.Remove(1);
            notifiedCount.ShouldBe(0);
            
            source.Remove(2);
            notifiedCount.ShouldBe(1);
        }

        [Test]
        public void ShouldExecuteAfterWards()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<int>();

            source.PushChangesTo(dest).If(NumberIsEven)
                .Execute(IncrementCount).Afterwards();

            source.Add(1);
            source.Remove(1);
            notifiedCount.ShouldBe(0);

            source.Add(2);
            source.Remove(2);
            notifiedCount.ShouldBe(2);
        }

        [Test]
        public void ShouldExecuteBeforeAdding()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<int>();

            source.PushChangesTo(dest).If(NumberIsEven)
                .Execute(IncrementCount).BeforeAdding();

            source.Add(1);
            notifiedCount.ShouldBe(0);

            source.Add(2);
            notifiedCount.ShouldBe(1);
        }

        [Test]
        public void ShouldExecuteBeforeRemoving()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<int>();

            source.PushChangesTo(dest).If(NumberIsEven)
                .Execute(IncrementCount).BeforeRemoving();

            source.Add(1);
            source.Add(2);
            notifiedCount.ShouldBe(0);

            source.Remove(1);
            notifiedCount.ShouldBe(0);
            
            source.Remove(2);
            notifiedCount.ShouldBe(1);
        }

        [Test]
        public void ShouldExecuteBefore()
        {
            var source = new ObservableCollection<int>();
            var dest = new List<int>();

            source.PushChangesTo(dest).If(NumberIsEven)
                .Execute(IncrementCount).Before();

            source.Add(1);
            source.Remove(1);
            notifiedCount.ShouldBe(0);

            source.Add(2);
            source.Remove(2);
            notifiedCount.ShouldBe(2);
        }
    }
}