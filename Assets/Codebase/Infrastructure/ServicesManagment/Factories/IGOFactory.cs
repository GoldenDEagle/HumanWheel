using Assets.Codebase.Mechanics.Collectables;
using Assets.Codebase.Mechanics.Units;
using UnityEngine;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Factories
{
    public interface IGOFactory : IService
    {
        public HumanUnit CreateHumanUnit();

        public CollectableCoin CreateCollectableCoin();

        public Transform CreateBackgroundPiece();
    }
}
