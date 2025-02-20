namespace Models
{
    public class Booking
    {
         public int Id { get; set; }
         public required Customer Customer { get; set; }
    }
}