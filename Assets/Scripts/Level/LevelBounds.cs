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

        private Bounds _bounds;

        private void Awake()
        {
            Vector2 min = new Vector2(this._leftBorder.position.x, this._downBorder.position.y);
            Vector2 max = new Vector2(this._rightBorder.position.x, this._topBorder.position.y);
            this._bounds = new Bounds(min, max);
        }

        public bool InBounds(Vector3 position)
        {
            return this._bounds.InBounds(position);
        }
    }
}
