using System.Collections.Generic;

namespace LayerCake.DataClasses
{
    public interface ISymbolList<T> where T : ISymbol
    {
        int Count { get; }

        Id AddSymbol(T symbol);
        IEnumerator<T> GetEnumerator();
        T GetSymbol(Id id);
    }
}