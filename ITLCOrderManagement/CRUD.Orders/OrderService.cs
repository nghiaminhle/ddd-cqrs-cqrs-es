using CRUD.Orders.DAO;
using CRUD.Orders.DAO.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Orders
{
    public class OrderService
    {
        private IOrderDAO orderDAO;

        public OrderService()
        {
            this.orderDAO = new OrderDAO();
        }

        public void CreateNewOrder()
        { }

        public void InsertOrderITem()
        { 
        }
    }
}
