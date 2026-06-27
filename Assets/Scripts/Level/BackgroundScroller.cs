using UnityEngine;

namespace ShootEmUp
{
    public sealed class BackgroundScroller
    {
        private readonly float _startY;
        private readonly float _endY;
        private readonly float _speed;
        private readonly float _x;
        private readonly float _z;

        public BackgroundScroller(float startY, float endY, float speed, float x, float z)
        {
            this._startY = startY;
            this._endY = endY;
            this._speed = speed;
            this._x = x;
            this._z = z;
        }

        public Vector3 Next(Vector3 current, float deltaTime)
        {
            float y = current.y <= this._endY
                ? this._startY
                : current.y - this._speed * deltaTime;

            return new Vector3(this._x, y, this._z);
        }
    }
}
