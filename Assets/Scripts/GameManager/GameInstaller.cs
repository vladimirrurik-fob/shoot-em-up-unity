using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameInstaller : MonoBehaviour
    {
        [SerializeField]
        private int _playerHitPoints = 5;

        [SerializeField]
        private PlayerHealth _playerHealth;

        [SerializeField]
        private PlayerAttack _playerAttack;

        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private GameLoop _gameLoop;

        [SerializeField]
        private CountdownManager _countdownManager;

        [SerializeField]
        private GameUI _gameUI;

        [SerializeField]
        private EnemyManager _enemyManager;

        [SerializeField]
        private EnemyPool _enemyPool;

        [SerializeField]
        private BulletSystem _bulletSystem;

        private void Awake()
        {
            this._gameLoop.enabled = false;

            this._gameManager.Construct(this._gameLoop, this._countdownManager);
            this._countdownManager.Construct(this._gameUI);
            this._gameUI.Construct(this._gameManager);

            IBulletLauncher launcher = this._bulletSystem;
            this._playerAttack.Construct(launcher);
            this._enemyManager.Construct(launcher);
            this._enemyPool.Construct(this._gameManager);

            Health playerHealth = new Health(this._playerHitPoints);
            this._playerHealth.Construct(playerHealth);
            this._playerHealth.Died += this._gameManager.FinishGame;

            foreach (MonoBehaviour listener in this.FindSceneListeners())
            {
                this._gameManager.AddListener(listener);
            }
        }

        private List<MonoBehaviour> FindSceneListeners()
        {
            var result = new List<MonoBehaviour>();

            foreach (GameObject root in this.gameObject.scene.GetRootGameObjects())
            {
                foreach (MonoBehaviour behaviour in root.GetComponentsInChildren<MonoBehaviour>(true))
                {
                    if (behaviour == null || !behaviour.gameObject.activeInHierarchy)
                    {
                        continue;
                    }

                    if (behaviour is IGameStartListener
                        || behaviour is IGamePauseListener
                        || behaviour is IGameResumeListener
                        || behaviour is IGameFinishListener
                        || behaviour is IGameUpdateListener
                        || behaviour is IGameFixedUpdateListener)
                    {
                        result.Add(behaviour);
                    }
                }
            }

            return result;
        }
    }
}
