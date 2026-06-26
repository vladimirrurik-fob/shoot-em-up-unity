using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerAttack : MonoBehaviour
    {
        [SerializeField]
        private BulletSystem _bulletSystem;

        [SerializeField]
        private BulletConfig _bulletConfig;

        private IPlayerInput _input;
        private WeaponComponent _weapon;

        private void Awake()
        {
            this._input = this.GetComponent<IPlayerInput>();
            this._weapon = this.GetComponent<WeaponComponent>();
        }

        private void FixedUpdate()
        {
            if (this._input.ConsumeFireRequest())
            {
                this.Fire();
            }
        }

        private void Fire()
        {
            Vector3 position = this._weapon.Position;
            Vector3 velocity = this._weapon.Rotation * Vector3.up * this._bulletConfig.Speed;

            this._bulletSystem.FlyBulletByArgs(new BulletSystem.Args(
                position,
                velocity,
                this._bulletConfig.Color,
                (int)this._bulletConfig.PhysicsLayer,
                this._bulletConfig.Damage,
                Team.Player));
        }
    }
}
