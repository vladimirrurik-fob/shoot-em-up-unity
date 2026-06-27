using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour, IBulletLauncher, IGameFixedUpdateListener
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

        private ObjectPool<Bullet> _pool;
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();

        private void Awake()
        {
            this._pool = new ObjectPool<Bullet>(
                factory: () => Instantiate(this._prefab, this._container),
                onGet: this.Acquire,
                onRelease: this.Return);

            for (int i = 0; i < this._initialCount; i++)
            {
                Bullet bullet = Instantiate(this._prefab, this._container);
                this._pool.Release(bullet);
            }
        }

        public void OnFixedUpdate(float deltaTime)
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

        public void Launch(BulletArgs args)
        {
            Bullet bullet = this._pool.Get();
            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.Setup(args.team, args.damage);
            bullet.SetVelocity(args.velocity);
            this._activeBullets.Add(bullet);
        }

        private void Acquire(Bullet bullet)
        {
            bullet.transform.SetParent(this._worldTransform);
            bullet.OnCollisionEntered += this.OnBulletCollision;
        }

        private void Return(Bullet bullet)
        {
            bullet.OnCollisionEntered -= this.OnBulletCollision;
            bullet.transform.SetParent(this._container);
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

            this._pool.Release(bullet);
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
    }
}
