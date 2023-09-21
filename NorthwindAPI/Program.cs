using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models.Context;
using NorthwindAPI.Repositories;
using NorthwindAPI.Services;

/*
 * WebApplication sýnýfý, web uygulamanýzýn yaþam döngüsünü (yapýlandýrma, hizmet enjeksiyonu, sunucu baþlatma vb.) yönetir.
 * 
 * CreateBuilder(args): Bu statik yöntem, bir WebApplicationBuilder nesnesi döndürür.
 * 
 * Builder yapýlandýrýcýsý, uygulamanýn yapýlandýrmalarýný, kullanýlacak hizmetleri, 
 * ortam ayarlarýný ve diðer baþlangýç yapýlandýrmalarýný eklemek için kullanýlýr. 
 * 
 * args parametresi, komut satýrýndan geçirilen argümanlarý içerir ve bu sayede 
 * örneðin uygulama profilini (Development, Production vb.) ayarlamak için kullanýlabilir.
*/
var builder = WebApplication.CreateBuilder(args);

/*
 * API Controller'lar tanýmlanýyor. 
 * Eðer mvc projesi olsaydý:
 * 
 * builder.Services.AddControllersWithViews() 
 * 
 * tanýmlayacaktýk.
 */
builder.Services.AddControllers();

//Dbcontext tanýmlýyoruz ve sql connection stringimizi appsettings üzerinden çekiyoruz
builder.Services.AddDbContext<NorthwndContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Scope iþlemlerini buranýn altýna tanýmla
builder.Services.AddScoped<IProductRepository, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryService>();

//Disturbed Session Configure
builder.Services.AddDistributedMemoryCache();

//Session
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "product_cart";
    options.IdleTimeout = TimeSpan.FromMinutes(5);
});

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", cors =>
    {
        cors.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:7152");
    });
});

//Disturbed Session Configure
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("CORS");
app.MapControllers();
app.UseSession();
app.Run();