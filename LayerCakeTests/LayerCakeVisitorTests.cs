using FluentAssertions;
using LayerCake.DataClasses;
using LayerCakeTests;
using NSubstitute;
using Xunit;

namespace LayerCake.Tests
{
    public class LayerCakeVisitorTests
    {
        readonly SymbolTable layerTable = Substitute.For<SymbolTable>();
        readonly LayerCakeVisitor uat;

        public LayerCakeVisitorTests()
        {
            uat = new LayerCakeVisitor(layerTable);
        }

        [Fact]
        public void VisitToggle_statement_CreatesKeyboardLayerToggle()
        {
            const string Input = @"toggle layer1 with caps_lock";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            uat.Visit(tree);

            //layerTable.Keyboard.Received().AddToggle("caps_lock", "layer1");
        }

        [Fact]
        public void VisitIs_statement_CreatesKeyboardLayerMapping()
        {
            const string Input = @"a is b";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            uat.Visit(tree);

            //layerTable.Keyboard.Received().AddMap("a", "b");
        }

        [Fact]
        public void VisitLayer_statement_CreatesNewLayer()
        {
            const string Input = @"
                navigation layer
                done
            ";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            uat.Visit(tree);

            //layerTable.Received().AddLayer(Arg.Is<ILayer>(layer => string.Equals(layer.Name, "navigation")));
        }

        [Fact]
        public void VisitWhen_block_CreatesNewConstraintLayer()
        {
            const string Input = @"
                when bundle is www.parsec.tv
                done
            ";
            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            uat.Visit(tree);
        }
    }
}
