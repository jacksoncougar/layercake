using System.Collections;
using System.Collections.Generic;

namespace LayerCake.DataClasses
{
    public interface ILayer
    {
        string Name { get; }

        IEnumerable<Mapping> Maps { get; }
        IEnumerable<Toggle> Toggles { get; }
        IEnumerable<ILayer> Children { get; }

        void AddNestedLayer(ILayer layer);
        void AddToggle(string key, string layer);
        void AddMap(string from, string to);
    }
}