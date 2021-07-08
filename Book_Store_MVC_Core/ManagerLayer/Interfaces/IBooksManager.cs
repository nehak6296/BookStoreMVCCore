using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interfaces
{
    public interface IBooksManager
    {
        List<Books> GetAllBooks();
       // bool UploadImage(int BookId, IFormFile image);
    }
}
