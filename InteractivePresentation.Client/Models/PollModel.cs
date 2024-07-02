using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InteractivePresentation.Client.Models
{
    public class PollModel
    {
        [JsonPropertyName("poll_id")]
        public Guid PollId { get; set; }
        public string Question { get; set; }
        public List<OptionModel> Options { get; set; }
        [JsonPropertyName("presentation_id")]
        public Guid PresentationId { get; set; }
    }
}
