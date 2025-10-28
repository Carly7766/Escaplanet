using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Common;
using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public class PlayerJumpLogic : IPlayerJumpLogic
    {
        private IGlobalValuePort _globalValuePort;

        public PlayerJumpLogic(IGlobalValuePort globalValuePort)
        {
            _globalValuePort = globalValuePort;
        }

        public void OnJumpInput(IPlayerMovementCore playerMovement, InputState inputState)
        {
            switch (inputState)
            {
                case InputState.Down:
                    if (!playerMovement.IsJumping || !playerMovement.IsFlayingAway)
                    {
                        playerMovement.IsJumpInputHeld = true;
                    }

                    break;
                case InputState.Hold:
                    if (playerMovement.IsJumpInputHeld)
                    {
                        playerMovement.IsJumpCharging = true;
                    }

                    break;
                case InputState.Up:
                    if (playerMovement.IsJumpCharging)
                    {
                        playerMovement.Jump(playerMovement.Up *
                                            playerMovement.JumpPower *
                                            playerMovement.ChargeJumpPowerMultiplier);
                        ResetJumpCharge(playerMovement);
                    }
                    else
                    {
                        playerMovement.Jump(playerMovement.Up *
                                            playerMovement.MaxJumpPower *
                                            playerMovement.JumpPowerMultiplier);
                    }

                    playerMovement.IsJumpInputHeld = false;
                    playerMovement.IsJumping = true;

                    break;
            }
        }

        public void UpdateJump(IPlayerMovementCore playerMovement)
        {
            if (playerMovement.IsJumpCharging)
            {
                playerMovement.JumpPower += playerMovement.JumpChargeSpeed * _globalValuePort.DeltaTime;

                if (playerMovement.JumpPower > playerMovement.MaxJumpPower)
                {
                    playerMovement.JumpPower = playerMovement.MaxJumpPower;
                }
            }
        }

        public void ResetJumpCharge(IPlayerMovementCore playerMovementCore)
        {
            playerMovementCore.IsJumpCharging = false;
            playerMovementCore.JumpPower = 0;
        }
    }
}