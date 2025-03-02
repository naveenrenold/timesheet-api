using TimeSheetAPI.DataLayer;
using TimeSheetAPI.Helper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
// builder.Services.AddSingleton<EmployeeDL>();
// builder.Services.AddSingleton<DatabaseHelper>();
// builder.Services.AddScoped<EmployeeDL>();
// builder.Services.AddSingleton<AttendanceDL>();
// builder.Services.AddScoped<AttendanceDL>();
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var dbHelper = new DatabaseHelper();
// dbHelper.TestConnection(); 
// dbHelper.GetData();
app.MapControllers();
app.Run();

