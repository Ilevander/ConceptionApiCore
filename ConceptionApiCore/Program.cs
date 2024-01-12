using ConceptionApiCore.Interfaces;
using ConceptionApiCore.Repository;
using Doctors.Data;
using Doctors.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using System.Text.Json.Serialization;
using ConceptionApiCore.Helper;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Other service registrations...

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddScoped<IDoctorRepository, DoctorRepository>(); // Replace DoctorRepository with the actual implementation class
builder.Services.AddScoped<IPatientRepository, PatientRepository>();//For Patient
builder.Services.AddScoped<IFeeRepository, FeeRepository>();//This assumes that you have a DataContext class that extends DbContext and contains a DbSet<Fee> property named Fees.
builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

// Register AutoMapper and the mapping profiles
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies().Concat(new[] { typeof(MappingProfile).Assembly,  /* Add other profile assemblies if needed */ }));
builder.Services.AddDbContext<DataContext>(options =>
{
    // Retrieve the connection string
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    // Check if the connection string is not null before using it
    if (connectionString != null)
    {
        options.UseSqlServer(connectionString);
    }
    else
    {
        // Log or handle the case where the connection string is null
        Console.Error.WriteLine("Connection string is null.");
    }
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

