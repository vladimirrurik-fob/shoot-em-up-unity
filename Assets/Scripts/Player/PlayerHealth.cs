using System;
using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class PlayerHealth : MonoBehaviour, IPlayerHealth
    {
        public bool IsAlive => this._health != null && this._health.IsAlive;

        public event Action Died;

        private Health _health;

        [Inject]
        public void Construct(Health health, GameManager gameManager)
        {
            this._health = health;
            this._health.Died += this.OnHealthDied;
            this.Died += gameManager.FinishGame;
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
