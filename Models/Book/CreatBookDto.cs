using System.ComponentModel.DataAnnotations;
using BookStore.Validations;

namespace BookStore.Model.Book
{
    public class CreatBookDto
    {
        //chera id nayad too in file :
        //Chon db khodesh generate mikone niazi nist ke ma id pass bedim
        [Required(ErrorMessage = "لطفا نام کتاب را وارد کنید")]
        [BannedKeyword]
        public string Title { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        [Range(0,50000000)]
        public int Amount { get; set; }
    }
}