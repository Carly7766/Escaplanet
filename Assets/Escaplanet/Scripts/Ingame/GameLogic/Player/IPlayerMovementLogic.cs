using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Common;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public interface IPlayerMovementLogic
    {
        public void UpdateMovement(IPlayerMovementCore playerMovement, IAttractableCore attractable,
            IPlayerInputCore playerInputCore, IUnityGlobalPort unityGlobalPort);
    }
}