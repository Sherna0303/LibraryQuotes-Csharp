using FluentValidation;
using LibraryQuotes.Models.DataBase;
using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.DTOS.Budget;
using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.DTOS.QuoteList;
using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Models.Factories;
using LibraryQuotes.Services;
using LibraryQuotes.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<Database>(options => options.UseSqlServer(builder.Configuration["SQLConnectionString"]));
builder.Services.AddSingleton<ICopyFactory, CopyFactory>();
builder.Services.AddTransient<IQuotationService, QuotationService>();
builder.Services.AddTransient<IQuoteListService, QuoteListService>();
builder.Services.AddTransient<IBudgetService, BudgetService>();
builder.Services.AddScoped<IDatabase, Database>();
builder.Services.AddScoped<IValidator<CopyDTO>, CopyValidator>();
builder.Services.AddScoped<IValidator<ClientListAndAmountDTO>, ClientListAndAmountValidator>();
builder.Services.AddScoped<IValidator<BudgetClientDTO>, BudgetClientValidator>();
builder.Services.AddScoped<IValidator<UserDTO>, UserValidator>();
builder.Services.AddScoped<IValidator<UserRegisterDTO>, UserRegisterValidator>();
builder.Services.AddScoped<IGetCopiesService, GetCopiesService>();
builder.Services.AddScoped <ILoginService, LoginService>();
builder.Services.AddScoped <IRegisterService, RegisterService>();
builder.Services.AddScoped <ICalculateSeniorityService, CalculateSeniorityService>();
builder.Services.AddScoped <ICreateTokenService, CreateTokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
