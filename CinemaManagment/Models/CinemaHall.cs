namespace CinemaManagment.Models
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public int HallNr { get; set; }
        public int NrOfSeats { get; set; }
        public bool AnyShows { get; set; } = false;

        //Navigation properties
        public List<Show>? Show { get; set; }
    }
}
