using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IOrdersRepo
    {
        Orders Checkout(Orders orders);

    }
}
