using Assets.Codebase.Models.Progress;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Progress
{
    /// <summary>
    /// Provides access to progress model.
    /// </summary>
    public interface IProgressService : IService
    {
        /// <summary>
        /// Reference to progress model.
        /// </summary>
        public IProgressModel ProgressModel { get; }

        /// <summary>
        /// Creates persistant data save.
        /// </summary>
        public void SaveProgress();
    }
}
