var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config => 
{
    config.DocumentName = "Minimal Api";
    config.Title = "Minimal Api v1";
    config.Version = "v1";
});

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config => 
    {
        config.DocumentTitle = "Minimal Api";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

var httpClient = new HttpClient();

app.MapGet("/gateway/bookings", async () => 
{
    return await httpClient.GetStringAsync("http://localhost:5075/bookings");
});

app.MapGet("/gateway/customers", async () => 
{
    return await httpClient.GetStringAsync("http://localhost:5228/customers");
});

app.Run();