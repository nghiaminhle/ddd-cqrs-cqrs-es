using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CRUD.Orders.DAO.Imp
{
    public class OrderDAO : IOrderDAO
    {
        public IEnumerable<Order> GetListOrder()
        {
            throw new NotImplementedException();
        }

        public void InsertNewOrder( Guid orderId, DateTime createDate, string description )
        {
            throw new NotImplementedException();
        }

        public void ConfirmOrder( Guid orderId )
        {
            throw new NotImplementedException();
        }

        public void CancelOrder( Guid orderId )
        {
            throw new NotImplementedException();
        }


        public void InsertOrderItem( List<OrderItem> items )
        {
            throw new NotImplementedException();
        }
    }
}
