using Core.Entities;
using Core.Interfaces;
using Infrastructure.Dbcontext;
using System.Collections;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public Hashtable Repos { get; set; }
        public ApplicationDbContext AppContext { get; }
        public UnitOfWork(ApplicationDbContext appContext)
        {
            AppContext = appContext;
        }


        public Task<int> Complete()
        => AppContext.SaveChangesAsync();

        public void Dispose()
        =>AppContext.Dispose();

        public IGenericRepo<T> GetRepo<T>() where T : BaseEntity
        {
            if(Repos == null)
                Repos = new Hashtable();

            var GenericType = typeof(T).Name;

            if(!Repos.ContainsKey(GenericType))
            {
                var repoType = typeof(GenericRepo<>);
                var repo = Activator.CreateInstance(repoType.MakeGenericType(typeof(T)),AppContext);
                Repos.Add(GenericType, repo);
            }

            return (IGenericRepo<T>) Repos[GenericType];
        }
    }
}
