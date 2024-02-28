using Assets.Codebase.Models.Progress;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Progress
{
    public class ProgressService : IProgressService
    {
        public IProgressModel ProgressModel { get; private set; }

        public ProgressService(IProgressModel progressModel)
        { 
            ProgressModel = progressModel;
        }

        public void SaveProgress()
        {
            ProgressModel.SaveProgress();
        }
    }
}
