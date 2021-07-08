using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store_MVC_Core.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksManager booksManager;
        public BooksController(IBooksManager booksManager)
        {
            this.booksManager = booksManager;
        }
        //GET: Books
        //public ActionResult GetAllBooks()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult GetAllBooks()
        {
            try
            {
                var result = this.booksManager.GetAllBooks();
                return View(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //[HttpPost]
        //public IActionResult UploadImage(int BookId, IFormFile image)
        //{
        //    try
        //    {
        //        //var imageUpload = CloudImageLink(image);
        //        bool result = this.booksManager.UploadImage(BookId, image);
        //        if (result == true)
        //        {
        //            return Json(new { status = true, Message = "Image added ", Data = result });
        //        }
        //        else
        //        {
        //            return Json(new { status = false, Message = "image not added " });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

    }

}
