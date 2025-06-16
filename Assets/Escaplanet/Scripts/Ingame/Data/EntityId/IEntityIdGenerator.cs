namespace Escaplanet.Ingame.Data.EntityId
{
    public interface IEntityIdGenerator
    {
        EntityId Generate(); // 新規発行
        void Recycle(EntityId id); // 破棄された ID を返却
        bool IsAlive(EntityId id); // 世代一致判定
    }
}