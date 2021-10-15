using System.Collections.Generic;

namespace LayerCake.DataClasses
{
    public class Layer : ILayer
    {
        readonly string name;
        readonly List<Toggle> toggles = new List<Toggle>();
        readonly List<Mapping> maps = new List<Mapping>();
        readonly List<ILayer> nested = new List<ILayer>();

        public Layer(string name)
        {
            this.name = name;
        }

        public string Name => name;

        public IEnumerable<Mapping> Maps => maps;

        public IEnumerable<Toggle> Toggles => toggles;

        public static ILayer Nothing => new Layer(nameof(Nothing));
        public static ILayer Keyboard => new Layer(nameof(Keyboard));

        public IEnumerable<ILayer> Children => nested;

        public void AddMap(string from, string to)
        {
            maps.Add(new Mapping(from, to));
        }

        public void AddToggle(string key, string layer)
        {
            toggles.Add(new Toggle(key, layer));
        }

        public void AddNestedLayer(ILayer layer)
        {
            nested.Add(layer);
        }
    }
}