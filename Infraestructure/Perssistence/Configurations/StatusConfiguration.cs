using Domain.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Perssistence.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasColumnType("int")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(s => s.Name)
                   .HasColumnType("varchar(25)")
                   .IsRequired();

            builder.HasData(
                new Status { Id = 1, Name = "Pending" },
                new Status { Id = 2, Name = "In progress" },
                new Status { Id = 3, Name = "Ready" },
                new Status { Id = 4, Name = "Delivery" },
                new Status { Id = 5, Name = "Closed" }
            );
        }
    }
}
