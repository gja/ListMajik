using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ListMajik
{ 
    class PushChangesProperties<TSource, TDest> : IPushChangesProperties<TSource, TDest>
    {
        private readonly ICollection<TDest> output;
        
        internal event ExecuteOnSource<TSource> executeAfterAdding = source => { };
        internal event ExecuteOnSource<TSource> executeAfterRemoving = source => { };

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
            RunActionOver(e.NewItems, output.Add, addCondition, executeAfterAdding);
            RunActionOver(e.OldItems, item => output.Remove(item), removeCondition, executeAfterRemoving);
        }

        private void RunActionOver(IList items, Action<TDest> action, Predicate<TSource> predicate, ExecuteOnSource<TSource> postCallback)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                ExecuteOnItem(action, (TSource) item, predicate, postCallback);
            }
        }

        private void ExecuteOnItem(Action<TDest> action, TSource item, Predicate<TSource> predicate, ExecuteOnSource<TSource> postCallback)
        {
            if (!predicate(item))
                return;
            
            action(mapping(item));
            postCallback(item);
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

        public IExecutePushChanges<TSource, TDest> Execute(ExecuteOnSource<TSource> action)
        {
            return new ExecutePushChanges<TSource, TDest>(this, action);
        }        
    }
}