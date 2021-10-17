namespace LayerCake.DataClasses
{
    public struct Key
    {
        string keyName;

        public Key(string name) : this()
        {
            keyName = name;
        }

        public override string ToString()
        {
            return $"{keyName}";
        }
    }
}