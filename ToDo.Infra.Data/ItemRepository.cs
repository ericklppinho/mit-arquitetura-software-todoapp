using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using ToDo.Domain.Entities;
using ToDo.Domain.Interface;
using Dapper;

namespace ToDo.Infra.Data
{

    public class ItemRepository : IItemRepository
    {

        private readonly string connectionString;

        public ItemRepository(IConfiguration  configuration)
        {
                connectionString = configuration.GetConnectionString("ToDoDb");
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            IEnumerable<Item> result;
            var query = "select * from Items";

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    result = await con.QueryAsync<Item>(query);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<Item> GetAsync(Guid id)
        {
            Item result;
            var query = "select * from Items where Id = @Id";

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    result = await con.QueryFirstAsync<Item>(query, new { Id = id });
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task AddAsync(Item item)
        {
            var query = "insert into Items (Id, Description, Done, CreatedAt) values (@Id, @Description, @Done, @CreatedAt)";

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    await con.ExecuteAsync(query, item);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public async Task UpdateAsync(Item item)
        {
            var query = "update Items set Description = @Description, Done = @Done where Id = @Id";

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    await con.ExecuteAsync(query, item);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = "delete from Items where Id = @Id";

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    await con.ExecuteAsync(query, new { Id = id });
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

    }

}

