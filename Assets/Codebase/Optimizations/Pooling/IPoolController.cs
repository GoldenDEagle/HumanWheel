using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Mechanics.Effects;
using UnityEngine.Pool;

namespace Assets.Codebase.Optimizations.Pooling
{
    public interface IPoolController : IService
    {
        public IObjectPool<IPoolObject> CoinCollectionEffects { get; }
    }
}
