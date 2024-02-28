using Assets.Codebase.Views.Base;

namespace Assets.Codebase.Infrastructure.ServicesManagment.ViewCreation
{
    /// <summary>
    /// Allows to create views.
    /// </summary>
    public interface IViewCreatorService : IService
    {
        public BaseView CreateView(ViewId viewId);
    }
}
