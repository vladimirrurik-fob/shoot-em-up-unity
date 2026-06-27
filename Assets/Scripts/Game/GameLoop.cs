using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class GameLoop : IGameLoop, ITickable, IFixedTickable
    {
        public bool Enabled { get; set; }

        private readonly List<IGameUpdateListener> _updateListeners;
        private readonly List<IGameFixedUpdateListener> _fixedUpdateListeners;

        private readonly List<IGameUpdateListener> _updateSnapshot = new();
        private readonly List<IGameFixedUpdateListener> _fixedSnapshot = new();

        public GameLoop(
            IReadOnlyList<IGameUpdateListener> updateListeners,
            IReadOnlyList<IGameFixedUpdateListener> fixedUpdateListeners)
        {
            this._updateListeners = new List<IGameUpdateListener>(updateListeners);
            this._fixedUpdateListeners = new List<IGameFixedUpdateListener>(fixedUpdateListeners);
        }

        public void AddListener(MonoBehaviour listener)
        {
            if (listener is IGameUpdateListener updateListener)
            {
                this._updateListeners.Add(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedListener)
            {
                this._fixedUpdateListeners.Add(fixedListener);
            }
        }

        public void AddListener(GameObject gameObject)
        {
            foreach (MonoBehaviour listener in gameObject.GetComponents<MonoBehaviour>())
            {
                this.AddListener(listener);
            }
        }

        public void RemoveListener(MonoBehaviour listener)
        {
            if (listener is IGameUpdateListener updateListener)
            {
                this._updateListeners.Remove(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedListener)
            {
                this._fixedUpdateListeners.Remove(fixedListener);
            }
        }

        public void RemoveListener(GameObject gameObject)
        {
            foreach (MonoBehaviour listener in gameObject.GetComponents<MonoBehaviour>())
            {
                this.RemoveListener(listener);
            }
        }

        public void Tick()
        {
            if (!this.Enabled)
            {
                return;
            }

            float deltaTime = Time.deltaTime;
            this._updateSnapshot.Clear();
            this._updateSnapshot.AddRange(this._updateListeners);

            for (int i = 0, count = this._updateSnapshot.Count; i < count; i++)
            {
                this._updateSnapshot[i].OnUpdate(deltaTime);
            }
        }

        public void FixedTick()
        {
            if (!this.Enabled)
            {
                return;
            }

            float deltaTime = Time.fixedDeltaTime;
            this._fixedSnapshot.Clear();
            this._fixedSnapshot.AddRange(this._fixedUpdateListeners);

            for (int i = 0, count = this._fixedSnapshot.Count; i < count; i++)
            {
                this._fixedSnapshot[i].OnFixedUpdate(deltaTime);
            }
        }
    }
}
