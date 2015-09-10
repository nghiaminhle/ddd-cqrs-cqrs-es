using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Orders.DAO
{
    public interface IOrderDAO
    {
        IEnumerable<Order> GetListOrder();
        void InsertNewOrder( Guid orderId, DateTime createDate, string description );
        void InsertOrderItem( List<OrderItem> items );
        void ConfirmOrder( Guid orderId );
        void CancelOrder( Guid orderId );
    }
}
