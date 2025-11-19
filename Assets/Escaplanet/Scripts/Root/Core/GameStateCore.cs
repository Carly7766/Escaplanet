namespace Escaplanet.Root.Core
{
    public interface IGameInfoCore
    {
        GameState CurrentGameState { get; set; }
        GameResult CurrentGameResult { get; set; }
        GameOverType CurrentGameOverType { get; set; }
    }
}