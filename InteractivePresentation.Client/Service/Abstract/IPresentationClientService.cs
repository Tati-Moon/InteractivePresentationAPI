using System;
using System.Threading.Tasks;
using InteractivePresentation.Client.Models;

namespace InteractivePresentation.Client.Service.Abstract
{
    public interface IPresentationClientService
    {
        Task<PresentationResponse> GetAsync(Guid presentationId);
        Task<PresentationResponse> PostAsync(PresentationRequest request);
    }
}
