using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Mechanics.Collectables;
using Assets.Codebase.Mechanics.Units;
using UnityEngine;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Factories
{
    public class GOFactory : IGOFactory
    {
        private const string CoinPath = "Collectables/GoldCoin";
        private const string HumansPath = "Units/Man_";
        private const string BackgroundPiecesPath = "Background/BackgroundPiece_";

        private IAssetProvider _assetProvider;

        public GOFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public CollectableCoin CreateCollectableCoin()
        {
            var coin = _assetProvider.Instantiate(CoinPath).GetComponent<CollectableCoin>();
            return coin;
        }

        public HumanUnit CreateHumanUnit()
        {
            int randomNumber = Random.Range(1, 7);
            var unit = _assetProvider.Instantiate(HumansPath + randomNumber).GetComponent<HumanUnit>();
            return unit;
        }

        public Transform CreateBackgroundPiece()
        {
            int randomNumber = Random.Range(1, 7);
            var piece = _assetProvider.Instantiate(BackgroundPiecesPath + 1).GetComponent<Transform>();
            return piece;
        }
    }
}
