namespace Yrr.UI.Core
{
    public interface IScreen
    {
        void Show(object args = null, ICallback callback = null);

        void Hide();

        public interface ICallback
        {
            void OnClose(IScreen frame);
        }
    }
}