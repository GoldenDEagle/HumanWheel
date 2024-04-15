using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Factories;
using Assets.Codebase.Utils.GOComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Mechanics.LevelGeneration
{
    public class LevelSegmentExtension : MonoBehaviour
    {
        [SerializeField] private int _heightGrowLevel = 0;
        [SerializeField] private bool _placeHumans = true;
        [SerializeField] private int _humanCount  = 0;
        [SerializeField] private bool _placeCoins = true;
        [SerializeField] private List<PlacementSpot> _humanPositions;
        [SerializeField] private List<PlacementSpot> _coinPositions;

        public bool PlaceHumans => _placeHumans;
        public bool PlaceCoins => _placeCoins;
        public int HeightGrowLevel => _heightGrowLevel;

        public void ConfigureCollectables(int coinsToSpawn)
        {
            SpawnHumans();

            SpawnCoins(coinsToSpawn);
        }

        private void SpawnCoins(int coinsToSpawn)
        {
            var collectablesFactory = ServiceLocator.Container.Single<IGOFactory>();

            // not to exceed positions count
            if (coinsToSpawn > _coinPositions.Count) coinsToSpawn = _coinPositions.Count;

            for (int i = 0; i <= coinsToSpawn; i++)
            {
                var coin = collectablesFactory.CreateCollectableCoin();
                bool spotWasFound = false;
                while (!spotWasFound)
                {
                    int randomIndex = Random.Range(0, _coinPositions.Count);

                    if (_coinPositions[randomIndex].IsOccupied) continue;

                    spotWasFound = true;
                    _coinPositions[randomIndex].IsOccupied = true;
                    coin.transform.SetParent(_coinPositions[randomIndex].transform, false);
                    coin.transform.localPosition = Vector3.zero;
                }
            }
        }

        private void SpawnHumans()
        {
            var collectablesFactory = ServiceLocator.Container.Single<IGOFactory>();

            for (int i = 0; i < _humanCount; i++)
            {
                var human = collectablesFactory.CreateHumanUnit();
                bool spotWasFound = false;
                while (!spotWasFound)
                {
                    int randomIndex = Random.Range(0, _humanPositions.Count);

                    if (_humanPositions[randomIndex].IsOccupied) continue;

                    spotWasFound = true;
                    _humanPositions[randomIndex].IsOccupied = true;
                    human.transform.SetParent(_humanPositions[randomIndex].transform, false);
                    human.transform.localPosition = Vector3.zero;
                    human.transform.Rotate(0f, 180f, 0f);
                }
            }
        }
    }
}