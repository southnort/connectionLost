using ConnectionLost.Models;
using ConnectionLost.Views;

namespace ConnectionLost.Controllers
{
    public sealed class BonusController : IContentController
    {
        private readonly BonusView _view;
        private readonly BonusBase _model;

        public BonusController(BonusView view, BonusBase model)
        {
            _view = view;
            _model = model;
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_view.gameObject);
        }
    }
}
