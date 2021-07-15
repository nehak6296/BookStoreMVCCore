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
    public class WishListRepo : IWishListRepo
    {
        private SqlConnection connection;
        private readonly IConfiguration configuration;
        string connectionString = null;
        //To Handle connection related activities
        public WishListRepo(IConfiguration configuration)
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
        public List<GetWishList> GetWishList()
        {
            Connection();
            List<GetWishList> wishList = new List<GetWishList>();
            SqlCommand cmd = new SqlCommand("sp_GetAllWishList", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", 1);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            connection.Open();
            da.Fill(dt);

            //Bind WishListModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                wishList.Add(
                    new GetWishList
                    {
                        WishListId = Convert.ToInt32(dr["WishListId"]),
                        WishListQuantity = Convert.ToInt32(dr["WishListQuantity"]),
                        BookId = Convert.ToInt32(dr["BookId"]),
                        BookName = Convert.ToString(dr["BookName"]),
                        Author = Convert.ToString(dr["Author"]),
                        Price = Convert.ToInt32(dr["Price"]),
                        Image = Convert.ToString(dr["Image"])
                    }
                    );
            }
            return wishList;
        }
        public WishList AddToWishList(WishList wishList)
        {
            try
            {
                Connection();
                SqlCommand cmd = new SqlCommand("sp_AddToWishList", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", wishList.BookId);
                cmd.Parameters.AddWithValue("@UserId", wishList.UserId);
                cmd.Parameters.AddWithValue("@Quantity", wishList.WishListQuantity);
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                //connection.Close();
                if (i >= 1)
                    return wishList;
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
        public int RemoveFromWishList(int wishListId)
        {
            try
            {
                Connection();
                SqlCommand cmd = new SqlCommand("sp_RemoveFromWishList", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WishListId", wishListId);
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
