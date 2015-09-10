using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Orders.DAO
{
    public interface IProductDAO
    {
        IEnumerable<Product> GetListProducts();
    }
}
