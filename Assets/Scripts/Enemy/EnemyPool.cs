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

        private GameManager _gameManager;
        private ObjectPool<GameObject> _pool;

        private void Awake()
        {
            this._pool = new ObjectPool<GameObject>(
                factory: () => Instantiate(this._prefab, this._container),
                onGet: enemy => enemy.transform.SetParent(this._worldTransform),
                onRelease: enemy => enemy.transform.SetParent(this._container));
        }

        public void Construct(GameManager gameManager)
        {
            this._gameManager = gameManager;
        }

        public GameObject SpawnEnemy()
        {
            GameObject enemy = this._pool.Get();

            enemy.transform.position = this._enemyPositions.RandomSpawnPosition().position;

            var moveAgent = new MoveAgent();
            var moveAdapter = enemy.GetComponent<EnemyMoveAgent>();
            moveAdapter.Construct(moveAgent);
            moveAdapter.SetDestination(this._enemyPositions.RandomAttackPosition().position);

            var attackAdapter = enemy.GetComponent<EnemyAttackAgent>();
            attackAdapter.Construct(new EnemyAttacker(attackAdapter.Countdown));
            attackAdapter.SetTarget(this._character);

            var healthAdapter = enemy.GetComponent<EnemyHealth>();
            healthAdapter.Construct(new Health(healthAdapter.HitPoints));

            this._gameManager.AddListener(enemy);
            return enemy;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            this._gameManager.RemoveListener(enemy);
            this._pool.Release(enemy);
        }
    }
}
