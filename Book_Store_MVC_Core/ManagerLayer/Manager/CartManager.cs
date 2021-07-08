using ManagerLayer.Interfaces;
using ModelsLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Manager
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepo cartRepo;
        public CartManager(ICartRepo cartRepo)
        {
            this.cartRepo = cartRepo;
        }

        public List<GetCart> GetCart()
        {
            return this.cartRepo.GetCart();
        }
        public Cart AddToCart(Cart cartModel)
        {
            return this.cartRepo.AddToCart(cartModel);
        }
        public int RemoveFromCart(int cartId)
        {
            return this.cartRepo.RemoveFromCart(cartId);
        }

    }

}
