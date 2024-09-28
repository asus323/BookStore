using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Interfaces;
using BookStore.Model.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookStoreRepository _bookStoreRepository;
        private readonly ILogger<BooksController> _logger;
        public BooksController(
            IBookStoreRepository bookStoreRepository,
            ILogger<BooksController> logger)
        {
            _bookStoreRepository = bookStoreRepository;
            _logger = logger;
        }
        //sql query
        //ORM 
        //Object relational Mapper
        //Ef core  
        //dapper
        //ADO.Net
        //Route: api/books
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllBooks()
        {
            var books =await _bookStoreRepository.GetAllBooks();
            return Ok(books);
        }
        [HttpGet("{enteryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBookDetailsById(int enteryId )
        {
            var book =await _bookStoreRepository.GetBookDetailsById(enteryId);
            if (book == null)
            {
                return NotFound("Your entered book isnt available in the list");
            }
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody]CreatBookDto model)
        {
            var id =await _bookStoreRepository.CreatBook(model);
            return Ok(id);
        }
        [HttpPut("{enteryId}")]
        public async Task<IActionResult> UpdateBook([FromBody]UpdateBookDto model,int enteryId)
        {
            var result =await _bookStoreRepository.UpdateBook(enteryId,model);
            if (!result)
            {
                return BadRequest("This book is not exist");
            }
            return Ok(result);
            
        }
        [HttpDelete("{enteryId}")]
        public async Task<IActionResult> RemoveBook(int enteryId)
        {
            var result =await _bookStoreRepository.RemoveBook(enteryId);
            if (!result)
            {
                return BadRequest("This book is not exist");
            }
            return Ok(result);
            
        }
        
        
        
    }
}
