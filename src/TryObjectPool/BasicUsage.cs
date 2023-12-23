using Microsoft.Extensions.ObjectPool;

namespace TryObjectPool;

internal class BasicUsage
{
    public static void Process()
    {
        ObjectPoolProvider objectPoolProvider = new DefaultObjectPoolProvider(); //ObjectPoolProviderを生成し、
        ObjectPool<HeavyObject1> objectPool = objectPoolProvider.Create<HeavyObject1>(); //ProviderからObjectPoolを生成する
        HeavyObject1 heavyObject = objectPool.Get(); //ObjectPoolからオブジェクトを取得する
        objectPool.Return(heavyObject); //オブジェクトをObjectPoolに返却する
    }

    //重いオブジェクト
    record HeavyObject1();
}
