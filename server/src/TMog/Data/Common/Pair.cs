using System;

namespace TMog.Data.Common
{
    internal class Pair<TFirst, TSecond> : IEquatable<Pair<TFirst, TSecond>>
    {
        private readonly TFirst first;

        private readonly TSecond second;

        public TFirst First
        {
            get
            {
                return this.first;
            }
        }

        public TSecond Second
        {
            get
            {
                return this.second;
            }
        }

        public Pair(TFirst first, TSecond second)
        {
            this.first = first;
            this.second = second;
        }

        public bool Equals(Pair<TFirst, TSecond> other)
        {
            return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || (object.Equals(other.first, this.first) && object.Equals(other.second, this.second)));
        }

        public override bool Equals(object obj)
        {
            return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != typeof(Pair<TFirst, TSecond>)) && this.Equals((Pair<TFirst, TSecond>)obj)));
        }

        public override int GetHashCode()
        {
            return (first.GetHashCode() * 397) ^ second.GetHashCode();
        }
    }
}
