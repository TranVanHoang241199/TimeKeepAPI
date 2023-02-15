using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TimekeeperAPI.Data.Data.Entities;

namespace TimekeeperAPI.Data.Data.DbContexts
{
    /// <summary>
    /// tạo csdl (dbcontext)  
    /// </summary>
    public class TkDbContext : DbContext
    {
        public TkDbContext(DbContextOptions<TkDbContext> options) : base(options) { }

        // tạo table user
        public DbSet<tk_User> Tk_Users { get; set; }
        // tạo table task
        public DbSet<tk_Task> Tk_Tasks { get; set; }
        // tạo table timesheet
        public DbSet<tk_Timesheet> Tk_Timesheets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<tk_User>().HasData(
                new tk_User()
                {
                    Id = Guid.Parse("9897385e-0b0f-4cd5-9cab-9a907e0d965c"),
                    Name = "hoang111",
                    Phone = "0961523842",
                    Password = "1234",
                    LastLogin = DateTime.Now,
                    Role = "user"
                },

                new tk_User()
                {
                    Id = Guid.Parse("c5ef2136-db28-4540-bcbb-354532c6917e"),
                    Name = "user111",
                    Phone = "0961523842",
                    Password = "1234",
                    LastLogin = DateTime.Now,
                    Role = "user"
                },
                new tk_User()
                {
                    Id = Guid.Parse("a914072e-881d-4a5d-98d7-9fc1aafcffa1"),
                    Name = "admin111",
                    Phone = "0961523842",
                    Password = "1234",
                    LastLogin = DateTime.Now,
                    Role = "admin"
                });
            #endregion User
            //------
            #region TimeSheet
            modelBuilder.Entity<tk_Timesheet>().HasData(
                new tk_Timesheet()
                {
                    Id = Guid.Parse("5bb48d30-8b08-4e08-b2f9-e33d87329dd2"),
                    TimeCheckin = new DateTime(2022),
                    TimeCheckout = new DateTime(),
                    TaskPlannedCount = 1,
                    CompletePlannedCount = 2,
                    OutStandingCount = 1,
                    CompletionRate = 100,
                    Tk_UsersId = Guid.Parse("9897385e-0b0f-4cd5-9cab-9a907e0d965c")
                },

                new tk_Timesheet()
                {
                    Id = Guid.Parse("2e74d739-470f-485c-9ecd-ef5ee312072f"),
                    TimeCheckin = new DateTime(2022),
                    TimeCheckout = new DateTime(),
                    TaskPlannedCount = 1,
                    CompletePlannedCount = 1,
                    OutStandingCount = 0,
                    CompletionRate = 100,
                    Tk_UsersId = Guid.Parse("9897385e-0b0f-4cd5-9cab-9a907e0d965c")
                },

                new tk_Timesheet()
                {
                    Id = Guid.Parse("d01b54a5-6982-476d-8335-69a6d006456e"),
                    TimeCheckin = new DateTime(2022),
                    TimeCheckout = new DateTime(),
                    TaskPlannedCount = 2,
                    CompletePlannedCount = 1,
                    OutStandingCount = 0,
                    CompletionRate = 50,
                    Tk_UsersId = Guid.Parse("c5ef2136-db28-4540-bcbb-354532c6917e")
                });
            #endregion TimeSheet
            //------
            #region Task
            modelBuilder.Entity<tk_Task>().HasData(
                new tk_Task()
                {
                    Id = Guid.Parse("d8dc89fd-bf74-48f1-aecd-d304f000c2d2"),
                    Title = "viec A",
                    Content = "sua chua may tinh a theo dung quy dinh của khách hàng",
                    CreationTime = "ON TIME",
                    CompletedStatus = "COMPLETE",
                    Type = "PLANNED",
                    TimeTask = DateTime.Now,
                    Tk_TimesheetsId = Guid.Parse("5bb48d30-8b08-4e08-b2f9-e33d87329dd2")
                },
                new tk_Task()
                {
                    Id = Guid.Parse("199c6a90-ff0e-4e28-a170-deb6d93abf06"),
                    Title = "viec B",
                    Content = "sua chua may tinh a theo dung quy dinh",
                    CreationTime = "ON TIME",
                    CompletedStatus = "COMPLETE",
                    Type = "PLANNED",
                    TimeTask = DateTime.Now,
                    Tk_TimesheetsId = Guid.Parse("5bb48d30-8b08-4e08-b2f9-e33d87329dd2")
                },
                new tk_Task()
                {
                    Id = Guid.Parse("3e18ab32-3808-4d48-a31f-c9da17d29056"),
                    Title = "viec C",
                    Content = "sua chua may tinh a theo dung quy dinh",
                    CreationTime = "ON TIME",
                    CompletedStatus = "COMPLETE",
                    Type = "OUTSTANDING",
                    TimeTask = DateTime.Now,
                    Tk_TimesheetsId = Guid.Parse("5bb48d30-8b08-4e08-b2f9-e33d87329dd2")
                },
                //--
                new tk_Task()
                {
                    Id = Guid.Parse("22a91358-26fe-468a-ac86-2c45111e2415"),
                    Title = "viec D",
                    Content = "sua chua may tinh a theo dung quy dinh",
                    CreationTime = "ON TIME",
                    CompletedStatus = "COMPLETE",
                    Type = "PLANNED",
                    TimeTask = DateTime.Now,
                    Tk_TimesheetsId = Guid.Parse("2e74d739-470f-485c-9ecd-ef5ee312072f")
                },
                //----
                new tk_Task()
                {
                    Id = Guid.Parse("612aadb3-6cc7-4a34-bd50-a620adcb942e"),
                    Title = "viec E",
                    Content = "sua chua may tinh a theo dung quy dinh",
                    CreationTime = "ON TIME",
                    CompletedStatus = "COMPLETE",
                    Type = "LATE",
                    TimeTask = DateTime.Now,
                    Tk_TimesheetsId = Guid.Parse("d01b54a5-6982-476d-8335-69a6d006456e")
                },
                new tk_Task()
                {
                    Id = Guid.Parse("af71727c-35c9-474d-9382-d0950015b944"),
                    Title = "viec F",
                    Content = "sua chua may tinh a theo dung quy dinh",
                    CreationTime = "ON TIME",
                    CompletedStatus = "UNFINISHED",
                    Type = "LATE",
                    TimeTask = DateTime.Now,
                    Tk_TimesheetsId = Guid.Parse("d01b54a5-6982-476d-8335-69a6d006456e")
                });
            #endregion Task

            base.OnModelCreating(modelBuilder);
        }
    }
}
