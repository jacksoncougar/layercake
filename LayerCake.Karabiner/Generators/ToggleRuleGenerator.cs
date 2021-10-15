using System.Linq;
using System.Text.Json;

namespace LayerCake.Karabiner
{
    public class ToggleRuleGenerator
    {
        public static Rule Generate(string fromLayer, string toLayer, string key, params string[] conditions)
        {
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
                            SetVariable = new SetVariable {Name = toLayer, Value = 0 } 
                        },
                        Conditions = conditions.Select(x => new Condition { Name = x, Value = 1 })
                            .ToArray()
                    }
                }
            };
        }
    }
}
