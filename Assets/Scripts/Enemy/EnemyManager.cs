using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyPool _enemyPool;

        [SerializeField]
        private BulletConfig _enemyBulletConfig;

        [SerializeField]
        private float _spawnInterval = 1.0f;

        private IBulletLauncher _launcher;
        private readonly HashSet<GameObject> _activeEnemies = new();

        public void Construct(IBulletLauncher launcher)
        {
            this._launcher = launcher;
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(this._spawnInterval);

                GameObject enemy = this._enemyPool.SpawnEnemy();
                if (enemy != null && this._activeEnemies.Add(enemy))
                {
                    if (enemy.TryGetComponent(out IEnemyHealth health))
                    {
                        health.Died += this.OnEnemyDied;
                    }

                    if (enemy.TryGetComponent(out EnemyAttackAgent agent))
                    {
                        agent.OnFire += this.OnFire;
                    }
                }
            }
        }

        private void OnEnemyDied(GameObject enemy)
        {
            if (!this._activeEnemies.Remove(enemy))
            {
                return;
            }

            if (enemy.TryGetComponent(out IEnemyHealth health))
            {
                health.Died -= this.OnEnemyDied;
            }

            if (enemy.TryGetComponent(out EnemyAttackAgent agent))
            {
                agent.OnFire -= this.OnFire;
            }

            this._enemyPool.UnspawnEnemy(enemy);
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            this._launcher.Launch(new BulletArgs(
                position,
                direction * this._enemyBulletConfig.Speed,
                this._enemyBulletConfig.Color,
                (int)PhysicsLayer.ENEMY_BULLET,
                this._enemyBulletConfig.Damage,
                Team.Enemy));
        }
    }
}
