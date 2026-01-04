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
    public class DeliveryTypeConfiguration : IEntityTypeConfiguration<DeliveryType>
    {
        public void Configure(EntityTypeBuilder<DeliveryType> builder)
        {
            builder.ToTable("DeliveryType");
            builder.HasKey(dt => dt.Id);

            builder.Property(dt => dt.Id)
                   .HasColumnType("int")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(dt => dt.Name)
                   .HasColumnType("varchar(25)")
                   .IsRequired();

            builder.HasData(
                new DeliveryType { Id = 1, Name = "Delivery" },
                new DeliveryType { Id = 2, Name = "Take away" },
                new DeliveryType { Id = 3, Name = "Dine in" }
            );
        }
    }
}
