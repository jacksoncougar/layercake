using FluentAssertions;
using LayerCake.DataClasses;
using LayerCakeTests;
using Xunit;

namespace LayerCake.Tests
{
    public class LayerCakeLayerVisitorTests
    {
        readonly LayerCakeLayerVisitor uat;

        public LayerCakeLayerVisitorTests()
        {
            uat = new LayerCakeLayerVisitor(new SymbolTable());
        }

        [Fact()]
        public void VisitLayer_header_SetsLayerName()
        {
            const string Input = @"
                navigation layer
                done
            ";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            var symbolTable = uat.Visit(tree);

            symbolTable.Layers.Should().ContainEquivalentOf(new Layer(name: "navigation"),
                options => options.Excluding(m => m.Id)
                                  .ComparingByMembers<Layer>());
        }

        [Fact()]
        public void VisitIs_statement_CreatesKeyMap()
        {
            const string Input = @"
                navigation layer
                  a is b
                done
            ";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            var symbolTable = uat.Visit(tree);

            symbolTable.Maps.Should().ContainEquivalentOf(
                new Map("a", "b"), 
                options => options.Excluding(m => m.Id)
                                  .ComparingByMembers<Map>());
        }

        [Fact()]
        public void VisitIs_statement_CreatesMoreThanOneKeyMap()
        {
            const string Input = @"
                navigation layer
                  a is b
                  b is a
                done
            ";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            var symbolTable = uat.Visit(tree);

            symbolTable.Maps.Should()
                .BeEquivalentTo(new[] {
                new Map(from: "a", to: "b"),
                new Map(from: "b", to: "a") },
                options => options.Excluding(m => m.Id)
                                  .ComparingByMembers<Map>());
        }

        [Fact()]
        public void VisitToggle_statement_CreatesToggle()
        {
            const string Input = @"
                navigation layer
                  toggle layer2 with caps_lock
                done
            ";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            var symbolTable = uat.Visit(tree);

            symbolTable.Toggles.Should().ContainEquivalentOf(new Toggle(key: "caps_lock", layer: "layer2"),
                options => options.Excluding(m => m.Id)
                                  .ComparingByMembers<Toggle>());
        }
    }
}
