﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ListMajik
{
    public static class PushChanges
    {
        public static IPushChangesProperties<T, T> PushChangesTo<T>(this INotifyCollectionChanged observed, ICollection<T> output)
        {
            return new PushChangesProperties<T, T>(observed, output);
        }

        public static IPushChangesProperties<TSource, TDest> PushChangesTo<TSource, TDest>(this INotifyCollectionChanged observed, ICollection<TDest> output)
        {
            return new PushChangesProperties<TSource, TDest>(observed, output);
        }

        public static IPushChangesProperties<TSource, TDest> PushChangesTo<TSource, TDest>(this ObservableCollection<TSource> observed, ICollection<TDest> output)
        {
            return new PushChangesProperties<TSource, TDest>(observed, output);
        }
    }

    public interface IPushChangesProperties<TSource, TDest>
    {
        IPushChangesProperties<TSource, TDest> WithMapping(Func<TSource, TDest> mapping);
        IPushChangesProperties<TSource, TDest> AddOnlyIf(Predicate<TSource> predicate);
        IPushChangesProperties<TSource, TDest> RemoveOnlyIf(Predicate<TSource> predicate);
        IPushChangesProperties<TSource, TDest> If(Predicate<TSource> predicate);
    }
}
