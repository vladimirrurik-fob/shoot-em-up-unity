using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        [FormerlySerializedAs("rigidbody2D")]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        [FormerlySerializedAs("speed")]
        private float _speed = 5.0f;

        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            Vector2 nextPosition = this._rigidbody2D.position + vector * this._speed;
            this._rigidbody2D.MovePosition(nextPosition);
        }
    }
}
