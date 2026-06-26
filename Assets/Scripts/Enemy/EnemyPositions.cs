using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class EnemyPositions : MonoBehaviour
    {
        [SerializeField]
        [FormerlySerializedAs("spawnPositions")]
        private Transform[] _spawnPositions;

        [SerializeField]
        [FormerlySerializedAs("attackPositions")]
        private Transform[] _attackPositions;

        public Transform RandomSpawnPosition()
        {
            return this.RandomTransform(this._spawnPositions);
        }

        public Transform RandomAttackPosition()
        {
            return this.RandomTransform(this._attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            int index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}
