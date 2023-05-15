using ConnectionLost.Models;
using ConnectionLost.Views;


namespace ConnectionLost.Controllers
{
    public sealed class BonusController : IContentController
    {
        private readonly BonusView _view;

        public BonusController(BonusView view)
        {
            _view = view;
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_view.gameObject);
        }
    }
}
