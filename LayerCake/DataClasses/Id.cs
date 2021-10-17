using System;

namespace LayerCake.DataClasses
{
    public struct Id
    {
        public readonly SymbolType type;
        public readonly int index;

        public Id(SymbolType type, int id)
        {
            this.type = type;
            this.index = id;
        }

        public override bool Equals(object obj)
        {
            return obj is Id id &&
                   type == id.type &&
                   this.index == id.index;
        }

        public override string ToString()
        {
            return $"{type}:{index}";
        }

        public static bool operator ==(Id left, Id right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Id left, Id right)
        {
            return !(left == right);
        }

        public static implicit operator bool(Id id)
        {
            return id.type is not SymbolType.Nothing;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(type, index);
        }
    }
}