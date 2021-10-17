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
            ComplexModification result = ComplexModificationGenerator.Generate(new SymbolTable());

            result.Should().NotBeNull();
            result.Rules.Should().BeEmpty();
        }

        [Fact()]
        public void Generate_GivenTopLevelToggle_GeneratesToggleRule()
        {
            SymbolTable layerTable = new SymbolTable();
            var layerId = layerTable.AddSymbol(new Layer("Keyboard"));
            var mapId = layerTable.AddSymbol(new Toggle("a", "layer2"));
            layerTable.AddLink(layerId, mapId);

            ComplexModification result = ComplexModificationGenerator.Generate(layerTable);

            result.Should().NotBeNull();
            result.Rules.Should().ContainSingle();
        }

        [Fact()]
        public void Generate_GivenTopLevelMapping_GeneratesMappingRule()
        {
            SymbolTable layerTable = new SymbolTable();
            var layerId = layerTable.AddSymbol(new Layer("Keyboard"));
            var mapId = layerTable.AddSymbol(new Map("a", "b"));
            layerTable.AddLink(layerId, mapId);

            ComplexModification result = ComplexModificationGenerator.Generate(layerTable);

            result.Should().NotBeNull();
            result.Rules.Should().ContainSingle();
        }
    }
}