using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Persistence.Helpers;
using GenericRepository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AppointmentSystemServer.Persistence.Context;

class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IUnitOfWork
{

    #region MODEL
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    #endregion

    #region OVERRIDE
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        IdentityModelBuilderHelper.IgnoreIdentityEntities(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    #endregion

    #region CTOR
    public AppDbContext() { }
    public AppDbContext(DbContextOptions options) : base(options) { }
    #endregion
}