using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ListMajik
{
    public interface IPushChangesProperties<TSource, TDest>
    {
    }

    class PushChangesProperties<TSource, TDest> : IPushChangesProperties<TSource, TDest>
    {
        private readonly ICollection<TDest> output;

        public PushChangesProperties(INotifyCollectionChanged observed, ICollection<TDest> output)
        {
            this.output = output;

            observed.CollectionChanged += SomethingChanged;
        }

        private void SomethingChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RunActionOver(e.NewItems, output.Add);
            RunActionOver(e.OldItems, item => output.Remove(item));
        }

        private void RunActionOver(IList items, Action<TDest> action)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                action((TDest) item);
            }
        }
    }
}