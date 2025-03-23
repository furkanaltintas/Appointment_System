using AppointmentSystemServer.Domain.Constants;
using AppointmentSystemServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentSystemServer.Persistence.Configurations;

class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.Property(d => d.FirstName).HasMaxLength(LengthConstants.MaxLenght50);
        builder.Property(d => d.LastName).HasMaxLength(LengthConstants.MaxLenght50);

        builder
            .HasOne(d => d.Department)
            .WithMany(d => d.Doctors)
            .HasForeignKey(d => d.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Doctor { Id = 1, FirstName = "John", LastName = "Doe", DepartmentId = 1 },
            new Doctor { Id = 2, FirstName = "Jane", LastName = "Smith", DepartmentId = 2 },
            new Doctor { Id = 3, FirstName = "Michael", LastName = "Brown", DepartmentId = 3 },
            new Doctor { Id = 4, FirstName = "Sarah", LastName = "Johnson", DepartmentId = 4 },
            new Doctor { Id = 5, FirstName = "David", LastName = "Wilson", DepartmentId = 5 },
            new Doctor { Id = 6, FirstName = "Emily", LastName = "Martinez", DepartmentId = 6 },
            new Doctor { Id = 7, FirstName = "James", LastName = "Garcia", DepartmentId = 7 },
            new Doctor { Id = 8, FirstName = "Jessica", LastName = "Miller", DepartmentId = 8 },
            new Doctor { Id = 9, FirstName = "William", LastName = "Davis", DepartmentId = 9 },
            new Doctor { Id = 10, FirstName = "Sophia", LastName = "Rodriguez", DepartmentId = 10 },
            new Doctor { Id = 11, FirstName = "Daniel", LastName = "Hernandez", DepartmentId = 11 },
            new Doctor { Id = 12, FirstName = "Olivia", LastName = "Lopez", DepartmentId = 12 },
            new Doctor { Id = 13, FirstName = "Matthew", LastName = "Gonzalez", DepartmentId = 13 },
            new Doctor { Id = 14, FirstName = "Isabella", LastName = "Perez", DepartmentId = 14 },
            new Doctor { Id = 15, FirstName = "Alexander", LastName = "Hall", DepartmentId = 15 },
            new Doctor { Id = 16, FirstName = "Mia", LastName = "Young", DepartmentId = 16 },
            new Doctor { Id = 17, FirstName = "Ethan", LastName = "Allen", DepartmentId = 17 },
            new Doctor { Id = 18, FirstName = "Ava", LastName = "King", DepartmentId = 18 },
            new Doctor { Id = 19, FirstName = "Benjamin", LastName = "Scott", DepartmentId = 19 },
            new Doctor { Id = 20, FirstName = "Charlotte", LastName = "Green", DepartmentId = 20 });
    }
}