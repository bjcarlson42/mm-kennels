﻿// <auto-generated />
using System;
using MM_Kennels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MM_Kennels.Migrations
{
    [DbContext(typeof(KennelDatabase))]
    partial class KennelDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MM_Kennels.Animal", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CageID");

                    b.Property<int>("LengthOfStay");

                    b.Property<int>("StartDate");

                    b.Property<int>("Weight");

                    b.HasKey("Name");

                    b.HasIndex("CageID");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("MM_Kennels.Cage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CageWeightMax");

                    b.Property<int>("CageWeightMin");

                    b.HasKey("ID");

                    b.ToTable("Cages");
                });

            modelBuilder.Entity("MM_Kennels.Animal", b =>
                {
                    b.HasOne("MM_Kennels.Cage", "Cage")
                        .WithMany()
                        .HasForeignKey("CageID");
                });
#pragma warning restore 612, 618
        }
    }
}
