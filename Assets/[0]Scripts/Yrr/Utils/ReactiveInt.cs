using System;


namespace Yrr.Utils
{
    public sealed class ReactiveInt : ReactiveValue<int>
    {
        public ReactiveInt(int startValue) : base(startValue) { }

        #region base operators
        public static ReactiveInt operator +(ReactiveInt a, ReactiveInt b)
        {
            a.Value += b.Value;
            return a;
        }

        public static ReactiveInt operator -(ReactiveInt a, ReactiveInt b)
        {
            a.Value -= b.Value;
            return a;
        }

        public static ReactiveInt operator *(ReactiveInt a, ReactiveInt b)
        {
            a.Value *= b.Value;
            return a;
        }

        public static ReactiveInt operator /(ReactiveInt a, ReactiveInt b)
        {
            a.Value /= b.Value;
            return a;
        }

        public static ReactiveInt operator ++(ReactiveInt a)
        {
            a.Value++;
            return a;
        }

        public static ReactiveInt operator --(ReactiveInt a)
        {
            a.Value--;
            return a;
        }


        public static bool operator ==(ReactiveInt a, ReactiveInt b)
            => a.Value == b.Value;


        public static bool operator !=(ReactiveInt a, ReactiveInt b)
            => a.Value != b.Value;


        public static bool operator <(ReactiveInt a, ReactiveInt b)
            => a.Value < b.Value;

        public static bool operator >(ReactiveInt a, ReactiveInt b)
            => a.Value > b.Value;

        public static bool operator <=(ReactiveInt a, ReactiveInt b)
           => a.Value <= b.Value;

        public static bool operator >=(ReactiveInt a, ReactiveInt b)
            => a.Value >= b.Value;

        #endregion


        #region int operators
        public static ReactiveInt operator +(ReactiveInt a, int b)
        {
            a.Value += b;
            return a;
        }

        public static ReactiveInt operator -(ReactiveInt a, int b)
        {
            a.Value -= b;
            return a;
        }

        public static ReactiveInt operator *(ReactiveInt a, int b)
        {
            a.Value *= b;
            return a;
        }

        public static ReactiveInt operator /(ReactiveInt a, int b)
        {
            a.Value /= b;
            return a;
        }


        public static bool operator ==(ReactiveInt a, int b)
            => a.Value == b;


        public static bool operator !=(ReactiveInt a, int b)
            => a.Value != b;

        public static bool operator <(ReactiveInt a, int b)
         => a.Value < b;

        public static bool operator >(ReactiveInt a, int b)
            => a.Value > b;

        public static bool operator <=(ReactiveInt a, int b)
           => a.Value <= b;

        public static bool operator >=(ReactiveInt a, int b)
            => a.Value >= b;

        #endregion


        public override bool Equals(object obj)
        {
            return obj is ReactiveInt @float &&
                   Value == @float.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Value);
        }
    }

}
