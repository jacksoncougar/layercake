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
            uat = new LayerCakeWhereVisitor();
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

            layer.Name.Should().Be("www.parsec.tv");
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

            layer.Maps.Should().Contain(new Mapping("a", "b"));
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
                .ContainInOrder(
                new Mapping("a", "b"),
                new Mapping("b", "a"));
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

            layer.Toggles.Should().Contain(new Toggle("caps_lock", "layer2"));
        }
    }
}
