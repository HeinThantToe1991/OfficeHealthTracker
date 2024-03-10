using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OfficeHealthTracker.Infrastructure.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(nullable: false),
                    Building = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Room = table.Column<string>(nullable: true),
                    CarPark = table.Column<string>(nullable: true),
                    Lobby = table.Column<string>(nullable: true),
                    Pantry = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "FieldTypes",
                columns: table => new
                {
                    FieldTypeId = table.Column<Guid>(nullable: false),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldTypes", x => x.FieldTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FilledForms",
                columns: table => new
                {
                    FilledFormId = table.Column<Guid>(nullable: false),
                    TemplateId = table.Column<Guid>(nullable: false),
                    FieldValues = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilledForms", x => x.FilledFormId);
                });

            migrationBuilder.CreateTable(
                name: "PayloadDatas",
                columns: table => new
                {
                    PayLoadId = table.Column<Guid>(nullable: false),
                    DeviceId = table.Column<string>(nullable: true),
                    Temperature = table.Column<int>(nullable: false),
                    Humidity = table.Column<int>(nullable: false),
                    Occupancy = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayloadDatas", x => x.PayLoadId);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    TemplateId = table.Column<Guid>(nullable: false),
                    TemplateName = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.TemplateId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateFields",
                columns: table => new
                {
                    TemplateFieldId = table.Column<Guid>(nullable: false),
                    TemplateId = table.Column<Guid>(nullable: false),
                    FieldTypeId = table.Column<Guid>(nullable: false),
                    FieldOptions = table.Column<string>(nullable: true),
                    Sorting = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateFields", x => x.TemplateFieldId);
                    table.ForeignKey(
                        name: "FK_TemplateFields_FieldTypes_FieldTypeId",
                        column: x => x.FieldTypeId,
                        principalTable: "FieldTypes",
                        principalColumn: "FieldTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateFields_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "TemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FieldTypes",
                columns: new[] { "FieldTypeId", "TypeName" },
                values: new object[,]
                {
                    { new Guid("3d986c86-321f-427c-8624-1b8f513c4203"), "DropDown" },
                    { new Guid("c649547c-70be-49fb-883b-9189433b7d65"), "TextBox" },
                    { new Guid("480874ca-c054-4d1a-9832-74ada72cd4f6"), "CheckBox" },
                    { new Guid("29460c45-7226-4fea-be84-28a74920ff3b"), "Radio" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { new Guid("55402d34-0234-4fa8-b564-fc96641b23d0"), "STPL", "HTT" });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateFields_FieldTypeId",
                table: "TemplateFields",
                column: "FieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateFields_TemplateId",
                table: "TemplateFields",
                column: "TemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "FilledForms");

            migrationBuilder.DropTable(
                name: "PayloadDatas");

            migrationBuilder.DropTable(
                name: "TemplateFields");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FieldTypes");

            migrationBuilder.DropTable(
                name: "Templates");
        }
    }
}
