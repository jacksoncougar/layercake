namespace LayerCake.DataClasses
{    public interface ISymbol
    {
        SymbolType SymbolType { get; }
        Id Id { get; set; }
    }
}