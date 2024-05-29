using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Mechanics.Effects;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace Assets.Codebase.Optimizations.Pooling
{
    public class PoolController : IPoolController
    {
        public IObjectPool<IPoolObject> CoinCollectionEffects { get; private set; }

        private const string CoinCollectionEffectPath = "Effects/CoinCollectEffect";

        private IAssetProvider _assetProvider;

        public PoolController(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            CoinCollectionEffects = new ObjectPool<IPoolObject>(CreateCollectionEffect, OnGetCoinCollectionEffect, OnEffectReleased);
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private CoinCollectionEffect CreateCollectionEffect()
        {
            var effect = _assetProvider.Instantiate(CoinCollectionEffectPath).GetComponent<CoinCollectionEffect>();
            effect.SetPool(CoinCollectionEffects);
            return effect;
        }

        private void OnGetCoinCollectionEffect(IPoolObject coinEffect)
        {
            coinEffect.gameObject.SetActive(true);
            coinEffect.OnGetFromPool();
        }

        public void OnEffectReleased(IPoolObject coinEffect)
        {
            coinEffect.gameObject.SetActive(false);
        }

        private void OnSceneChanged(Scene arg0, Scene arg1)
        {
            CoinCollectionEffects.Clear();
        }
    }
}
