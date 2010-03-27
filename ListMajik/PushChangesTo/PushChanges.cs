using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ListMajik
{
    public static class PushChanges
    {
        public static IPushChangesProperties<TSource, TDest> PushChangesTo<TSource, TDest>(this INotifyCollectionChanged observed, ICollection<TDest> output)
        {
            return new PushChangesProperties<TSource, TDest>(observed, output);
        }

        public static IPushChangesProperties<T, T> PushChangesTo<T>(this INotifyCollectionChanged observed, ICollection<T> output)
        {
            return PushChangesTo<T, T>(observed, output);
        }

        public static IPushChangesProperties<TSource, TDest> PushChangesTo<TSource, TDest>(this ObservableCollection<TSource> observed, ICollection<TDest> output)
        {
            return PushChangesTo<TSource, TDest>((INotifyCollectionChanged) observed, output);
        }

        public static IPushChangesProperties<TSource, TDest> MonitorChangesTo<TSource, TDest>(this ICollection<TDest> output, INotifyCollectionChanged observed)
        {
            return PushChangesTo<TSource, TDest>(observed, output);
        }

        public static IPushChangesProperties<T, T> MonitorChangesTo<T>(this ICollection<T> output, INotifyCollectionChanged observed)
        {
            return PushChangesTo(observed, output);
        }

        public static IPushChangesProperties<TSource, TDest> MonitorChangesTo<TSource, TDest>(this ICollection<TDest> output, ObservableCollection<TSource> observed)
        {
            return PushChangesTo(observed, output);
        }
    }

    public delegate void ExecuteOnSource<TSource>(TSource item);

    public interface IPushChangesProperties<TSource, TDest>
    {
        IPushChangesProperties<TSource, TDest> WithMapping(Func<TSource, TDest> mapping);
        IPushChangesProperties<TSource, TDest> AddOnlyIf(Predicate<TSource> predicate);
        IPushChangesProperties<TSource, TDest> RemoveOnlyIf(Predicate<TSource> predicate);
        IPushChangesProperties<TSource, TDest> If(Predicate<TSource> predicate);
        
        IExecutePushChanges<TSource, TDest> Execute(ExecuteOnSource<TSource> action);
    }

    public interface IExecutePushChanges<TSource, TDest>
    {
        IPushChangesProperties<TSource, TDest> AfterAdding();
        IPushChangesProperties<TSource, TDest> AfterRemoving();
        IPushChangesProperties<TSource, TDest> Afterwards();
        IPushChangesProperties<TSource, TDest> BeforeAdding();
        IPushChangesProperties<TSource, TDest> BeforeRemoving();
        IPushChangesProperties<TSource, TDest> Before();
    }
}
