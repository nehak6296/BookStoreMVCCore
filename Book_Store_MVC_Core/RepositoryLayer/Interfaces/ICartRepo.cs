using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRepo
    {
        List<GetCart> GetCart();
        Cart AddToCart(Cart cartModel);
        int RemoveFromCart(int cartId);
    }
}
