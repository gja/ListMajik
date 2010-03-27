using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ListMajik
{
    class PushChangesProperties<TSource, TDest> : IPushChangesProperties<TSource, TDest>
    {
        private readonly ICollection<TDest> output;

        private Func<TSource, TDest> mapping = source => (TDest) (object) source;
        private Predicate<TSource> addCondition = source => true;
        private Predicate<TSource> removeCondition = source => true;

        public PushChangesProperties(INotifyCollectionChanged observed, ICollection<TDest> output)
        {
            this.output = output;

            observed.CollectionChanged += SomethingChanged;
        }

        private void SomethingChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RunActionOver(e.NewItems, output.Add, addCondition);
            RunActionOver(e.OldItems, item => output.Remove(item), removeCondition);
        }

        private void RunActionOver(IList items, Action<TDest> action, Predicate<TSource> predicate)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                if (predicate((TSource) item))
                    action(mapping((TSource) item));
            }
        }

        public IPushChangesProperties<TSource, TDest> WithMapping(Func<TSource, TDest> mapping)
        {
            this.mapping = mapping;
            return this;
        }

        public IPushChangesProperties<TSource, TDest> AddOnlyIf(Predicate<TSource> predicate)
        {
            addCondition = predicate;
            return this;
        }

        public IPushChangesProperties<TSource, TDest> RemoveOnlyIf(Predicate<TSource> predicate)
        {
            removeCondition = predicate;
            return this;
        }

        public IPushChangesProperties<TSource, TDest> If(Predicate<TSource> predicate)
        {
            return AddOnlyIf(predicate).RemoveOnlyIf(predicate);
        }
    }
}