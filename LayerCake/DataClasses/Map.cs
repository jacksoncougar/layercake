using System;
using System.Collections.Generic;

namespace LayerCake.DataClasses
{
    public struct Map : ISymbol
    {
        Id id;
        public readonly Key from;
        public readonly Key to;

        public Map(string from, string to) : this()
        {
            this.from = new Key(from);
            this.to = new Key(to);
            id = new Id { };
        }

        public Id Id { get => id; set => id = value; }

        public SymbolType SymbolType => SymbolType.Map;

        public override string ToString()
        {
            return $"{from}:{to}";
        }

        public override bool Equals(object obj)
        {
            return obj is Map map &&
                   EqualityComparer<Key>.Default.Equals(from, map.from) &&
                   EqualityComparer<Key>.Default.Equals(to, map.to) &&
                   EqualityComparer<Id>.Default.Equals(Id, map.Id) &&
                   SymbolType == map.SymbolType;
        }

        public static bool operator ==(Map left, Map right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Map left, Map right)
        {
            return !(left == right);
        }

        public override int GetHashCode() => HashCode.Combine(id, from, to);
    }
}