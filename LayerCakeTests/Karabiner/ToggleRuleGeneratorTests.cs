using FluentAssertions;
using LayerCake.Karabiner;
using System.Text.Json;
using Xunit;

namespace LayerCake.Karabiner.Tests
{
    public class ToggleRuleGeneratorTests
    {
        [Fact()]
        public void Generate_CreatesDescription()
        {
            Rule result = ToggleRuleGenerator.Generate("from", "to", "x");
            result.Description.Should().BeEquivalentTo("from: x toggles to");
        }

        [Fact()]
        public void Generate_CreatesToSetVariableCondition()
        {
            Rule result = ToggleRuleGenerator.Generate("from", "to", "x");
            result.Manipulators.Should().BeEquivalentTo(new Manipulator[] {
                new Manipulator
                {
                    To = new ToEventObject[]
                    {
                        new ToEventObject {
                            SetVariable = new SetVariable {Name = "to", Value = 1 }
                        }
                    }
                }
            }, options => options.Including(x => x.To));
        }

        [Fact()]
        public void Generate_CreatesToAfterKeyUpSetVariableCondition()
        {
            Rule result = ToggleRuleGenerator.Generate("from", "to", "x");
            result.Manipulators.Should().BeEquivalentTo(new Manipulator[] {
                new Manipulator
                {
                    ToAfterKeyUp = new ToAfterKeyUpEventObject
                    {
                        SetVariable = new SetVariable {Name = "to", Value = 0 }
                    }
                }
            }, options => options.Including(x => x.ToAfterKeyUp));
        }

        [Fact()]
        public void Generate_CreatesFromModifer()
        {
            Rule result = ToggleRuleGenerator.Generate("from", "to", "x");
            result.Manipulators.Should().BeEquivalentTo(new Manipulator[] {
                new Manipulator
                {
                    From = new From
                    {
                        Modifiers = new Modifiers
                        {
                            Optional = new []{ "any" }
                        }
                    }
                }
            }, options => options.Including(x => x.From.Modifiers));
        }

        [Fact()]
        public void Generate_CreatesFromKeyCode()
        {
            Rule result = ToggleRuleGenerator.Generate("from", "to", "x");
            result.Manipulators.Should().BeEquivalentTo(new Manipulator[] {
                new Manipulator
                {
                    From = new From
                    {
                        KeyCode = "x"
                    }
                }
            }, options => options.Including(x => x.From.KeyCode));
        }

        [Fact()]
        public void Generate_CreatesCondition()
        {
            Rule result = ToggleRuleGenerator.Generate("from", "to", "x", new[] { "y" });
            result.Manipulators.Should().BeEquivalentTo(new Manipulator[] {
                new Manipulator
                {
                    Conditions = new SetVariableCondition[]
                    {
                        new SetVariableCondition{
                            Name= "y",
                            Value = 1
                        }
                    }
                }
            }, options => options.Including(x => x.Conditions));
        }

        [Fact()]
        public void Generate_CreatesmoreThanOneConditions()
        {
            Rule result = ToggleRuleGenerator.Generate("from", "to", "x", new []{ "y", "z"});
            result.Manipulators.Should().BeEquivalentTo(new Manipulator[] {
                new Manipulator
                {
                    Conditions = new SetVariableCondition[]
                    {
                        new SetVariableCondition{
                            Name= "y",
                            Value = 1
                        },
                        new SetVariableCondition{
                            Name= "z",
                            Value = 1
                        }
                    }
                }
            }, options => options.Including(x => x.Conditions));
        }
    }
}