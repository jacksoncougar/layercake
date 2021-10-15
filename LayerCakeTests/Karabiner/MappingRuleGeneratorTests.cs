using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace LayerCake.Karabiner.Tests
{
    public class MappingRuleGeneratorTests
    { 
        [Fact()]
        public void Generate_CreatesDescription()
        {
            Rule result = MappingRuleGenerator.Generate("keyboard", "a", "b");
            result.Description.Should().BeEquivalentTo("keyboard: a is b");
        }

        [Fact()]
        public void MappingRuleGenerator_CreatesFromEvent()
        {
            Rule result = MappingRuleGenerator.Generate("keyboard", "a", "b");
            result.Manipulators.Should().BeEquivalentTo(new Manipulator[] {
                new Manipulator
                {
                    From = new From
                    {
                        KeyCode = "a",
                        Modifiers = new Modifiers
                        {
                            Optional = new []{ "any" }
                        }
                    }
                }
            }, options => options.Including(x => x.From));
        }


        [Fact()]
        public void MappingRuleGenerator_CreatesToEvent()
        {
            Rule result = MappingRuleGenerator.Generate("keyboard", "a", "b");
            result.Manipulators.Should().BeEquivalentTo(new Manipulator[] {
                new Manipulator
                {
                    To = new ToEventObject[]
                    {
                        new ToEventObject {
                               KeyCode = "b" 
                        }
                    }
                }
            }, options => options.Including(x => x.To));
        }

        [Fact()]
        public void MappingNothingRuleGenerator_CreatesEmptyToEvent()
        {
            Rule result = MappingRuleGenerator.Generate("keyboard", "a", "nothing");
            result.Manipulators.Should().BeEquivalentTo(new Manipulator[] {
                new Manipulator
                {
                    To = System.Array.Empty<ToEventObject>()
                }
            }, options => options.Including(x => x.To));
        }

        [Fact()]
        public void MappingRuleGenerator_CreatesVariableIfCondition()
        {
            Rule result = MappingRuleGenerator.Generate("keyboard", "a", "b", "x");
            result.Manipulators.Should().BeEquivalentTo(new Manipulator[] {
                new Manipulator
                {
                    Conditions = new Condition[]
                    {
                        new Condition
                        {
                            Name = "x",
                            Value = 1
                        }
                    }
                }
            }, options => options.Including(x => x.Conditions));
        }

        [Fact()]
        public void MappingRuleGenerator_CreatesMoreThanOneVariableIfConditions()
        {
            Rule result = MappingRuleGenerator.Generate("keyboard", "a", "b", "x", "y");
            result.Manipulators.Should().BeEquivalentTo(new Manipulator[] {
                new Manipulator
                {
                    Conditions = new Condition[]
                    {
                        new Condition
                        {
                            Name = "x",
                            Value = 1
                        },
                        new Condition
                        {
                            Name = "y",
                            Value = 1
                        }
                    }
                }
            }, options => options.Including(x => x.Conditions));
        }
    }
}