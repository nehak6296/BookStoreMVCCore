using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interfaces
{
    public interface IOrdersManager
    {
        Orders Checkout(Orders orders);

    }
}
