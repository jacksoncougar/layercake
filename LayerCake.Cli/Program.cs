using Antlr4.Runtime;
using LayerCake.DataClasses;
using LayerCake.Karabiner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LayerCake.Cli
{
    public struct Key
    {
        public readonly int size;
        public readonly char label;

        public Key(int size, char label)
        {
            // 1 = 1
            // 1.25 = 2
            // 2 = 8
            this.size = size;
            this.label = label;
        }
    }

    public class KeyboardDiagramGenerator
    {


        public string Render(IEnumerable<Key> keys)
        {
            int w = keys.Sum(x => x.size);
            return string.Create(SizeOf(w), keys.Select(x => Render(x)), (span, keys) =>
            {


            });
        }

        public string Render(Key key)
        {
            return string.Create(SizeOf(key.size), (key.label, key.size), (span, data) =>
            {
                int w = data.size;
                int stride = w + 4;
                span[0] = '┏';
                span[1] = '━';
                span[2] = '━';
                for (int i = 0; i < w; i++) span[3 + i] = '━';
                span[3 + w] = '┓';

                span[0 + stride] = '┃';
                span[1 + stride] = ' ';
                span[2 + stride] = data.label;
                for (int i = 0; i < w; i++) span[3 + i + stride] = ' ';
                span[3 + w + stride] = '┃';

                span[0 + stride * 2] = '┗';
                span[1 + stride * 2] = '━';
                span[2 + stride * 2] = '━';
                for (int i = 0; i < w; i++) span[3 + i + stride * 2] = '━';
                span[3 + w + stride * 2] = '┛';
            });

        }

        private static int SizeOf(int size)
        {
            return ((4 + size) * 3);
        }
    }


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
