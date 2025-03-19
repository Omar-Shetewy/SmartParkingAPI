using SmartParking.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICarService, CarService>();

builder.Services.AddAutoMapper(typeof(Program));

var configuration = builder.Configuration;
var connectionStrings = new Dictionary<string, string>
{
    { "MedoConnection", configuration.GetConnectionString("MedoConnection") },
    { "RokaConnection", configuration.GetConnectionString("RokaConnection") },
    { "AmorConnection", configuration.GetConnectionString("AmorConnection") }
};

string? activeConnection = connectionStrings.FirstOrDefault(c => IsDatabaseAvailable(c.Value)).Value;

if (activeConnection != null)
{
    Console.WriteLine($"? Using active connection: {activeConnection}");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(activeConnection));
}

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



static bool IsDatabaseAvailable(string connectionString)
{
    try
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            return true;
        }
    }
    catch
    {
        return false;
    }
}