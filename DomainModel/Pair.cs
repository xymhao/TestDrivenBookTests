using System;

namespace TestDrivenBookTests
{
    public class Pair
    {
        private string from;
        private string to;

        public Pair(string from, string to)
        {
            this.from = from;
            this.to = to;
        }

        public override bool Equals(object obj)
        {
            var pair = obj as Pair;
            return pair.@from.Equals(@from) && pair.to.Equals(to);
        }

        protected bool Equals(Pair other)
        {
            return @from == other.@from && to == other.to;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(@from, to);
        }
    }
}