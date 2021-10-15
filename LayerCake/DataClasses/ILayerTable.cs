using System.Collections.Generic;

namespace LayerCake.DataClasses
{
    public interface ILayerTable
    {
        ILayer Keyboard { get; }
        ILayer Nothing { get; }
        IEnumerable<ILayer> Layers { get; }

        void AddLayer(ILayer layer);
        IEnumerable<string> ConditionsOf(ILayer layer);
    }
}