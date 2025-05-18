using Game.Scripts.UI;

namespace Game.Scripts.Services.View
{
    public interface IViewContainer : IService
    {
        public T ShowView<T>() where T : BaseView;

        public T GetView<T>(bool isHide = true) where T : BaseView;
    }
}