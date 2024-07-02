using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InteractivePresentation.Client.Client;
using InteractivePresentation.Client.Client.Abstract;
using InteractivePresentation.Client.Models;
using InteractivePresentation.Client.Service.Abstract;
using Microsoft.Extensions.Options;

namespace InteractivePresentation.Client.Service
{
    public class PresentationClientService(IApiClient apiClient, IOptions<ApplicationSettings> settings)
        : IPresentationClientService
    {
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>()
        {
            { "Content-Type", "application/json" }
        };
        private readonly ApplicationSettings _apiSettings = settings.Value;

        public async Task<PresentationResponse> GetAsync(Guid presentationId)
        {
            if (presentationId == Guid.Empty)
            {
                throw new ArgumentNullException("PresentationId is Null");
            }

            var apiClientRequest = new ApiClientRequest<string>
            {
                Headers = _headers,
                Path = $"{_apiSettings.ClientUrl}/presentations/{presentationId}",
            };

            var apiClientResponse = await apiClient.GetAsync<PresentationResponse, string>(apiClientRequest);
            return apiClientResponse?.Data;
        }

        public async Task<PresentationResponse> PostAsync(PresentationRequest request)
        {
            var apiClientRequest = new ApiClientRequest<PresentationRequest>
            {
                Path = $"{_apiSettings.ClientUrl}/presentations",
                Query = request
            };

            var apiClientResponse = await apiClient.PostAsync<PresentationResponse, PresentationRequest>(apiClientRequest);

            var presentationId = apiClientResponse?.Data?.PresentationId;
            if (presentationId != Guid.Empty && presentationId != null)
            { 
                return await GetAsync(presentationId.Value);
            }

            return null;
        }
    }
}
