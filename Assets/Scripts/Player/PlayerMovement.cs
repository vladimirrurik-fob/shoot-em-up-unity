using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 5.0f;

        private Rigidbody2D _rigidbody2D;
        private IPlayerInput _input;

        private void Awake()
        {
            this._rigidbody2D = this.GetComponent<Rigidbody2D>();
            this._input = this.GetComponent<IPlayerInput>();
        }

        private void FixedUpdate()
        {
            Vector2 direction = this._input.MoveDirection;
            if (direction.sqrMagnitude <= 0.0f)
            {
                return;
            }

            Vector2 nextPosition = this._rigidbody2D.position
                                   + direction * (this._speed * Time.fixedDeltaTime);

            this._rigidbody2D.MovePosition(nextPosition);
        }
    }
}
