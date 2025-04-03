using _2day;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавляем строку подключения
var connectionString = "Server=DESKTOP-VPV9CV8\\SQLEXPRESS;Database=Ado14;Integrated Security=True;TrustServerCertificate=True;";

// Регистрируем DbContext с заданной строкой подключения
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(connectionString));

/*// Регистрируем контроллеры с представлениями (если используете Razor Pages или MVC)
builder.Services.AddControllersWithViews();*/
builder.Services.AddRazorPages(); // Добавление сервисов Razor Pages
// Добавляем Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Настраиваем промежуточное ПО (Middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Перенаправление HTTP на HTTPS
app.UseRouting(); // Настройка маршрутизации

app.UseAuthorization(); // Авторизация

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
*/
app.Run(); // Запуск приложения
