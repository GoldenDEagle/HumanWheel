using Assets.Codebase.Models.Gameplay;
using Assets.Codebase.Models.Progress;

namespace Assets.Codebase.Infrastructure.ServicesManagment.ModelAccess
{
    public interface IModelAccesService : IService
    {
        /// <summary>
        /// Reference to progress model.
        /// </summary>
        public IProgressModel ProgressModel { get; }
        /// <summary>
        /// Reference to gameplay model.
        /// </summary>
        public IGameplayModel GameplayModel { get; }
    }
}
