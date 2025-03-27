using AppointmentSystemServer.Domain.Constants;
using AppointmentSystemServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentSystemServer.Persistence.Configurations;

class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.FirstName).HasMaxLength(LengthConstants.MaxLenght50);
        builder.Property(p => p.LastName).HasMaxLength(LengthConstants.MaxLenght50);
        builder.Property(p => p.City).HasMaxLength(LengthConstants.MaxLenght50);
        builder.Property(p => p.Town).HasMaxLength(LengthConstants.MaxLenght50);
        builder.Property(p => p.FullAddress).HasMaxLength(LengthConstants.MaxLenght250);
        builder.Property(p => p.IdentityNumber).HasMaxLength(LengthConstants.MaxLenght11);

        builder.HasIndex(p => p.IdentityNumber).IsUnique();
    }
}