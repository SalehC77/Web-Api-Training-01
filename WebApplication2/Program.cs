using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using WebApplication2;
using WebApplication2.Authentication;
using WebApplication2.Data;
using WebApplication2.Filters;
using WebApplication2.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("config.json");// this is how to add config Json file to the application

builder.Services.AddLogging(cfg => cfg.AddDebug());// all out put will share in the same console and debeg widow;

builder.Services.Configure<AttachmentOptions>(builder.Configuration.GetSection("Attchaments"));

//var AttchmentOptions = builder.Configuration.GetSection("Attachments").Get<AttachmentOptions>();
//builder.Services.AddSingleton(AttchmentOptions);

//var AttchmentOptions = new AttachmentOptions();
//builder.Configuration.GetSection("Attchaments").Bind(AttchmentOptions);
//builder.Services.AddSingleton(AttchmentOptions);


// Add services to the container.
// the options delegeate adding by me in the code below;
builder.Services.AddControllers( Options =>
    {
        Options.Filters.Add<LogActivityFilter>();// is global Action Filter for every Action in project
       /* Options.Filters.Add<LogSensitiveActionAttribute>();*/// here i can make this fillter global and is also work for conroller or Action just;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>//["ConnectionStrings:DefaultConnection"]
    (cfg => cfg.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic",null);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<RateLimitingMiddleware>();
app.UseMiddleware<ProfilingMiddeleware>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();

//saleh

