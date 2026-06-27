using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerMovementController
    {
        private readonly IPlayerInput _input;
        private readonly IMover _mover;

        public PlayerMovementController(IPlayerInput input, IMover mover)
        {
            this._input = input;
            this._mover = mover;
        }

        public void Tick(float deltaTime)
        {
            Vector2 direction = this._input.MoveDirection;
            if (direction.sqrMagnitude <= 0.0f)
            {
                return;
            }

            this._mover.Move(direction * deltaTime);
        }
    }
}
