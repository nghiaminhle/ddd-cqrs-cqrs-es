using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CRUD.Orders.DAO.Imp
{
    public class ProductDAO : IProductDAO
    {
        public IEnumerable<Product> GetListProducts()
        {
            List<Product> products = new List<Product>();
            using ( SqlConnection con = new SqlConnection( DbSettings.ConnectionString ) )
            {
                con.Open();
                string sqlCommand = "Select * From Product.Products";
                using ( SqlCommand cmd = new SqlCommand( sqlCommand, con ) )
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while ( reader.Read() )
                    {
                        products.Add( new Product()
                        {
                            Id = (int)reader[ "Id" ],
                            Name = (string)reader[ "Name" ],
                            CreateDate = (DateTime)reader[ "CreateDate" ],
                            Price = (Decimal)reader[ "Price" ],
                            StockNumber = (int)reader[ "StockNumber" ]
                        } );
                    }
                }
            }

            return products;
        }
    }
}
