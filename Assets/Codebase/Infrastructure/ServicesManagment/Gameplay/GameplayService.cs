using Assets.Codebase.Models.Gameplay;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Gameplay
{
    public class GameplayService : IGameplayService
    {
        public IGameplayModel GameplayModel { get ; private set; }

        public GameplayService(IGameplayModel gameplayModel)
        {
            GameplayModel = gameplayModel;
        }
    }
}
