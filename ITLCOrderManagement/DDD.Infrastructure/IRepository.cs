using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infrastructure
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        T Find( Guid id );

        void Save( T aggregate );
    }
}
