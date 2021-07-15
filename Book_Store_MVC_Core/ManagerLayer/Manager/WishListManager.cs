using ManagerLayer.Interfaces;
using ModelsLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Manager
{
    public class WishListManager : IWishListManager
    {
        private readonly IWishListRepo wishListRepo;
        public WishListManager(IWishListRepo wishListRepo)
        {
            this.wishListRepo = wishListRepo;
        }

        public List<GetWishList> GetWishList()
        {
            return this.wishListRepo.GetWishList();
        }
        public WishList AddToWishList(WishList wishList)
        {
            return this.wishListRepo.AddToWishList(wishList);
        }
        public int RemoveFromWishList(int wishListId)
        {
            return this.wishListRepo.RemoveFromWishList(wishListId);

        }

    }
}
