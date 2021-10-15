using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LayerCake.Karabiner
{
    public record Manipulator
    {

        public string Type => "basic";
        public Condition[] Conditions { get; set; }
        public From From { get; set; }
        public object[] To { get; set; }

        [JsonPropertyName("to_after_key_up")]
        public ToAfterKeyUpEventObject ToAfterKeyUp { get; set; }

    }
}
