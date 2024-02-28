using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;

namespace Assets.Codebase.Infrastructure.ServicesManagment.PresenterManagement
{
    /// <summary>
    /// Provides acces to presenters for Gameobjects.
    /// </summary>
    public interface IPresentersService : IService
    {
        /// <summary>
        /// Get presenter by target view ID.
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public BasePresenter GetPresenter(ViewId viewId);

        /// <summary>
        /// Close view by id.
        /// </summary>
        /// <param name="viewId"></param>
        public void CloseView(ViewId viewId);
    }
}
