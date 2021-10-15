public readonly struct Mapping
{
    readonly string from;
    readonly string to;

    public Mapping(string from, string to)
    {
        this.from = from;
        this.to = to;
    }

    public string From => from;

    public string To => to;
}
