/*
 * What is DTO?
 * Data Transfer Object
 * Ye zarfe baraye enteghale dadeha be samte karbat
 * asan shayad karbar ye chizi ro nabayad bebine
 */
namespace BookStore.Model
{
    public class BookDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}