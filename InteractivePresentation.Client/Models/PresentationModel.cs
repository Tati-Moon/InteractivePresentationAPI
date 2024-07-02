using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InteractivePresentation.Client.Models
{
    public class PresentationModel
    {
        [JsonPropertyName("current_poll_index")]
        public int CurrentPollIndex { get; set; }
        public List<PollModel> Polls { get; set; }
    }
}
