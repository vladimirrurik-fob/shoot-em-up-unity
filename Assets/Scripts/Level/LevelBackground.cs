using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField]
        [FormerlySerializedAs("m_params")]
        private Params _params;

        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;
        private Transform _transform;

        private void Awake()
        {
            this._startPositionY = this._params.StartPositionY;
            this._endPositionY = this._params.EndPositionY;
            this._movingSpeedY = this._params.MovingSpeedY;
            this._transform = this.transform;

            Vector3 position = this._transform.position;
            this._positionX = position.x;
            this._positionZ = position.z;
        }

        private void FixedUpdate()
        {
            Vector3 currentPosition = this._transform.position;

            float positionY = currentPosition.y <= this._endPositionY
                ? this._startPositionY
                : currentPosition.y - this._movingSpeedY * Time.fixedDeltaTime;

            this._transform.position = new Vector3(this._positionX, positionY, this._positionZ);
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField]
            [FormerlySerializedAs("m_startPositionY")]
            private float _startPositionY;

            [SerializeField]
            [FormerlySerializedAs("m_endPositionY")]
            private float _endPositionY;

            [SerializeField]
            [FormerlySerializedAs("m_movingSpeedY")]
            private float _movingSpeedY;

            public float StartPositionY => this._startPositionY;

            public float EndPositionY => this._endPositionY;

            public float MovingSpeedY => this._movingSpeedY;
        }
    }
}
