using AppointmentSystemServer.Domain.Constants;
using AppointmentSystemServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentSystemServer.Persistence.Configurations;

class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.Property(d => d.Name).HasMaxLength(LengthConstants.MaxLenght100).IsRequired();

        builder
            .HasMany(d => d.Doctors)
            .WithOne(d => d.Department) // Doktorun sadece bir tane departmanı olabilir
            .HasForeignKey(d => d.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Department { Id = 1, Name = "Emergency" },
            new Department { Id = 2, Name = "Outpatient" },
            new Department { Id = 3, Name = "Inpatient" },
            new Department { Id = 4, Name = "Surgery" },
            new Department { Id = 5, Name = "Cardiology" },
            new Department { Id = 6, Name = "Neurology" },
            new Department { Id = 7, Name = "Orthopedics" },
            new Department { Id = 8, Name = "Pediatrics" },
            new Department { Id = 9, Name = "Gynecology" },
            new Department { Id = 10, Name = "Internal Medicine" },
            new Department { Id = 11, Name = "Dermatology" },
            new Department { Id = 12, Name = "Radiology" },
            new Department { Id = 13, Name = "Pathology" },
            new Department { Id = 14, Name = "Anesthesia" },
            new Department { Id = 15, Name = "Ophthalmology" },
            new Department { Id = 16, Name = "ENT" },
            new Department { Id = 17, Name = "Psychiatry" },
            new Department { Id = 18, Name = "Laboratory" },
            new Department { Id = 19, Name = "Physiotherapy" },
            new Department { Id = 20, Name = "Urology" });
    }
}
