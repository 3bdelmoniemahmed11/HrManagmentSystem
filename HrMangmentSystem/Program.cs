using HrManagment.DAL.DBContext;
using HrManagmentSystem.Core;
using Microsoft.EntityFrameworkCore;
using HrManagment.DAL.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HrManagmentContext>(option =>
option.UseSqlServer(builder.Configuration["ConnectionStrings:HrManagement"])
);

builder.Services.RegisterServices();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
                policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                }));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NgOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
