namespace CinemaManagment.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int MovieLenght { get; set; }

        //Navigation properties
        public List<Show?>? Show { get; set; }
    }
}
