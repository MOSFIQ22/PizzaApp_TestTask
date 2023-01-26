

using TestTask.Server.Repositories.Interfaces;
using TestTask.Shared.Models;

namespace TestTask.Server.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        PizzaAppDbContext db;
        public UnitOfWork(PizzaAppDbContext db)
        {
            this.db = db;
        }
        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.db.Dispose();
        }

        public IGenericRepo<T> GetRepo<T>() where T : class, new()
        {
            return new GenericRepo<T>(this.db);
        }
    }
}
