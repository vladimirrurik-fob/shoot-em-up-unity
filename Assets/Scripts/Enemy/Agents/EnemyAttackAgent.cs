using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        public delegate void FireHandler(GameObject enemy, Vector2 position, Vector2 direction);

        public event FireHandler OnFire;

        [SerializeField]
        [FormerlySerializedAs("countdown")]
        private float _countdown = 1.0f;

        public float Countdown => this._countdown;

        private EnemyAttacker _attacker;
        private WeaponComponent _weapon;
        private EnemyMoveAgent _moveAgent;
        private GameObject _target;
        private IPlayerHealth _targetHealth;

        public void Construct(EnemyAttacker attacker)
        {
            this._attacker = attacker;
            this._weapon = this.GetComponent<WeaponComponent>();
            this._moveAgent = this.GetComponent<EnemyMoveAgent>();
            this._attacker.FireRequested += this.OnFireRequested;
        }

        public void SetTarget(GameObject target)
        {
            this._target = target;
            this._targetHealth = target != null ? target.GetComponent<IPlayerHealth>() : null;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (this._attacker == null || this._targetHealth == null || this._target == null)
            {
                return;
            }

            bool canFire = this._moveAgent != null && this._moveAgent.IsReached && this._targetHealth.IsAlive;
            this._attacker.Tick(
                canFire,
                this._weapon.Position,
                (Vector2)this._target.transform.position,
                deltaTime);
        }

        private void OnFireRequested(Vector2 position, Vector2 direction)
        {
            this.OnFire?.Invoke(this.gameObject, position, direction);
        }
    }
}
