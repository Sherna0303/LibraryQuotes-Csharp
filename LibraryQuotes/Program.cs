using FluentValidation;
using LibraryQuotes.Models.DataBase;
using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Factories;
using LibraryQuotes.Services;
using LibraryQuotes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<Database>(options => options.UseSqlServer(builder.Configuration["SQLConnectionString"]));
builder.Services.AddSingleton<ICopyFactory, CopyFactory>();
builder.Services.AddTransient<IQuotationService, QuotationService>();
builder.Services.AddTransient<IQuoteListService, QuoteListService>();
builder.Services.AddTransient<IBudgetService, BudgetService>();
builder.Services.AddScoped<IDatabase, Database>();
builder.Services.AddScoped<IValidator<ClientDTO>, ClientValidator>();
builder.Services.AddScoped<IValidator<BudgetClientDTO>, BudgetClientValidator>();
builder.Services.AddScoped<IGetCopiesService, GetCopiesService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "LibraryQuotes API",
        Description = "An ASP.NET Core Web API",
        Contact = new OpenApiContact
        {
            Name = "GitHub",
            Url = new Uri("https://github.com/Sherna0303/LibraryQuotes-Csharp")
        },
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
