using Gss.Services.Common.Exceptions;

var builder = WebApplication.CreateBuilder(args);
var corsOrigins = "allorigins";
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsOrigins,
        builder =>
        {
            builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
});

builder.Services.AddControllers(
    configure => configure.Filters.Add<CustomJsonExceptionFilter>()
)
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add this to access the http context
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(corsOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
