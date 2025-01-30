using DemoWebAPI.Data;
using DemoWebAPI.Services.Designation;
using DemoWebAPI.Services.Employee;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoWebAPI_UNDP V1.0");
        c.RoutePrefix = string.Empty;
    });
}

// Use CORS before other middleware
app.UseCors("PublicPolicy");

app.UseHttpsRedirection();
app.UseStaticFiles(); // Enable serving static files

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

// Add fallback route for Angular app
app.MapFallbackToFile("index.html");

app.Run();
