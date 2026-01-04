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
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dish");
            builder.HasKey(d => d.DishId);

            builder.Property(d => d.DishId)
                   .HasColumnType("uniqueidentifier")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(d => d.Name)
                   .HasColumnType("varchar(255)")
                   .IsRequired();

            builder.Property(d => d.Description)
                   .HasColumnType("varchar(MAX)")
                   .IsRequired();

            builder.Property(d => d.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.Available)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(d => d.Category)
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(d => d.ImageUrl)
                   .HasColumnType("varchar(MAX)");

            builder.Property(d => d.CreateDate)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(d => d.UpdateDate)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()")
                   .ValueGeneratedOnAddOrUpdate();

            builder.HasOne(d => d.TheCategory)
                   .WithMany()
                   .HasForeignKey(d => d.Category)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
