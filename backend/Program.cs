using Backend_API.data;
using Backend_API.repositories;
using Backend_API.services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5000");

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Connection
builder.Services.AddSingleton(
    new DbConnection(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Repositories
builder.Services.AddScoped<PatientRepository>();
builder.Services.AddScoped<DoctorRepository>();
builder.Services.AddScoped<AppointmentRepository>();
builder.Services.AddScoped<MedicalRecordRepository>();

// Services
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<MedicalRecordService>();

// CORS (IMPORTANT)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Swagger (ONLY IN DEV)
app.UseSwagger();
app.UseSwaggerUI();

// CORS (ONLY ONCE)
app.UseCors("AllowAll");

app.UseAuthorization();

// Controllers
app.MapControllers();

app.Run();