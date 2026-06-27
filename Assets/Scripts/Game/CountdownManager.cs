using System;
using UnityEngine;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class CountdownManager : ICountdown, ITickable
    {
        private bool _active;
        private float _time;
        private string _shown;
        private Action _onComplete;
        private Action<string> _onDisplay;

        public void Begin(Action onComplete, Action<string> onDisplay)
        {
            this._onComplete = onComplete;
            this._onDisplay = onDisplay;
            this._active = true;
            this._time = 3.0f;
            this._shown = "3";
            this._onDisplay?.Invoke(this._shown);
        }

        public void Tick()
        {
            if (!this._active)
            {
                return;
            }

            this._time -= Time.deltaTime;

            string next = this._time > 2.0f ? "3"
                : this._time > 1.0f ? "2"
                : this._time > 0.0f ? "1"
                : this._time > -0.5f ? "GO!"
                : null;

            if (next == null)
            {
                this._active = false;
                this._onComplete?.Invoke();
                return;
            }

            if (next != this._shown)
            {
                this._shown = next;
                this._onDisplay?.Invoke(next);
            }
        }
    }
}
