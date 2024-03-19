using Assets.Codebase.Models.Gameplay;
using Assets.Codebase.Models.Progress;

namespace Assets.Codebase.Infrastructure.ServicesManagment.ModelAccess
{
    public class ModelAccessService : IModelAccesService
    {
        public IProgressModel ProgressModel { get; private set; }
        public IGameplayModel GameplayModel { get; private set; }

        public ModelAccessService(IProgressModel progressModel, IGameplayModel gameplayModel)
        {
            ProgressModel = progressModel;
            GameplayModel = gameplayModel;
        }
    }
}
