using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerMovement : MonoBehaviour, IGameFixedUpdateListener
    {
        private PlayerMovementController _controller;

        private void Awake()
        {
            IPlayerInput input = this.GetComponent<IPlayerInput>();
            IMover mover = this.GetComponent<IMover>();
            this._controller = new PlayerMovementController(input, mover);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            this._controller.Tick(deltaTime);
        }
    }
}
