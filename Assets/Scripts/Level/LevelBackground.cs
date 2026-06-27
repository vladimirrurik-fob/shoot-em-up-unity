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

        private BackgroundScroller _scroller;
        private Transform _transform;

        private void Awake()
        {
            this._transform = this.transform;
            Vector3 position = this._transform.position;
            this._scroller = new BackgroundScroller(
                this._params.StartPositionY,
                this._params.EndPositionY,
                this._params.MovingSpeedY,
                position.x,
                position.z);
        }

        private void FixedUpdate()
        {
            this._transform.position = this._scroller.Next(this._transform.position, Time.fixedDeltaTime);
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
