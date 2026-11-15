using Microsoft.EntityFrameworkCore;
using Orban_Gabriela_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Books");
    options.Conventions.AllowAnonymousToPage("/Books/Index");
    options.Conventions.AllowAnonymousToPage("/Books/Details");
    options.Conventions.AuthorizeFolder("/Members", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Publishers", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Categories", "AdminPolicy");
});
builder.Services.AddDbContext<Orban_Gabriela_Lab2Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Orban_Gabriela_Lab2Context")
        ?? throw new InvalidOperationException("Connection string not found.")));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("LibraryIdentityContext")));
builder.Services.AddDefaultIdentity<Microsoft.AspNetCore.Identity.IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<Microsoft.AspNetCore.Identity.IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser>>();
    foreach (var r in new[] { "Admin", "User" })
        if (!await roleManager.RoleExistsAsync(r))
            await roleManager.CreateAsync(new Microsoft.AspNetCore.Identity.IdentityRole(r));
    var admin = await userManager.FindByEmailAsync("admin@gmail.com");
    if (admin != null && !await userManager.IsInRoleAsync(admin, "Admin"))
    {
        await userManager.RemoveFromRoleAsync(admin, "User");
        await userManager.AddToRoleAsync(admin, "Admin");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.Use(async (ctx, next) =>
{
    // EnsureRole: orice utilizator logat fara rol primeste rolul User
    if (ctx.User.Identity?.IsAuthenticated == true && !ctx.User.IsInRole("Admin") && !ctx.User.IsInRole("User"))
    {
        var um = ctx.RequestServices.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser>>();
        var u = await um.GetUserAsync(ctx.User);
        if (u != null) await um.AddToRoleAsync(u, "User");
    }
    await next();
});
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
