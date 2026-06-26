using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class LevelBounds : MonoBehaviour
    {
        [SerializeField]
        [FormerlySerializedAs("leftBorder")]
        private Transform _leftBorder;

        [SerializeField]
        [FormerlySerializedAs("rightBorder")]
        private Transform _rightBorder;

        [SerializeField]
        [FormerlySerializedAs("downBorder")]
        private Transform _downBorder;

        [SerializeField]
        [FormerlySerializedAs("topBorder")]
        private Transform _topBorder;

        public bool InBounds(Vector3 position)
        {
            float positionX = position.x;
            float positionY = position.y;
            return positionX > this._leftBorder.position.x
                   && positionX < this._rightBorder.position.x
                   && positionY > this._downBorder.position.y
                   && positionY < this._topBorder.position.y;
        }
    }
}
