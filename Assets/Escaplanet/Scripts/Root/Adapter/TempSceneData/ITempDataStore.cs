namespace Escaplanet.Root.Adapter.TempSceneData
{
    public interface ITempDataStore<T>
    {
        void RestoreTempData(T data);
        T LoadTempData();
    }
}