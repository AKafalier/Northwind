using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models.Context;
using NorthwindAPI.Repositories;
using NorthwindAPI.Services;

/*
 * WebApplication s�n�f�, web uygulaman�z�n ya�am d�ng�s�n� (yap�land�rma, hizmet enjeksiyonu, sunucu ba�latma vb.) y�netir.
 * 
 * CreateBuilder(args): Bu statik y�ntem, bir WebApplicationBuilder nesnesi d�nd�r�r.
 * 
 * Builder yap�land�r�c�s�, uygulaman�n yap�land�rmalar�n�, kullan�lacak hizmetleri, 
 * ortam ayarlar�n� ve di�er ba�lang�� yap�land�rmalar�n� eklemek i�in kullan�l�r. 
 * 
 * args parametresi, komut sat�r�ndan ge�irilen arg�manlar� i�erir ve bu sayede 
 * �rne�in uygulama profilini (Development, Production vb.) ayarlamak i�in kullan�labilir.
*/
var builder = WebApplication.CreateBuilder(args);

/*
 * API Controller'lar tan�mlan�yor. 
 * E�er mvc projesi olsayd�:
 * 
 * builder.Services.AddControllersWithViews() 
 * 
 * tan�mlayacakt�k.
 */
builder.Services.AddControllers();

//Dbcontext tan�ml�yoruz ve sql connection stringimizi appsettings �zerinden �ekiyoruz
builder.Services.AddDbContext<NorthwndContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Scope i�lemlerini buran�n alt�na tan�mla
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