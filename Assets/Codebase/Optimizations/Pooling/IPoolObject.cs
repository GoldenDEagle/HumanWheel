using Assets.Codebase.Utils.CustomTypes;
using UnityEngine.Pool;

namespace Assets.Codebase.Optimizations.Pooling
{
    public interface IPoolObject : IComponent
    {
        public void SetPool(IObjectPool<IPoolObject> pool);
        public void OnGetFromPool();
    }
}
