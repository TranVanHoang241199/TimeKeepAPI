using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimekeeperAPI.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    LastLogin = table.Column<DateTime>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timesheet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeCheckin = table.Column<DateTime>(nullable: false),
                    TimeCheckout = table.Column<DateTime>(nullable: true),
                    WorkingTime = table.Column<string>(nullable: true),
                    TaskPlannedCount = table.Column<int>(nullable: false),
                    CompletePlannedCount = table.Column<int>(nullable: false),
                    OutStandingCount = table.Column<int>(nullable: false),
                    CompletionRate = table.Column<double>(nullable: false),
                    Tk_UsersId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timesheet_User_Tk_UsersId",
                        column: x => x.Tk_UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    CreationTime = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    CompletedStatus = table.Column<string>(nullable: true),
                    TimeTask = table.Column<DateTime>(nullable: false),
                    Tk_TimesheetsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Timesheet_Tk_TimesheetsId",
                        column: x => x.Tk_TimesheetsId,
                        principalTable: "Timesheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "LastLogin", "Password", "Phone", "Role", "Name" },
                values: new object[,]
                {
                    { new Guid("9897385e-0b0f-4cd5-9cab-9a907e0d965c"), new DateTime(2022, 3, 22, 14, 11, 33, 506, DateTimeKind.Local).AddTicks(7021), "1234", "0961523842", "user", "hoang111" },
                    { new Guid("c5ef2136-db28-4540-bcbb-354532c6917e"), new DateTime(2022, 3, 22, 14, 11, 33, 507, DateTimeKind.Local).AddTicks(2798), "1234", "0961523842", "user", "user111" },
                    { new Guid("a914072e-881d-4a5d-98d7-9fc1aafcffa1"), new DateTime(2022, 3, 22, 14, 11, 33, 507, DateTimeKind.Local).AddTicks(2836), "1234", "0961523842", "admin", "admin111" }
                });

            migrationBuilder.InsertData(
                table: "Timesheet",
                columns: new[] { "Id", "CompletePlannedCount", "CompletionRate", "OutStandingCount", "TaskPlannedCount", "TimeCheckin", "TimeCheckout", "Tk_UsersId", "WorkingTime" },
                values: new object[,]
                {
                    { new Guid("5bb48d30-8b08-4e08-b2f9-e33d87329dd2"), 2, 100.0, 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2022), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9897385e-0b0f-4cd5-9cab-9a907e0d965c"), null },
                    { new Guid("2e74d739-470f-485c-9ecd-ef5ee312072f"), 1, 100.0, 0, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2022), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9897385e-0b0f-4cd5-9cab-9a907e0d965c"), null },
                    { new Guid("d01b54a5-6982-476d-8335-69a6d006456e"), 1, 50.0, 0, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2022), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c5ef2136-db28-4540-bcbb-354532c6917e"), null }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "CompletedStatus", "Content", "CreationTime", "Note", "TimeTask", "Title", "Tk_TimesheetsId", "Type" },
                values: new object[,]
                {
                    { new Guid("d8dc89fd-bf74-48f1-aecd-d304f000c2d2"), "COMPLETE", "sua chua may tinh a theo dung quy dinh của khách hàng", "ON TIME", null, new DateTime(2022, 3, 22, 14, 11, 33, 509, DateTimeKind.Local).AddTicks(3461), "viec A", new Guid("5bb48d30-8b08-4e08-b2f9-e33d87329dd2"), "PLANNED" },
                    { new Guid("199c6a90-ff0e-4e28-a170-deb6d93abf06"), "COMPLETE", "sua chua may tinh a theo dung quy dinh", "ON TIME", null, new DateTime(2022, 3, 22, 14, 11, 33, 509, DateTimeKind.Local).AddTicks(4135), "viec B", new Guid("5bb48d30-8b08-4e08-b2f9-e33d87329dd2"), "PLANNED" },
                    { new Guid("3e18ab32-3808-4d48-a31f-c9da17d29056"), "COMPLETE", "sua chua may tinh a theo dung quy dinh", "ON TIME", null, new DateTime(2022, 3, 22, 14, 11, 33, 509, DateTimeKind.Local).AddTicks(4157), "viec C", new Guid("5bb48d30-8b08-4e08-b2f9-e33d87329dd2"), "OUTSTANDING" },
                    { new Guid("22a91358-26fe-468a-ac86-2c45111e2415"), "COMPLETE", "sua chua may tinh a theo dung quy dinh", "ON TIME", null, new DateTime(2022, 3, 22, 14, 11, 33, 509, DateTimeKind.Local).AddTicks(4161), "viec D", new Guid("2e74d739-470f-485c-9ecd-ef5ee312072f"), "PLANNED" },
                    { new Guid("612aadb3-6cc7-4a34-bd50-a620adcb942e"), "COMPLETE", "sua chua may tinh a theo dung quy dinh", "ON TIME", null, new DateTime(2022, 3, 22, 14, 11, 33, 509, DateTimeKind.Local).AddTicks(4165), "viec E", new Guid("d01b54a5-6982-476d-8335-69a6d006456e"), "LATE" },
                    { new Guid("af71727c-35c9-474d-9382-d0950015b944"), "UNFINISHED", "sua chua may tinh a theo dung quy dinh", "ON TIME", null, new DateTime(2022, 3, 22, 14, 11, 33, 509, DateTimeKind.Local).AddTicks(4171), "viec F", new Guid("d01b54a5-6982-476d-8335-69a6d006456e"), "LATE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_Tk_TimesheetsId",
                table: "Task",
                column: "Tk_TimesheetsId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheet_Tk_UsersId",
                table: "Timesheet",
                column: "Tk_UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Timesheet");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
