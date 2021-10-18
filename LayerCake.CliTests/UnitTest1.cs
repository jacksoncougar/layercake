using FluentAssertions;
using LayerCake.Cli;
using System;
using Xunit;

namespace LayerCake.CliTests
{
    public class KeyboardDiagramGeneratorTests
    {
        readonly KeyboardDiagramGenerator uat = new KeyboardDiagramGenerator();

        [Fact]
        public void Render_Size1()
        {
            string result = uat.Render(new Key(1, 'a'));

            result.Should().BeEquivalentTo(
                "┏━━━┓" +
                "┃ a ┃" +
                "┗━━━┛");
        }

        [Fact]
        public void Render_Size2()
        {
            string result = uat.Render(new Key(2, 'a'));

            result.Should().BeEquivalentTo(
                "┏━━━━┓" +
                "┃ a  ┃" +
                "┗━━━━┛");
        }

        [Fact]
        public void Render_Size8()
        {
            string result = uat.Render(new Key(8, 'a'));

            result.Should().BeEquivalentTo(
                "┏━━━━━━━━━━┓" +
                "┃ a        ┃" +
                "┗━━━━━━━━━━┛");
        }
    }
}
