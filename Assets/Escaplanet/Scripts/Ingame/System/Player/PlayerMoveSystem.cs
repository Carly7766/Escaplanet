using Escaplanet.Ingame.Data.Attract;
using Escaplanet.Ingame.Data.Player;
using R3;
using UnityEngine;
using VContainer.Unity;
using Vector2 = Escaplanet.Ingame.Data.Common.Vector2;

namespace Escaplanet.Ingame.System.Player
{
    public class PlayerMoveSystem : IFixedTickable
    {
        private IAttractableEntity _attractableEntity;
        private IPlayerInputEntity _playerInputEntity;
        private IPlayerMoveEntity _playerMoveEntity;

        public PlayerMoveSystem(IAttractableEntity attractableEntity, IPlayerInputEntity playerInputEntity,
            IPlayerMoveEntity playerMoveEntity)
        {
            _attractableEntity = attractableEntity;
            _playerInputEntity = playerInputEntity;
            _playerMoveEntity = playerMoveEntity;

            _playerInputEntity.OnJump.Subscribe(OnJump);
        }

        private void OnJump(Unit unit)
        {
            // Handle player jump logic here
            // For example, apply a jump force to the player
        }

        //TODO: ValueObject化してこの関数からプリミティブ型を排除する 
        public void FixedTick()
        {
            if (_attractableEntity.NearestSource == null) return;

            // Rotate
            var direction = _attractableEntity.NearestSource.Position.Subtract(_playerMoveEntity.Position).Normalize();
            var angle = Mathf.Atan2(direction.Y, direction.X) * Mathf.Rad2Deg;
            var targetRotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            var targetAngle = targetRotation.eulerAngles.z;

            var currentRotationSR = Quaternion.Euler(0, 0, _playerMoveEntity.Rotation);
            var nextRotationSR = Quaternion.Slerp(currentRotationSR, targetRotation,
                _playerMoveEntity.RotateSpeed * Time.fixedDeltaTime);
            _playerMoveEntity.Rotate(nextRotationSR.eulerAngles.z);


            if (_playerMoveEntity.IsFlayingAway) return;

            var diff = _playerMoveEntity.Position.Subtract(_attractableEntity.NearestSource.Position);

            var perpendicular = new Vector2(diff.Y, -diff.X);
            var perpendicularNormalized = perpendicular.Normalize();

            var perpendicularSpeed = Vector2.Dot(_playerMoveEntity.Velocity, perpendicularNormalized);

            if (perpendicularSpeed.Absolute().Value > _playerMoveEntity.MoveSpeed)
            {
                _playerMoveEntity.IsFlayingAway = true;
                return;
            }

            var targetSpeed = _playerInputEntity.MoveInput * _playerMoveEntity.MoveSpeed;
            targetSpeed = Mathf.Lerp(perpendicularSpeed.Value, targetSpeed, _playerMoveEntity.MovementLerpAmount);

            var accelRate = _playerMoveEntity.Acceleration / _playerMoveEntity.MoveSpeed * (1.0f / Time.fixedDeltaTime);

            var speedDif = targetSpeed - perpendicularSpeed.Value;
            var movement = speedDif * accelRate;

            _playerMoveEntity.Move(new Vector2(perpendicularNormalized.X * movement,
                perpendicularNormalized.Y * movement));
        }
    }
}