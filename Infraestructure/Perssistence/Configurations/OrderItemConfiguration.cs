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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");
            builder.HasKey(oi => oi.OrderItemId);

            builder.Property(oi => oi.OrderItemId)
                   .HasColumnType("bigint")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(oi => oi.Order)
                   .HasColumnType("bigint")
                   .IsRequired();

            builder.Property(oi => oi.Dish)
                   .HasColumnType("uniqueidentifier")
                   .IsRequired();

            builder.Property(oi => oi.Notes)
                   .HasColumnType("varchar(MAX)");

            builder.Property(oi => oi.Quantity)
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(oi => oi.Status)
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(oi => oi.CreateDate)
                     .HasColumnType("datetime2")
                     .HasDefaultValueSql("GETDATE()")
                     .ValueGeneratedOnAdd()
                     .IsRequired();

            builder.HasOne(oi => oi.TheDish)
                   .WithMany()
                   .HasForeignKey(oi => oi.Dish)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(oi => oi.TheStatus)
                     .WithMany()
                     .HasForeignKey(oi => oi.Status)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(oi => oi.TheOrder)
                    .WithMany(o => o.TheOrderItems)
                    .HasForeignKey(oi => oi.Order)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
