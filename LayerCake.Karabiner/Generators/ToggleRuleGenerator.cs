using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace LayerCake.Karabiner
{
    public class ConditionGenerator
    { 
        public static object[] Generate((string, int)[] conditions, string when)
        {
            List<object> result = new List<object>();
            result.AddRange(conditions.Select(x => new SetVariableCondition { Name = x.Item1, Value = x.Item2 }));

            if (when is not null)
            {
                result.Add(new FrontmostApplicationIf { Identifiers = new[] { Regex.Escape(when) } });
            }

            return result.ToArray();
        }
    }
    public class ToggleRuleGenerator
    {
        public static Rule Generate(string fromLayer, string toLayer, string key, (string,int)[] conditions = null, string when = null)
        {
            conditions ??= Array.Empty<(string, int)>();
            return new Rule
            {
                Description = $"{fromLayer}: {key} toggles {toLayer}",
                Manipulators = new Manipulator[] {
                    new Manipulator {
                        From = new From
                        {
                            KeyCode = key,
                            Modifiers = new Modifiers
                            {
                                Optional = new []{ "any" }
                            }
                        },
                        To = GenerateToConditions(toLayer, conditions, 1, true),
                        ToAfterKeyUp = GenerateToConditions(toLayer, conditions, 0),
                        Conditions = ConditionGenerator.Generate(conditions, when)
                    }
                }
            };
        }

        private static ToEventObject[] GenerateToConditions(string toLayer, (string, int)[] conditions, int value, bool negate = false)
        {
            List<ToEventObject> results = new List<ToEventObject>();

            results.Add(
                new ToEventObject {
                    SetVariable = new SetVariable { Name = toLayer, Value = value }
                });

            foreach(var condition in conditions)
            {
                results.Add(
                new ToEventObject
                {
                    SetVariable = new SetVariable { 
                        Name = condition.Item1,
                        Value = negate ? (condition.Item2 + 1) % 2 : condition.Item2 
                    }
                });
            }

            return results.ToArray();
        }

    }
}
