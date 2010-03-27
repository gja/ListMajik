using System;

namespace ListMajik
{
    class ExecutePushChanges<TSource, TDest> : IExecutePushChanges<TSource, TDest>
    {
        private readonly PushChangesProperties<TSource, TDest> properties;
        private readonly ExecuteOnSource<TSource> action;

        public ExecutePushChanges(PushChangesProperties<TSource, TDest> properties, ExecuteOnSource<TSource> action)
        {
            this.properties = properties;
            this.action = action;
        }

        public IPushChangesProperties<TSource, TDest> AfterAdding()
        {
            properties.executeAfterAdding += action;
            return properties;
        }

        public IPushChangesProperties<TSource, TDest> AfterRemoving()
        {
            properties.executeAfterRemoving += action;
            return properties;
        }

        public IPushChangesProperties<TSource, TDest> Afterwards()
        {
            return AfterAdding().Execute(action).AfterRemoving();
        }
    }
}