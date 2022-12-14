namespace CinemaManagment.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public int SeatNr { get; set; }

        //Navigation Properties
        public int ShowId { get; set; }
        public Show? Show { get; set; }
    }
}
