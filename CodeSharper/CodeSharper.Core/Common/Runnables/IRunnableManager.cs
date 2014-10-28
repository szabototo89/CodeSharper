using System;

namespace CodeSharper.Core.Common.Runnables
{
    public interface IRunnableManager
    {
        RunnableDescriptor Register(IRunnable runnable);
        RunnableDescriptor Register(Type type);
        RunnableDescriptor GetRunnableDescriptor(Type type);
    }
}