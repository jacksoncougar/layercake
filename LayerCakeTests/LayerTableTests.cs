using FluentAssertions;
using LayerCake.DataClasses;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class LayerTableTests
    {
        readonly LayerTable uat = new LayerTable();

        [Fact]
        public void Layers_ShouldReturnNothingAndKeyboardLayers()
        {
            uat.Layers.Should().BeEquivalentTo(new List<ILayer>()
            {
                new Layer("Nothing"),
                new Layer("Keyboard")
            }, options => options.ComparingByValue<Layer>());
        }

        [Fact]
        public void Layers_NothingLayerHasNoConstraints()
        {
            uat.ConditionsOf(uat.Nothing).Should().BeEmpty();
        }

        [Fact]
        public void Layers_KeyboardLayerHasNoConstraints()
        {
            uat.ConditionsOf(uat.Keyboard).Should().BeEmpty();
        }

        [Fact]
        public void Layers_FirstLayerHasPreviousLayerConstraint()
        {
            Layer layerOne = new Layer("LayerOne");

            uat.AddLayer(layerOne);

            uat.ConditionsOf(layerOne).Count().Should().Be(1);
            uat.ConditionsOf(layerOne).Last().Should().Be(layerOne.Name);
        }

        [Fact]
        public void Layers_SecondLayerHasAllPreviousLayerConstraints()
        {
            Layer layerOne = new Layer("LayerOne");
            Layer layerTwo = new Layer("LayerTwo");

            uat.AddLayer(layerOne);
            uat.AddLayer(layerTwo);
            IEnumerable<string> constraints = uat.ConditionsOf(layerTwo);

            constraints.Count().Should().Be(2);
            constraints.ElementAt(0).Should().Be(layerOne.Name);
            constraints.ElementAt(1).Should().Be(layerTwo.Name);
        }
    }
}