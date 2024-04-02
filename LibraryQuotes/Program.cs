using LibraryQuotes.Models.Factories;
using LibraryQuotes.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ICopyFactory, CopyFactory>();
builder.Services.AddTransient<IQuotationService, QuotationService>();
builder.Services.AddTransient<IQuoteListService, QuoteListService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
