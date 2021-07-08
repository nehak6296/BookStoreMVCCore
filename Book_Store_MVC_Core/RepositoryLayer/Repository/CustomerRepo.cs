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
    public class CustomerRepo : ICustomerRepo
    {
        private SqlConnection connection;
        private readonly IConfiguration configuration;
        string connectionString = null;
        //To Handle connection related activities
        public CustomerRepo(IConfiguration configuration)
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
        public List<Customer> GetAllCustomerDetails(int userId)
        {
            try
            {
                Connection();
                List<Customer> customerList = new List<Customer>();
                SqlCommand cmd = new SqlCommand("sp_GetAllCustomerDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                connection.Open();
                da.Fill(dt);
                connection.Close();
                //Bind CustomerModel generic list using dataRow     
                foreach (DataRow dr in dt.Rows)
                {
                    customerList.Add(
                        new Customer
                        {
                            // CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            // UserId = Convert.ToInt32(dr["UserId"]),
                            Name = Convert.ToString(dr["Name"]),
                            Address = Convert.ToString(dr["Address"]),
                            Locality = Convert.ToString(dr["Locality"]),
                            Landmark = Convert.ToString(dr["Landmark"]),
                            Pincode = Convert.ToInt32(dr["Pincode"]),
                            PhoneNumber = Convert.ToString(dr["PhoneNumber"]),
                            City = Convert.ToString(dr["City"]),
                            Type = Convert.ToString(dr["Type"])
                        }
                        );
                }
                return customerList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Customer AddCustomerDetails(Customer customer)
        {
            try
            {
                Connection();
                SqlCommand cmd = new SqlCommand("sp_AddCustomerDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", 1);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@City", customer.City);
                cmd.Parameters.AddWithValue("@Locality", customer.Locality);
                cmd.Parameters.AddWithValue("@Landmark", customer.Landmark);
                cmd.Parameters.AddWithValue("@Pincode", customer.Pincode);
                cmd.Parameters.AddWithValue("@Type", customer.Type);
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                if (i >= 1)
                    return customer;
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
