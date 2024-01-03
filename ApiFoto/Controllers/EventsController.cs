using ApiFoto.Domain.Event;
using ApiFoto.Infrastructure.Communication;
using ApiFoto.Services.Events;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiFoto.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _service; 
        public EventsController(IEventService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<GenericResponse<EventResponse>> GetAll()
            => await _service.GetAll();

        [HttpGet("GetById/{id}")]
        public async Task<GenericResponse<EventResponse>> GetById(int id)
            => await _service.GetById(id);

        [HttpPost("Save")]
        public async Task<GenericResponse<EventResponse>> Save(IFormFile eventRequest, IFormFile? file)
        {
            string fileContent = null;
            using (var reader = new StreamReader(eventRequest.OpenReadStream()))
            {
                fileContent = reader.ReadToEnd();
            }
            EventRequest evnt = JsonConvert.DeserializeObject<EventRequest>(fileContent);

            return await _service.Save(evnt, file);
        }

        [HttpPut("PublishUnpublish")]
        public async Task<GenericResponse<EventResponse>> PublishUnpublish(EventRequest evnt)
            => await _service.PublishUnpublish(evnt);

        [HttpDelete("Delete/{id}")]
        public async Task<GenericResponse<EventResponse>> Delete(int id)
            => await _service.Delete(id);

    }
}
