using Antlr4.Runtime;
using LayerCake;

namespace LayerCakeTests
{
    public class ConfigFactory
    {
        public static LayerCakeParser.ConfigContext CreateConfigContext(string Input)
        {
            AntlrInputStream antlrInputStream = new AntlrInputStream(Input);
            LayerCakeLexer layerCakeLexer = new LayerCakeLexer(antlrInputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(layerCakeLexer);
            LayerCakeParser layerCakeParser = new LayerCakeParser(commonTokenStream);
            LayerCakeParser.ConfigContext tree = layerCakeParser.config();

            return tree;
        }
    }
}
