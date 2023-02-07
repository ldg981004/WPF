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
    [Migration("20220921053151_V8")]
    partial class V8
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NetServerDemo.Model+ChatRoom_TBL", b =>
                {
                    b.Property<int>("ChatRoom_Num")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("ChatRoom_Birth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ChatRoom_Id")
                        .HasColumnType("longtext");

                    b.Property<string>("ChatRoom_Maker")
                        .HasColumnType("longtext");

                    b.Property<string>("ChatRoom_User")
                        .HasColumnType("longtext");

                    b.HasKey("ChatRoom_Num");

                    b.ToTable("ChatRoom_TBLs");
                });

            modelBuilder.Entity("NetServerDemo.Model+User_TBL", b =>
                {
                    b.Property<int>("User_Num")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("User_Birth")
                        .HasColumnType("longtext");

                    b.Property<string>("User_Id")
                        .HasColumnType("longtext");

                    b.Property<string>("User_Name")
                        .HasColumnType("longtext");

                    b.Property<string>("User_Phone")
                        .HasColumnType("longtext");

                    b.Property<string>("User_Pw")
                        .HasColumnType("longtext");

                    b.HasKey("User_Num");

                    b.ToTable("User_TBLs");
                });
#pragma warning restore 612, 618
        }
    }
}
