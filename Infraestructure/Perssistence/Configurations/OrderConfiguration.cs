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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.OrderId)
                   .HasColumnType("bigint")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(o => o.DeliveryType)
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(o => o.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(o => o.OverallStatus)
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(o => o.CreateDate)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(o => o.UpdateDate)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAddOrUpdate();

            builder.HasOne(o => o.TheDeliveryType)
                   .WithMany()
                   .HasForeignKey(o => o.DeliveryType)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.TheStatus)
                   .WithMany()
                   .HasForeignKey(o => o.OverallStatus)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
