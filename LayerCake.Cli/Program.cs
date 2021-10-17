using Antlr4.Runtime;
using LayerCake.DataClasses;
using LayerCake.Karabiner;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LayerCake.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(args[0]);

            AntlrInputStream antlrInputStream = new AntlrInputStream(input);
            LayerCakeLexer layerCakeLexer = new LayerCakeLexer(antlrInputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(layerCakeLexer);
            LayerCakeParser layerCakeParser = new LayerCakeParser(commonTokenStream);
            LayerCakeParser.ConfigContext context = layerCakeParser.config();

            SymbolTable layerTable = new SymbolTable();
            new LayerCakeVisitor(layerTable).Visit(context);

            ComplexModification complexModification = ComplexModificationGenerator.Generate(layerTable);

            Console.Write(JsonSerializer.Serialize(complexModification, typeof(ComplexModification), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            }));
        }
    }
}
