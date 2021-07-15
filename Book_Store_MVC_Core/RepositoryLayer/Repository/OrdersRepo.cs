using Microsoft.Extensions.Configuration;
using ModelsLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Repository
{
    public class OrdersRepo : IOrdersRepo
    {
        private SqlConnection connection;
        private readonly IConfiguration configuration;
        string connectionString = null;
        //To Handle connection related activities
        public OrdersRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private void Connection()
        {
            try
            {
                connectionString = configuration.GetSection("ConnectionStrings").GetSection("ConnectionString").Value;
                connection = new SqlConnection(connectionString);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public Orders Checkout(Orders orders)
        {
            try
            {
                Connection();
                SqlCommand cmd = new SqlCommand("sp_PlaceOrder", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", orders.UserId);
                cmd.Parameters.AddWithValue("@CartId", orders.CartId);
                //cmd.Parameters.AddWithValue("@OrderId",orders.OrderId,out);
                connection.Open();
                var i = cmd.ExecuteNonQuery();
                connection.Close();
                if (i <=0)
                    return orders;
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }

}
