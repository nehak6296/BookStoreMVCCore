using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interfaces
{
    public interface ICustomerManager
    {
        List<Customer> GetAllCustomerDetails(int userId);
        Customer AddCustomerDetails(Customer customer);

    }
}
