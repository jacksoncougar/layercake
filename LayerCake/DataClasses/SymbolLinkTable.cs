using System;
using System.Collections.Generic;

namespace LayerCake.DataClasses
{
    public class SymbolLinkTable
    {
        readonly Dictionary<Id, Dictionary<SymbolType, List<Id>>> links = new Dictionary<Id, Dictionary<SymbolType, List<Id>>>();

        public void AddLink(Id from, Id to)
        {
            Add(from, to);
            Add(to, from);
        }

        private void Add(Id from, Id to)
        {
            if (links.ContainsKey(from))
            {
                if (links[from].ContainsKey(to.type))
                {
                    links[from][to.type].Add(to);
                }
                else
                {
                    links[from][to.type] = new List<Id> { to };
                }
            }
            else
            {
                links.Add(from, new Dictionary<SymbolType, List<Id>>() { [to.type] = new List<Id> { to } });
            }
        }

        public IEnumerable<Id> LinksOf(Id id, SymbolType type)
        {
            try
            {
                return links[id][type];
            }
            catch
            {
                return Array.Empty<Id>();
            }
        }
    }
}