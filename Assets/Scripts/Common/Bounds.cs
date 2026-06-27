using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bounds
    {
        private readonly Vector2 _min;
        private readonly Vector2 _max;

        public Bounds(Vector2 min, Vector2 max)
        {
            this._min = min;
            this._max = max;
        }

        public bool InBounds(Vector2 position)
        {
            return position.x > this._min.x
                   && position.x < this._max.x
                   && position.y > this._min.y
                   && position.y < this._max.y;
        }
    }
}
