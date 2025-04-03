using _2day;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������ �����������
var connectionString = "Server=DESKTOP-VPV9CV8\\SQLEXPRESS;Database=Ado14;Integrated Security=True;TrustServerCertificate=True;";

// ������������ DbContext � �������� ������� �����������
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(connectionString));

/*// ������������ ����������� � ��������������� (���� ����������� Razor Pages ��� MVC)
builder.Services.AddControllersWithViews();*/
builder.Services.AddRazorPages(); // ���������� �������� Razor Pages
// ��������� Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ����������� ������������� �� (Middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // ��������������� HTTP �� HTTPS
app.UseRouting(); // ��������� �������������

app.UseAuthorization(); // �����������

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
*/
app.Run(); // ������ ����������
