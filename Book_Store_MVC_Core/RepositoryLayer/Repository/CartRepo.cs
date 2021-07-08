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
    public class CartRepo : ICartRepo
    {
        private SqlConnection connection;
        private readonly IConfiguration configuration;
        string connectionString = null;
        //To Handle connection related activities
        public CartRepo(IConfiguration configuration)
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

        public List<GetCart> GetCart()
        {
            Connection();
            List<GetCart> CartList = new List<GetCart>();
            SqlCommand cmd = new SqlCommand("sp_GetAllCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", 1);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            connection.Open();
            da.Fill(dt);
            //Bind CartModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                CartList.Add(
                    new GetCart
                    {
                        CartId = Convert.ToInt32(dr["CartId"]),
                        BookId = Convert.ToInt32(dr["BookId"]),
                        BookName = Convert.ToString(dr["BookName"]),
                        Author = Convert.ToString(dr["Author"]),
                        Price = Convert.ToInt32(dr["Price"]),
                        Image = Convert.ToString(dr["Image"]),
                        CartBookQuantity = Convert.ToInt32(dr["cartBookQuantity"])
                    }
                    );
            }
            return CartList;
        }
        public Cart AddToCart(Cart cartModel)
        {
            try
            {
                Connection();
                SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", cartModel.BookId);
                cmd.Parameters.AddWithValue("@UserId", cartModel.UserId);
                cmd.Parameters.AddWithValue("@CartBookQuantity", cartModel.CartBookQuantity);
                connection.Open();
                int i = cmd.ExecuteNonQuery();

                if (i >= 1)
                    return cartModel;
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
        public int RemoveFromCart(int cartId)
        {
            try
            {
                Connection();
                SqlCommand cmd = new SqlCommand("sp_RemoveFromCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CartId", cartId);
                // cmd.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                if (i >= 1)
                    return 1;
                else
                    return 0;
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
