using UnityEngine;

namespace ShootEmUp
{
    public sealed class KeyboardPlayerInput : MonoBehaviour, IPlayerInput
    {
        public Vector2 MoveDirection { get; private set; }

        private bool _fireRequested;

        private void Update()
        {
            this.MoveDirection = this.ReadMoveDirection();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this._fireRequested = true;
            }
        }

        public bool ConsumeFireRequest()
        {
            if (!this._fireRequested)
            {
                return false;
            }

            this._fireRequested = false;
            return true;
        }

        private Vector2 ReadMoveDirection()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return Vector2.left;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                return Vector2.right;
            }

            return Vector2.zero;
        }
    }
}
