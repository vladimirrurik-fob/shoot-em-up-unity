using System;

namespace ShootEmUp
{
    public sealed class Health
    {
        public bool IsAlive => this._current > 0;

        public event Action Died;

        private int _current;

        public Health(int initial)
        {
            this._current = initial;
        }

        public void TakeDamage(int damage)
        {
            if (!this.IsAlive)
            {
                return;
            }

            this._current -= damage;

            if (!this.IsAlive)
            {
                this.Died?.Invoke();
            }
        }

        public void Reset(int value)
        {
            this._current = value;
        }
    }
}
