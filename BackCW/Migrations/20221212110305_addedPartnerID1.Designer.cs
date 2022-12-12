﻿// <auto-generated />
using BackCW.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackCW.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221212110305_addedPartnerID1")]
    partial class addedPartnerID1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BackCW.Data.SecretSanta", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Likes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PartnerIDId")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PartnerIDId");

                    b.ToTable("SecretSantas");
                });

            modelBuilder.Entity("BackCW.Data.SecretSanta", b =>
                {
                    b.HasOne("BackCW.Data.SecretSanta", "PartnerID")
                        .WithMany()
                        .HasForeignKey("PartnerIDId");

                    b.Navigation("PartnerID");
                });
#pragma warning restore 612, 618
        }
    }
}
