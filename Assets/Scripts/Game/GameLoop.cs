using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameLoop : MonoBehaviour
    {
        private readonly List<IGameUpdateListener> _updateListeners = new();
        private readonly List<IGameFixedUpdateListener> _fixedUpdateListeners = new();

        private readonly List<IGameUpdateListener> _updateSnapshot = new();
        private readonly List<IGameFixedUpdateListener> _fixedSnapshot = new();

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

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            this._updateSnapshot.Clear();
            this._updateSnapshot.AddRange(this._updateListeners);

            for (int i = 0, count = this._updateSnapshot.Count; i < count; i++)
            {
                this._updateSnapshot[i].OnUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
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
