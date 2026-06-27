using System;
using System.Collections.Generic;

namespace ShootEmUp
{
    public sealed class ObjectPool<T> where T : class
    {
        private readonly Queue<T> _available = new();
        private readonly Func<T> _factory;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onRelease;

        public ObjectPool(Func<T> factory, Action<T> onGet = null, Action<T> onRelease = null)
        {
            this._factory = factory;
            this._onGet = onGet;
            this._onRelease = onRelease;
        }

        public T Get()
        {
            T item = this._available.Count > 0
                ? this._available.Dequeue()
                : this._factory();

            this._onGet?.Invoke(item);
            return item;
        }

        public void Release(T item)
        {
            this._onRelease?.Invoke(item);
            this._available.Enqueue(item);
        }
    }
}
