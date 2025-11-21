using Escaplanet.Ingame.Core.Player;
using Escaplanet.Root.Core.Common;
using Escaplanet.Root.Core.Common.ValueObject;

namespace Escaplanet.Ingame.GameLogic.Player
{
    public class PlayerJumpLogic : IPlayerJumpLogic
    {
        private readonly IGlobalValuePort _globalValuePort;
        private IFloatMathPort _floatMathPort;

        public PlayerJumpLogic(IGlobalValuePort globalValuePort)
        {
            _globalValuePort = globalValuePort;
        }

        public void OnJumpInput(IPlayerMovementCore playerMovement, InputState inputState)
        {
            switch (inputState)
            {
                case InputState.Down:
                    if (!playerMovement.IsJumping && !playerMovement.IsBlownAway) playerMovement.IsJumpInputHeld = true;

                    break;
                case InputState.Hold:
                    if (playerMovement.IsJumpInputHeld) playerMovement.IsJumpCharging = true;

                    break;
                case InputState.Up:
                    if (playerMovement.IsJumping || !playerMovement.IsJumpInputHeld) break;
                    if (playerMovement.IsJumpCharging)
                    {
                        playerMovement.Jump(playerMovement.Up *
                                            playerMovement.JumpCharge *
                                            playerMovement.ChargedJumpMultiplier);
                        ResetJumpCharge(playerMovement);
                    }
                    else
                    {
                        playerMovement.Jump(playerMovement.Up *
                                            playerMovement.MaxJumpPower *
                                            playerMovement.UnchargedJumpMultiplier);
                    }

                    playerMovement.IsJumpInputHeld = false;
                    playerMovement.IsJumping = true;

                    break;
            }
        }

        public void UpdateJumpCharge(IPlayerMovementCore playerMovement)
        {
            if (playerMovement.IsJumpCharging)
            {
                playerMovement.JumpCharge += playerMovement.JumpPowerChargeSpeed * _globalValuePort.DeltaTime;

                if (playerMovement.JumpCharge > playerMovement.MaxJumpPower)
                    playerMovement.JumpCharge = playerMovement.MaxJumpPower;
            }
        }

        public void FixedUpdateJump(IPlayerMovementCore playerMovement)
        {
        }

        public void ResetJumpCharge(IPlayerMovementCore playerMovementCore)
        {
            playerMovementCore.IsJumpCharging = false;
            playerMovementCore.JumpCharge = 0;
        }
    }
}