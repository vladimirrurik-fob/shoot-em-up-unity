using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public delegate void FireHandler(GameObject enemy, Vector2 position, Vector2 direction);

        public event FireHandler OnFire;

        [SerializeField]
        [FormerlySerializedAs("weaponComponent")]
        private WeaponComponent _weaponComponent;

        [SerializeField]
        [FormerlySerializedAs("moveAgent")]
        private EnemyMoveAgent _moveAgent;

        [SerializeField]
        [FormerlySerializedAs("countdown")]
        private float _countdown = 1.0f;

        private GameObject _target;
        private IPlayerHealth _targetHealth;
        private float _currentTime;

        public void SetTarget(GameObject target)
        {
            this._target = target;
            this._targetHealth = target.GetComponent<IPlayerHealth>();
        }

        public void Reset()
        {
            this._currentTime = this._countdown;
        }

        private void FixedUpdate()
        {
            if (!this._moveAgent.IsReached)
            {
                return;
            }

            if (this._targetHealth == null || !this._targetHealth.IsAlive)
            {
                return;
            }

            this._currentTime -= Time.fixedDeltaTime;
            if (this._currentTime <= 0.0f)
            {
                this.Fire();
                this._currentTime += this._countdown;
            }
        }

        private void Fire()
        {
            Vector2 startPosition = this._weaponComponent.Position;
            Vector2 vector = (Vector2)this._target.transform.position - startPosition;
            Vector2 direction = vector.normalized;
            this.OnFire?.Invoke(this.gameObject, startPosition, direction);
        }
    }
}
