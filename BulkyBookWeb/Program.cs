using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Infrastructure.IRepository;
using BulkyBook.DataAccess.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// dependency Injection
{
    builder.Services.AddControllersWithViews();
    
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    
    builder.Services.AddDbContext<DatabaseContext>(options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")
            );
    });
}

var app = builder.Build();
// MiddleWares
{ 
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    // // if not inside [ wwwroot ] Folder 
    //app.UseStaticFiles(new StaticFileOptions()
    //{
    //    FileProvider = new PhysicalFileProvider(
    //        Path.Combine(builder.Environment.ContentRootPath, "Static")),
    //    RequestPath = "/Static"
    //});
    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}