using System;


namespace Yrr.Utils
{
    public abstract class ReactiveValue<T>
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

        public override bool Equals(object obj)
        {
            return Value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Value);
        }
    }
}
