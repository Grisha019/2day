using _2day;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавляем строку подключения к SQL Server
var connectionString = "Server=DESKTOP-VPV9CV8\\SQLEXPRESS;Database=Ado14;Integrated Security=True;TrustServerCertificate=True;";

// Регистрируем DbContext с использованием SQL Server
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(connectionString));

// Регистрируем сервисы для Razor Pages (при необходимости можно добавить и контроллеры)
// builder.Services.AddControllersWithViews(); // Если нужен MVC с контроллерами и представлениями
builder.Services.AddRazorPages();

// Регистрируем Swagger для документирования API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();

// Конфигурация Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Маппинг Razor Pages (а при наличии контроллеров можно добавить и app.MapControllers();)
app.MapRazorPages();

app.Run();
