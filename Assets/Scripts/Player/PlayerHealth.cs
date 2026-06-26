using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerHealth : MonoBehaviour, IPlayerHealth
    {
        public bool IsAlive => this._hitPoints > 0;

        public event Action Died;

        [SerializeField]
        private int _hitPoints = 5;

        public void TakeDamage(int damage)
        {
            if (!this.IsAlive)
            {
                return;
            }

            this._hitPoints -= damage;

            if (!this.IsAlive)
            {
                this.Died?.Invoke();
            }
        }
    }
}
