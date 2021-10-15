using FluentAssertions;
using LayerCake.DataClasses;
using LayerCakeTests;
using NSubstitute;
using System.Linq;
using Xunit;

namespace LayerCake.Tests
{
    public class GrammarIntegrationTests
    {
        readonly ILayerTable layerTable = new LayerTable();
        readonly LayerCakeVisitor uat;

        public GrammarIntegrationTests()
        {
            uat = new LayerCakeVisitor(layerTable);
        }

        [Fact]
        public void VisitToggle_statement_CreatesKeyboardLayerToggle()
        {
            const string Input = @"
                navigation layer
                    a is b
                    when bundle is www.parsec.tv
                      a is b
                      b is a
                    done
                done
            ";

            LayerCakeParser.ConfigContext tree = ConfigFactory.CreateConfigContext(Input);

            uat.Visit(tree);

            layerTable.Layers.ElementAt(2).Children.Should().ContainSingle();
            layerTable.Layers.ElementAt(2).Children.ElementAt(0).Name.Should().Be("www.parsec.tv");
        }
    }
}
