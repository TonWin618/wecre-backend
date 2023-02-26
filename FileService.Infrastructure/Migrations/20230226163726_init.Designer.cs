﻿// <auto-generated />
using System;
using FileService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FileService.Infrastructure.Migrations
{
    [DbContext(typeof(FileDbContext))]
    [Migration("20230226163726_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FileService.Domain.Entities.FileItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BackupUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FileSHA256Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("bigint");

                    b.Property<string>("RemoteUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FileItems");
                });

            modelBuilder.Entity("FileService.Domain.Entities.FileItem", b =>
                {
                    b.OwnsOne("FileService.Domain.Entities.FileIdentifier", "FileIdentifier", b1 =>
                        {
                            b1.Property<Guid>("FileItemId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("FileType")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("ProjectName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("UserName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("VersionName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("FileItemId");

                            b1.ToTable("FileItems");

                            b1.WithOwner()
                                .HasForeignKey("FileItemId");
                        });

                    b.Navigation("FileIdentifier")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
