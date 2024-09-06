using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookStoreRepository :IBookStoreRepository
    {
        private readonly BookStoreContext _context;
        public BookStoreRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<BookDetailsDTO>> GetAllBooks()
        {
            var books = await _context.Books.Select(
                x=> new BookDetailsDTO()
                {
                    Id=x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Amount = x.Amount
                }
                ).ToListAsync();
            return books;
        }
    }
}