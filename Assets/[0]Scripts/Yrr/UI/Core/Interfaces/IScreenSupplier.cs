namespace Yrr.UI.Core
{
    public interface IScreenSupplier<in TKey, out TValue> where TValue : IScreen
    {
        TValue LoadScreen(TKey key);       
    }
}