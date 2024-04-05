using Assets.Codebase.Mechanics.Collectables;
using Assets.Codebase.Mechanics.Units;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Factories
{
    public interface ICollectablesFactory : IService
    {
        public HumanUnit CreateHumanUnit();

        public CollectableCoin CreateCollectableCoin();
    }
}
