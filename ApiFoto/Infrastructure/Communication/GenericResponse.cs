using System.Text.Json.Serialization;

namespace ApiFoto.Infrastructure.Communication
{
    public class GenericResponse<T> where T : class
    {
        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }

        [JsonPropertyName("rowCount")]
        public int RowCount { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("entities")]
        public IEnumerable<T> Entities { get; set; }

        [JsonPropertyName("entity")]
        public T Entity { get; set; }

        public GenericResponse() : this(StatusCodes.Status200OK, string.Empty, Array.Empty<T>()) { }

        public GenericResponse(int statusCode, string message, IEnumerable<T> entities)
        {
            PageCount = 0;
            RowCount = entities.Count();
            StatusCode = statusCode;
            Message = message;
            Entities = entities;
        }

        public GenericResponse(int statusCode, string message, T entity)
        {
            PageCount = 0;
            RowCount = 0;
            StatusCode = statusCode;
            Message = message;
            Entity = entity;
        }

        public GenericResponse(string message) : this(StatusCodes.Status200OK, message, Array.Empty<T>()) { }
        public GenericResponse(string message, int statusCode) : this(statusCode, message, Array.Empty<T>()) { }
        public GenericResponse(string message, IEnumerable<T> entities) : this(StatusCodes.Status200OK, message, entities) { }
        public GenericResponse(string message, T entity) : this(StatusCodes.Status200OK, message, entity) { }
        public GenericResponse(IEnumerable<T> entities) : this(StatusCodes.Status200OK, string.Empty, entities) { }
        public GenericResponse(T entity) : this(StatusCodes.Status200OK, string.Empty, entity) { }

    }
}
