using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [SerializeField]
        [FormerlySerializedAs("rigidbody2D")]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        [FormerlySerializedAs("spriteRenderer")]
        private SpriteRenderer _spriteRenderer;

        private Team _team;
        private int _damage;

        public Team Team => this._team;

        public int Damage => this._damage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollisionEntered?.Invoke(this, collision);
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        public void SetVelocity(Vector2 velocity)
        {
            this._rigidbody2D.linearVelocity = velocity;
        }

        public void SetColor(Color color)
        {
            this._spriteRenderer.color = color;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            this.gameObject.layer = physicsLayer;
        }

        public void Setup(Team team, int damage)
        {
            this._team = team;
            this._damage = damage;
        }
    }
}
