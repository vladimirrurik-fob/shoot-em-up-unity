using UnityEngine;

namespace ShootEmUp
{
    public interface IGameLoop
    {
        bool Enabled { get; set; }

        void AddListener(MonoBehaviour listener);

        void AddListener(GameObject gameObject);

        void RemoveListener(MonoBehaviour listener);

        void RemoveListener(GameObject gameObject);
    }
}
