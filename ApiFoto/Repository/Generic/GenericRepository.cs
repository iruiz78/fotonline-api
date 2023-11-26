using ApiFoto.Domain;
using ApiFoto.Infrastructure.Dapper;
using Dapper;
using Loggin;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ApiFoto.Repository.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> 
        where T : Audit, new()
    {
        public readonly DapperContext _context;
        public readonly string _tableName;
        public readonly Log _log;

        public GenericRepository(DapperContext context, IOptions<Log> log)
        {
            _context = context;
            _tableName = GenericRepository<T>.GetTableName();
            _log = log.Value;
        }

        public async Task<T> Get(int id)
        {
            using (var connection = _context.CreateConnectionSQL())
            {
                var result = await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id=@Id", new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");

                return result;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (var connection = _context.CreateConnectionSQL())
            {
                return await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
            }
        }

        public async Task Insert(T t)
        {
            t.CreatedDate = DateTime.Now;
            t.UserCreatedId = 9;
            t.ModifiedDate = DateTime.Now;
            t.UserModifiedId = 9;

            using (var connection = _context.CreateConnectionSQL())
            {
                await connection.QuerySingleOrDefaultAsync<int>(GenerateInsertQuery(), t);
            }
        }

        public async Task Update(T t)
        {
            t.ModifiedDate = DateTime.Now;
            t.UserModifiedId = 9;

            using (var connection = _context.CreateConnectionSQL())
            {
                await connection.ExecuteAsync(GenerateUpdateQuery(), t);
            }
        }

        public async Task<int> SaveRange(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                item.CreatedDate = DateTime.Now;
                item.UserCreatedId = 9;
                item.ModifiedDate = DateTime.Now;
                item.UserModifiedId = 9;
            }

            var inserted = 0;
            using (var connection = _context.CreateConnectionSQL())
            {
                inserted += await connection.ExecuteAsync(GenerateInsertQuery(), list);
            }
            return inserted;
        }

        public async  Task Delete(int id)
        {
            using (var connection = _context.CreateConnectionSQL())
            {
                await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id=@Id", new { Id = id });
            }
        }

        public string GenerateUpdateQuery([Optional] string tableName, [Optional] IEnumerable<PropertyInfo> listOfProperties)
        {
            tableName = tableName == null ? _tableName : tableName;

            var updateQuery = new StringBuilder($"UPDATE {tableName} SET ");
            var properties = GenerateListOfProperties(listOfProperties != null ? listOfProperties : GenericRepository<T>.GetProperties);

            properties.Remove("CreatedDate");
            properties.Remove("UserCreatedId");

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append(" OUTPUT INSERTED.* WHERE Id=@Id");

            return updateQuery.ToString();
        }

        public string GenerateInsertQuery([Optional] string tableName, [Optional] IEnumerable<PropertyInfo> listOfProperties)
        {
            tableName = tableName == null ? _tableName : tableName;
            var insertQuery = new StringBuilder($"INSERT INTO {tableName} ");

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(listOfProperties != null ? listOfProperties : GenericRepository<T>.GetProperties);

            properties.Remove("Id");

            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") OUTPUT INSERTED.* VALUES (");
            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(");");

            return insertQuery.ToString();
        }

        #region Private Methods

        private static string GetTableName()
            => typeof(T).GetCustomAttribute<TableAttribute>().Name;

        private static IEnumerable<PropertyInfo> GetProperties
            => typeof(T).GetProperties();

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }

        #endregion

    }
}
