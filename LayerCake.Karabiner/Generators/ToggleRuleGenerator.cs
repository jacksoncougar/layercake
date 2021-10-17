using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LayerCake.Karabiner
{
    public class ConditionGenerator
    { 
        public static object[] Generate(string[] conditions, string when)
        {
            List<object> result = new List<object>();
            result.AddRange(conditions.Select(x => new SetVariableCondition { Name = x, Value = 1 }));

            if (when is not null)
            {
                result.Add(new FrontmostApplicationIf { Identifiers = new[] { when } });
            }

            return result.ToArray();
        }
    }
    public class ToggleRuleGenerator
    {
        public static Rule Generate(string fromLayer, string toLayer, string key, string[] conditions = null, string when = null)
        {
            conditions ??= Array.Empty<string>();
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
                        To = new ToEventObject[]
                        {
                           new ToEventObject {
                               SetVariable = new SetVariable { Name = toLayer, Value = 1 }
                               }
                        },
                        ToAfterKeyUp = new ToAfterKeyUpEventObject
                        {
                            SetVariable = new SetVariable { Name = toLayer, Value = 0 }
                        },
                        Conditions = ConditionGenerator.Generate(conditions, when)
                    }
                }
            };
        }

        
    }
}
