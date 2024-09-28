using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Model.Book;

namespace BookStore.Interfaces
{
    public interface IBookStoreRepository
    {
        Task<List<BookDetailsDTO>> GetAllBooks();
        Task<BookDetailsDTO> GetBookDetailsById(int enteryId);
        Task<int> CreatBook(CreatBookDto model);
        Task<bool> UpdateBook(int enteryId, UpdateBookDto model);
        Task<bool> RemoveBook(int enteryId);
    }
}