using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour, IMover
    {
        [SerializeField]
        [FormerlySerializedAs("rigidbody2D")]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        [FormerlySerializedAs("speed")]
        private float _speed = 5.0f;

        public void Move(Vector2 direction)
        {
            Vector2 nextPosition = this._rigidbody2D.position + direction * this._speed;
            this._rigidbody2D.MovePosition(nextPosition);
        }
    }
}
