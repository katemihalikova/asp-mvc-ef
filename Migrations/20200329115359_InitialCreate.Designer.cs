﻿// <auto-generated />
using System;
using ASP1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASP1.Migrations
{
    [DbContext(typeof(AgencyContext))]
    [Migration("20200329115359_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASP1.Models.Destination", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("ASP1.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Attendees");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.Property<string>("Phone");

                    b.Property<string>("Surname");

                    b.Property<int>("TimeslotID");

                    b.HasKey("ID");

                    b.HasIndex("TimeslotID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ASP1.Models.Timeslot", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<int>("DestinationID");

                    b.HasKey("ID");

                    b.HasIndex("DestinationID");

                    b.ToTable("Timeslots");
                });

            modelBuilder.Entity("ASP1.Models.Order", b =>
                {
                    b.HasOne("ASP1.Models.Timeslot", "Timeslot")
                        .WithMany("Orders")
                        .HasForeignKey("TimeslotID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ASP1.Models.Timeslot", b =>
                {
                    b.HasOne("ASP1.Models.Destination", "Destination")
                        .WithMany("Timeslots")
                        .HasForeignKey("DestinationID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}