using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerAttack : MonoBehaviour
    {
        [SerializeField]
        private BulletConfig _bulletConfig;

        private IBulletLauncher _launcher;
        private IPlayerInput _input;
        private WeaponComponent _weapon;

        public void Construct(IBulletLauncher launcher)
        {
            this._launcher = launcher;
        }

        private void Awake()
        {
            this._input = this.GetComponent<IPlayerInput>();
            this._weapon = this.GetComponent<WeaponComponent>();
        }

        private void FixedUpdate()
        {
            if (this._launcher == null)
            {
                return;
            }

            if (this._input.ConsumeFireRequest())
            {
                this.Fire();
            }
        }

        private void Fire()
        {
            Vector3 position = this._weapon.Position;
            Vector3 velocity = this._weapon.Rotation * Vector3.up * this._bulletConfig.Speed;

            this._launcher.Launch(new BulletArgs(
                position,
                velocity,
                this._bulletConfig.Color,
                (int)this._bulletConfig.PhysicsLayer,
                this._bulletConfig.Damage,
                Team.Player));
        }
    }
}
