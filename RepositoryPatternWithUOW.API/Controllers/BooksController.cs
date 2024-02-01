using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Core.Repositories;

namespace RepositoryPatternWithUOW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetById")]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Books.GetById(1));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Books.GetAll());
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            return Ok(_unitOfWork.Books.Find(b => b.Title == "NewBook", new[] {"Author"}));
        }

        [HttpGet("GetAllByName")]
        public IActionResult GetAllByName()
        {
            return Ok(_unitOfWork.Books.FindAll(b => b.Title.Contains("NewBook"), new[] { "Author" }));
        }
       
        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered()
        {
            return Ok(_unitOfWork.Books.FindAll(b => b.Title.Contains("NewBook"), null, null, b => b.Id, OrderBy.Descending));
        }

        [HttpPost("AddOne")]
        public IActionResult AddOne()
        {
            var book = _unitOfWork.Books.Add(new Book { Title = "NewBook6", AuthorId = 2 });
            _unitOfWork.Complete();
            return Ok(book);
        }

        [HttpGet("AddRange")]
        public IActionResult AddRange()
        {
            var books = _unitOfWork.Books.AddRange(new Book[] { new Book { Title = "NewBook4", AuthorId = 2 },
                                                             new Book { Title = "NewBook5", AuthorId = 1 } });
            _unitOfWork.Complete();
            return Ok(books);
        }
    }
}
