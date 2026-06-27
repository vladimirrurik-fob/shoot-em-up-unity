using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerMovement : MonoBehaviour
    {
        private PlayerMovementController _controller;

        private void Awake()
        {
            IPlayerInput input = this.GetComponent<IPlayerInput>();
            IMover mover = this.GetComponent<IMover>();
            this._controller = new PlayerMovementController(input, mover);
        }

        private void FixedUpdate()
        {
            this._controller.Tick(Time.fixedDeltaTime);
        }
    }
}
