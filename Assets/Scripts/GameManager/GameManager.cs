using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerHealth _playerHealth;

        private void OnEnable()
        {
            if (this._playerHealth == null)
            {
                return;
            }

            this._playerHealth.Died += this.OnPlayerDied;
        }

        private void OnDisable()
        {
            if (this._playerHealth == null)
            {
                return;
            }

            this._playerHealth.Died -= this.OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            this.FinishGame();
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0.0f;
        }
    }
}
