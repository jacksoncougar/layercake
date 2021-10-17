using LayerCake.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayerCake.Karabiner
{
    public record ComplexModification(string Desciption, Rule[] Rules);
    public class ComplexModificationGenerator
    {
        public static ComplexModification Generate(SymbolTable data)
        {

            ToggleRuleGenerator toggleRuleGenerator = new ToggleRuleGenerator();

            IEnumerable<Rule> toggleRules = Synthesis.SynthesizeToggles(data).Select(x =>
            {
                return ToggleRuleGenerator.Generate(
                    x.fromLayer,
                    x.toLayer,
                    x.key,
                    x.conditions,
                    x.when);
            });

            MappingRuleGenerator mappingRuleGenerator = new MappingRuleGenerator();

            IEnumerable<Rule> mappingRules = Synthesis.SynthesizeMaps(data).Select(x =>
            {
                return MappingRuleGenerator.Generate(
                    x.name,
                    x.from,
                    x.to,
                    x.conditions,
                    x.when);
            });
            return new ComplexModification("", toggleRules.Concat(mappingRules).ToArray());
        }
    }
}
