using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interfaces
{
    public interface IWishListManager
    {
        List<GetWishList> GetWishList();
        WishList AddToWishList(WishList wishList);
        int RemoveFromWishList(int WishListId);
    }
}
