using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SOLOLOGIN.Data;
using SOLOLOGIN.Repositories;

var builder = WebApplication.CreateBuilder(args);
//--------------------6----------------------
//HABILITAMOS SESSION DENTRO DE NUESTRO SERVIDOR
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
//--------------------2----------------------

//HABILITAMOS LA SEGURIDAD EN SERVICIOS
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();



//------------------------------------------


// Add services to the container.
builder.Services.AddControllersWithViews();

//-------------------1-----------------------






string connectionString =
    builder.Configuration.GetConnectionString("SqlHospital");
builder.Services.AddTransient<RepositoryEmpleados>();
builder.Services.AddDbContext<EmpleadosContext>
    (options => options.UseSqlServer(connectionString));

//--------------------3----------------------

//PERSONALIZAMOS NUESTRAS RUTAS
builder.Services.AddControllersWithViews
    (options => options.EnableEndpointRouting = false).AddSessionStateTempDataProvider();

//------------------------------------------










var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//--------------------4----------------------
app.UseAuthentication();
app.UseAuthorization();
//--------------------5----------------------
app.UseSession();
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});



app.Run();
