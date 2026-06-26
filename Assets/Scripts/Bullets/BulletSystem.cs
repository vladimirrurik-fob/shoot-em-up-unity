using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField]
        private int _initialCount = 50;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private Bullet _prefab;

        [SerializeField]
        private Transform _worldTransform;

        [SerializeField]
        private LevelBounds _levelBounds;

        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();

        private void Awake()
        {
            for (int i = 0; i < this._initialCount; i++)
            {
                Bullet bullet = Instantiate(this._prefab, this._container);
                this._bulletPool.Enqueue(bullet);
            }
        }

        private void FixedUpdate()
        {
            this._cache.Clear();
            this._cache.AddRange(this._activeBullets);

            for (int i = 0, count = this._cache.Count; i < count; i++)
            {
                Bullet bullet = this._cache[i];
                if (!this._levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            if (!this._bulletPool.TryDequeue(out Bullet bullet))
            {
                bullet = Instantiate(this._prefab, this._worldTransform);
            }
            else
            {
                bullet.transform.SetParent(this._worldTransform);
            }

            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.Setup(args.team, args.damage);
            bullet.SetVelocity(args.velocity);

            if (this._activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += this.OnBulletCollision;
            }
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            this.DealDamage(bullet, collision.gameObject);
            this.RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (!this._activeBullets.Remove(bullet))
            {
                return;
            }

            bullet.OnCollisionEntered -= this.OnBulletCollision;
            bullet.transform.SetParent(this._container);
            this._bulletPool.Enqueue(bullet);
        }

        private void DealDamage(Bullet bullet, GameObject other)
        {
            int damage = bullet.Damage;

            if (bullet.Team == Team.Player)
            {
                if (other.TryGetComponent(out IEnemyHealth enemyHealth))
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
            else
            {
                if (other.TryGetComponent(out IPlayerHealth playerHealth))
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }

        public readonly struct Args
        {
            public readonly Vector2 position;
            public readonly Vector2 velocity;
            public readonly Color color;
            public readonly int physicsLayer;
            public readonly int damage;
            public readonly Team team;

            public Args(
                Vector2 position,
                Vector2 velocity,
                Color color,
                int physicsLayer,
                int damage,
                Team team)
            {
                this.position = position;
                this.velocity = velocity;
                this.color = color;
                this.physicsLayer = physicsLayer;
                this.damage = damage;
                this.team = team;
            }
        }
    }
}
