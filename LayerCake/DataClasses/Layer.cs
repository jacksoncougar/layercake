namespace LayerCake.DataClasses
{
    public struct Layer : ISymbol
    {
        Id id;
        public readonly string name;
        public Layer(string name)
        {
            this.name = name;
            id = new Id { };
        }

        public SymbolType SymbolType => SymbolType.Layer;

        public Id Id { get => id; set => id = value;  }

        public override string ToString()
        {
            return $"{id}";
        }
    }
}