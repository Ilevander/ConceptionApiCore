using ConceptionApiCore.Interfaces;
using ConceptionApiCore.Repository;
using Doctors.Data;
using Doctors.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Add the repository and Interface services ==>>> scoped service in your dependency injection container in the Program.cs
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>(); // Replace DoctorRepository with the actual implementation class
builder.Services.AddScoped<IPatientRepository, PatientRepository>();//For Patient
builder.Services.AddScoped<IFeeRepository, FeeRepository>();//This assumes that you have a DataContext class that extends DbContext and contains a DbSet<Fee> property named Fees.
builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();


// builder.Services.AddTransient<Seed>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});




var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();