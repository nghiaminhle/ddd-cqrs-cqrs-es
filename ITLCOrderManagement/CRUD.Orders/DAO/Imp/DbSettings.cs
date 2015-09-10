using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CRUD.Orders.DAO.Imp
{
    public class DbSettings
    {
        public static readonly string ConnectionString = "Data Source=(local);Initial Catalog=ITLeaderClub;Integrated Security=True";
    }
}
