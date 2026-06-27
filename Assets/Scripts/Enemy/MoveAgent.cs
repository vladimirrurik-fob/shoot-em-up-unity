using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveAgent
    {
        public bool IsReached => this._isReached;

        private readonly float _arrivalThreshold;
        private Vector2 _destination;
        private bool _isReached = true;

        public MoveAgent(float arrivalThreshold = 0.25f)
        {
            this._arrivalThreshold = arrivalThreshold;
        }

        public void SetDestination(Vector2 destination)
        {
            this._destination = destination;
            this._isReached = false;
        }

        public Vector2 NextDirection(Vector2 current)
        {
            if (this._isReached)
            {
                return Vector2.zero;
            }

            Vector2 toDestination = this._destination - current;
            if (toDestination.magnitude <= this._arrivalThreshold)
            {
                this._isReached = true;
                return Vector2.zero;
            }

            return toDestination.normalized;
        }
    }
}
