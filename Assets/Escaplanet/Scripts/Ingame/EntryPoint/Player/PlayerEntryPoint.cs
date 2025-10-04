using System;
using Escaplanet.Ingame.Core.Attract;
using Escaplanet.Ingame.Core.Player;
using Escaplanet.Ingame.GameLogic.Player;
using Escaplanet.Root.Common;
using R3;
using VContainer.Unity;

namespace Escaplanet.Ingame.EntryPoint.Player
{
    public class PlayerEntryPoint : IStartable, IFixedTickable, IDisposable
    {
        private IRotateAttractableCore _rotateAttractable;
        private IPlayerMovementCore _playerMovement;
        private IPlayerInputCore _playerInput;
        private IUnityGlobalPort _unityGlobalPort;

        private IPlayerMovementLogic _playerMovementLogic;

        CompositeDisposable _disposable = new();

        public PlayerEntryPoint(IRotateAttractableCore rotateAttractable, IPlayerMovementCore playerMovement,
            IPlayerInputCore playerInput, IPlayerMovementLogic playerMovementLogic, IUnityGlobalPort unityGlobalPort)
        {
            _rotateAttractable = rotateAttractable;
            _playerMovement = playerMovement;
            _playerInput = playerInput;
            _playerMovementLogic = playerMovementLogic;
            _unityGlobalPort = unityGlobalPort;
        }

        public void Start()
        {
        }

        public void FixedTick()
        {
            _playerMovementLogic.UpdateMovement(_playerMovement, _rotateAttractable, _playerInput, _unityGlobalPort);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}