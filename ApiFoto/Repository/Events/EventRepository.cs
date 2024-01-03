using ApiFoto.Domain.Event;
using ApiFoto.Infrastructure.Dapper;
using ApiFoto.Repository.Generic;
using AutoMapper;
using Dapper;
using Loggin;
using Microsoft.Extensions.Options;

namespace ApiFoto.Repository.Events
{
    public class EventRepository : GenericRepository<Event>
    {
        private readonly IMapper _mapper;
        public EventRepository(DapperContext context, IMapper mapper, IOptions<Log> log) : base(context, log)
        {
            _mapper = mapper;
        }

     
        public async new Task<IEnumerable<EventResponse>> GetAll()
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                return await conn.QueryAsync<EventResponse>(
                    "SELECT Ev.Id, Ev.Name, Ev.Description, Ev.BannerUrl, Ev.Active, " +
                    "Ev.PhotoPrice, Ev.PhotoPricePackage, Ev.PackageQuantity,  " +
                    "(SELECT COUNT(*) FROM Photos Ph WHERE Ph.EventId = Ev.Id) AS PhotosCount " +
                    "FROM Events Ev");
            }
        }

        public async Task<EventResponse?> GetById(int id)
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                return await conn.QueryFirstOrDefaultAsync<EventResponse>("SELECT Ev.Id, Ev.Name, Ev.Description, Ev.BannerUrl, Ev.Active, " +
                    "Ev.PhotoPrice, Ev.PhotoPricePackage, Ev.PackageQuantity, " +
                    "(SELECT COUNT(*) FROM Photos Ph WHERE Ph.EventId = Ev.Id) AS PhotosCount " +
                    "FROM Events Ev WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<int> InsertEvent(EventRequest evnt)
        {
            Event evnMap = _mapper.Map<Event>(evnt);
            evnMap.CreatedDate = DateTime.Now;
            evnMap.UserCreatedId = 1;
            evnMap.ModifiedDate = DateTime.Now;
            evnMap.UserModifiedId = 1;
            using (var conn = _context.CreateConnectionSQL())
            {
                int eventId = await conn.ExecuteScalarAsync<int>(GenerateInsertQuery(), evnMap);
                return eventId;
            }
        }

        public async Task UpdateEvent(EventRequest evnt)
        {
            Event evnMap = _mapper.Map<Event>(evnt);
            evnMap.ModifiedDate = DateTime.Now;
            evnMap.UserModifiedId = 1;
            using (var conn = _context.CreateConnectionSQL())
            {
                await conn.ExecuteAsync(GenerateUpdateQuery(), evnMap);
            }
        }

        public async Task UpdateBannerUrl(int eventId, string bannerUrl)
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                await conn.ExecuteAsync("UPDATE Events SET BannerUrl = @BannerUrl WHERE Id = @Id", new { BannerUrl = bannerUrl, Id = eventId });
            }
        }

        public async Task PublishUnpublish(EventRequest evnt)
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                await conn.ExecuteAsync("UPDATE Events SET Active = @Active, ModifiedDate = @ModifiedDate, UserModifiedId = @UserModifiedId WHERE Id = @Id", new { Active = evnt.Active, Id = evnt.Id, ModifiedDate = DateTime.Now, UserModifiedId = 1 });
            }
        }


        public async new Task Delete(int evntId)
        {
            using (var conn = _context.CreateConnectionSQL())
            {
                await conn.ExecuteAsync("DELETE FROM Events WHERE Id = @Id", new { Id = evntId });
            }
        }
    }
}
