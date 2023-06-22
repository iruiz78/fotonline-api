using ApiFoto.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFoto.Infrastructure.Auth.Domain
{
    [Table("Auth")] // ToDo: Esto quedo para que no se rompa el GenericRepository
    public class Auth : Audit
    {
    }
}
