using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Database
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}
