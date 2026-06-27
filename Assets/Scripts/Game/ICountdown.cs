using System;

namespace ShootEmUp
{
    public interface ICountdown
    {
        void Begin(Action onComplete, Action<string> onDisplay);
    }
}
