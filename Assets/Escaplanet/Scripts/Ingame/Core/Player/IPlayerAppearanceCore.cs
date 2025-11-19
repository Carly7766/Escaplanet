namespace Escaplanet.Ingame.Core.Player
{
    public interface IPlayerAppearanceCore
    {
        bool IsFacingRight { get; }
        void Flip(bool facingRight);
    }
}