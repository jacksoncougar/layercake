using FluentAssertions;
using LayerCake.DataClasses;
using LayerCakeTests;
using Xunit;

namespace LayerCake.Tests
{
    public class LayerCakeWhereVisitorTests
    {
        readonly LayerCakeWhereVisitor uat;

        public LayerCakeWhereVisitorTests()
        {
            SymbolTable symbolTable = new SymbolTable();
            var currentLayer = symbolTable.AddSymbol(new Layer("Keyboard"));
            uat = new LayerCakeWhereVisitor(symbolTable, currentLayer);
        }

        [Fact()]
        public void VisitLayer_header_SetsLayerName()
        {
            const string Input = @"
                when bundle is www.parsec.tv
                done
            ";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            var layer = uat.Visit(tree);

            layer.Whens[^1].applicationName.Should().Be("www.parsec.tv");
        }

        [Fact()]
        public void VisitIs_statement_CreatesKeyMap()
        {
            const string Input = @"
                when bundle is www.parsec.tv
                  a is b
                done
            ";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            var layer = uat.Visit(tree);

            layer.Maps[^1].Should().BeEquivalentTo(new Map("a", "b"),
                options => options.Excluding(m => m.Id)
                                  .ComparingByMembers<Map>());
        }

        [Fact()]
        public void VisitIs_statement_CreatesMoreThanOneKeyMap()
        {
            const string Input = @"
                when bundle is www.parsec.tv
                  a is b
                  b is a
                done
            ";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            var layer = uat.Visit(tree);

            layer.Maps.Should()
                .BeEquivalentTo(new[] {
                new Map("a", "b"),
                new Map("b", "a") },
                options => options.Excluding(m => m.Id)
                                  .ComparingByMembers<Map>());
        }

        [Fact()]
        public void VisitToggle_statement_CreatesToggle()
        {
            const string Input = @"
                when bundle is www.parsec.tv
                  toggle layer2 with caps_lock
                done
            ";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            var layer = uat.Visit(tree);

            layer.Toggles.Should().ContainEquivalentOf(new Toggle("caps_lock", "layer2"),
                options => options.Excluding(m => m.Id)
                                  .ComparingByMembers<Toggle>());
        }



        [Fact()]
        public void VisitToggle_statement_ReturnsCondition()
        {
            const string Input = @"
                when bundle is www.parsec.tv
                  toggle layer2 with caps_lock
                done
            ";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            var layer = uat.Visit(tree);
            layer.Toggles[^1].to.Should().Be("layer2");
            layer.LinksOf(layer.Toggles[^1].Id, SymbolType.Condition).Should().ContainEquivalentOf(layer.Whens[^1].Id);
        }
    }
}
