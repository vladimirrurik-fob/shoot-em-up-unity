using UnityEngine;

namespace ShootEmUp
{
    public interface IPlayerInput
    {
        Vector2 MoveDirection { get; }

        bool ConsumeFireRequest();
    }
}
