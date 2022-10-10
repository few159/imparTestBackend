using Microsoft.EntityFrameworkCore;
using imparTest.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<imparTestContext>(opt =>
    opt.UseSqlServer("Data Source=sql-felipefernandes.database.windows.net; Initial Catalog=SQLDB-FelipeFernandes; User Id=adm; Password=VRsASQTVz2xBHdRzWMTY8I5H5ZwXdbng")
    );
//opt.UseInMemoryDatabase("imparTestDb"));
//builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors("corsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
