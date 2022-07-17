using Castle.DynamicProxy;
using System;


namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; } //öncelik sıralaması loglama, authorization, validate...

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
