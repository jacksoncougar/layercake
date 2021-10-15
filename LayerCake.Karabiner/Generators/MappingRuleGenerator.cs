using System.Linq;
using System.Text.Json;

namespace LayerCake.Karabiner
{
    public class MappingRuleGenerator
    {
        public static Rule Generate(string layer, string from, string to, params string[] conditions)
        {
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
                        Conditions = conditions.Select(x => new Condition { Name = x, Value = 1 })
                            .ToArray()
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
