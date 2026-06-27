using UnityEngine;

namespace ShootEmUp
{
    public readonly struct BulletArgs
    {
        public readonly Vector2 position;
        public readonly Vector2 velocity;
        public readonly Color color;
        public readonly int physicsLayer;
        public readonly int damage;
        public readonly Team team;

        public BulletArgs(
            Vector2 position,
            Vector2 velocity,
            Color color,
            int physicsLayer,
            int damage,
            Team team)
        {
            this.position = position;
            this.velocity = velocity;
            this.color = color;
            this.physicsLayer = physicsLayer;
            this.damage = damage;
            this.team = team;
        }
    }
}
