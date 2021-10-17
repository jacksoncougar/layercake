namespace LayerCake.DataClasses
{
    public struct When : ISymbol
    {
        Id id;
        public readonly string applicationName;

        public Id Id { get => id; set => id = value; }

        public SymbolType SymbolType => SymbolType.Condition;

        public When(string name) : this()
        {
            applicationName = name;
            id = new Id { };
        }
    }
}