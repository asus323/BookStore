using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Model.Book;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public   class BookStoreRepository :IBookStoreRepository
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
        public async Task<BookDetailsDTO> GetBookDetailsById(int enteryId)
        {
            var book = await _context.Books.
                Where(x=>x.Id ==enteryId)
                .Select(
                x=> new BookDetailsDTO()
                {
                    Id=x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Amount = x.Amount
                }
                ).FirstOrDefaultAsync();
            return book;
        }
        public async Task<int> CreatBook(CreatBookDto model)
        {
            var book = new Book()
            {
                Title = model.Title,
                Description = model.Description,
                Amount = model.Amount
            };
            _context.Books.Add(book);
            //age in khatezir ro nanevisim tamame taghirate ma Rollback mishan
            //be in amaliata transactional migan
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task<bool> UpdateBook(int enteryId, UpdateBookDto model)
        {
            var book = await _context.Books.Where(x=>x.Id ==enteryId).FirstOrDefaultAsync();
            if (book != null)
            {
                book.Title = model.Title;
                book.Description = model.Description;
                book.Amount = model.Amount;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveBook(int enteryId)
        {
            var book = await _context.Books.Where(x=>x.Id ==enteryId).FirstOrDefaultAsync();
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
    }
}