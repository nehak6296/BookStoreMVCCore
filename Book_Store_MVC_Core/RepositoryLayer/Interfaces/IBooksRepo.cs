using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBooksRepo
    {
        List<Books> GetAllBooks();
        //bool UploadImage(int BookId, IFormFile image);

    }
}
