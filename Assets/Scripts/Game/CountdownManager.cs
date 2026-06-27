using System;
using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CountdownManager : MonoBehaviour
    {
        private GameUI _gameUI;

        public void Construct(GameUI gameUI)
        {
            this._gameUI = gameUI;
        }

        public void StartCountdown(Action onComplete)
        {
            this.StartCoroutine(this.Routine(onComplete));
        }

        private IEnumerator Routine(Action onComplete)
        {
            for (int i = 3; i > 0; i--)
            {
                this._gameUI.SetCountdown(i.ToString());
                yield return new WaitForSeconds(1.0f);
            }

            this._gameUI.SetCountdown("GO!");
            yield return new WaitForSeconds(0.5f);
            this._gameUI.HideCountdown();

            onComplete?.Invoke();
        }
    }
}
