using Models;

var customers = new List<Customer>
{
    new Customer(0, "Kalle"),
    new Customer(1, "Kajsa"),
    new Customer(2, "Knatte"),
    new Customer(3, "Joakim")
    
};
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/customers", () => customers);

app.MapGet("/customers/{id}", (int id) => 
{
    var customer = customers.FirstOrDefault(c => c.Id == id);
    if(customer == null)
    {
        return Results.NotFound("Costumer not found.");
    }
    return Results.Ok(customer);
});

app.MapPost("/customers", (Customer customer) => 
{
    if(customer == null)
    {
        return Results.Problem("Customer object Empty.");
    }
    customers.Add(customer);
    return Results.Created($"/customers/{customer.Id}", customer);
});

app.MapPut("/customers", (int id, Customer updatedCustomer) => 
{
    var customer = customers.FirstOrDefault(c => c.Id == id);
    if (customer is null) return Results.NotFound("Customer not found.");

    customer.Name = updatedCustomer.Name;
    return Results.Ok(customer);

});

app.MapDelete("customers/{id}", (int id) =>
{
    var customer = customers.FirstOrDefault(c => c.Id == id);
    if(customer == null)
    {
        return Results.NotFound("Costumer not found.");
    }
    customers.Remove(customer);
    return Results.Ok($"{customer} is deleted.");
});
    
app.Run();
