using System.Text.Json.Serialization;

namespace LayerCake.Karabiner
{
    public record From
    {
        [JsonPropertyName("key_code")]
        public string KeyCode { get; set; } = "";
        public Modifiers Modifiers { get; set; }
    }

    public record Modifiers
    {
        public string[] Mandatory { get; set; } = System.Array.Empty<string>();
        public string[] Optional { get; set; } = System.Array.Empty<string>();
    }
}
