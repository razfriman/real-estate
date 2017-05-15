using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using app;

namespace app.api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("app.Data.Property", b =>
                {
                    b.Property<int>("PropertyID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<DateTime>("DateLastPaid");

                    b.Property<string>("ImageURL");

                    b.Property<int>("MonthsPaid");

                    b.Property<decimal>("RentPayment");

                    b.Property<decimal>("ReturnOnInvestment");

                    b.Property<int?>("TenantID");

                    b.Property<string>("Title");

                    b.Property<decimal>("TotalRentPaid");

                    b.Property<int?>("UserID");

                    b.Property<decimal>("Value");

                    b.HasKey("PropertyID");

                    b.HasIndex("TenantID");

                    b.HasIndex("UserID");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("app.Data.Tenant", b =>
                {
                    b.Property<int>("TenantID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("ImageURL");

                    b.Property<string>("LastName");

                    b.Property<string>("Phone");

                    b.HasKey("TenantID");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("app.Data.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("ImageURL");

                    b.Property<string>("LastName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PasswordSalt");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("app.Data.Property", b =>
                {
                    b.HasOne("app.Data.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantID");

                    b.HasOne("app.Data.User")
                        .WithMany("Properties")
                        .HasForeignKey("UserID");
                });
        }
    }
}
