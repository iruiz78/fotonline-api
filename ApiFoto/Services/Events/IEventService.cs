using ApiFoto.Domain.Event;
using ApiFoto.Infrastructure.Communication;

namespace ApiFoto.Services.Events
{
    public interface IEventService
    {
        Task<GenericResponse<EventResponse>> GetAll();
        Task<GenericResponse<EventResponse>> Save(EventRequest evnt, IFormFile file);
        Task<GenericResponse<EventResponse>> GetById(int id);
        Task<GenericResponse<EventResponse>> PublishUnpublish(EventRequest evnt);
        Task<GenericResponse<EventResponse>> Delete(int id);
    }
}