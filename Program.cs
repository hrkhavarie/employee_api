using DemoWebAPI.Data;
using DemoWebAPI.Services.Designation;
using DemoWebAPI.Services.Employee;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
});

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDesignationService, DesignationService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PublicPolicy", builder =>
    {
        builder
            .AllowAnyOrigin()  // Allow any origin in development
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable detailed error messages in production temporarily for debugging
app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoWebAPI_UNDP V1.0");
    c.RoutePrefix = string.Empty;
});

// Use CORS before other middleware
app.UseCors("PublicPolicy");

app.UseHttpsRedirection();
app.UseStaticFiles(); // Enable serving static files

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

// Ensure database is created and migrations are applied
try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
        dbContext.Database.Migrate();
    }
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while migrating the database.");
}

// Add fallback route for Angular app
app.MapFallbackToFile("index.html");

app.Run();
