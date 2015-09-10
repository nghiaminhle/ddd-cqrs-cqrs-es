using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infrastructure
{
    public abstract class Repository<T> : IRepository<T> where T : IAggregateRoot
    {

        public T Find( Guid id )
        {
            throw new NotImplementedException();
        }

        public void Save( T aggregate )
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
