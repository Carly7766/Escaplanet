using Escaplanet.Ingame.Core;

namespace Escaplanet.Ingame.System
{
    public interface ISystem<in TEntity> where TEntity : IEntity
    {
        void Register(TEntity entity);
        void Unregister(TEntity entity);

        void Execute();
    }

    public interface IFixedExecuteSystem<in TEntity> : ISystem<TEntity> where TEntity : IEntity
    {
        void FixedExecute();
    }
}