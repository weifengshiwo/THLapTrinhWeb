using GREEN_STORE.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Thêm các dịch vụ MVC vào ứng dụng
builder.Services.AddControllersWithViews();

// ĐĂNG KÝ REPOSITORIES Ở ĐÂY (Sử dụng Singleton để giữ nguyên dữ liệu mock khi chạy web)
builder.Services.AddSingleton<IProductRepository, MockProductRepository>();
builder.Services.AddSingleton<ICategoryRepository, MockCategoryRepository>();

var app = builder.Build();

// Cấu hình HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Cho phép truy cập file tĩnh trong thư mục wwwroot

app.UseRouting();
app.UseAuthorization();

// Cấu hình định tuyến mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}"); // Chuyển controller mặc định thành Product

app.Run();