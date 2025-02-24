using LoanManagementSystem.Web.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.AddInfrastructureServices();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("LoanManagementSystem API");
        options.WithTheme(ScalarTheme.BluePlanet);
        options.WithSidebar(false);
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseExceptionHandler((_ => { }));

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
