using Assets.Codebase.Optimizations.Pooling;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Codebase.Mechanics.Effects
{
    public class CoinCollectionEffect : MonoBehaviour, IPoolObject
    {
        private ParticleSystem _particleSystem;
        private IObjectPool<IPoolObject> _linkedPool;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void SetPool(IObjectPool<IPoolObject> pool)
        {
            _linkedPool = pool;
        }

        public void OnGetFromPool()
        {
            _particleSystem.Play();
        }

        private void OnParticleSystemStopped()
        {
            _linkedPool.Release(this);
        }
    }
}
