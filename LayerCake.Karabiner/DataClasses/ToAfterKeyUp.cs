using System.Text.Json.Serialization;

namespace LayerCake.Karabiner
{
    public class ToAfterKeyUpEventObject
    {
        [JsonPropertyName("set_variable")]
        public SetVariable SetVariable { get; set; }
    }
}
