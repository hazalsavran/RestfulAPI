using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IOC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//her isteğin yanıtına takip serviceCollection.AddSingleton();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>(); //Bu kod cache mekanizmasının çalışmasını sağlıyor, bağımlılık zincirine ekler

            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
