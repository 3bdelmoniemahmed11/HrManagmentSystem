using AuthenticationWithIdentity.Data;
using AuthenticationWithIdentity.Models;
using AuthenticationWithIdentity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string txt = "hi";
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(txt,
    builder =>
    {
        builder.AllowAnyOrigin();
        //                    builder.WithOrigins('','')
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});
builder.Services.AddDbContext<ApplicationContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());

}); ;

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
      .AddEntityFrameworkStores<ApplicationContext>()
      .AddDefaultTokenProviders();

builder.Services.AddScoped<RoleManager<IdentityRole>>();

builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddAuthorization();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
            };
        });

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Manager", "Member" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    string email = "admin@admin.com";
    string password = "Abc@123";
    /**
     * {email: 'exeeeeamm@wwww.com', password: 'AAAbbb1233??'} -->Member
     */

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new ApplicationUser();
        user.UserName = email;
        user.Email = email;
        user.GroupId = 1;
        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(txt);
app.MapControllers();

app.Run();
