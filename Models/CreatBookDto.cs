namespace BookStore.Model
{
    public class CreatBookDto
    {
        //chera id nayad too in file :
        //Chon db khodesh generate mikone niazi nist ke ma id pass bedim
        public string Title { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}