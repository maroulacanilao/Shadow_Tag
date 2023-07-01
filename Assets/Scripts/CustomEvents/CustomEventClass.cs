using System;

namespace CustomEvent
{
    public class Evt
    {
        private event Action _evt = delegate { };

        public void Invoke()
        {
            _evt?.Invoke();
        }

        public void AddListener(Action listener)
        {
            _evt -= listener;
            _evt += listener;
        }

        public void RemoveListener(Action listener)
        {
            _evt -= listener;
        }
    }

    public class Evt<T>
    {
        private event Action<T> _evt = delegate { };

        public void Invoke(T param)
        {
            _evt?.Invoke(param);
        }

        public void AddListener(Action<T> listener)
        {
            _evt -= listener;
            _evt += listener;
        }

        public void RemoveListener(Action<T> listener)
        {
            _evt -= listener;
        }
    }

    public class Evt<T, U>
    {
        private event Action<T, U> _evt = delegate { };

        public void Invoke(T param1, U param2)
        {
            _evt?.Invoke(param1, param2);
        }

        public void AddListener(Action<T, U> listener)
        {
            _evt -= listener;
            _evt += listener;
        }

        public void RemoveListener(Action<T, U> listener)
        {
            _evt -= listener;
        }
    }
}