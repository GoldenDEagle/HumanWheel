using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Progress.Data;
using Assets.Codebase.Utils.Extensions;
using UnityEngine;

namespace Assets.Codebase.Models.Progress
{
    /// <summary>
    /// Model for local saving case (PlayerPrefs).
    /// </summary>
    public class LocalProgressModel : BaseModel, IProgressModel
    {
        private const string ProgressKey = "Progress";

        public SessionProgress SessionProgress { get; private set; }

        private PersistantProgress _persistantProgress;

        public LocalProgressModel()
        {
            LoadProgress();
        }

        public void InitModel()
        {
        }

        protected bool CanFindSave()
        {
            return PlayerPrefs.HasKey(ProgressKey);
        }

        protected void CreateNewProgress()
        {
            SessionProgress = new SessionProgress();
        }

        public void SaveProgress()
        {
            // New PersistantProgress is created on first save
            if (_persistantProgress == null)
            {
                _persistantProgress = new PersistantProgress();
            }

            _persistantProgress.SetValues(SessionProgress);
            PlayerPrefs.SetString(ProgressKey, _persistantProgress.ToJson());
        }

        public void LoadProgress()
        {
            if (CanFindSave())
            {
                GetProgressFromPrefs();
            }
            else
            {
                CreateNewProgress();
            }
        }

        private void GetProgressFromPrefs()
        {
            var progress = PlayerPrefs.GetString(ProgressKey).ToDeserealized<PersistantProgress>();
            SessionProgress = new SessionProgress(progress);
        }
    }
}
