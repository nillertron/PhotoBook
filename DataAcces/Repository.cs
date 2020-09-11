using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;
namespace DataAcces
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected Context dbContext;
        protected DbSet<T> table;
        public Repository(Context dbContext)
        {
            this.dbContext = dbContext;
            
        }
        public async Task Create(T model)
        {
            await dbContext.AddAsync(model);
            await dbContext.SaveChangesAsync();
        }
        public async Task Edit(T model)
        {
            dbContext.Update(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(T model)
        {
            dbContext.Remove<T>(model);
            await dbContext.SaveChangesAsync();
        }
        public async Task<T> Get(int id)
        {
            var item = await dbContext.FindAsync<T>(id);
            return item;
        }
        public async Task<List<T>> GetAll()
        {
            var list = await table.ToListAsync();
            return list;
        }
    }
}
