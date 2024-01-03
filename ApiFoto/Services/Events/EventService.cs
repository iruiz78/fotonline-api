using ApiFoto.Domain.Event;
using ApiFoto.Infrastructure.Communication;
using ApiFoto.Infrastructure.Communication.Exceptions;
using ApiFoto.Infrastructure.ULogged;
using ApiFoto.Repository.Events;
using ApiFoto.Repository.Generic;

namespace ApiFoto.Services.Events
{
    public class EventService : IEventService
    {
        private readonly EventRepository _repository;
        private readonly IUserLogged _userLogged;
        public EventService(IGenericRepository<Event> repository, IUserLogged userLogged)
        {
            _repository = (EventRepository)repository;
            _userLogged = userLogged;
        }

        public async Task<GenericResponse<EventResponse>> GetAll()
            => new GenericResponse<EventResponse>(await _repository.GetAll());

        public async Task<GenericResponse<EventResponse>> GetById(int id)
        {
            EventResponse? evnt = await _repository.GetById(id);
            return evnt is null ? throw new AppException("Evento no encontrado.") : new GenericResponse<EventResponse>(evnt);
        }

        public async Task<GenericResponse<EventResponse>> Save(EventRequest evnt, IFormFile file)
        {
            if(evnt.Id == 0)
            {
                await Validate(evnt);
                int evntId = await _repository.InsertEvent(evnt);
                // Ahora si vino una foto, se tiene que actualizar la BannerUrl del evento
                await VerifyNewPhotoAndUpload(file, evntId);
                return new GenericResponse<EventResponse>("Evento creado correctamente.");
            }
            else
            {
                await Validate(evnt);
                await _repository.UpdateEvent(evnt);
                await VerifyNewPhotoAndUpload(file, evnt.Id);
                return new GenericResponse<EventResponse>("Evento actualizado correctamente.");
            }
        }

        public async Task<GenericResponse<EventResponse>> PublishUnpublish(EventRequest evnt)
        {
            await _repository.PublishUnpublish(evnt);
            return new GenericResponse<EventResponse>("Evento actualizado correctamente.");
        }

        public async Task<GenericResponse<EventResponse>> Delete(int id)
        {
            // Aca esto tiene que eliminar la foto de portada, miniaturas, redimencionadas, originales, los registros de la tabla Photos y el evento en si.

            // Eliminar evento de la BD
            await _repository.Delete(id);
            return new GenericResponse<EventResponse>("Evento eliminado correctamente.");
        }

        #region Private Methods
        private async Task Validate(EventRequest evnt)
        {
            if (string.IsNullOrWhiteSpace(evnt.Name)) throw new AppException("Debe indicar nombre de evento.");
            if (evnt.PhotoPrice <= 0) throw new AppException("Precio de foto incorrecto.");
            if (evnt.PhotoPricePackage < 0) throw new AppException("Precio de foto por cantidad incorrecto.");
            if (evnt.PhotoPricePackage == 0 && evnt.PackageQuantity != 0) throw new AppException("Precio de foto por paquete es obligatorio.");
            if (evnt.PhotoPricePackage > 0 && evnt.PackageQuantity <= 0) throw new AppException("Cantidad de fotos por paquete debe ser mayor o cero.");
            if (evnt.PhotoPricePackage < 0 && evnt.PhotoPricePackage <= evnt.PhotoPrice) throw new AppException(" Precio de foto por cantidad no puede ser menor o igual que Precio de foto.");
        }

        private async Task VerifyNewPhotoAndUpload(IFormFile file, int evntId)
        {
            if (file is not null) // Ver despues como ves si el IFormFile tiene algo mas
            {
                string bannerUrl = await UploadBannerPhotoS3(file, evntId);
                await _repository.UpdateBannerUrl(evntId, bannerUrl);
            }
        }

        private async Task<string> UploadBannerPhotoS3(IFormFile bannerPhoto, int eventId)
        {
            // Logica para subir la foto por evento id al s3, retornando la url de la foto
            string[] palabras = { "https://fotonline-prueba-s3.s3.amazonaws.com/1.jpg",
                "https://fotonline-prueba-s3.s3.amazonaws.com/2.jpg",
                "https://fotonline-prueba-s3.s3.amazonaws.com/3.jpg",
                "https://fotonline-prueba-s3.s3.amazonaws.com/4.jpg",
                "https://fotonline-prueba-s3.s3.amazonaws.com/5.png" };
            Random random = new();
            return palabras[random.Next(palabras.Length)];
        }
        #endregion





    }
}
