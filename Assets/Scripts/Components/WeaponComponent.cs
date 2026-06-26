using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField]
        [FormerlySerializedAs("firePoint")]
        private Transform _firePoint;

        public Vector2 Position => this._firePoint.position;

        public Quaternion Rotation => this._firePoint.rotation;
    }
}
