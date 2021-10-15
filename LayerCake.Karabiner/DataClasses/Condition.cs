namespace LayerCake.Karabiner
{
    public record Condition
    {
        public string Type => "variable_if";
        public string Name { get; set;  }
        public decimal Value { get; set; }
    }
}
