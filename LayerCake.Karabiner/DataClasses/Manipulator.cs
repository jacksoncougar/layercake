using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LayerCake.Karabiner
{

    public class FrontmostApplicationIf
    {
        public string Type => "frontmost_application_if";
        [JsonPropertyName("bundle_identifiers")]
        public string[] Identifiers { get; set; }
    }

    public record Manipulator
    {

        public string Type => "basic";
        public object[] Conditions { get; set; }
        public From From { get; set; }
        public object[] To { get; set; }

        [JsonPropertyName("to_after_key_up")]
        public ToAfterKeyUpEventObject ToAfterKeyUp { get; set; }

    }
}
