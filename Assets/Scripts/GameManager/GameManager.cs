using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager
    {
        public GameState State { get; private set; } = GameState.Boot;

        private readonly IGameLoop _gameLoop;

        private readonly List<IGameStartListener> _startListeners = new();
        private readonly List<IGamePauseListener> _pauseListeners = new();
        private readonly List<IGameResumeListener> _resumeListeners = new();
        private readonly List<IGameFinishListener> _finishListeners = new();

        public GameManager(IGameLoop gameLoop)
        {
            this._gameLoop = gameLoop;
        }

        public void AddListener(MonoBehaviour listener)
        {
            if (listener is IGameStartListener startListener)
            {
                this._startListeners.Add(startListener);
            }

            if (listener is IGamePauseListener pauseListener)
            {
                this._pauseListeners.Add(pauseListener);
            }

            if (listener is IGameResumeListener resumeListener)
            {
                this._resumeListeners.Add(resumeListener);
            }

            if (listener is IGameFinishListener finishListener)
            {
                this._finishListeners.Add(finishListener);
            }

            this._gameLoop.AddListener(listener);
        }

        public void AddListener(GameObject gameObject)
        {
            foreach (MonoBehaviour listener in gameObject.GetComponents<MonoBehaviour>())
            {
                this.AddListener(listener);
            }
        }

        public void RemoveListener(GameObject gameObject)
        {
            foreach (MonoBehaviour listener in gameObject.GetComponents<MonoBehaviour>())
            {
                this.RemoveListener(listener);
            }
        }

        public void RemoveListener(MonoBehaviour listener)
        {
            if (listener is IGameStartListener startListener)
            {
                this._startListeners.Remove(startListener);
            }

            if (listener is IGamePauseListener pauseListener)
            {
                this._pauseListeners.Remove(pauseListener);
            }

            if (listener is IGameResumeListener resumeListener)
            {
                this._resumeListeners.Remove(resumeListener);
            }

            if (listener is IGameFinishListener finishListener)
            {
                this._finishListeners.Remove(finishListener);
            }

            this._gameLoop.RemoveListener(listener);
        }

        public void StartGame()
        {
            if (this.State != GameState.Boot)
            {
                return;
            }

            this.State = GameState.Countdown;
        }

        public void BeginPlay()
        {
            this.State = GameState.Playing;
            this._gameLoop.Enabled = true;
            Time.timeScale = 1.0f;
            this.Dispatch(this._startListeners, listener => listener.OnStartGame());
        }

        public void PauseGame()
        {
            if (this.State != GameState.Playing)
            {
                return;
            }

            this.State = GameState.Paused;
            this._gameLoop.Enabled = false;
            Time.timeScale = 0.0f;
            this.Dispatch(this._pauseListeners, listener => listener.OnPauseGame());
        }

        public void ResumeGame()
        {
            if (this.State != GameState.Paused)
            {
                return;
            }

            this.State = GameState.Playing;
            this._gameLoop.Enabled = true;
            Time.timeScale = 1.0f;
            this.Dispatch(this._resumeListeners, listener => listener.OnResumeGame());
        }

        public void FinishGame()
        {
            if (this.State != GameState.Playing && this.State != GameState.Paused)
            {
                return;
            }

            this.State = GameState.Finished;
            this._gameLoop.Enabled = false;
            Time.timeScale = 0.0f;
            this.Dispatch(this._finishListeners, listener => listener.OnFinishGame());
        }

        private void Dispatch<T>(List<T> listeners, Action<T> action)
        {
            for (int i = 0, count = listeners.Count; i < count; i++)
            {
                action(listeners[i]);
            }
        }
    }
}
