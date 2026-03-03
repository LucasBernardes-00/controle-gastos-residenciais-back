using Domain.Contracts.Repositories;
using Infra;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using WebApplication1.Middlewares;
using WebApplication1.UseCases.Categories;
using WebApplication1.UseCases.Persons;
using WebApplication1.UseCases.Report;
using WebApplication1.UseCases.Transactions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => {
    options.AddPolicy("MinhaAppReact", policy => {
        policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddScoped<InPersonRepository, PersonRepository>();
builder.Services.AddScoped<InCategoryRepository, CategoryRepository>();
builder.Services.AddScoped<InTransactionRepository, TransactionRepository>();

builder.Services.AddScoped<SavePersonUseCase>();
builder.Services.AddScoped<GetAllUseCase>();
builder.Services.AddScoped<GetPersonUseCase>();
builder.Services.AddScoped<UpdatePersonUseCase>();
builder.Services.AddScoped<DeletePersonUseCase>();

builder.Services.AddScoped<SaveCategoryUseCase>();
builder.Services.AddScoped<GetAllCategoryUseCase>();
builder.Services.AddScoped<GetCategoryUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<DeleteCategoryUseCase>();

builder.Services.AddScoped<SaveTransactionUseCase>();
builder.Services.AddScoped<GetAllTransactionUseCase>();
builder.Services.AddScoped<GetTransactionUseCase>();
builder.Services.AddScoped<UpdateTransactionUseCase>();
builder.Services.AddScoped<DeleteTransactionUseCase>();

builder.Services.AddScoped<PersonTransactionReportUseCase>();
builder.Services.AddScoped<CategoryTransactionReportUseCase>();

var app = builder.Build();
app.UseCors("MinhaAppReact");

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.Run();
