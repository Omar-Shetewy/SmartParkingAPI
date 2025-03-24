using System;
using SmartParking.API.Services.Implementation;
using SmartParking.API.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Authentication Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)), // this ! means not null
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<IGarageService, GarageService>();
builder.Services.AddScoped<IAuthService, AuthServicie>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddAutoMapper(typeof(Program));

var configuration = builder.Configuration;
var connectionStrings = new Dictionary<string, string>
{
    //{ "MedoConnection", configuration.GetConnectionString("MedoConnection") },
    { "RokaConnection", configuration.GetConnectionString("RokaConnection") }
    //{ "AmorConnection", configuration.GetConnectionString("AmorConnection") }
};

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("AmorConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MedoConnection")));

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("RokaConnection")));

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
