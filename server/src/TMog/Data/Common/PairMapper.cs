using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TMog.Data.Common
{
    internal class PairMapper<TFirst, TSecond> : IEnumerable<Pair<TFirst, TSecond>>, IEnumerable
    {
        private readonly Collection<Pair<TFirst, TSecond>> mapper = new Collection<Pair<TFirst, TSecond>>();

        public PairMapper()
        {
        }

        public PairMapper(params Pair<TFirst, TSecond>[] pairs)
        {
            if (pairs == null)
            {
                throw new ArgumentNullException("pairs");
            }
            Array.ForEach<Pair<TFirst, TSecond>>(pairs, delegate (Pair<TFirst, TSecond> pair)
            {
                this.Add(pair.First, pair.Second);
            });
        }

        public PairMapper(IEnumerable<Pair<TFirst, TSecond>> pairs)
        {
            if (pairs == null)
            {
                throw new ArgumentNullException("pairs");
            }
            foreach (Pair<TFirst, TSecond> current in pairs)
            {
                this.Add(current.First, current.Second);
            }
        }

        public IEnumerator<Pair<TFirst, TSecond>> GetEnumerator()
        {
            return this.mapper.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public TSecond Map(TFirst first)
        {
            TSecond result;
            try
            {
                result = (from pair in this.mapper
                          where object.Equals(pair.First, first)
                          select pair.Second).First<TSecond>();
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentOutOfRangeException("first", (first == null) ? "null" : first.ToString());
            }
            return result;
        }

        public TFirst Map(TSecond second)
        {
            TFirst result;
            try
            {
                result = (from pair in this.mapper
                          where object.Equals(pair.Second, second)
                          select pair.First).First<TFirst>();
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentOutOfRangeException("second", (second == null) ? "null" : second.ToString());
            }
            return result;
        }

        public void Add(TFirst first, TSecond second)
        {
            if (this.mapper.Any((Pair<TFirst, TSecond> pair) => object.Equals(pair.First, first)))
            {
                throw new ArgumentException("First value already exists in mapper.", "first");
            }
            if (this.mapper.Any((Pair<TFirst, TSecond> pair) => object.Equals(pair.Second, second)))
            {
                throw new ArgumentException("Second value already exists in mapper.", "second");
            }
            this.mapper.Add(new Pair<TFirst, TSecond>(first, second));
        }

        public void Add(Pair<TFirst, TSecond> pair)
        {
            if (pair == null)
            {
                throw new ArgumentNullException("pair");
            }
            this.Add(pair.First, pair.Second);
        }
    }
}
