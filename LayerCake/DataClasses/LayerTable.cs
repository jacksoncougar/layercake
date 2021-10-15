using System;
using System.Collections.Generic;
using System.Linq;

namespace LayerCake.DataClasses
{
    public class LayerTable : ILayerTable
    {
        readonly ILayer nothing = Layer.Nothing;
        readonly ILayer keyboard = Layer.Keyboard;
        readonly List<ILayer> layers = new List<ILayer>();

        public ILayer Keyboard => keyboard;

        public ILayer Nothing => nothing;

        public IEnumerable<ILayer> Layers => new ILayer[] { nothing, keyboard }.Concat(layers);

        public void AddLayer(ILayer layer)
        {
            layers.Add(layer);
        }

        public IEnumerable<string> ConditionsOf(ILayer layer)
        {
            if (layer == nothing || layer == keyboard) return Array.Empty<string>();

            int index = layers.IndexOf(layer);
            if (index == -1) return Array.Empty<string>();

            return layers.Take(index + 1).Select(x => x.Name);
        }
    }
}