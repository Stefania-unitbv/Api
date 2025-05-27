using Microsoft.EntityFrameworkCore;
using api_tema1.Database.Context;
using api_tema1.Database.Repositories;
using api_tema1.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// Database context
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Services
builder.Services.AddScoped<IBookService, BookService>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Adaugã middleware-ul de tratare a erorilor
app.UseMiddleware<api_tema1.GlobalExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();