using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemyHealth
    {
        bool IsAlive { get; }

        event Action<GameObject> Died;

        void TakeDamage(int damage);
    }
}
