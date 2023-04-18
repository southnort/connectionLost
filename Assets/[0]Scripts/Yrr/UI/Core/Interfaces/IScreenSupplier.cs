namespace Yrr.UI.Core
{
    public interface IScreenSupplier<in TKey, TValue> where TValue : IScreen
    {
        TValue LoadScreen(TKey key);

        void UnloadScreen(TValue frame);
    }
}