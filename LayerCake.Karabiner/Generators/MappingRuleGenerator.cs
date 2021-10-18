
using System;
using System.Linq;
using System.Text.Json;

namespace LayerCake.Karabiner
{
    public class MappingRuleGenerator
    {
        public static Rule Generate(string layer, string from, string to, (string, int)[] conditions = null, string when = null)
        {
            conditions ??= Array.Empty<(string,int)>();

            return new Rule
            {
                Description = $"{layer}: {from} is {to}",
                Manipulators = new Manipulator[] {
                    new Manipulator {
                        From = new From
                        {
                            KeyCode = from,
                            Modifiers = new Modifiers
                            {
                                Optional = new []{ "any" }
                            }
                        },
                        To = GenerateToProperty(to),
                        Conditions = ConditionGenerator.Generate(conditions, when)
                    }
                }
            };
        }

        private static object[] GenerateToProperty(string to)
        {
            if (to is "nothing") return System.Array.Empty<object>();

            return new ToEventObject[]
            {
                new ToEventObject
                {
                    KeyCode = to
                }
            };
        }
    }
}
