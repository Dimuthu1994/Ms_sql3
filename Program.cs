var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors((options)=>
{
    options.AddPolicy("DevCores",(corsBuilder) =>
    {
        corsBuilder.WithOrigins("http://localhost:4200","http://localhost:3000","http://localhost:8000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });

    options.AddPolicy("ProdCores",(corsBuilder) =>
    {
        corsBuilder.WithOrigins("https://myProductionSite.com")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCores");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("ProdCores");
    app.UseHttpsRedirection();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
