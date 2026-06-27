using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerHealth : MonoBehaviour, IPlayerHealth
    {
        public bool IsAlive => this._health != null && this._health.IsAlive;

        public event Action Died;

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
            this.Died?.Invoke();
        }
    }
}
