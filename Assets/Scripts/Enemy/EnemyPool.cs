using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField]
        [FormerlySerializedAs("enemyPositions")]
        private EnemyPositions _enemyPositions;

        [SerializeField]
        [FormerlySerializedAs("character")]
        private GameObject _character;

        [SerializeField]
        [FormerlySerializedAs("worldTransform")]
        private Transform _worldTransform;

        [Header("Pool")]
        [SerializeField]
        [FormerlySerializedAs("container")]
        private Transform _container;

        [SerializeField]
        [FormerlySerializedAs("prefab")]
        private GameObject _prefab;

        private readonly Queue<GameObject> _enemyPool = new();

        private void Awake()
        {
            for (int i = 0; i < 7; i++)
            {
                GameObject enemy = Instantiate(this._prefab, this._container);
                this._enemyPool.Enqueue(enemy);
            }
        }

        public GameObject SpawnEnemy()
        {
            if (!this._enemyPool.TryDequeue(out GameObject enemy))
            {
                return null;
            }

            enemy.transform.SetParent(this._worldTransform);

            Transform spawnPosition = this._enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            Transform attackPosition = this._enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(this._character);

            return enemy;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this._container);
            this._enemyPool.Enqueue(enemy);
        }
    }
}
