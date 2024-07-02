using System;
using System.Text.Json.Serialization;

namespace InteractivePresentation.Client.Models
{
    public class PresentationResponse: PresentationModel
    {
        [JsonPropertyName("presentation_id")]
        public Guid PresentationId { get; set; }
    }
}
