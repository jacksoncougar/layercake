using FluentAssertions;
using LayerCake.DataClasses;
using LayerCakeTests;
using Xunit;

namespace LayerCake.Tests
{
    public class LayerCakeLayerVisitorTests
    {
        readonly LayerCakeLayerVisitor<Layer> uat;

        public LayerCakeLayerVisitorTests()
        {
            uat = new LayerCakeLayerVisitor<Layer>();
        }

        [Fact()]
        public void VisitLayer_header_SetsLayerName()
        {
            const string Input = @"
                navigation layer
                done
            ";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            var layer = uat.Visit(tree);

            layer.Name.Should().Be("navigation");
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

            var layer = uat.Visit(tree);

            layer.Maps.Should().Contain(new Mapping("a", "b"));
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

            var layer = uat.Visit(tree);

            layer.Maps.Should()
                .ContainInOrder(
                new Mapping("a", "b"),
                new Mapping("b", "a"));
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

            var layer = uat.Visit(tree);

            layer.Toggles.Should().Contain(new Toggle("caps_lock", "layer2"));
        }
    }
}
