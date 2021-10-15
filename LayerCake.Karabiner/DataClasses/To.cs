using System.Text.Json.Serialization;

namespace LayerCake.Karabiner
{
    public class ToEventObject {
        [JsonPropertyName("key_code")]
        public string KeyCode { get; set; }

        [JsonPropertyName("set_variable")]
        public SetVariable SetVariable { get; set; }
    }
}
