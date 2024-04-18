using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;

namespace Assets.Codebase.Presenters.PreGame
{
    public class PreGamePresenter : BasePresenter, IPreGamePresenter
    {
        public PreGamePresenter()
        {
            CorrespondingViewId = ViewId.PreGameView;
        }
    }
}
