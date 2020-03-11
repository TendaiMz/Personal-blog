using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service.Domain.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public List<T> collection = new List<T>();
        public Task Delete(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> Get(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return Task.Run(() => collection.AsEnumerable());
        }

        public Task Save(T entity)
        {
            return Task.Run(() => collection.Add(entity));
        }

        public Task Update(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
