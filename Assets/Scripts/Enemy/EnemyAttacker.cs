using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttacker
    {
        public event Action<Vector2, Vector2> FireRequested;

        private readonly float _countdown;
        private float _currentTime;

        public EnemyAttacker(float countdown)
        {
            this._countdown = countdown;
            this._currentTime = countdown;
        }

        public void Reset()
        {
            this._currentTime = this._countdown;
        }

        public void Tick(bool canFire, Vector2 firePosition, Vector2 targetPosition, float deltaTime)
        {
            if (!canFire)
            {
                return;
            }

            this._currentTime -= deltaTime;
            if (this._currentTime > 0.0f)
            {
                return;
            }

            Vector2 direction = (targetPosition - firePosition).normalized;
            this.FireRequested?.Invoke(firePosition, direction);
            this._currentTime += this._countdown;
        }
    }
}
