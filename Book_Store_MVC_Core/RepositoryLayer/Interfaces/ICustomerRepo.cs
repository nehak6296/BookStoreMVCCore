using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICustomerRepo
    {
        List<Customer> GetAllCustomerDetails(int userId);
        Customer AddCustomerDetails(Customer customer);

    }
}
