namespace Escaplanet.Ingame.Core.GameOver
{
    public interface IGameOverPolicy
    {
        float GraceSeconds { get; }
        int CountdownSeconds { get; }
    }
}