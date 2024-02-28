using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Progress.Data;

namespace Assets.Codebase.Models.Progress
{
    /// <summary>
    /// Model that contains all data that needs to be saved.
    /// </summary>
    public interface IProgressModel : IModel
    {
        /// <summary>
        /// Contains all progress data and notifies presenters.
        /// </summary>
        public SessionProgress SessionProgress { get; }

        /// <summary>
        /// Creates a save.
        /// </summary>
        public void SaveProgress();

        /// <summary>
        /// Loads data from saving place.
        /// </summary>
        public void LoadProgress();
    }
}
