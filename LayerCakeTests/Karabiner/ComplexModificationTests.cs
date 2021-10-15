using FluentAssertions;
using LayerCake.DataClasses;
using Xunit;

namespace LayerCake.Karabiner.Tests
{
    public class ComplexModificationTests
    {
        [Fact()]
        public void Generate_GivenDefaultLayerTable_GeneratesEmptyComplexModification()
        {
            ComplexModification result = ComplexModificationGenerator.Generate(new LayerTable());

            result.Should().NotBeNull();
            result.Rules.Should().BeEmpty();
        }

        [Fact()]
        public void Generate_GivenTopLevelToggle_GeneratesToggleRule()
        {
            LayerTable layerTable = new LayerTable();
            layerTable.Keyboard.AddToggle("caps_lock", "layer_one");

            ComplexModification result = ComplexModificationGenerator.Generate(layerTable);

            result.Should().NotBeNull();
            result.Rules.Should().ContainSingle();
        }

        [Fact()]
        public void Generate_GivenTopLevelMapping_GeneratesMappingRule()
        {
            LayerTable layerTable = new LayerTable();
            layerTable.Keyboard.AddMap("a", "b");

            ComplexModification result = ComplexModificationGenerator.Generate(layerTable);

            result.Should().NotBeNull();
            result.Rules.Should().ContainSingle();
        }
    }
}