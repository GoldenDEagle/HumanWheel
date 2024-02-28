using Assets.Codebase.Models.Gameplay;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Gameplay
{
    /// <summary>
    /// Provides access to gameplay model.
    /// </summary>
    public interface IGameplayService : IService
    {
        /// <summary>
        /// Reference to gameplay model.
        /// </summary>
        public IGameplayModel GameplayModel { get; }
    }
}
