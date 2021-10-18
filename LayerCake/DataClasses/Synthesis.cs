using System.Collections.Generic;
using System.Linq;

namespace LayerCake.DataClasses
{
    public class Synthesis
    {
        public static IEnumerable<(string fromLayer, string key, string toLayer, (string, int)[] conditions, string when)> SynthesizeToggles(SymbolTable table)
        {
            foreach(var toggle in table.Toggles)
            {

                (string,int)[] conditions = null;
                string layerName = "keyboard";
                string whenCondition = null;
                
                Id layerId = table.LinksOf(toggle.Id, SymbolType.Layer).FirstOrDefault();
                if (layerId)
                {
                    Layer layer = table.GetSymbol<Layer>(layerId);
                    conditions = new[] { (layer.name, 1) };
                    layerName = layer.name;
                }

                Id when = table.LinksOf(toggle.Id, SymbolType.Condition).FirstOrDefault();
                if (when)
                {
                    var condition = table.GetSymbol<When>(when);
                    whenCondition = condition.applicationName;
                }
                
                yield return (layerName, toggle.key.ToString(), toggle.to, conditions, whenCondition);
            }
        }

        public static IEnumerable<(string name, string from, string to, (string, int)[] conditions, string when)> SynthesizeMaps(SymbolTable table)
        {
            foreach (var map in table.Maps)
            {

                (string,int)[] conditions = null;
                string layerName = "keyboard";
                string whenCondition = null;

                Id layerId = table.LinksOf(map.Id, SymbolType.Layer).FirstOrDefault();
                if (layerId)
                {
                    Layer layer = table.GetSymbol<Layer>(layerId);
                    conditions = new[] { (layer.name, 1) };
                    layerName = layer.name;
                }

                Id when = table.LinksOf(map.Id, SymbolType.Condition).FirstOrDefault();
                if (when)
                {
                    var condition = table.GetSymbol<When>(when);
                    whenCondition = condition.applicationName;
                }

                yield return (layerName, map.from.ToString(), map.to.ToString(), conditions, whenCondition);
            }
        }
    }
}