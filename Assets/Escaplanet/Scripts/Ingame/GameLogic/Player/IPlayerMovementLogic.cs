using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.Player;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public interface IPlayerMovementLogic
    {
        public void UpdateMovement(IAttractableCore attractable, IPlayerMovementCore playerMovement,
            IPlayerInputCore playerInput, IPlayerAppearanceCore playerAppearance);
    }
}