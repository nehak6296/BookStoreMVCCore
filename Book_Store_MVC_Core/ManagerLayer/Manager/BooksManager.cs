using ManagerLayer.Interfaces;
using ModelsLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Manager
{
    public class BooksManager : IBooksManager
    {
        private readonly IBooksRepo bookRepository;

        public BooksManager(IBooksRepo bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public List<Books> GetAllBooks()
        {
            return this.bookRepository.GetAllBooks();
        }

        //public bool UploadImage(int BookId, IFormFile image)
        //{
        //    return this.bookRepository.UploadImage(BookId, image);
        //}
    }
}
