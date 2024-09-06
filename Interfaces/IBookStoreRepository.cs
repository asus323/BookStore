using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Model;

namespace BookStore.Interfaces
{
    public interface IBookStoreRepository
    {
        Task<List<BookDetailsDTO>> GetAllBooks();
    }
}