using Microsoft.Extensions.ObjectPool;

namespace TryObjectPool;

internal class WithCts
{
    public static void Process()
    {
        ObjectPoolProvider objectPoolProvider = new DefaultObjectPoolProvider();
        ObjectPool<CancellationTokenSource> objectPool = objectPoolProvider.Create(new CtsPooledObjectPolicy());

        CancellationTokenSource cts = objectPool.Get();
        cts.CancelAfter(TimeSpan.FromSeconds(3)); //3秒後にタイムアウトするイメージ

        objectPool.Return(cts);
    }

    public class CtsPooledObjectPolicy : PooledObjectPolicy<CancellationTokenSource>
    {
        public override CancellationTokenSource Create() => new();

        public override bool Return(CancellationTokenSource obj)
        {
            //タイムアウトしていない場合は、リセットしてからObjectPoolに返却する
            //Returnが（＝TryResetが）falseを返すと、ObjectPoolに返却されない（＝再利用できないものは返却されない）
            return obj.TryReset();
        }
    }
}
