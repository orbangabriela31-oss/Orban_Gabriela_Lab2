using Microsoft.EntityFrameworkCore;
using Orban_Gabriela_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Orban_Gabriela_Lab2Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Orban_Gabriela_Lab2Context")
        ?? throw new InvalidOperationException("Connection string not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
