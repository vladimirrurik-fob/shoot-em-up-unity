using System;

namespace ShootEmUp
{
    public interface IPlayerHealth
    {
        bool IsAlive { get; }

        event Action Died;

        void TakeDamage(int damage);
    }
}
