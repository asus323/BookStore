using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAllBooks()
        {
            var books =await _bookStoreRepository.GetAllBooks();
            return Ok(books);
        }
    }
}
