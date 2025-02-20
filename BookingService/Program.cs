using Models;

List<Booking> bookings = new List<Booking>
{
    new Booking() {Id = 0, Customer = new Customer(0, "Kalle") },
    new Booking() {Id = 1, Customer = new Customer(1, "Kajsa")},
    new Booking() {Id = 2, Customer = new Customer(2, "Knatte")}
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/bookings", () => bookings);

app.MapGet("/bookings/{id}", (int id) => 
{
    var booking = bookings.FirstOrDefault(b => b.Id == id);
    if(booking == null)
    {
        return Results.NotFound("Booking not found.");
    }
    return Results.Ok(booking);
});

app.MapPost("/bookings", (Booking booking) => 
{
    if(booking == null)
    {
        return Results.Problem("Booking object Empty.");
    }
    bookings.Add(booking);
    return Results.Created($"/bookings/{booking.Id}", booking);
});

app.MapPut("/bookings", (int id, Booking updatedBooking) => 
{
    var booking = bookings.FirstOrDefault(b => b.Id == id);
    if (booking is null) return Results.NotFound("Booking not found.");

    booking.Customer = updatedBooking.Customer;
    return Results.Ok(booking);

});

app.MapDelete("bookings/{id}", (int id) =>
{
    var booking = bookings.FirstOrDefault(b => b.Id == id);
    if(booking == null) return Results.NotFound("Booking not found.");
    
    bookings.Remove(booking);
    return Results.Ok($"{booking} is deleted.");
});

app.Run();
