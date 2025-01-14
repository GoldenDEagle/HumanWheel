﻿using Assets.Codebase.Data.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.ModelAccess;
using Assets.Codebase.Mechanics.Controller;
using Assets.Codebase.Optimizations.Pooling;
using DG.Tweening;
using UnityEngine;

namespace Assets.Codebase.Mechanics.Collectables
{
    [RequireComponent(typeof(Collider))]
    public class CollectableCoin : MonoBehaviour
    {
        [SerializeField] private Transform _coinMesh;
        [SerializeField] private float _coinRotationSpeed = 10f;

        private Collider _collider;
        private IModelAccesService _modelAccesService;
        private IAssetProvider _assetProvider;
        private IPoolController _poolController;

        private bool _isBeingDestroyed = false;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _modelAccesService = ServiceLocator.Container.Single<IModelAccesService>();
            _assetProvider = ServiceLocator.Container.Single<IAssetProvider>();
            _poolController = ServiceLocator.Container.Single<IPoolController>();
        }

        private void Start()
        {
            // Random rotation starting point
            _coinMesh.Rotate(Vector3.up, Random.Range(0f, 180f));
        }

        private void Update()
        {
            if (_isBeingDestroyed) return;

            // Always rotate
            _coinMesh.Rotate(Vector3.up, Time.deltaTime * _coinRotationSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerWheel = other.GetComponent<PlayerController>();
            if (playerWheel)
            {
                _collider.enabled = false;
                _isBeingDestroyed = true;

                // add coin logic
                _modelAccesService.GameplayModel.ModifyRunCoinAmount(1);

                //var effect = _assetProvider.Instantiate("Effects/CoinCollectEffect");

                var effect = _poolController.CoinCollectionEffects.Get();
                effect.transform.position = transform.position;

                // animated destroy
                ServiceLocator.Container.Single<IAudioService>().PlaySfxSound(SoundId.CoinCollected);
                Debug.Log("Coin collected");
                Destroy(gameObject);
            }
        }
    }
}