﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TimekeeperAPI.Data.Data.DbContexts;

namespace TimekeeperAPI.Data.Migrations
{
    [DbContext(typeof(TkDbContext))]
    [Migration("20220322071133_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TimekeeperAPI.Data.Data.Entities.tk_Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CompletedStatus")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("CreationTime")
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeTask")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("Tk_TimesheetsId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Tk_TimesheetsId");

                    b.ToTable("Task");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d8dc89fd-bf74-48f1-aecd-d304f000c2d2"),
                            CompletedStatus = "COMPLETE",
                            Content = "sua chua may tinh a theo dung quy dinh của khách hàng",
                            CreationTime = "ON TIME",
                            TimeTask = new DateTime(2022, 3, 22, 14, 11, 33, 509, DateTimeKind.Local).AddTicks(3461),
                            Title = "viec A",
                            Tk_TimesheetsId = new Guid("5bb48d30-8b08-4e08-b2f9-e33d87329dd2"),
                            Type = "PLANNED"
                        },
                        new
                        {
                            Id = new Guid("199c6a90-ff0e-4e28-a170-deb6d93abf06"),
                            CompletedStatus = "COMPLETE",
                            Content = "sua chua may tinh a theo dung quy dinh",
                            CreationTime = "ON TIME",
                            TimeTask = new DateTime(2022, 3, 22, 14, 11, 33, 509, DateTimeKind.Local).AddTicks(4135),
                            Title = "viec B",
                            Tk_TimesheetsId = new Guid("5bb48d30-8b08-4e08-b2f9-e33d87329dd2"),
                            Type = "PLANNED"
                        },
                        new
                        {
                            Id = new Guid("3e18ab32-3808-4d48-a31f-c9da17d29056"),
                            CompletedStatus = "COMPLETE",
                            Content = "sua chua may tinh a theo dung quy dinh",
                            CreationTime = "ON TIME",
                            TimeTask = new DateTime(2022, 3, 22, 14, 11, 33, 509, DateTimeKind.Local).AddTicks(4157),
                            Title = "viec C",
                            Tk_TimesheetsId = new Guid("5bb48d30-8b08-4e08-b2f9-e33d87329dd2"),
                            Type = "OUTSTANDING"
                        },
                        new
                        {
                            Id = new Guid("22a91358-26fe-468a-ac86-2c45111e2415"),
                            CompletedStatus = "COMPLETE",
                            Content = "sua chua may tinh a theo dung quy dinh",
                            CreationTime = "ON TIME",
                            TimeTask = new DateTime(2022, 3, 22, 14, 11, 33, 509, DateTimeKind.Local).AddTicks(4161),
                            Title = "viec D",
                            Tk_TimesheetsId = new Guid("2e74d739-470f-485c-9ecd-ef5ee312072f"),
                            Type = "PLANNED"
                        },
                        new
                        {
                            Id = new Guid("612aadb3-6cc7-4a34-bd50-a620adcb942e"),
                            CompletedStatus = "COMPLETE",
                            Content = "sua chua may tinh a theo dung quy dinh",
                            CreationTime = "ON TIME",
                            TimeTask = new DateTime(2022, 3, 22, 14, 11, 33, 509, DateTimeKind.Local).AddTicks(4165),
                            Title = "viec E",
                            Tk_TimesheetsId = new Guid("d01b54a5-6982-476d-8335-69a6d006456e"),
                            Type = "LATE"
                        },
                        new
                        {
                            Id = new Guid("af71727c-35c9-474d-9382-d0950015b944"),
                            CompletedStatus = "UNFINISHED",
                            Content = "sua chua may tinh a theo dung quy dinh",
                            CreationTime = "ON TIME",
                            TimeTask = new DateTime(2022, 3, 22, 14, 11, 33, 509, DateTimeKind.Local).AddTicks(4171),
                            Title = "viec F",
                            Tk_TimesheetsId = new Guid("d01b54a5-6982-476d-8335-69a6d006456e"),
                            Type = "LATE"
                        });
                });

            modelBuilder.Entity("TimekeeperAPI.Data.Data.Entities.tk_Timesheet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CompletePlannedCount")
                        .HasColumnType("integer");

                    b.Property<double>("CompletionRate")
                        .HasColumnType("double precision");

                    b.Property<int>("OutStandingCount")
                        .HasColumnType("integer");

                    b.Property<int>("TaskPlannedCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeCheckin")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("TimeCheckout")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("Tk_UsersId")
                        .HasColumnType("uuid");

                    b.Property<string>("WorkingTime")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Tk_UsersId");

                    b.ToTable("Timesheet");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5bb48d30-8b08-4e08-b2f9-e33d87329dd2"),
                            CompletePlannedCount = 2,
                            CompletionRate = 100.0,
                            OutStandingCount = 1,
                            TaskPlannedCount = 1,
                            TimeCheckin = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2022),
                            TimeCheckout = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Tk_UsersId = new Guid("9897385e-0b0f-4cd5-9cab-9a907e0d965c")
                        },
                        new
                        {
                            Id = new Guid("2e74d739-470f-485c-9ecd-ef5ee312072f"),
                            CompletePlannedCount = 1,
                            CompletionRate = 100.0,
                            OutStandingCount = 0,
                            TaskPlannedCount = 1,
                            TimeCheckin = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2022),
                            TimeCheckout = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Tk_UsersId = new Guid("9897385e-0b0f-4cd5-9cab-9a907e0d965c")
                        },
                        new
                        {
                            Id = new Guid("d01b54a5-6982-476d-8335-69a6d006456e"),
                            CompletePlannedCount = 1,
                            CompletionRate = 50.0,
                            OutStandingCount = 0,
                            TaskPlannedCount = 2,
                            TimeCheckin = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2022),
                            TimeCheckout = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Tk_UsersId = new Guid("c5ef2136-db28-4540-bcbb-354532c6917e")
                        });
                });

            modelBuilder.Entity("TimekeeperAPI.Data.Data.Entities.tk_User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9897385e-0b0f-4cd5-9cab-9a907e0d965c"),
                            LastLogin = new DateTime(2022, 3, 22, 14, 11, 33, 506, DateTimeKind.Local).AddTicks(7021),
                            Password = "1234",
                            Phone = "0961523842",
                            Role = "user",
                            Name = "hoang111"
                        },
                        new
                        {
                            Id = new Guid("c5ef2136-db28-4540-bcbb-354532c6917e"),
                            LastLogin = new DateTime(2022, 3, 22, 14, 11, 33, 507, DateTimeKind.Local).AddTicks(2798),
                            Password = "1234",
                            Phone = "0961523842",
                            Role = "user",
                            Name = "user111"
                        },
                        new
                        {
                            Id = new Guid("a914072e-881d-4a5d-98d7-9fc1aafcffa1"),
                            LastLogin = new DateTime(2022, 3, 22, 14, 11, 33, 507, DateTimeKind.Local).AddTicks(2836),
                            Password = "1234",
                            Phone = "0961523842",
                            Role = "admin",
                            Name = "admin111"
                        });
                });

            modelBuilder.Entity("TimekeeperAPI.Data.Data.Entities.tk_Task", b =>
                {
                    b.HasOne("TimekeeperAPI.Data.Data.Entities.tk_Timesheet", "Tk_Timesheets")
                        .WithMany("Tk_Tasks")
                        .HasForeignKey("Tk_TimesheetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TimekeeperAPI.Data.Data.Entities.tk_Timesheet", b =>
                {
                    b.HasOne("TimekeeperAPI.Data.Data.Entities.tk_User", "Tk_Users")
                        .WithMany("Tk_Timesheets")
                        .HasForeignKey("Tk_UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}