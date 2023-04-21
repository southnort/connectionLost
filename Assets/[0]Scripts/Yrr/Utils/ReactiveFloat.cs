using System;


namespace Yrr.Utils
{
    public sealed class ReactiveFloat : ReactiveValue<float>
    {
        public ReactiveFloat(float startValue) : base(startValue) { }

        #region base operators
        public static ReactiveFloat operator +(ReactiveFloat a, ReactiveFloat b)
        {
            a.Value += b.Value;
            return a;
        }

        public static ReactiveFloat operator -(ReactiveFloat a, ReactiveFloat b)
        {
            a.Value -= b.Value;
            return a;
        }

        public static ReactiveFloat operator *(ReactiveFloat a, ReactiveFloat b)
        {
            a.Value *= b.Value;
            return a;
        }

        public static ReactiveFloat operator /(ReactiveFloat a, ReactiveFloat b)
        {
            a.Value /= b.Value;
            return a;
        }

        public static ReactiveFloat operator ++(ReactiveFloat a)
        {
            a.Value++;
            return a;
        }

        public static ReactiveFloat operator --(ReactiveFloat a)
        {
            a.Value--;
            return a;
        }


        public static bool operator ==(ReactiveFloat a, ReactiveFloat b)
            => a.Value == b.Value;


        public static bool operator !=(ReactiveFloat a, ReactiveFloat b)
            => a.Value != b.Value;


        public static bool operator <(ReactiveFloat a, ReactiveFloat b)
            => a.Value < b.Value;

        public static bool operator >(ReactiveFloat a, ReactiveFloat b)
            => a.Value > b.Value;

        public static bool operator <=(ReactiveFloat a, ReactiveFloat b)
           => a.Value <= b.Value;

        public static bool operator >=(ReactiveFloat a, ReactiveFloat b)
            => a.Value >= b.Value;


        #endregion


        #region float operators
        public static ReactiveFloat operator +(ReactiveFloat a, float b)
        {
            a.Value += b;
            return a;
        }

        public static ReactiveFloat operator -(ReactiveFloat a, float b)
        {
            a.Value -= b;
            return a;
        }

        public static ReactiveFloat operator *(ReactiveFloat a, float b)
        {
            a.Value *= b;
            return a;
        }

        public static ReactiveFloat operator /(ReactiveFloat a, float b)
        {
            a.Value /= b;
            return a;
        }


        public static bool operator ==(ReactiveFloat a, float b)
            => a.Value == b;


        public static bool operator !=(ReactiveFloat a, float b)
            => a.Value != b;

        public static bool operator <(ReactiveFloat a, float b)
           => a.Value < b;

        public static bool operator >(ReactiveFloat a, float b)
            => a.Value > b;

        public static bool operator <=(ReactiveFloat a, float b)
           => a.Value <= b;

        public static bool operator >=(ReactiveFloat a, float b)
            => a.Value >= b;

        #endregion


        #region int operators
        public static ReactiveFloat operator +(ReactiveFloat a, int b)
        {
            a.Value += b;
            return a;
        }

        public static ReactiveFloat operator -(ReactiveFloat a, int b)
        {
            a.Value -= b;
            return a;
        }

        public static ReactiveFloat operator *(ReactiveFloat a, int b)
        {
            a.Value *= b;
            return a;
        }

        public static ReactiveFloat operator /(ReactiveFloat a, int b)
        {
            a.Value /= b;
            return a;
        }


        public static bool operator ==(ReactiveFloat a, int b)
            => a.Value == b;


        public static bool operator !=(ReactiveFloat a, int b)
            => a.Value != b;

        public static bool operator <(ReactiveFloat a, int b)
         => a.Value < b;

        public static bool operator >(ReactiveFloat a, int b)
            => a.Value > b;

        public static bool operator <=(ReactiveFloat a, int b)
           => a.Value <= b;

        public static bool operator >=(ReactiveFloat a, int b)
            => a.Value >= b;

        #endregion


        public override bool Equals(object obj)
        {
            return obj is ReactiveFloat @float &&
                   Value == @float.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Value);
        }
    }
}