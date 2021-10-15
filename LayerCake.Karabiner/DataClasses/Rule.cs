namespace LayerCake.Karabiner
{
    public record Rule
    {
        public string Description { get; set; }
        public Manipulator[] Manipulators { get; set; }
    }
}
