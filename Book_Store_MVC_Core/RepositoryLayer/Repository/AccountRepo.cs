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
    public class AccountRepo : IAccountRepo
    {
        private SqlConnection connection;
        private readonly IConfiguration configuration;
        string connectionString = null;
        //To Handle connection related activities
        public AccountRepo(IConfiguration configuration)
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
        public bool LoginUser(Login login)
        {
            try
            {
                Connection();
                string password = Encryptdata(login.Password);
                SqlCommand cmd = new SqlCommand("sp_LoginUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", login.Email);
                cmd.Parameters.AddWithValue("@Password", password);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                //While Loop For Reading status result from SqlDataReader.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string Email = reader["Email"].ToString();
                        string Password = reader["Password"].ToString();
                        //int UserId = Convert.ToInt32(reader["UserId"]);

                        //if (UserId != 0)
                        //{
                        //    return UserId;
                        //}
                        return true;
                    }
                }
                connection.Close();
                return false;

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
        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        public Register RegisterUser(Register registrationModel)
        {
            try
            {
                Connection();
                string password = Encryptdata(registrationModel.Password);
                SqlCommand cmd = new SqlCommand("sp_RegisterUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", registrationModel.FirstName);
                cmd.Parameters.AddWithValue("@LastName", registrationModel.LastName);
                cmd.Parameters.AddWithValue("@Email", registrationModel.Email);
                cmd.Parameters.AddWithValue("@Password", password);

                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                if (i <= 1)
                    return registrationModel;
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
