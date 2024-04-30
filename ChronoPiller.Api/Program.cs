var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();
app.MapGet("/prescriptions", () =>
{
    // Here you should return a list of prescriptions from your database
}).WithName("GetPrescriptionsDto");

app.MapPost("/prescriptions", (PrescriptionDto prescriptionDto) =>
{
    // Here you should add the prescription to your database
}).WithName("AddPrescriptionDto");

app.MapPut("/prescriptions/{id}", (int id, PrescriptionDto prescriptionDto) =>
{
    // Here you should update the prescription in your database with the given id
}).WithName("UpdatePrescriptionDto");

app.MapDelete("/prescriptions/{id}", (int id) =>
{
    // Here you should delete the prescription from your database with the given id
}).WithName("DeletePrescriptionDto");

app.MapGet("/users", () =>
{
    // Here you should return a list of users from your database
}).WithName("GetUsersDto");

app.MapPost("/users", (UserDto userDto) =>
{
    // Here you should add the user to your database
}).WithName("AddUserDto");

app.MapPut("/users/{id}", (int id, UserDto userDto) =>
{
    // Here you should update the user in your database with the given id
}).WithName("UpdateUserDto");

app.MapDelete("/users/{id}", (int id) =>
{
    // Here you should delete the user from your database with the given id
}).WithName("DeleteUserDto");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public record PrescriptionDto(int Id, string Name, string DoctorName, DateTime dateOfCreation)