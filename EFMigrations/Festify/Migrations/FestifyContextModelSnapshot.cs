﻿// <auto-generated />
using System;
using Festify.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Festify.Migrations
{
    [DbContext(typeof(FestifyContext))]
    partial class FestifyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Festify.Database.Conference", b =>
                {
                    b.Property<int>("ConferenceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ConferenceId");

                    b.HasAlternateKey("Identifier");

                    b.ToTable("Conference");
                });

            modelBuilder.Entity("Festify.Database.Reach", b =>
                {
                    b.Property<int>("ReachId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("ReachId");

                    b.ToTable("Reach");

                    b.HasData(
                        new { ReachId = 1, Description = "Keynote" },
                        new { ReachId = 2, Description = "Breakout" },
                        new { ReachId = 3, Description = "Open Space" }
                    );
                });

            modelBuilder.Entity("Festify.Database.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConferenceId");

                    b.Property<int>("ReachId");

                    b.Property<Guid>("SessionGuid");

                    b.Property<string>("Speaker")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("SessionId");

                    b.HasAlternateKey("SessionGuid");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("ReachId");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("Festify.Database.Session", b =>
                {
                    b.HasOne("Festify.Database.Conference", "Conference")
                        .WithMany("Sessions")
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Festify.Database.Reach", "Reach")
                        .WithMany()
                        .HasForeignKey("ReachId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
