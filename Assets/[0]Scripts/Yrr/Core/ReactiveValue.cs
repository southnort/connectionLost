using System;


namespace Yrr.Core
{
    public sealed class ReactiveValue<T>
    {
        private T _currentValue;
        public event Action<T> OnChange;

        public ReactiveValue(T startValue)
        {
            _currentValue = startValue;
        }

        public T Value
        {
            get => _currentValue;
            set
            {
                if (value.Equals(_currentValue)) return;

                _currentValue = value;
                OnChange?.Invoke(_currentValue);
            }
        }
    }
}
