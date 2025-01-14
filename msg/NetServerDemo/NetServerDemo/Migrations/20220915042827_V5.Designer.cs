﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetServerDemo;

#nullable disable

namespace NetServerDemo.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20220915042827_V5")]
    partial class V5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NetServerDemo.Model+User_TBL", b =>
                {
                    b.Property<string>("User_Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("User_Num")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("User_Birth")
                        .HasColumnType("longtext");

                    b.Property<string>("User_Name")
                        .HasColumnType("longtext");

                    b.Property<string>("User_Phone")
                        .HasColumnType("longtext");

                    b.Property<string>("User_Pw")
                        .HasColumnType("longtext");

                    b.HasKey("User_Id", "User_Num");

                    b.ToTable("User_TBLs");
                });
#pragma warning restore 612, 618
        }
    }
}
