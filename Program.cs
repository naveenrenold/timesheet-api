using TimeSheetAPI.Helper;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
// Enable CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
         policy => policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();
//Configure the HTTP request pipeline.
app.UseCors("AllowReactApp");  // Apply the CORS policy
                               // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "Version One");
        }
    );
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

