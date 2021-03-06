﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBase.Migrations
{
    public partial class DeleteSomeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAdditionalInfos");

            migrationBuilder.DropTable(
                name: "UserTours");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.UpdateData(
                table: "TourTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "TourDateFrom", "TourDateTo" },
                values: new object[] { new DateTime(2020, 4, 14, 1, 19, 50, 116, DateTimeKind.Local).AddTicks(2153), new DateTime(2020, 4, 17, 1, 19, 50, 121, DateTimeKind.Local).AddTicks(8472) });

            migrationBuilder.UpdateData(
                table: "TourTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "TourDateFrom", "TourDateTo" },
                values: new object[] { new DateTime(2020, 4, 17, 1, 19, 50, 122, DateTimeKind.Local).AddTicks(677), new DateTime(2020, 4, 19, 1, 19, 50, 122, DateTimeKind.Local).AddTicks(726) });

            migrationBuilder.UpdateData(
                table: "TourTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "TourDateFrom", "TourDateTo" },
                values: new object[] { new DateTime(2020, 4, 15, 1, 19, 50, 122, DateTimeKind.Local).AddTicks(768), new DateTime(2020, 4, 22, 1, 19, 50, 122, DateTimeKind.Local).AddTicks(778) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TourLength = table.Column<int>(type: "int", nullable: false),
                    TourName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TourTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tour_TourTypes_TourTypeId",
                        column: x => x.TourTypeId,
                        principalTable: "TourTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAdditionalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdditionalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAdditionalInfos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HowFoundUs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTours_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTours_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "TourTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "TourDateFrom", "TourDateTo" },
                values: new object[] { new DateTime(2020, 4, 14, 1, 1, 22, 877, DateTimeKind.Local).AddTicks(3159), new DateTime(2020, 4, 17, 1, 1, 22, 883, DateTimeKind.Local).AddTicks(2963) });

            migrationBuilder.UpdateData(
                table: "TourTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "TourDateFrom", "TourDateTo" },
                values: new object[] { new DateTime(2020, 4, 17, 1, 1, 22, 883, DateTimeKind.Local).AddTicks(6236), new DateTime(2020, 4, 19, 1, 1, 22, 883, DateTimeKind.Local).AddTicks(6468) });

            migrationBuilder.UpdateData(
                table: "TourTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "TourDateFrom", "TourDateTo" },
                values: new object[] { new DateTime(2020, 4, 15, 1, 1, 22, 883, DateTimeKind.Local).AddTicks(6539), new DateTime(2020, 4, 22, 1, 1, 22, 883, DateTimeKind.Local).AddTicks(6549) });

            migrationBuilder.CreateIndex(
                name: "IX_Tour_TourTypeId",
                table: "Tour",
                column: "TourTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdditionalInfos_UserId",
                table: "UserAdditionalInfos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTours_TourId",
                table: "UserTours",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTours_UserId",
                table: "UserTours",
                column: "UserId");
        }
    }
}
