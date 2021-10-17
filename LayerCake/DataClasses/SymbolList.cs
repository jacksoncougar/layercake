using System.Collections;
using System.Collections.Generic;

namespace LayerCake.DataClasses
{
    public class SymbolList<T> : IReadOnlyList<T>, ISymbolList<T> where T : ISymbol
    {
        readonly SymbolType type;

        public SymbolList(SymbolType type)
        {
            this.type = type;
        }

        readonly List<T> layers = new List<T>();

        public T this[int index] => ((IReadOnlyList<T>)layers)[index];

        public int Count => ((IReadOnlyCollection<T>)layers).Count;

        public Id AddSymbol(T symbol)
        {
            symbol.Id = new Id(type, layers.Count);
            layers.Add(symbol);

            return symbol.Id;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)layers).GetEnumerator();
        }

        public T GetSymbol(Id id)
        {
            return layers[id.index];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)layers).GetEnumerator();
        }
    }
}