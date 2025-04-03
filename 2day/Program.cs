using _2day;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������ ����������� � SQL Server
var connectionString = "Server=DESKTOP-VPV9CV8\\SQLEXPRESS;Database=Ado14;Integrated Security=True;TrustServerCertificate=True;";

// ������������ DbContext � �������������� SQL Server
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(connectionString));

// ������������ ������� ��� Razor Pages (��� ������������� ����� �������� � �����������)
// builder.Services.AddControllersWithViews(); // ���� ����� MVC � ������������� � ���������������
builder.Services.AddRazorPages();

// ������������ Swagger ��� ���������������� API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();

// ������������ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ������� Razor Pages (� ��� ������� ������������ ����� �������� � app.MapControllers();)
app.MapRazorPages();

app.Run();
