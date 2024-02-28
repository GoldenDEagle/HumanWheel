using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using System.Collections.Generic;

namespace Assets.Codebase.Infrastructure.ServicesManagment.PresenterManagement
{
    public class PresentersService : IPresentersService
    {
        private List<BasePresenter> _presenters;

        public PresentersService(List<BasePresenter> presenters)
        {
            _presenters = presenters;
        }

        public void CloseView(ViewId viewId)
        {
            var presenter = GetPresenter(viewId);
            presenter.CloseView();
        }

        public BasePresenter GetPresenter(ViewId viewId)
        {
            foreach (var presenter in _presenters)
            {
                if (presenter.GetCorrespondingViewId() == viewId)
                {
                    return presenter;
                }
            }

            return null;
        }
    }
}
