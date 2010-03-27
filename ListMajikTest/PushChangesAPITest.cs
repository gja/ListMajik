using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ListMajik;
using ListMajikTest.Extensions;
using NUnit.Framework;

namespace ListMajikTest
{    
    [TestFixture]
    //This test should poke every PushChangeTo/MonitorChangesTo method, and ensure none of them are recursive
    public class PushChangesAPITest
    {
        private ObservableCollection<int> observable;
        private DummyNotify notify;
        private List<int> list;

        [Test]
        public void ShouldCallPushChangesToWithTwoTemplateParameters()
        {
            notify.PushChangesTo<int, int>(list);
        }

        [Test]
        public void ShouldCallPushChangesToWithOneTemplateParameter()
        {
            notify.PushChangesTo<int>(list);
        }

        [Test]
        public void ShouldCallPushChangesToWithObservableCollection()
        {
            observable.PushChangesTo(list);
        }

        [Test]
        public void ShouldCallMonitorChangesToWithTwoTemplateParameters()
        {
            list.MonitorChangesTo<int, int>(notify);
        }

        [Test]
        public void ShouldCallMonitorChangesWithOneTemplateParameter()
        {
            list.MonitorChangesTo<int>(notify);
        }

        [Test]
        public void ShouldCallMonitorChangesWithObservableCollection()
        {
            list.MonitorChangesTo(observable);
        }

        [SetUp]
        public void SetUp()
        {
            observable = new ObservableCollection<int>();
            notify = new DummyNotify();
            list = new List<int>();
        }

        [TearDown]
        public void TearDown()
        {
            observable.Add(1);
            notify.Change();
            list.ShouldContain(1);
        }
    }

    class DummyNotify : INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged = (sender,args) => { };

        public void Change()
        {
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, 1));
        }
    }
}
