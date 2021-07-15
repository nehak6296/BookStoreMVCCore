using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRepo
    {
        List<GetWishList> GetWishList();
        WishList AddToWishList(WishList wishList);
        int RemoveFromWishList(int wishListId);
    }
}
