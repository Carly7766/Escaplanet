namespace Escaplanet.Root.Domain.TempSceneData
{
    public interface ITempDataRepository<T, TFormat>
    {
        T GetData();
        void SetData(TFormat data);
    }
}