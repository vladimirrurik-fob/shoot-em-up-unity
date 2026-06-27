using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyHealth : MonoBehaviour, IEnemyHealth
    {
        public bool IsAlive => this._health != null && this._health.IsAlive;

        public event Action<GameObject> Died;

        [SerializeField]
        private int _hitPoints = 3;

        public int HitPoints => this._hitPoints;

        private Health _health;

        public void Construct(Health health)
        {
            this._health = health;
            this._health.Died += this.OnHealthDied;
        }

        public void TakeDamage(int damage)
        {
            this._health?.TakeDamage(damage);
        }

        private void OnHealthDied()
        {
            this.Died?.Invoke(this.gameObject);
        }
    }
}
