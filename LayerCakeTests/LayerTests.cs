using FluentAssertions;
using LayerCake.DataClasses;
using Xunit;

namespace Tests
{
    public class LayerTests
    {
        readonly Layer uat = new Layer("keyboard");

        [Fact()]
        public void LayerDefaultConstruction()
        {
            uat.Name.Should().Be("keyboard");
            uat.Maps.Should().BeEmpty();
            uat.Toggles.Should().BeEmpty();
        }

        [Fact()]
        public void AddMapTest()
        {
            uat.AddMap("from", "to");
            uat.Maps.Should().ContainEquivalentOf(new Mapping("from", "to"));
        }

        [Fact()]
        public void AddToggleTest()
        {
            uat.AddToggle("key", "layer");
            uat.Toggles.Should().ContainEquivalentOf(new Toggle("key", "layer"));
        }
    }
}