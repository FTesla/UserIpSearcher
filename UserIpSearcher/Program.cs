using Microsoft.EntityFrameworkCore;
using UserIpSearcher.DbContexts;
using UserIpSearcher.Extensions;
using UserIpSearcher.DbUtils;

var builder = WebApplication.CreateBuilder(args);

// register the DbContext on the container, getting the
// connection string from appSettings   
builder.Services.AddDbContext<UserIpSearcherDbContext>(o => o.UseSqlServer(builder.Configuration["ConnectionStrings:UserIpSearcherDBConnectionString"]));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterCreateUserIpDataEndpoint();
app.RegisterSearchEndpoints();

//Automatically apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<UserIpSearcherDbContext>();
    await AutoMigrateDatabase.MigrateDatabase(dbContext);
}

app.Run();