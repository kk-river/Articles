using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;

namespace TryObjectPool;

internal class NoteOfIDisposable
{
    public static void CannotDispose()
    {
        ObjectPoolProvider objectPoolProvider = new DefaultObjectPoolProvider();
        ObjectPool<CancellationTokenSource> objectPool = objectPoolProvider.Create<CancellationTokenSource>();

        //このusingができない
        //using ObjectPool<CancellationTokenSource> objectPoolWithUsing = objectPoolProvider.Create<CancellationTokenSource>();

        //当然Disposeもできない
        //objectPool.Dispose();
    }

    public static void CheckTypeDispose()
    {
        ObjectPoolProvider objectPoolProvider = new DefaultObjectPoolProvider();
        ObjectPool<CancellationTokenSource> objectPool = objectPoolProvider.Create<CancellationTokenSource>();

        if (objectPool is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    public static void WithDI()
    {
        //ServiceCollectionを使って、ObjectPoolProviderを登録する
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>(); //ObjectPoolProviderを登録する
        serviceCollection.AddSingleton(serviceProvider => //ObjectPoolを登録する
        {
            ObjectPoolProvider provider = serviceProvider.GetRequiredService<ObjectPoolProvider>();
            DefaultPooledObjectPolicy<CancellationTokenSource> policy = new();
            return provider.Create(policy);
        });

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        //ServiceProviderが、ObjectPoolProviderを用いてObjectPoolを生成してくれる
        ObjectPool<CancellationTokenSource> objectPool = serviceProvider.GetRequiredService<ObjectPool<CancellationTokenSource>>();

        //ServiceProviderをDisposeすると、管理下にあるIDisposableなオブジェクトもDisposeされる
        //つまり、ObjectPool<CancellationTokenSource>もDisposeされる
        serviceProvider.Dispose();
    }
}
