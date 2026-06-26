using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Bullets/New BulletConfig")]
    public sealed class BulletConfig : ScriptableObject
    {
        [SerializeField]
        [FormerlySerializedAs("physicsLayer")]
        private PhysicsLayer _physicsLayer;

        [SerializeField]
        [FormerlySerializedAs("color")]
        private Color _color;

        [SerializeField]
        [FormerlySerializedAs("damage")]
        private int _damage;

        [SerializeField]
        [FormerlySerializedAs("speed")]
        private float _speed;

        public PhysicsLayer PhysicsLayer => this._physicsLayer;

        public Color Color => this._color;

        public int Damage => this._damage;

        public float Speed => this._speed;
    }
}
