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
        private EnemyManager _enemyManager;

        [SerializeField]
        private BulletSystem _bulletSystem;

        private void Awake()
        {
            IBulletLauncher launcher = this._bulletSystem;

            this._playerAttack.Construct(launcher);
            this._enemyManager.Construct(launcher);

            Health playerHealth = new Health(this._playerHitPoints);
            this._playerHealth.Construct(playerHealth);
            this._playerHealth.Died += this._gameManager.FinishGame;
        }
    }
}
