using LayerCake.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayerCake.Karabiner
{
    public record ComplexModification(string Desciption, Rule[] Rules);
    public class ComplexModificationGenerator
    {
        public static ComplexModification Generate(ILayerTable data)
        {
            ToggleRuleGenerator toggleRuleGenerator = new ToggleRuleGenerator();

            IEnumerable<Rule> toggleRules = data.Layers.SelectMany(x =>
            {
                return x.Toggles.Select(toggle => ToggleRuleGenerator.Generate(
                    x.Name,
                    toggle.ToLayer,
                    toggle.Key,
                    data.ConditionsOf(x).ToArray()));
            });

            MappingRuleGenerator mappingRuleGenerator = new MappingRuleGenerator();

            IEnumerable<Rule> mappingRules = data.Layers.SelectMany(x =>
            {
                return x.Maps.Select(map => MappingRuleGenerator.Generate(
                    x.Name,
                    map.From,
                    map.To,
                    data.ConditionsOf(x).ToArray()));
            });

            return new ComplexModification("", toggleRules.Concat(mappingRules).ToArray());
        }
    }
}
