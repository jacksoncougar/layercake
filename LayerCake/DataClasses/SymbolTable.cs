using System;
using System.Collections.Generic;
using System.Linq;

namespace LayerCake.DataClasses
{
    public class SymbolTable
    {
        readonly SymbolList<Layer> layers = new SymbolList<Layer>(SymbolType.Layer);
        readonly SymbolList<Map> maps = new SymbolList<Map>(SymbolType.Map);
        readonly SymbolList<Toggle> toggles = new SymbolList<Toggle>(SymbolType.Toggle);
        readonly SymbolList<When> whens = new SymbolList<When>(SymbolType.Condition);

        readonly SymbolLinkTable links = new SymbolLinkTable();

        public IReadOnlyList<Layer> Layers { get => layers; }
        public IReadOnlyList<Map> Maps { get => maps; }
        public IReadOnlyList<Toggle> Toggles { get => toggles;  }
        public IReadOnlyList<When> Whens { get => whens;  }

        public Id AddSymbol<T>(T symbol) where T : ISymbol
        {
            switch (symbol)
            {
                case Layer layer:
                    return layers.AddSymbol(layer);

                case Map map:
                    return maps.AddSymbol(map);

                case Toggle toggle:
                    return toggles.AddSymbol(toggle);

                case When condition:
                    return whens.AddSymbol(condition);

                default: throw new ArgumentException("Bad type given", nameof(symbol));
            }
        }

        public IEnumerable<Layer> LayersOf(Id id)
        {
            if (id.type != SymbolType.Layer) throw new ArgumentException("Bad layer Id", nameof(id));
            return layers.Take(id.index + 1);
        }

        public IEnumerable<Id> LinksOf(Id id, SymbolType type)
        {
            return links.LinksOf(id, type);
        }

        public void AddLink(Id from, Id to)
        {
            links.AddLink(from, to);
        }
        public T GetSymbol<T>(Id id) where T : struct, ISymbol
        {
            switch (id.type)
            {
                case SymbolType.Map:
                    return (T)(ISymbol)maps.GetSymbol(id);
                case SymbolType.Toggle:
                    return (T)(ISymbol)toggles.GetSymbol(id);
                case SymbolType.Condition:
                    return (T)(ISymbol)whens.GetSymbol(id);
                case SymbolType.Layer:
                    return (T)(ISymbol)layers.GetSymbol(id);
                default: throw new ArgumentException("Bad Id given", nameof(id));
            }
        }
    }
}