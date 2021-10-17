namespace LayerCake.DataClasses
{
    public struct Toggle : ISymbol
    {
        Id id;
        public readonly Key key;
        public readonly string to;

        public Toggle(string key, string layer) : this()
        {
            this.key = new Key(key);
            this.to = layer;
            id = new Id { };
        }

        public Id Id { get => id; set => id = value; }
        public SymbolType SymbolType => SymbolType.Toggle;

        public override string ToString()
        {
            return $"{key}:{to}";
        }
    }
}